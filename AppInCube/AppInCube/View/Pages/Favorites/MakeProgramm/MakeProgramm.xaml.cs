using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite.Maked;
using AppInCube.View.Pages.Favorites.MakeProgramm.UnderPagesInMakeProgramm;

namespace AppInCube.View.Pages.Favorites.MakeProgramm
{
    public partial class MakeProgramm : ContentPage
    {
        public MakeProgramm()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadCreatedPrograms(); // Загружаем созданные программы при появлении страницы
        }
        private async Task LoadCreatedPrograms()
        {
            try
            {
                var createdPrograms = await App.DatabaseMakePrograms.GetProgramsAsync(); // Получаем все созданные программы
                ProgramsListView.ItemsSource = createdPrograms; // Привязка данных к CollectionView
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Ошибка при загрузке данных: {ex.Message}", "OK");
            }
        }
        private async void OnProgramSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                var selectedProgram = e.CurrentSelection[0] as SQLliteTableBaseInfoMake;
                if (selectedProgram != null)
                {
                    // Переход на страницу с деталями программы
                    await Navigation.PushAsync(new ProgramDetailPageMake(selectedProgram));
                }
                // Сброс выбора
                ProgramsListView.SelectedItem = null;
            }


        }

        private async void OnCreateProgramButtonClicked(object sender, EventArgs e)
        {
            // Переход на страницу UnderPagesMakeProgramm
            await Navigation.PushAsync(new UnderPagesMakeProgramm());
        }

        private async void OnStartButtonClicked(object sender, EventArgs e)
        {
            // Получаем объект программы, которую нужно запустить
            var button = sender as Button;
            var program = button?.CommandParameter as SQLliteTableBaseInfoMake;

            if (program != null)
            {
                // Логика запуска программы
                await StartProgram(program);
            }
        }
        private async void OnProgramTapped(object sender, EventArgs e)
        {
            // Получаем объект программы, на которую нажали
            var tappedItem = (sender as StackLayout).BindingContext as SQLliteTableBaseInfoMake;
            if (tappedItem != null)
            {
                // Переход на страницу с деталями программы
                await Navigation.PushAsync(new ProgramDetailPageMake(tappedItem));
            }
        }

        private async Task StartProgram(SQLliteTableBaseInfoMake program)
        {
            // Логика запуска программы
            // ...
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            // Получаем объект программы, который нужно удалить
            var button = sender as Button;
            var program = button?.CommandParameter as SQLliteTableBaseInfoMake;

            if (program != null)
            {
                await DeleteProgram(program);
            }
        }

        private async Task DeleteProgram(SQLliteTableBaseInfoMake program)
        {
            // Подтверждение удаления
            bool confirm = await DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить эту программу?", "Да", "Нет");
            if (confirm)
            {
                try
                {
                    // Удаляем программу из базы данных по ID
                    int result = await App.DatabaseMakePrograms.DeleteProgramAsync(program.IdMakeBird); // Используем IdBirdInMySQL

                    if (result > 0)
                    {
                        await LoadCreatedPrograms(); // Обновляем список
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
    }
}
