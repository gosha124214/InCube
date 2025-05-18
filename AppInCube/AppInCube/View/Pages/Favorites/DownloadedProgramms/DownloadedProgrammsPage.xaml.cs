using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using AppInCube.View.Pages.Favorites.DownloadedProgramms.UnderPagesInFavorites;
using AppInCube.View.Pages.Favorites.ProgrammsInProcess;
using AppInCube.Classes.SQLite.Downloaded;
using AppInCube.Classes.SQLite.Partyes;
using SQLite;

namespace AppInCube.View.Pages.Favorites.DownloadedProgramms
{
    public partial class DownloadedProgrammsPage : ContentPage
    {
        public DownloadedProgrammsPage()
        {
            InitializeComponent();
        }


        private async void OnStartButtonClicked(object sender, EventArgs e)
        {
            // Получаем объект программы, которую нужно запустить
            var button = sender as Button;
            var program = button?.CommandParameter as SQLliteTableBaseInfo;

            if (program != null)
            {
                // Создаем новую запись в базе данных для запущенной программы
                await StartProgram(program);

                // Переход на страницу ProgrammsInProcess
                await Navigation.PushAsync(new ProgrammsInProcess());
            }
        }


        private async Task StartProgram(SQLliteTableBaseInfo program)
        {
            // Создаем новую запись в базе данных
            var newParty = new SQLliteTableParty
            {
                IdProgramInMySQL = program.IdProgramInMySQL,
                IdBirdInMySQL = program.IdBirdInMySQL,
                DateTimeValue = DateTime.Now // Устанавливаем текущее время
            };

            try
            {
                // Сохраняем новую запись в базе данных
                await App.Database.SavePartyAsync(newParty);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Ошибка при запуске программы: {ex.Message}", "OK");
            }
        }




        private async Task StartProgram(SQLliteTableBaseInfo program)
{
    // Создаем новую запись в базе данных
    var newParty = new SQLliteTableParty
    {
        IdProgramInMySQL = program.IdProgramInMySQL,
        IdBirdInMySQL = program.IdBirdInMySQL,
        DateTimeValue = DateTime.Now // Устанавливаем текущее время
    };

    try
    {
        // Сохраняем новую запись в базе данных
        await App.Database.SavePartyAsync(newParty);
    }
    catch (Exception ex)
    {
        await Application.Current.MainPage.DisplayAlert("Ошибка", $"Ошибка при запуске программы: {ex.Message}", "OK");
    }
}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFavoritePrograms(); // Загружаем программы при появлении страницы
        }

        private async Task LoadFavoritePrograms()
        {
            try
            {
                var favoritePrograms = await App.Database.GetProgramsAsync(); // Предполагается, что этот метод возвращает все программы

                // Привязка данных к интерфейсу
                ProgramsListView.ItemsSource = favoritePrograms; // Привязка данных к CollectionView
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Ошибка при загрузке данных: {ex.Message}", "OK");
            }
        }


        private async void OnProgramTapped(object sender, EventArgs e)
        {

            // Блокируем взаимодействие с текущей страницей
            this.IsEnabled = false;

            var tappedItem = (sender as StackLayout).BindingContext as SQLliteTableBaseInfo;
            if (tappedItem != null)
            {
                await Navigation.PushAsync(new ProgramDetailPage(tappedItem));
            }


            this.IsEnabled = true; // Разблокируем взаимодействие с текущей страницей
        }


        private async void DeleteProgram(SQLliteTableBaseInfo program)
        {
            // Подтверждение удаления
            bool confirm = await DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить эту программу?", "Да", "Нет");
            if (confirm)
            {
                try
                {
                    // Удаляем программу из базы данных по ID
                    int result = await App.Database.DeleteProgramAsync(program.IdBirdInMySQL); // Используем ProgramId

                    if (result > 0)
                    {
                        await LoadFavoritePrograms(); // Обновляем список
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Программа не найдена для удаления.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", $"Ошибка при удалении программы: {ex.Message}", "OK");
                }
            }
        }
        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            // Получаем объект программы, который нужно удалить
            var button = sender as Button;
            var program = button?.CommandParameter as SQLliteTableBaseInfo;

            if (program != null)
            {
                DeleteProgram(program);
            }
        }



    }
}