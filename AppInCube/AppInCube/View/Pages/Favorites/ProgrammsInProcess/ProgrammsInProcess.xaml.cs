using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite.Partyes;
using AppInCube.Services;

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
                var runningPrograms = await App.DatabaseParty.GetPartiesAsync();
                RunningProgramsListView.ItemsSource = runningPrograms;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Ошибка при загрузке данных: {ex.Message}", "OK");
            }
        }

        private async void OnProgramTapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var tappedItem = (sender as StackLayout).BindingContext as SQLliteTableParty;

            if (tappedItem != null)
            {
                await Navigation.PushAsync(new UnderPagesInProcess.UnderPagesInProcess(tappedItem));
            }
            this.IsEnabled = true;
        }

        private async void OnCancelPartyButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var party = button?.CommandParameter as SQLliteTableParty;

            if (party != null)
            {
                bool confirm = await DisplayAlert("Подтверждение",
                    $"Отменить партию #{party.IdParty}?\n" +
                    $"Удалятся все связанные уведомления.",
                    "Да", "Нет");

                if (confirm)
                {
                    button.IsEnabled = false;
                    button.Text = "Отмена...";

                    try
                    {
                        // Отменяем все уведомления для этой партии
                        await CancelPartyNotificationsById(party.IdParty);

                        // Удаляем партию из базы данных
                        await App.DatabaseParty.DeletePartyAsync(party.IdParty);
                        await LoadRunningPrograms();

                        await DisplayAlert("Успех",
                            $"Партия #{party.IdParty} отменена.",
                            "OK");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Ошибка",
                            $"Ошибка при отмене партии: {ex.Message}",
                            "OK");
                    }
                    finally
                    {
                        button.IsEnabled = true;
                        button.Text = "Отменить партию";
                    }
                }
            }
        }

        // Метод для отмены уведомлений по ID партии
        private async Task CancelPartyNotificationsById(uint partyId)
        {
            try
            {
                var notificationService = ServiceProviderHelper.GetService<IMyNotificationService>();
                if (notificationService != null)
                {
                    string partyIdentifier = $"PID{partyId}";
                    var allNotifications = await notificationService.GetScheduledNotificationsAsync();

                    // Находим уведомления с указанным ID партии
                    var partyNotifications = allNotifications
                        .Where(n => n.Title.Contains(partyIdentifier) ||
                                   n.Message.Contains($"Программа #{partyId}"))
                        .ToList();

                    int canceledCount = 0;
                    foreach (var notification in partyNotifications)
                    {
                        await notificationService.CancelNotificationAsync(notification.Id);
                        canceledCount++;
                        Console.WriteLine($"Отменено уведомление #{notification.Id} для партии {partyId}");
                    }

                    if (canceledCount > 0)
                    {
                        Console.WriteLine($"✅ Отменено {canceledCount} уведомлений для партии {partyId}");
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ Не найдено уведомлений для партии {partyId}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка при отмене уведомлений для партии {partyId}: {ex.Message}");
            }
        }

        // Метод для проверки различий параметров
        private bool ParametersDiffer(SQLliteTableDopInfoParty currentDay, SQLliteTableDopInfoParty nextDay)
        {
            return currentDay.MinTemperature != nextDay.MinTemperature ||
                   currentDay.MaxTemperature != nextDay.MaxTemperature ||
                   currentDay.MinHumidity != nextDay.MinHumidity ||
                   currentDay.MaxHumidity != nextDay.MaxHumidity ||
                   currentDay.MinАmountTurn != nextDay.MinАmountTurn ||
                   currentDay.MaxАmountTurn != nextDay.MaxАmountTurn ||
                   currentDay.АmountCooling != nextDay.АmountCooling ||
                   currentDay.MinTimeCooling != nextDay.MinTimeCooling ||
                   currentDay.MaxTimeCooling != nextDay.MaxTimeCooling;
        }
    }
}