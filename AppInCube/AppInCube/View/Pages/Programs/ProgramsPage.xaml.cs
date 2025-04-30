using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppInCube.Classes;
using Microsoft.Maui.Controls;
using static AppInCube.Classes.MetodMetodsGet;
using System.ComponentModel;
using AppInCube.View.Pages.Programs.UnderPages; // ���������, ��� ��� ������������ ���� ����������
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLBD;

namespace AppInCube.View.Pages.Programs
{
    public partial class ProgramsPage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<TableBird> TableBird { get; set; } // ��������� ��� �������� ��������

        private uint _currentOffset = 1; // ���������� ��� ������������ �������� ��������
        private const int _pageSize = 2; // ���������� ����������� �������� �� ���


        private uint? _numberOfAllObjects = 0; // ����� ���������� ��������

        private readonly ApiService _apiService = new ApiService();

        public ProgramsPage()
        {
            InitializeComponent();

            TableBird = new ObservableCollection<TableBird>(); // ������������� ���������
            BindingContext = this; // ������������� �������� ��������

            // ��������� ����������� ����� ��� �������� ������
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
                    OnPropertyChanged(nameof(NumberOfAllObjectsInDataBase)); // ���������� �� ���������
                }
            }
        }

        public uint LoadedProgramsCount
        {
            get { return (uint)TableBird.Count; }
        }

        private async void LoadInitialPrograms()
        {
            await LoadPrograms(_pageSize); // ��������� ������ 2 ���������
        }

        private async void OnLoadMoreClicked(object sender, EventArgs e)
        {
            await LoadPrograms(_pageSize); // ��������� ��� 2 ��������� ��� ������� ������
        }

        private async Task LoadPrograms(int count)
        {
            // ��������� �������������� � ������� ���������
            this.IsEnabled = false;

            NumberOfAllObjectsInDataBase = await _apiService.GetTableBirdCountAllProgram(); // �������� ����� ���������� ��������

            if (NumberOfAllObjectsInDataBase >= _currentOffset)
            {
                for (int i = 0; i < count; i++)
                {

                    TableBird? newBird = await _apiService.GetTableBirdAsync(_currentOffset);


                   

                    if (newBird != null && newBird.Id != 0) // ���������, ��� ��������� ���� ���������
                    {
                        // ����������� ������� ������ � ImageSource
                        newBird.ImageSource = ImageSource.FromStream(() => new MemoryStream(newBird.ImageBirdFile));




                        // ���������� � ���������
                        TableBird.Add(newBird);

                        _currentOffset++;
                        OnPropertyChanged(nameof(LoadedProgramsCount));
                    }
                    else
                    {
                        ShowMessage("�� ������� ��������� ���������.\n��������� ���������� � ����������");
                        break;
                    }
                }
            }
            else
            {
                ShowMessage("�������� ������ ���");
            }

            this.IsEnabled = true; // ������������ �������������� � ������� ���������
        }


        private async void ShowMessage(string message, int duration = 5000)
        {
            MessageLabel.Text = message; // ������������� ����� ���������
            MessageFrame.IsVisible = true; // ������ Frame �������

            // ���� ��������� �����
            await Task.Delay(duration);

            // �������� Frame
            MessageFrame.IsVisible = false;
        }


        private async void OnProgramTapped(object sender, EventArgs e)
        {

            // ��������� �������������� � ������� ���������
            this.IsEnabled = false;

            var tappedItem = (sender as StackLayout).BindingContext as TableBird;
            List<TableProgram>? newProgram = await _apiService.GetTableProgramAsync(tappedItem.IdProgram);



            if (tappedItem != null)
            {
                await Navigation.PushAsync(new ProgramDetailPage(tappedItem, newProgram));
            }


            this.IsEnabled = true; // ������������ �������������� � ������� ���������
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}