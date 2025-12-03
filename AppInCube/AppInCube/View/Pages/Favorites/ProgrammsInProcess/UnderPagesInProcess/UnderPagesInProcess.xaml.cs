using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite.Partyes;
using SQLite;

namespace AppInCube.View.Pages.Favorites.ProgrammsInProcess.UnderPagesInProcess
{
    public partial class UnderPagesInProcess : ContentPage
    {
        private DateTime partyStartTime;
        private TimeSpan totalDuration;
        private DateTime partyEndTime;
        private ObservableCollection<SQLliteTableDopInfoParty> dopInfoList;
        private SQLliteTableDopInfoParty selectedDayForRecording;
        private int selectedDayFrom = 1;
        private int selectedDayTo = 1;

        public UnderPagesInProcess(SQLliteTableParty party)
        {
            InitializeComponent();

            BindingContext = party;

            // Инициализация времени
            partyStartTime = party.DateTimeValue;
            totalDuration = TimeSpan.FromDays(party.DopInfoParty.Count);
            partyEndTime = partyStartTime.Add(totalDuration);

            // Инициализация ObservableCollection
            dopInfoList = new ObservableCollection<SQLliteTableDopInfoParty>(party.DopInfoParty);

            // ОБНОВЛЯЕМ СТАТУСЫ ПРИ ЗАПУСКЕ
            UpdateAllStatuses();

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
        // ОБНОВЛЕННЫЙ МЕТОД ДЛЯ ПОКАЗА ФОРМЫ
        private async void ShowRecordForm(SQLliteTableDopInfoParty dopInfo)
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
            MinCoolingTimeEntry.Text = dopInfo.MinTimeCooling?.TotalMinutes.ToString("F0") ?? "";
            MaxCoolingTimeEntry.Text = dopInfo.MaxTimeCooling?.TotalMinutes.ToString("F0") ?? "";

            // Устанавливаем начальный день
            selectedDayForRecording = dopInfo;
            selectedDayFrom = dopInfo.Day;
            selectedDayTo = dopInfo.Day; // Начально конечный день равен начальному

            // Обновляем лейблы
            SelectedDayFromLabel.Text = selectedDayFrom.ToString();
            SelectedDayToLabel.Text = selectedDayTo.ToString();
            SelectedDayLabel.Text = $"День {selectedDayFrom}";

            // Показываем форму
            AddRecordFrame.IsVisible = true;

            // Автоматически пытаемся определить период
            //await TryAutoDetectPeriod();
        }
        
