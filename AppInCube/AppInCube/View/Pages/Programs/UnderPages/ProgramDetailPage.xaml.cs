using AppInCube.Classes; // Убедитесь, что это пространство имен правильное
using AppInCube.Classes.SQLite;
using AppInCube.Classes.SQLBD;
using Microsoft.Maui.Controls;
using SQLite;
using System;
//using static Android.Webkit.ConsoleMessage;

namespace AppInCube.View.Pages.Programs.UnderPages
{
    public partial class ProgramDetailPage : ContentPage
    {

        TableBird program = new TableBird();


        public ProgramDetailPage(TableBird bird, List<TableProgram> programs)
        {
            InitializeComponent();
            program.Id = bird.Id;
            // Устанавливаем контекст привязки на новый объект, который содержит как птицу, так и программы
            BindingContext = new SQLliteTableBaseInfo
            {

                IdBirdInMySQL = bird.Id,//

                NameBird = bird.NameBird,
                Content = bird.Content,

                IdProgramInMySQL = bird.IdProgram, //

                DateTimeValue = bird.DateTimeValue,
                DaysUntilHatching = bird.DaysUntilHatching,

                ImageBirdFile = bird.ImageBirdFile, //

                tablePrograms = programs // Привязываем программы//
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
            SaveProgramData(program.Id);

        }
        private async void SaveProgramData(uint birdId)
        {
            // Проверяем, существует ли программа с указанным ID
            var existingProgram = await App.Database.GetProgramByIdAsync(birdId);
            if (existingProgram == null) // Если такой программы нет, то загружаем
            {
                // Получаем данные из BindingContext
                var programBaseInfo = BindingContext as SQLliteTableBaseInfo;


                // Создаем объект SQLliteTableBaseInfo и заполняем его данными из объекта TableBird
                var programDataBaseInfo = new SQLliteTableBaseInfo
                {
                    IdBirdInMySQL = programBaseInfo.IdBirdInMySQL, // Используем Id из TableBird
                    IdProgramInMySQL = programBaseInfo.IdProgramInMySQL,
                    NameBird = programBaseInfo.NameBird,
                    Content = programBaseInfo.Content,
                    DaysUntilHatching = programBaseInfo.DaysUntilHatching,
                    DateTimeValue = programBaseInfo.DateTimeValue,
                    ImageBirdFile = programBaseInfo.ImageBirdFile, // Сохраняем массив байтов изображения
                    tablePrograms = programBaseInfo.tablePrograms
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