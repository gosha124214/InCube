using AppInCube.Classes; // Убедитесь, что это пространство имен правильное
using AppInCube.Classes.SQLite;
using AppInCube.Classes.SQLBD;
using Microsoft.Maui.Controls;
using SQLite;
//using static Android.Webkit.ConsoleMessage;

namespace AppInCube.View.Pages.Programs.UnderPages
{
    public partial class ProgramDetailPage : ContentPage
    {

        TableBird program = null;


        public ProgramDetailPage(TableBird bird, List<TableProgram> programs)
        {
            InitializeComponent();

            // Устанавливаем контекст привязки на новый объект, который содержит как птицу, так и программы
            BindingContext = new
            {
                NameBird = bird.NameBird,
                Content = bird.Content,
                Id = bird.Id,
                IdProgram = bird.IdProgram,
                DateTimeValue = bird.DateTimeValue,
                DaysUntilHatching = bird.DaysUntilHatching,
                ImageSource = ImageSource.FromStream(() => new MemoryStream(bird.ImageBirdFile)),
                tableProgram = programs // Привязываем программы
            };

            LoadProgramById(bird.Id);
        }

        private async void LoadProgramById(uint programId)
        {
            var idProgram = await App.Database.GetProgramByIdAsync(programId);
            if (idProgram != null)
            {
                GoToProgramButton.IsVisible = true;
            }
            else
            {
                GoToProgramButton.IsVisible = false;
            }
        }



        private void DownloadTheProgram(object sender, EventArgs e)
        {
            // Вызов метода для сохранения данных
            SaveProgramData(program.Id, program.IdProgram);

        }
        private async void SaveProgramData(uint programId, uint IdProgram)
        {
            // Проверяем, существует ли программа с указанным ID
            var existingProgram = await App.Database.GetProgramByIdAsync(programId);
            var existingProgramDop = await App.Database.GetProgramByIdAsyncDop(IdProgram);
            if (existingProgram == null) // Если такой программы нет, то загружаем
            {
                // Получаем данные из BindingContext
                var programBaseInfo = BindingContext as TableBird;

                // Создаем объект SQLliteTableBaseInfo и заполняем его данными из объекта TableBird
                var programDataBaseInfo = new SQLliteTableBaseInfo
                {
                    IdBirdInMySQL = programBaseInfo.Id, // Используем Id из TableBird
                    IdProgramInMySQL = programBaseInfo.IdProgram,
                    NameBird = programBaseInfo.NameBird,
                    Content = programBaseInfo.Content,
                    DaysUntilHatching = programBaseInfo.DaysUntilHatching,
                    DateTimeValue = programBaseInfo.DateTimeValue,
                    ImageBirdFile = programBaseInfo.ImageBirdFile // Сохраняем массив байтов изображения
                };

                    // Получаем данные из BindingContext
                    var program = BindingContext as TableProgram;

                    var programData = new SQLliteTableDopInfo
                    {
                        IdProgram = program.IdProgram,
                        Day = program.Day,
                        MinHumidity = program.MinHumidity,
                        MaxHumidity = program.MaxHumidity,
                        MinTemperature = program.MinTemperature,
                        MaxTemperature = program.MinTemperature,
                        MinTimeCooling = program.MinTimeCooling,
                        MaxTimeCooling = program.MaxTimeCooling,
                        MinАmountTurn = program.MinАmountTurn,
                        MaxАmountTurn = program.MaxАmountTurn,
                        АmountCooling = program.АmountCooling

                    };
                

                try
                {
                    // Используем метод SaveProgramAsync для сохранения данных в таблицу
                    await App.Database.SaveProgramAsync(programDataBaseInfo); // Убедитесь, что метод принимает SQLliteTableBaseInfo

                    GoToProgramButton.IsVisible = true;
                    Console.WriteLine("Данные успешно сохранены!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
                }

                ShowMessage("Программа загружена");
            }
            else
            {
                ShowMessage("Эта программа уже загружена");
            }
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



        // Обработчик события для перехода к программе
        private async void GoToProgram(object sender, EventArgs e)
        {
            // Логика перехода к программе
            await DisplayAlert("Переход", "Вы перешли к программе!", "OK");
            // Здесь вы можете добавить логику для перехода к другой странице или выполнения других действий
        }

    }
}