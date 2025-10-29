using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite.Partyes;

namespace AppInCube.View.Pages.Favorites.ProgrammsInProcess.UnderPagesInProcess
{
    public partial class UnderPagesInProcess : ContentPage
    {
        private DateTime partyStartTime;
        private TimeSpan totalDuration;
        private DateTime partyEndTime;
        private ObservableCollection<SQLliteTableDopInfoParty> dopInfoList;
        private SQLliteTableDopInfoParty selectedDayForRecording;
        public UnderPagesInProcess(SQLliteTableParty party)
        {
            InitializeComponent();

            // Устанавливаем контекст привязки на новый объект, который содержит как птицу, так и программы
            BindingContext = party;

            // Инициализация времени
            partyStartTime = party.DateTimeValue;
            totalDuration = TimeSpan.FromDays(party.DopInfoParty.Count); // Количество дней из списка DopInfoParty
            partyEndTime = partyStartTime.Add(totalDuration); // Время окончания партии

            // Инициализация ObservableCollection
            dopInfoList = new ObservableCollection<SQLliteTableDopInfoParty>(party.DopInfoParty);

            // Запуск таймеров
            if (partyEndTime >= DateTime.Now)
            {
                StartTimers();
            }
            else
            {
                TimeStartProgramm.Text = "Партия завершена!";
            }

        }

