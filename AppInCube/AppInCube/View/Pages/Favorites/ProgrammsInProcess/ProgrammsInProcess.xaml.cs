using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite.Partyes;

namespace AppInCube.View.Pages.Favorites.ProgrammsInProcess
{
    public partial class ProgrammsInProcess : ContentPage
    {
        public ProgrammsInProcess()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadRunningPrograms();
        }

        private async Task LoadRunningPrograms()
        {
            try
            {
                var runningPrograms = await App.DatabaseParty.GetPartiesAsync(); // Получаем все партии
                RunningProgramsListView.ItemsSource = runningPrograms; // Привязываем запущенные программы
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Ошибка при загрузке данных: {ex.Message}", "OK");
            }
        }
        private async void OnProgramTapped(object sender, EventArgs e)
        {
            // Блокируем взаимодействие с текущей страницей
            this.IsEnabled = false;

            var tappedItem = (sender as StackLayout).BindingContext as SQLliteTableParty;


            if (tappedItem != null)
            {
                await Navigation.PushAsync(new UnderPagesInProcess.UnderPagesInProcess(tappedItem));
            }

            this.IsEnabled = true; // Разблокируем взаимодействие с текущей страницей
        }
        private async void OnCancelPartyButtonClicked(object sender, EventArgs e)
        {
            // Получаем объект партии, которую нужно отменить
            var button = sender as Button;
            var party = button?.CommandParameter as SQLliteTableParty;

            if (party != null)
            {
                // Подтверждение отмены
                bool confirm = await DisplayAlert("Подтверждение", "Вы уверены, что хотите отменить эту партию?", "Да", "Нет");
                if (confirm)
                {
                    try
                    {
                        // Удаляем партию из базы данных
                        await App.DatabaseParty.DeletePartyAsync(party.IdParty);
                        await LoadRunningPrograms(); // Обновляем список
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Ошибка", $"Ошибка при отмене партии: {ex.Message}", "OK");
                    }
                }
            }
        }
    }
}
