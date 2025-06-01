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
            StartTimers();
        }

        private void StartTimers()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                UpdateTimers();
                return true; // Возвращаем true, чтобы таймер продолжал работать
            });
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
                TotalTimeRemainingLabel.Text = "Партия завершена!";
                return; // Выходим из метода, так как партия завершена
            }

            // Обновляем оставшееся время до конца текущего дня
            UpdateEndOfDayTimer();

            // Обновляем оставшееся время до следующей фазы
            UpdateNextPhaseTimer();
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
                NextDayTimeRemainingLabel.Text = "Сегодняшний день завершен!";
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

        private async void OnCompletedButtonClicked(object sender, EventArgs e)
        {
            // Получаем объект дня, который был завершен
            var button = sender as Button;
            var dopInfo = button?.CommandParameter as SQLliteTableDopInfoParty;

            if (dopInfo != null)
            {
                // Проверяем, наступил ли день
                if (dopInfo.IsNotCompleted)
                {
                    await DisplayAlert("Ошибка", "День еще не наступил!", "OK");
                    return; // Если день не наступил, выходим из метода
                }

                // Отображаем сообщение с вариантами
                var action = await DisplayActionSheet("Выберите действие", "Отмена", null, "Выполнено", "Не выполнено");

                switch (action)
                {
                    case "Выполнено":
                        dopInfo.IsCompleted = true;
                        dopInfo.IsNotCompleted = false;
                        await DisplayAlert("Завершено", $"День {dopInfo.Day} завершен!", "OK");
                        break;

                    case "Не выполнено":
                        dopInfo.IsNotCompleted = true;
                        dopInfo.IsCompleted = false;
                        await DisplayAlert("Не выполнено", $"День {dopInfo.Day} не выполнен.", "OK");
                        break;

                    case "Отмена":
                        // Действие отмены
                        break;
                }

                // Обновление интерфейса произойдет автоматически благодаря ObservableCollection
            }
        }
    }
}
