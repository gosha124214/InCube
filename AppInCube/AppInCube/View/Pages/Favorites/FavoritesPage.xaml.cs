using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite;

namespace AppInCube.View.Pages.Favorites
{
    public partial class FavoritesPage : ContentPage
    {
        public FavoritesPage()
        {
            InitializeComponent();
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
        private async void DeleteProgram(SQLliteTableBaseInfo program)
        {
            // Подтверждение удаления
            bool confirm = await DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить эту программу?", "Да", "Нет");
            if (confirm)
            {
                try
                {
                    // Удаляем программу из базы данных по ID
                    int result = await App.Database.DeleteProgramAsync(program.IdProgramInMySQL); // Используем ProgramId

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