        private void StartTimers()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {

                UpdateStartTimers();

                UpdateTimers();

                UpdateEndOfDayTimer(); // Обновляем оставшееся время до конца текущего дня

                UpdateNextPhaseTimer(); // Обновляем оставшееся время до следующей фазы

                return true; // Возвращаем true, чтобы таймер продолжал работать
            });
        }
        private void UpdateStartTimers()
        {
            // Обновляем оставшееся время до завершения партии
            var timeRemaining = DateTime.Now - partyStartTime;

            if (timeRemaining.TotalSeconds > 0)
            {
                TimeStartProgramm.Text = $"Партия длится: {timeRemaining.Days} дн. {timeRemaining.Hours} ч. {timeRemaining.Minutes} мин. {timeRemaining.Seconds} сек.";
            }
            else
            {
                TimeStartProgramm.Text = "";
                return; // Выходим из метода, так как партия завершена
            }


        }
        private void UpdateTimers()
        {
            // Обновляем оставшееся время до завершения партии
            var timeRemaining = partyEndTime - DateTime.Now;

            if (timeRemaining.TotalSeconds > 0)
            {
                TotalTimeRemainingLabel.Text = $"Осталось до завершения партии: {timeRemaining.Days} дн. {timeRemaining.Hours} ч. {timeRemaining.Minutes} мин. {timeRemaining.Seconds} сек.";
            }
            else
            {
                TotalTimeRemainingLabel.Text = " ";
                return; // Выходим из метода, так как партия завершена
            }


        }

        private void UpdateEndOfDayTimer()
        {
            // Вычисляем оставшееся время до конца текущего дня
            var endOfDay = partyStartTime.AddDays(GetCurrentDayIndex() + 1);
            var timeToEndOfDay = endOfDay - DateTime.Now;

            if (timeToEndOfDay.TotalSeconds > 0)
            {
                NextDayTimeRemainingLabel.Text = $"Осталось до конца дня: {timeToEndOfDay.Hours} ч. {timeToEndOfDay.Minutes} мин. {timeToEndOfDay.Seconds} сек.";
            }
            else
            {
                NextDayTimeRemainingLabel.Text = " ";
                partyStartTime = endOfDay; // Устанавливаем новое время начала на следующий день
                UpdateEndOfDayTimer(); // Обновляем таймер
            }
        }

        private void UpdateNextPhaseTimer()
        {
            // Находим текущий день
            int currentDayIndex = GetCurrentDayIndex();

            // Сравниваем текущий день с последующими
            for (int i = currentDayIndex; i < dopInfoList.Count - 1; i++)
            {
                if (ParametersDiffer(dopInfoList[i], dopInfoList[i + 1]))
                {
                    // Проверяем, наступил ли день
                    bool isDayPassed = DateTime.Now >= partyStartTime.AddDays(i + 1);
                    dopInfoList[i].IsNotCompleted = !isDayPassed; // Если день не наступил, кнопка будет неактивна

                    // Вычисляем оставшееся время до следующего отличительного дня
                    var nextPhaseTimeRemaining = partyStartTime.AddDays(i + 1) - DateTime.Now;

                    if (nextPhaseTimeRemaining.TotalSeconds > 0)
                    {
                        NextPhaseTimeRemainingLabel.Text = $"Осталось до следующей фазы: {nextPhaseTimeRemaining.Days} дн. {nextPhaseTimeRemaining.Hours} ч. {nextPhaseTimeRemaining.Minutes} мин. {nextPhaseTimeRemaining.Seconds} сек.";
                    }
                    else
                    {
                        NextPhaseTimeRemainingLabel.Text = "Следующая фаза началась!";
                        partyStartTime = partyStartTime.AddDays(1); // Устанавливаем новое время начала на следующий день
                        UpdateNextPhaseTimer(); // Обновляем таймер
                    }
                    return; // Выходим из метода, так как нашли первое различие
                }
            }

            // Если различий не найдено
            NextPhaseTimeRemainingLabel.Text = "Все параметры одинаковы, следующая фаза не начнется.";
        }

        private int GetCurrentDayIndex()
        {
            // Определяем текущий день на основе времени
            for (int i = 0; i < dopInfoList.Count; i++)
            {
                if (DateTime.Now < partyStartTime.AddDays(i + 1))
                {
                    return i;
                }
            }
            return dopInfoList.Count - 1; // Если все дни прошли, возвращаем последний индекс
        }

        private bool ParametersDiffer(SQLliteTableDopInfoParty currentDay, SQLliteTableDopInfoParty nextDay)
        {
            // Сравниваем параметры текущего и следующего дня
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

        // ОБНОВЛЕННЫЙ МЕТОД ДЛЯ КНОПКИ "ЗАПИСАТЬ"
        private async void OnCompletedButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var dopInfo = button?.CommandParameter as SQLliteTableDopInfoParty;

            if (dopInfo != null)
            {
                // Проверяем, наступил ли день
                if (DateTime.Now < partyStartTime.AddDays(dopInfo.Day))
                {
                    await DisplayAlert("Ошибка", "День еще не наступил!", "OK");
                    return;
                }

                // Сохраняем выбранный день для записи
                selectedDayForRecording = dopInfo;

                // Показываем форму для ввода данных
                ShowRecordForm(dopInfo);
            }
        }

        private void ShowRecordForm(SQLliteTableDopInfoParty dopInfo)
        {
            // Устанавливаем выбранный день
            SelectedDayLabel.Text = dopInfo.Day.ToString();

            // Заполняем поля значениями по умолчанию
            MinTempEntry.Text = dopInfo.MinTemperature.ToString();
            MaxTempEntry.Text = dopInfo.MaxTemperature.ToString();
            MinHumidityEntry.Text = dopInfo.MinHumidity.ToString();
            MaxHumidityEntry.Text = dopInfo.MaxHumidity.ToString();
            MinTurnEntry.Text = dopInfo.MinАmountTurn?.ToString() ?? "";
            MaxTurnEntry.Text = dopInfo.MaxАmountTurn?.ToString() ?? "";
            CoolingAmountEntry.Text = dopInfo.АmountCooling?.ToString() ?? "";

            // Для TimeSpan преобразуем в минуты для удобства ввода
            MinCoolingTimeEntry.Text = dopInfo.MinTimeCooling?.TotalMinutes.ToString("F0") ?? "";
            MaxCoolingTimeEntry.Text = dopInfo.MaxTimeCooling?.TotalMinutes.ToString("F0") ?? "";

            // Показываем форму
            AddRecordFrame.IsVisible = true;
        }
        private async void OnSaveRecordClicked(object sender, EventArgs e)
        {
            if (selectedDayForRecording == null)
                return;

            try
            {
                // Находим индекс выбранного дня в dopInfoList
                var dayIndex = dopInfoList.ToList().FindIndex(d => d.Day == selectedDayForRecording.Day);
                if (dayIndex == -1)
                {
                    await DisplayAlert("Ошибка", "Не удалось найти выбранный день в списке", "OK");
                    return;
                }

                // Обновляем данные в существующей записи в dopInfoList
                var dayToUpdate = dopInfoList[dayIndex];
                dayToUpdate.MinTemperature = ParseFloat(MinTempEntry.Text);
                dayToUpdate.MaxTemperature = ParseFloat(MaxTempEntry.Text);
                dayToUpdate.MinHumidity = ParseInt(MinHumidityEntry.Text);
                dayToUpdate.MaxHumidity = ParseInt(MaxHumidityEntry.Text);
                dayToUpdate.MinАmountTurn = ParseNullableByte(MinTurnEntry.Text);
                dayToUpdate.MaxАmountTurn = ParseNullableByte(MaxTurnEntry.Text);
                dayToUpdate.АmountCooling = ParseNullableByte(CoolingAmountEntry.Text);
                dayToUpdate.MinTimeCooling = ParseTimeSpan(MinCoolingTimeEntry.Text);
                dayToUpdate.MaxTimeCooling = ParseTimeSpan(MaxCoolingTimeEntry.Text);
                dayToUpdate.IsCompleted = true;
                dayToUpdate.IsNotCompleted = false;

                // Сохраняем изменения в базу данных
                await UpdatePartyInDatabase();

                // ОБНОВЛЯЕМ BindingContext с обновленными данными
                await RefreshDataFromDatabase();

                // Показываем сообщение об успехе
                await DisplayAlert("Успех", $"Данные для дня {selectedDayForRecording.Day} обновлены!", "OK");

                // Скрываем форму
                AddRecordFrame.IsVisible = false;
                selectedDayForRecording = null;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось обновить данные: {ex.Message}", "OK");
            }
        }

        // Метод для обновления данных из базы
        private async Task RefreshDataFromDatabase()
        {
            try
            {
                var party = BindingContext as SQLliteTableParty;
                if (party != null)
                {
                    // Загружаем свежие данные из базы
                    var freshParty = await App.DatabaseParty.GetPartyByIdAsync(party.IdParty);
                    if (freshParty != null)
                    {
                        // Обновляем BindingContext
                        BindingContext = freshParty;

                        // Обновляем локальную коллекцию
                        dopInfoList = new ObservableCollection<SQLliteTableDopInfoParty>(freshParty.DopInfoParty);

                        // Принудительно обновляем CollectionView
                        RefreshCollectionView();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении данных: {ex.Message}");
            }
        }

        // Метод для принудительного обновления CollectionView
        private void RefreshCollectionView()
        {
            // Обновляем ItemsSource CollectionView
            var collectionView = this.FindByName<CollectionView>("DopInfoPartyCollectionView");
            if (collectionView != null)
            {
                var currentItems = collectionView.ItemsSource;
                collectionView.ItemsSource = null;
                collectionView.ItemsSource = currentItems;
            }
        }
        private async Task UpdatePartyInDatabase()
        {
            try
            {
                var party = BindingContext as SQLliteTableParty;
                if (party != null)
                {
                    // Полностью заменяем коллекцию DopInfoParty обновленными данными из dopInfoList
                    party.DopInfoParty = dopInfoList.ToList();

                    // Сохраняем обновленную партию в базу данных
                    int result = await App.DatabaseParty.UpdatePartyAsync(party);

                    if (result > 0)
                    {
                        Console.WriteLine($"Партия {party.IdParty} успешно обновлена в базе данных");

                        // Принудительно обновляем привязку данных
                        OnPropertyChanged(nameof(party.DopInfoParty));
                    }
                    else
                    {
                        Console.WriteLine($"Не удалось обновить партию {party.IdParty}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении в базе данных: {ex.Message}");
                throw;
            }
        }
        private void OnCancelRecordClicked(object sender, EventArgs e)

        
    {
    // Скрываем форму без сохранения
    AddRecordFrame.IsVisible = false;
    selectedDayForRecording = null;
    
    // Опционально: очищаем поля формы
    ClearFormFields();
}

// Опциональный метод для очистки полей формы
private void ClearFormFields()
{
    MinTempEntry.Text = "";
    MaxTempEntry.Text = "";
    MinHumidityEntry.Text = "";
    MaxHumidityEntry.Text = "";
    MinTurnEntry.Text = "";
    MaxTurnEntry.Text = "";
    CoolingAmountEntry.Text = "";
    MinCoolingTimeEntry.Text = "";
    MaxCoolingTimeEntry.Text = "";
    SelectedDayLabel.Text = "";
}
        // Вспомогательные методы для парсинга разных типов данных
        private float ParseFloat(string text)
        {
            if (float.TryParse(text, out float result))
                return result;
            return 0f;
        }

        private int ParseInt(string text)
        {
            if (int.TryParse(text, out int result))
                return result;
            return 0;
        }

        private byte? ParseNullableByte(string text)
        {
            if (byte.TryParse(text, out byte result))
                return result;
            return null; // Возвращаем null если не удалось распарсить
        }

        private TimeSpan? ParseTimeSpan(string text)
        {
            // Предполагаем, что время вводится в минутах
            if (int.TryParse(text, out int minutes))
            {
                return TimeSpan.FromMinutes(minutes);
            }

            // Или пытаемся распарсить в формате "hh:mm"
            if (TimeSpan.TryParse(text, out TimeSpan timeSpan))
            {
                return timeSpan;
            }

            return null; // Возвращаем null если не удалось распарсить
        }
    }
}