        // НОВЫЙ МЕТОД: Попытка автоматического определения периода
        private async Task OnChangePeriodClicked()
        {
            try
            {
                int autoDayTo = await FindLastDayWithSameProgramParameters(selectedDayFrom);

                if (autoDayTo > selectedDayFrom)
                {
                    // Спрашиваем пользователя, хочет ли он использовать автопериод
                    bool useAutoPeriod = await DisplayAlert(
                        "Автопериод",
                        $"Найдены дни с одинаковыми параметрами в программе: {selectedDayFrom}-{autoDayTo}.\n" +
                        $"Использовать этот период ({autoDayTo - selectedDayFrom + 1} дней)?",
                        "Да", "Нет");

                    if (useAutoPeriod)
                    {
                        selectedDayTo = autoDayTo;
                        SelectedDayToLabel.Text = autoDayTo.ToString();
                        SelectedDayLabel.Text = $"Дни {selectedDayFrom}-{selectedDayTo}";
                    }
                }
            }
            catch (Exception ex)
            {
                // Игнорируем ошибки при автоопределении
                Console.WriteLine($"Ошибка при автоопределении периода: {ex.Message}");
            }
        }
        // НОВЫЙ МЕТОД: Кнопка "Автопериод"
        private async void OnAutoPeriodClicked(object sender, EventArgs e)
        {
            if (selectedDayForRecording == null)
                return;

            try
            {
                // Ищем последний день с такими же параметрами в исходной программе
                int lastSimilarDay = await FindLastDayWithSameProgramParameters(selectedDayFrom);

                if (lastSimilarDay > selectedDayFrom)
                {

                        selectedDayTo = lastSimilarDay;
                        SelectedDayToLabel.Text = lastSimilarDay.ToString();
                        SelectedDayLabel.Text = $"Дни {selectedDayFrom}-{selectedDayTo}";

                }
                else
                {
                    await DisplayAlert("Автопериод",
                        "Это последний день фазы. Автопериод не нужен.",
                        "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось определить автопериод: {ex.Message}", "OK");
            }
        }
        // ОБНОВЛЕННЫЙ МЕТОД ДЛЯ КНОПКИ
        private async void OnCompletedButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var dopInfo = button?.CommandParameter as SQLliteTableDopInfoParty;

            if (dopInfo != null)
            {
                // Проверяем доступность кнопки через статус
                if (DateTime.Now < partyStartTime.AddDays(dopInfo.Day))
                {
                    await DisplayAlert("Ошибка", "День еще не наступил!", "OK");
                    return;
                }

                selectedDayForRecording = dopInfo;
                ShowRecordForm(dopInfo);
            }
        }
        // НОВЫЙ МЕТОД: Изменение только конечного дня
        private async void OnChangeEndDayClicked(object sender, EventArgs e)
        {
            if (selectedDayForRecording == null)
                return;

            try
            {
                // Запрашиваем у пользователя только конечный день
                string result = await DisplayPromptAsync(
                    "Изменить конечный день",
                    $"Введите конечный день (сейчас: {selectedDayTo}):\n" +
                    $"Начальный день: {selectedDayFrom}",
                    "OK",
                    "Отмена",
                    selectedDayTo.ToString(),
                    3,
                    Keyboard.Numeric);

                if (!string.IsNullOrEmpty(result))
                {
                    // Парсим ввод пользователя
                    if (int.TryParse(result, out int newEndDay))
                    {
                        // Проверяем валидность ввода
                        if (newEndDay >= selectedDayFrom && newEndDay <= dopInfoList.Count)
                        {
                            selectedDayTo = newEndDay;
                            SelectedDayToLabel.Text = newEndDay.ToString();

                            // Обновляем текст в зависимости от того, один день или диапазон
                            if (selectedDayFrom == selectedDayTo)
                            {
                                SelectedDayLabel.Text = $"День {selectedDayFrom}";
                            }
                            else
                            {
                                SelectedDayLabel.Text = $"Дни {selectedDayFrom}-{selectedDayTo}";
                            }

                        }
                        else
                        {
                            await DisplayAlert("Ошибка",
                                $"Конечный день должен быть от {selectedDayFrom} до {dopInfoList.Count}",
                                "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Неверный формат числа", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось изменить день: {ex.Message}", "OK");
            }
        }


       

        // НОВЫЙ МЕТОД: Поиск последнего дня с такими же параметрами в исходной программе
        private async Task<int> FindLastDayWithSameProgramParameters(int startDay)
        {
            var party = BindingContext as SQLliteTableParty;
            if (party == null || dopInfoList == null || startDay < 1 || startDay > dopInfoList.Count)
                return startDay;

            // Получаем параметры стартового дня из программы
            var startDayProgramData = await GetProgramDayData(startDay);
            if (startDayProgramData == null)
                return startDay;

            int lastSimilarDay = startDay;

            // Проверяем последующие дни
            for (int day = startDay + 1; day <= dopInfoList.Count; day++)
            {
                var nextDayProgramData = await GetProgramDayData(day);
                if (nextDayProgramData == null)
                    break;

                // Сравниваем параметры дней в программе
                if (!ProgramDaysAreEqual(startDayProgramData, nextDayProgramData))
                {
                    // Нашли день с отличающимися параметрами в программе
                    break;
                }

                lastSimilarDay = day;
            }

            return lastSimilarDay;
        }

        // НОВЫЙ МЕТОД: Получение данных дня из программы (созданной или скачанной)
        private async Task<dynamic> GetProgramDayData(int day)
        {
            var party = BindingContext as SQLliteTableParty;
            if (party == null)
                return null;

            try
            {
                // Преобразуем int в byte для метода
                byte dayByte = (byte)day;

                if (party.IdMake.HasValue)
                {
                    // Программа создана - используем IdMake
                    return await App.DatabaseMakePrograms.GetDopInfoByProgramIdAndDayAsync(party.IdMake.Value, dayByte);
                }
                else if (party.IdProgramInMySQL.HasValue)
                {
                    // Программа скачана - используем IdProgramInMySQL
                    return await App.DatabaseProgram.GetDopInfoByProgramIdAndDayAsync(party.IdProgramInMySQL.Value, dayByte);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных дня {day} из программы: {ex.Message}");
            }

            return null;
        }

        // НОВЫЙ МЕТОД: Сравнение двух дней из программы
        private bool ProgramDaysAreEqual(dynamic day1, dynamic day2)
        {
            if (day1 == null || day2 == null)
                return false;

            // Сравниваем все параметры программы
            return day1.MinTemperature == day2.MinTemperature &&
                   day1.MaxTemperature == day2.MaxTemperature &&
                   day1.MinHumidity == day2.MinHumidity &&
                   day1.MaxHumidity == day2.MaxHumidity &&
                   day1.MinАmountTurn == day2.MinАmountTurn &&
                   day1.MaxАmountTurn == day2.MaxАmountTurn &&
                   day1.АmountCooling == day2.АmountCooling &&
                   day1.MinTimeCooling == day2.MinTimeCooling &&
                   day1.MaxTimeCooling == day2.MaxTimeCooling;
        }



        private async void OnSaveRecordClicked(object sender, EventArgs e)
        {
            if (selectedDayForRecording == null)
                return;

            try
            {
                // ВАЛИДАЦИЯ ДАННЫХ ПЕРЕД СОХРАНЕНИЕМ
                if (!ValidateInputData())
                {
                    return; // Если валидация не пройдена, выходим из метода
                }

                // Спрашиваем подтверждение для периода
                if (selectedDayFrom != selectedDayTo)
                {
                    bool confirm = await DisplayAlert(
                        "Подтверждение",
                        $"Вы собираетесь изменить данные для {selectedDayTo - selectedDayFrom + 1} дней " +
                        $"(с {selectedDayFrom} по {selectedDayTo}). Продолжить?",
                        "Да", "Нет");

                    if (!confirm) return;
                }
                else
                {
                    bool confirm = await DisplayAlert(
                        "Подтверждение",
                        $"Вы собираетесь изменить данные для дня {selectedDayFrom}. Продолжить?",
                        "Да", "Нет");

                    if (!confirm) return;
                }

                // Получаем данные из формы
                float minTemp = ParseFloat(MinTempEntry.Text);
                float maxTemp = ParseFloat(MaxTempEntry.Text);
                int minHumidity = ParseInt(MinHumidityEntry.Text);
                int maxHumidity = ParseInt(MaxHumidityEntry.Text);
                byte? minTurn = ParseNullableByte(MinTurnEntry.Text);
                byte? maxTurn = ParseNullableByte(MaxTurnEntry.Text);
                byte? coolingAmount = ParseNullableByte(CoolingAmountEntry.Text);
                TimeSpan? minCoolingTime = ParseTimeSpan(MinCoolingTimeEntry.Text);
                TimeSpan? maxCoolingTime = ParseTimeSpan(MaxCoolingTimeEntry.Text);

                // Обновляем данные для выбранного диапазона
                for (int day = selectedDayFrom; day <= selectedDayTo; day++)
                {
                    var dayToUpdate = dopInfoList.FirstOrDefault(d => d.Day == day);
                    if (dayToUpdate != null)
                    {
                        // Применяем данные из формы
                        dayToUpdate.MinTemperature = minTemp;
                        dayToUpdate.MaxTemperature = maxTemp;
                        dayToUpdate.MinHumidity = minHumidity;
                        dayToUpdate.MaxHumidity = maxHumidity;
                        dayToUpdate.MinАmountTurn = minTurn;
                        dayToUpdate.MaxАmountTurn = maxTurn;
                        dayToUpdate.АmountCooling = coolingAmount;
                        dayToUpdate.MinTimeCooling = minCoolingTime;
                        dayToUpdate.MaxTimeCooling = maxCoolingTime;

                        // Обновляем статусы
                        dayToUpdate.IsCompleted = true;
                        dayToUpdate.IsNotCompleted = false;

                        // Сравниваем с программой
                        bool dataMatches = await CompareWithProgramData(dayToUpdate);
                        dayToUpdate.Status = dataMatches ? DayStatus.Completed : DayStatus.Available;
                    }
                }

                // Обновляем UI
                RefreshCollectionView();

                // Сохраняем в базу данных
                await UpdatePartyInDatabase();

                // Скрываем форму
                AddRecordFrame.IsVisible = false;

                // Показываем сообщение об успехе
                string message = selectedDayFrom == selectedDayTo
                    ? $"Данные для дня {selectedDayFrom} сохранены!"
                    : $"Данные для дней с {selectedDayFrom} по {selectedDayTo} ({selectedDayTo - selectedDayFrom + 1} дней) сохранены!";

                await DisplayAlert("Успех", message, "OK");

                selectedDayForRecording = null;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось обновить данные: {ex.Message}", "OK");
            }
        }

        private void OnCancelRecordClicked(object sender, EventArgs e)
        {
            // Скрываем форму без сохранения
            AddRecordFrame.IsVisible = false;
            selectedDayForRecording = null;

            // Очищаем поля формы
            ClearFormFields();
        }

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
            SelectedDayFromLabel.Text = "1";
            SelectedDayToLabel.Text = "1";
        }

        // МЕТОД ДЛЯ ОБНОВЛЕНИЯ 
        private void UpdateAllStatuses()
        {
            bool hasChanges = false;

            foreach (var item in dopInfoList)
            {
                var oldStatus = item.Status;
                DateTime dayEndTime = partyStartTime.AddDays(item.Day);

                if ((DateTime.Now > dayEndTime) && (item.Status == DayStatus.Waiting))
                {
                    item.Status = DayStatus.NotRecorded;
                }

                // Проверяем, изменился ли статус
                if (oldStatus != item.Status)
                {
                    hasChanges = true;
                }
            }

            // Если были изменения, обновляем UI
            if (hasChanges)
            {
                RefreshCollectionView();
            }
        }

        // МЕТОД ДЛЯ ПРИНУДИТЕЛЬНОГО ОБНОВЛЕНИЯ COLLECTIONVIEW
        private void RefreshCollectionView()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                // Просто обновляем ItemsSource
                DopInfoPartyCollectionView.ItemsSource = null;
                DopInfoPartyCollectionView.ItemsSource = dopInfoList;
            });
        }

        // Добавьте это свойство для привязки
        public ObservableCollection<SQLliteTableDopInfoParty> DopInfoParty => dopInfoList;

        private void StartTimers()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                UpdateStartTimers();
                UpdateTimers();
                UpdateEndOfDayTimer();
                UpdateNextPhaseTimer();

                return true;
            });
        }

        // МЕТОД ВАЛИДАЦИИ ВВОДНЫХ ДАННЫХ
        private bool ValidateInputData()
        {
            // Проверка температур
            float minTemp = ParseFloat(MinTempEntry.Text);
            float maxTemp = ParseFloat(MaxTempEntry.Text);

            if (minTemp > maxTemp)
            {
                DisplayAlert("Ошибка", "Минимальная температура не может быть выше максимальной", "OK");
                return false;
            }

            if (minTemp < 0 || maxTemp < 0)
            {
                DisplayAlert("Ошибка", "Температура должна быть положительной", "OK");
                return false;
            }

            // Проверка влажности
            int minHumidity = ParseInt(MinHumidityEntry.Text);
            int maxHumidity = ParseInt(MaxHumidityEntry.Text);

            if (minHumidity > maxHumidity)
            {
                DisplayAlert("Ошибка", "Минимальная влажность не может быть выше максимальной", "OK");
                return false;
            }

            if (minHumidity < 0 || maxHumidity < 0 || minHumidity > 100 || maxHumidity > 100)
            {
                DisplayAlert("Ошибка", "Влажность должна быть в диапазоне от 0 до 100%", "OK");
                return false;
            }

            // Проверка поворотов
            byte? minTurn = ParseNullableByte(MinTurnEntry.Text);
            byte? maxTurn = ParseNullableByte(MaxTurnEntry.Text);

            if (minTurn.HasValue && maxTurn.HasValue && minTurn > maxTurn)
            {
                DisplayAlert("Ошибка", "Минимальное количество поворотов не может быть больше максимального", "OK");
                return false;
            }

            // Проверка времени охлаждения
            TimeSpan? minCoolingTime = ParseTimeSpan(MinCoolingTimeEntry.Text);
            TimeSpan? maxCoolingTime = ParseTimeSpan(MaxCoolingTimeEntry.Text);

            if (minCoolingTime.HasValue && maxCoolingTime.HasValue && minCoolingTime > maxCoolingTime)
            {
                DisplayAlert("Ошибка", "Минимальное время охлаждения не может быть больше максимального", "OK");
                return false;
            }

            // Проверка на отрицательное время
            if (minCoolingTime.HasValue && minCoolingTime.Value.TotalMinutes < 0)
            {
                DisplayAlert("Ошибка", "Время охлаждения не может быть отрицательным", "OK");
                return false;
            }

            return true;
        }

        // ОБНОВЛЕННЫЙ МЕТОД ДЛЯ СРАВНЕНИЯ ДАННЫХ С ПРОГРАММОЙ С УЧЕТОМ ДИАПАЗОНОВ
        private async Task<bool> CompareWithProgramData(SQLliteTableDopInfoParty dayData)
        {
            var party = BindingContext as SQLliteTableParty;
            if (party == null)
                return false;

            // Определяем тип программы по заполненным ID
            if (party.IdMake.HasValue)
            {
                // Программа создана - используем IdMake
                return await CompareWithMakedProgram(party.IdMake.Value, dayData);
            }
            else if (party.IdProgramInMySQL.HasValue)
            {
                // Программа скачана - используем IdProgramInMySQL
                return await CompareWithDownloadedProgram(party.IdProgramInMySQL.Value, dayData);
            }

            // Если ни одно ID не заполнено - не можем сравнить
            Console.WriteLine("Не удалось определить тип программы: IdMake и IdProgramInMySQL не заполнены");
            return false;
        }

        // СУПЕР-ОПТИМИЗИРОВАННОЕ СРАВНЕНИЕ С СОЗДАННОЙ ПРОГРАММОЙ
        private async Task<bool> CompareWithMakedProgram(uint idMake, SQLliteTableDopInfoParty dayData)
        {
            try
            {
                // Используем оптимизированный метод для получения одного дня
                var specificDay = await App.DatabaseMakePrograms.GetDopInfoByProgramIdAndDayAsync(idMake, dayData.Day);

                if (specificDay == null)
                {
                    Console.WriteLine($"Не найден день {dayData.Day} в созданной программе с IdMakeProgram = {idMake}");
                    return false;
                }

                // Сравниваем с учетом допустимых диапазонов
                bool temperatureMatches = IsInTemperatureRange(specificDay, dayData);
                bool humidityMatches = IsInHumidityRange(specificDay, dayData);
                bool turnsMatch = IsInTurnsRange(specificDay, dayData);
                bool coolingMatches = (specificDay.АmountCooling ?? 0) == dayData.АmountCooling || specificDay.АmountCooling == dayData.АmountCooling;
                bool timeMatches = IsInTimeRange(specificDay, dayData);

                bool allMatches = temperatureMatches && humidityMatches && turnsMatch && coolingMatches && timeMatches;

                Console.WriteLine($"Сравнение с созданной программой (День {dayData.Day}): {allMatches}");

                return allMatches;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сравнении с созданной программой: {ex.Message}");
                return false;
            }
        }

        // СУПЕР-ОПТИМИЗИРОВАННОЕ СРАВНЕНИЕ СО СКАЧАННОЙ ПРОГРАММОЙ
        private async Task<bool> CompareWithDownloadedProgram(uint idProgram, SQLliteTableDopInfoParty dayData)
        {
            try
            {
                // Используем оптимизированный метод для получения одного дня
                var specificDay = await App.DatabaseProgram.GetDopInfoByProgramIdAndDayAsync(idProgram, dayData.Day);

                if (specificDay == null)
                {
                    Console.WriteLine($"Не найден день {dayData.Day} в скачанной программе с IdProgram = {idProgram}");
                    return false;
                }

                // Сравниваем с учетом допустимых диапазонов
                bool temperatureMatches = IsInTemperatureRange(specificDay, dayData);
                bool humidityMatches = IsInHumidityRange(specificDay, dayData);
                bool turnsMatch = IsInTurnsRange(specificDay, dayData);
                bool coolingMatches = (specificDay.АmountCooling ?? 0) == dayData.АmountCooling || specificDay.АmountCooling == dayData.АmountCooling;
                bool timeMatches = IsInTimeRange(specificDay, dayData);

                bool allMatches = temperatureMatches && humidityMatches && turnsMatch && coolingMatches && timeMatches;

                Console.WriteLine($"Сравнение со скачанной программой (День {dayData.Day}): {allMatches}");

                return allMatches;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сравнении со скачанной программой: {ex.Message}");
                return false;
            }
        }

        // МЕТОДЫ ДЛЯ ПРОВЕРКИ ДИАПАЗОНОВ

        // Проверка температуры: факт должен быть в диапазоне мин-макс программы
        private bool IsInTemperatureRange(dynamic programDay, SQLliteTableDopInfoParty actualDay)
        {
            return actualDay.MinTemperature >= programDay.MinTemperature &&
                   actualDay.MaxTemperature <= programDay.MaxTemperature;
        }

        // Проверка влажности: факт должен быть в диапазоне мин-макс программы
        private bool IsInHumidityRange(dynamic programDay, SQLliteTableDopInfoParty actualDay)
        {
            return actualDay.MinHumidity >= programDay.MinHumidity &&
                   actualDay.MaxHumidity <= programDay.MaxHumidity;
        }

        // Проверка поворотов: факт должен быть в диапазоне мин-макс программы
        private bool IsInTurnsRange(dynamic programDay, SQLliteTableDopInfoParty actualDay)
        {
            // Получаем значения как nullable
            byte? programMinTurn = programDay.MinАmountTurn;
            byte? programMaxTurn = programDay.MaxАmountTurn;

            // Если в программе не указаны повороты
            if (!programMinTurn.HasValue && !programMaxTurn.HasValue)
                return (actualDay.MinАmountTurn ?? 0) == 0 && (actualDay.MaxАmountTurn ?? 0) == 0;

            // Если указан только минимальный поворот
            if (programMinTurn.HasValue && !programMaxTurn.HasValue)
                return (actualDay.MinАmountTurn ?? 0) >= programMinTurn.Value &&
                       (actualDay.MaxАmountTurn ?? 0) >= programMinTurn.Value;

            // Если указан только максимальный поворот
            if (!programMinTurn.HasValue && programMaxTurn.HasValue)
                return (actualDay.MinАmountTurn ?? 0) <= programMaxTurn.Value &&
                       (actualDay.MaxАmountTurn ?? 0) <= programMaxTurn.Value;

            // Если указаны оба предела
            return (actualDay.MinАmountTurn ?? 0) >= programMinTurn.Value &&
                   (actualDay.MaxАmountTurn ?? 0) <= programMaxTurn.Value;
        }

        // Проверка времени охлаждения: факт должен быть в диапазоне мин-макс программы
        private bool IsInTimeRange(dynamic programDay, SQLliteTableDopInfoParty actualDay)
        {
            // Получаем значения как nullable
            TimeSpan? programMinTime = programDay.MinTimeCooling;
            TimeSpan? programMaxTime = programDay.MaxTimeCooling;

            // Если в программе не указано время охлаждения, считаем что любые значения подходят
            if (!programMinTime.HasValue && !programMaxTime.HasValue)
                return (actualDay.MinTimeCooling ?? TimeSpan.Zero) == TimeSpan.Zero &&
                       (actualDay.MaxTimeCooling ?? TimeSpan.Zero) == TimeSpan.Zero;

            // Если указано только минимальное время
            if (programMinTime.HasValue && !programMaxTime.HasValue)
                return (actualDay.MinTimeCooling ?? TimeSpan.Zero) >= programMinTime.Value &&
                       (actualDay.MaxTimeCooling ?? TimeSpan.Zero) >= programMinTime.Value;

            // Если указано только максимальное время
            if (!programMinTime.HasValue && programMaxTime.HasValue)
                return (actualDay.MinTimeCooling ?? TimeSpan.Zero) <= programMaxTime.Value &&
                       (actualDay.MaxTimeCooling ?? TimeSpan.Zero) <= programMaxTime.Value;

            // Если указаны оба предела
            return (actualDay.MinTimeCooling ?? TimeSpan.Zero) >= programMinTime.Value &&
                   (actualDay.MaxTimeCooling ?? TimeSpan.Zero) <= programMaxTime.Value;
        }

        // Вспомогательный метод для сравнения float с допуском
        private bool CompareFloatsWithTolerance(float expected, float actual, float tolerance = 0.01f)
        {
            return Math.Abs(expected - actual) <= tolerance;
        }

        private async Task UpdatePartyInDatabase()
        {
            try
            {
                var party = BindingContext as SQLliteTableParty;
                if (party != null)
                {
                    party.DopInfoParty = dopInfoList.ToList();
                    int result = await App.DatabaseParty.UpdatePartyAsync(party);

                    if (result > 0)
                    {
                        Console.WriteLine($"Партия {party.IdParty} успешно обновлена в базе данных");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении в базе данных: {ex.Message}");
                throw;
            }
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
                UpdateAllStatuses();
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
            return null;
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

            return null;
        }
    }
}