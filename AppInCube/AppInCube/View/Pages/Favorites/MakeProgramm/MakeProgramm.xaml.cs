using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite.Maked;
using AppInCube.View.Pages.Favorites.MakeProgramm.UnderPagesInMakeProgramm;
using AppInCube.Classes.SQLite.Partyes;
using AppInCube.Services;

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


        private async Task StartProgram(SQLliteTableBaseInfoMake program)
        {
            // Создаем новую запись в базе данных для запущенной программы
            var newParty = new SQLliteTableParty
            {
                IdProgramInMySQL = program.IdProgramInMySQL,
                IdBirdInMySQL = program.IdBirdInMySQL,
                IdMake = program.IdMakeProgram,
                DateTimeValue = DateTime.Now,
                ImageBirdFile = program.ImageBirdFile,
                DopInfoParty = new List<SQLliteTableDopInfoParty>()
            };

            try
            {
                // Получаем информацию о доп. данных программы по ID
                List<SQLliteTableDopInfoMake> existingDopInfo = await App.DatabaseMakePrograms.GetDopInfoByProgramIdAsync(program.IdMakeProgram);

                // Создаем временный список для хранения информации о днях
                var dopInfoPartyList = new List<SQLliteTableDopInfoParty>();

                // Добавляем информацию о днях в временный список
                foreach (var dopInfo in existingDopInfo)
                {
                    dopInfoPartyList.Add(new SQLliteTableDopInfoParty
                    {
                        IdParty = newParty.IdParty,
                        Day = dopInfo.Day,
                        MinTemperature = dopInfo.MinTemperature,
                        MaxTemperature = dopInfo.MaxTemperature,
                        MinHumidity = dopInfo.MinHumidity,
                        MaxHumidity = dopInfo.MaxHumidity,
                        MinАmountTurn = dopInfo.MinАmountTurn,
                        MaxАmountTurn = dopInfo.MaxАmountTurn,
                        АmountCooling = dopInfo.АmountCooling,
                        MinTimeCooling = dopInfo.MinTimeCooling,
                        MaxTimeCooling = dopInfo.MaxTimeCooling
                    });
                }

                // Присваиваем временный список в свойство DopInfoParty
                newParty.DopInfoParty = dopInfoPartyList;

                // Сохраняем новую запись в базе данных
                await App.DatabaseParty.SavePartyAsync(newParty);

                // Получаем сохраненную партию с реальным ID
                var savedParty = await App.DatabaseParty.GetPartyByIdAsync(newParty.IdParty);

                if (savedParty != null)
                {
                    // Планируем уведомления с указанием ID партии
                    await SchedulePartyNotifications(savedParty);

                    await Application.Current.MainPage.DisplayAlert("Успех",
                        $"Программа '{program.NameBird}' успешно запущена!\n" +
                        $"ID партии: {savedParty.IdParty}\n" +
                        $"Создано {GetNumberOfScheduledNotifications(dopInfoPartyList)} уведомлений.",
                        "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Ошибка при запуске программы: {ex.Message}", "OK");
            }
        }

        // Метод для планирования уведомлений с ID партии
        private async Task SchedulePartyNotifications(SQLliteTableParty party)
        {
            try
            {
                var notificationService = ServiceProviderHelper.GetService<IMyNotificationService>();
                if (notificationService == null)
                {
                    Console.WriteLine("Сервис уведомлений не найден");
                    return;
                }

                var dopInfoList = party.DopInfoParty;
                var startTime = party.DateTimeValue;
                var sortedDays = dopInfoList.OrderBy(d => d.Day).ToList();

                // Уникальный идентификатор для уведомлений этой партии
                string partyIdentifier = $"PID{party.IdParty}"; // PID = Party ID

                // 1. Уведомление о начале программы
                await notificationService.ScheduleExactNotificationAsync(
                    $"🚀 Инкубация началась [{partyIdentifier}]",
                    $"Программа #{party.IdParty}\n" +
                    $"Начало: {startTime:dd.MM.yyyy HH:mm}\n" +
                    $"Первый день: {sortedDays[0].MinTemperature}°C - {sortedDays[0].MaxTemperature}°C",
                    startTime);

                Console.WriteLine($"✅ Уведомление о начале для партии {party.IdParty}");

                // 2. Уведомления о смене параметров
                for (int i = 1; i < sortedDays.Count; i++)
                {
                    if (ParametersDiffer(sortedDays[i - 1], sortedDays[i]))
                    {
                        var notificationTime = startTime.AddDays(sortedDays[i].Day);

                        await notificationService.ScheduleExactNotificationAsync(
                            $"🔄 Смена параметров [{partyIdentifier}]",
                            $"Программа #{party.IdParty}, День {sortedDays[i].Day}\n" +
                            $"Температура: {sortedDays[i].MinTemperature}°C - {sortedDays[i].MaxTemperature}°C\n" +
                            $"Влажность: {sortedDays[i].MinHumidity}% - {sortedDays[i].MaxHumidity}%",
                            notificationTime);

                        Console.WriteLine($"✅ Уведомление на день {sortedDays[i].Day} для партии {party.IdParty}");
                    }
                }

                // 3. Уведомление о завершении партии
                var endTime = startTime.AddDays(sortedDays.Last().Day);
                await notificationService.ScheduleExactNotificationAsync(
                    $"🏁 Инкубация завершена [{partyIdentifier}]",
                    $"Программа #{party.IdParty} завершена!\n" +
                    $"Начало: {startTime:dd.MM.yyyy HH:mm}\n" +
                    $"Завершение: {endTime:dd.MM.yyyy HH:mm}",
                    endTime);

                Console.WriteLine($"✅ Уведомление о завершении для партии {party.IdParty}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка при планировании уведомлений для партии: {ex.Message}");
            }
        }

   


        // Метод для проверки различий параметров (такой же как в UnderPagesInProcess)
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

        // Вспомогательный метод для подсчета количества запланированных уведомлений
        private int GetNumberOfScheduledNotifications(List<SQLliteTableDopInfoParty> dopInfoList)
        {
            if (dopInfoList == null || dopInfoList.Count < 2)
                return 1; // Только уведомление о начале

            var sortedDays = dopInfoList.OrderBy(d => d.Day).ToList();
            int count = 1; // Уведомление о начале

            for (int i = 1; i < sortedDays.Count; i++)
            {
                if (ParametersDiffer(sortedDays[i - 1], sortedDays[i]))
                {
                    count++;
                }
            }

            return count;
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
