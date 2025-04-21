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

        public ProgramDetailPage(TableBird program)
        {
            this.program = program;

            LoadProgramById(program.Id);
            InitializeComponent();
            BindingContext = program; // Устанавливаем контекст привязки на переданный объект

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
            SaveProgramData(program.Id);

        }
        private async void SaveProgramData(uint programId)
        {
            // Проверяем, существует ли программа с указанным ID
            var existingProgram = await App.Database.GetProgramByIdAsync(programId);
            if (existingProgram == null) // Если такой программы нет, то загружаем
            {
                // Получаем данные из BindingContext
                var program = BindingContext as TableBird;

                // Создаем объект SQLliteTableBaseInfo и заполняем его данными из объекта TableBird
                var programData = new SQLliteTableBaseInfo
                {
                    IdBirdInMySQL = program.Id, // Используем Id из TableBird
                    IdProgramInMySQL = program.IdProgram,
                    NameBird = program.NameBird,
                    Content = program.Content,
                 //   DaysUntilHatching = program.DaysUntilHatching,
                    DateTimeValue = program.DateTimeValue,
                    ImageBirdFile = program.ImageBirdFile // Сохраняем массив байтов изображения
                };

                try
                {
                    // Используем метод SaveProgramAsync для сохранения данных в таблицу
                    await App.Database.SaveProgramAsync(programData); // Убедитесь, что метод принимает SQLliteTableBaseInfo

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