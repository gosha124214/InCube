using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppInCube.Classes;
using Microsoft.Maui.Controls;
using static AppInCube.Classes.MetodMetodsGet;
using System.ComponentModel;
using AppInCube.View.Pages.Programs.UnderPages; // Убедитесь, что это пространство имен правильное
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLBD;

namespace AppInCube.View.Pages.Programs
{
    public partial class ProgramsPage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<BirdProgram> BirdProgram { get; set; } // Коллекция для хранения программ

        private uint _currentOffset = 1; // Переменная для отслеживания текущего смещения
        private const int _pageSize = 2; // Количество загружаемых программ за раз


        private uint? _numberOfAllObjects = 0; // Общее количество объектов

        private readonly ApiService _apiService = new ApiService();

        public ProgramsPage()
        {
            InitializeComponent();

            BirdProgram = new ObservableCollection<BirdProgram>(); // Инициализация коллекции
            BindingContext = this; // Устанавливаем контекст привязки

            // Запускаем асинхронный метод для загрузки данных
            LoadInitialPrograms();
        }

        public uint? NumberOfAllObjectsInDataBase
        {
            get { return _numberOfAllObjects; }
            set
            {
                
                _numberOfAllObjects = value;
                if (value != null)
                {
                    OnPropertyChanged(nameof(NumberOfAllObjectsInDataBase)); // Уведомляем об изменении
                }
            }
        }

        public uint LoadedProgramsCount
        {
            get { return (uint)BirdProgram.Count; }
        }

        private async void LoadInitialPrograms()
        {
            await LoadPrograms(_pageSize); // Загружаем первые 2 программы
        }

        private async void OnLoadMoreClicked(object sender, EventArgs e)
        {
            await LoadPrograms(_pageSize); // Загружаем еще 2 программы при нажатии кнопки
        }

        private async Task LoadPrograms(int count)
        {
            // Блокируем взаимодействие с текущей страницей
            this.IsEnabled = false;

            NumberOfAllObjectsInDataBase = await _apiService.GetTableBirdCountAllProgram(); // Получаем общее количество объектов

            if (NumberOfAllObjectsInDataBase >= _currentOffset)
            {
                for (int i = 0; i < count; i++)
                {
                    TableBird? newBird = await _apiService.GetTableBirdAsync(_currentOffset);
                    List<TableProgram>? newProgram = await _apiService.GetTableProgramAsync(newBird.IdProgram);

                    if ((newBird != null && newBird.Id != 0) && newProgram != null) // Проверяем, что программа была загружена
                    {
                        // Конвертация массива байтов в ImageSource
                        newBird.ImageSource = ImageSource.FromStream(() => new MemoryStream(newBird.ImageBirdFile));

                        //Вычисление количества дней до вылупления
                        newBird.DaysUntilHatching = (byte)newProgram.Count;


                        BirdProgram birdProgram = new BirdProgram
                        {
                            tableBird = newBird,
                            tableProgram = newProgram
                        };

                        // Добавление в коллекцию
                        BirdProgram.Add(birdProgram);

                        _currentOffset++;
                        OnPropertyChanged(nameof(LoadedProgramsCount));
                    }
                    else
                    {
                        ShowMessage("Не удалось загрузить программы.\nПроверьте соединение с интернетом");
                        break;
                    }
                }
            }
            else
            {
                ShowMessage("Программ больше нет");
            }

            this.IsEnabled = true; // Разблокируем взаимодействие с текущей страницей
        }


        private async void ShowMessage(string message, int duration = 5000)
        {
            MessageLabel.Text = message; // Устанавливаем текст сообщения
            MessageFrame.IsVisible = true; // Делаем Frame видимым

            // Ждем указанное время
            await Task.Delay(duration);

            // Скрываем Frame
            MessageFrame.IsVisible = false;
        }


        private async void OnProgramTapped(object sender, EventArgs e)
        {

            // Блокируем взаимодействие с текущей страницей
            this.IsEnabled = false;

            var tappedItem = (sender as StackLayout).BindingContext as BirdProgram;
            if (tappedItem != null)
            {
                await Navigation.PushAsync(new ProgramDetailPage(tappedItem.tableBird, tappedItem.tableProgram));
            }


            this.IsEnabled = true; // Разблокируем взаимодействие с текущей страницей
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}