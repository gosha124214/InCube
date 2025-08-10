using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AppInCube.Classes.SQLite.Maked;
using System.Linq;
using System.Globalization;

namespace AppInCube.View.Pages.Favorites.MakeProgramm.UnderPagesInMakeProgramm
{
    public partial class UnderPagesMakeProgramm : ContentPage
    {
        private bool isUpdating = false;
        private readonly Dictionary<Entry, string> previousValues = new();
        public byte[] ImageBirdFile { get; set; }
        private const int MaxDays = 100; // Максимальное количество дней
        //// В классе UnderPagesMakeProgramm добавьте:
        //private Image selectedImage; // Это должно быть автоматически связано через x:Name в XAML
        // Коллекция для хранения ссылок на элементы управления фазами
        private List<PhaseControls> phaseControlsList = new();

        public class PhaseControls
        {
            public Entry DayToEntry { get; set; }
            public Entry TempMinEntry { get; set; }
            public Entry TempMaxEntry { get; set; }
            public Entry HumidityMinEntry { get; set; }
            public Entry HumidityMaxEntry { get; set; }
            public Entry TurnMinEntry { get; set; }
            public Entry TurnMaxEntry { get; set; }
            public Entry CoolingAmountEntry { get; set; }
            public Entry CoolingMinEntry { get; set; }
            public Entry CoolingMaxEntry { get; set; }
        }

        public UnderPagesMakeProgramm()
        {
            InitializeComponent();
            AddBirdFields();
            AddFirstPhase();
            UpdateCancelButtonState();
        }

        private void AddBirdFields()
        {
            UpdateBirdFieldsState(switchCreateOrSelect.IsToggled);
            entryBirdId.Text = "0";
            entryProgramId.Text = "0";

            previousValues[entryBirdId] = entryBirdId.Text;
            previousValues[entryProgramId] = entryProgramId.Text;

            entryBirdId.Unfocused += (s, e) => ValidateEntryOnUnfocused(entryBirdId, ValidateIDToValue);
            entryProgramId.Unfocused += (s, e) => ValidateEntryOnUnfocused(entryProgramId, ValidateIDToValue);
        }
        // Новая валидация температуры
        private bool ValidateTemperature(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true; // Разрешаем пустую строку для промежуточного ввода

            // Заменяем запятые на точки
            string normalized = input.Replace(',', '.');

            // Проверяем количество точек
            if (normalized.Count(c => c == '.') > 1)
                return false;

            // Проверяем общий формат числа
            if (!float.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out float value))
                return false;

            // Проверяем количество цифр до и после точки
            string[] parts = normalized.Split('.');
            if (parts[0].Length > 2) return false;
            if (parts.Length > 1 && parts[1].Length > 2) return false;

            return true;
        }

        // Валидация влажности (0-100)
        private bool ValidateHumidity(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            if (!int.TryParse(input, out int value))
                return false;

            return value >= 0 && value <= 100;
        }

        // Обновленный метод для получения float значения
        private float GetFloatValue(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            string normalized = input.Replace(',', '.');
            if (float.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out float value))
                return value;

            return 0;
        }

        // Обновленная валидация дней
        private void ValidateDayToEntry_Unfocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                if (!int.TryParse(entry.Text, out int currentValue) || currentValue < 1 || currentValue > MaxDays)
                {
                    if (previousValues.TryGetValue(entry, out string oldValue))
                        entry.Text = oldValue;
                    DisplayAlert("Ошибка", $"Значение 'День По:' должно быть от 1 до {MaxDays}", "ОК");
                }
                else
                {
                    previousValues[entry] = entry.Text;
                }
            }
        }
        private bool ValidateCoolingTime(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            if (!int.TryParse(input, out int minutes))
                return false;

            return minutes >= 0 && minutes <= 1440; // 24 часа в минутах
        }
        private async void OnSelectImageClicked(object sender, System.EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Выберите изображение",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    using (var stream = await result.OpenReadAsync())
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        ImageBirdFile = memoryStream.ToArray();

                        // Обновляем изображение и делаем его видимым
                        selectedImage.Source = ImageSource.FromStream(() => new MemoryStream(ImageBirdFile));
                        imageFrame.IsVisible = true; // Показываем Frame только после выбора картинки
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось выбрать изображение: {ex.Message}", "OK");
            }
        }

        // Добавьте этот метод в ваш класс
        private bool ValidateFloatWithAnySeparator(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            // Заменяем запятые на точки для унификации
            string normalizedInput = input.Replace(',', '.');

            // Проверяем, что после замены осталась только одна точка
            int dotCount = normalizedInput.Count(c => c == '.');
            if (dotCount > 1)
                return false;

            // Проверяем, что остальная строка - валидное число
            return float.TryParse(normalizedInput, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
        }



        private void AddFirstPhase()
        {
            var firstPhase = CreatePhase("1");
            PhasesContainer.Children.Add(firstPhase);
        }

        private VerticalStackLayout CreatePhase(string dayToValue = null)
        {
            var controls = new PhaseControls();
            phaseControlsList.Add(controls);

            var phase = new VerticalStackLayout { Spacing = 10 };

            // День По:
            var dayStack = new HorizontalStackLayout { Spacing = 10 };
            controls.DayToEntry = new Entry
            {
                Keyboard = Keyboard.Numeric,
                Text = dayToValue ?? "0",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            controls.DayToEntry.Focused += Entry_FocusedStoreOldValue;
            controls.DayToEntry.Unfocused += ValidateDayToEntry_Unfocused;
            dayStack.Children.Add(new Label { Text = "День По:", FontSize = 24 });
            dayStack.Children.Add(controls.DayToEntry);
            phase.Children.Add(dayStack);

            // Температура
            var tempStack = new HorizontalStackLayout { Spacing = 10 };
            controls.TempMinEntry = CreateValidatedEntry("0", ValidateTemperature);
            controls.TempMaxEntry = CreateValidatedEntry("0", ValidateTemperature);
            tempStack.Children.Add(new Label { Text = "Температура", FontSize = 24 });
            tempStack.Children.Add(new Label { Text = "Мин:", FontSize = 24 });
            tempStack.Children.Add(controls.TempMinEntry);
            tempStack.Children.Add(new Label { Text = "Макс:", FontSize = 24 });
            tempStack.Children.Add(controls.TempMaxEntry);
            phase.Children.Add(tempStack);

            // Влажность
            var humidityStack = new HorizontalStackLayout { Spacing = 10 };
            controls.HumidityMinEntry = CreateValidatedEntry("0", ValidateHumidity);
            controls.HumidityMaxEntry = CreateValidatedEntry("0", ValidateHumidity);
            humidityStack.Children.Add(new Label { Text = "Влажность", FontSize = 24 });
            humidityStack.Children.Add(new Label { Text = "Мин:", FontSize = 24 });
            humidityStack.Children.Add(controls.HumidityMinEntry);
            humidityStack.Children.Add(new Label { Text = "Макс:", FontSize = 24 });
            humidityStack.Children.Add(controls.HumidityMaxEntry);
            phase.Children.Add(humidityStack);

            // Повороты
            var turnsStack = new HorizontalStackLayout { Spacing = 10 };
            controls.TurnMinEntry = CreateValidatedEntry("0", ValidateTinyInt);
            controls.TurnMaxEntry = CreateValidatedEntry("0", ValidateTinyInt);
            turnsStack.Children.Add(new Label { Text = "Повороты", FontSize = 24 });
            turnsStack.Children.Add(new Label { Text = "Мин:", FontSize = 24 });
            turnsStack.Children.Add(controls.TurnMinEntry);
            turnsStack.Children.Add(new Label { Text = "Макс:", FontSize = 24 });
            turnsStack.Children.Add(controls.TurnMaxEntry);
            phase.Children.Add(turnsStack);

            // Охлаждение
            var coolingStack = new HorizontalStackLayout { Spacing = 10 };
            controls.CoolingAmountEntry = CreateValidatedEntry("0", ValidateTinyInt);
            controls.CoolingMinEntry = CreateValidatedEntry("0", ValidateCoolingTime);
            controls.CoolingMaxEntry = CreateValidatedEntry("0", ValidateCoolingTime);
            coolingStack.Children.Add(new Label { Text = "Охлаждение", FontSize = 24 });
            coolingStack.Children.Add(new Label { Text = "Колл:", FontSize = 24 });
            coolingStack.Children.Add(controls.CoolingAmountEntry);
            coolingStack.Children.Add(new Label { Text = "Мин:", FontSize = 24 });
            coolingStack.Children.Add(controls.CoolingMinEntry);
            coolingStack.Children.Add(new Label { Text = "Макс:", FontSize = 24 });
            coolingStack.Children.Add(controls.CoolingMaxEntry);
            phase.Children.Add(coolingStack);

            // Добавляем валидацию для охлаждения
            controls.CoolingMinEntry.Unfocused += (s, e) => ValidateCoolingMinMax(controls.CoolingMinEntry, controls.CoolingMaxEntry);
            controls.CoolingMaxEntry.Unfocused += (s, e) => ValidateCoolingMinMax(controls.CoolingMaxEntry, controls.CoolingMinEntry);

            return phase;
        }

        private Entry CreateValidatedEntry(string text, System.Func<string, bool> validateFunc)
        {
            var entry = new Entry
            {
                Keyboard = Keyboard.Numeric,
                Text = text,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            entry.Focused += Entry_FocusedStoreOldValue;
            entry.Unfocused += (s, e) => ValidateEntryOnUnfocused(entry, validateFunc);
            return entry;
        }

        // Обновленный метод OnNextPhaseClicked
        private void OnNextPhaseClicked(object sender, System.EventArgs e)
        {
            if (!IsLastPhaseDayToFilled())
            {
                DisplayAlert("Ошибка", "Пожалуйста, заполните поле 'День По:' в текущей фазе.", "ОК");
                return;
            }

            string lastValue = GetLastPhaseDayToValue();
            if (!int.TryParse(lastValue, out int lastDay))
                lastDay = 0;

            lastDay++;

            if (lastDay > MaxDays)
            {
                DisplayAlert("Ошибка", $"Максимальное количество дней не может превышать {MaxDays}", "ОК");
                return;
            }

            var newPhase = CreatePhase(lastDay.ToString());
            PhasesContainer.Children.Add(newPhase);
            UpdateCancelButtonState();
        }

        private string GetLastPhaseDayToValue()
        {
            if (phaseControlsList.Count == 0) return string.Empty;
            return phaseControlsList.Last().DayToEntry.Text ?? string.Empty;
        }

        private bool IsLastPhaseDayToFilled()
        {
            if (phaseControlsList.Count == 0) return true;
            return !string.IsNullOrWhiteSpace(phaseControlsList.Last().DayToEntry.Text);
        }

        private async void OnProgramReadyClicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            try
            {
                // 1. Проверка базовых данных
                ValidateBaseInfo();

                // 2. Проверка фаз программы и подготовка данных для сохранения
                var phasesData = ValidateAndPreparePhasesData();

                // 3. Сохранение данных
                uint programId = await SaveProgramData(phasesData);

                await DisplayAlert("Успех", "Программа сохранена!", "OK");
                ClearForm();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось сохранить: {ex.Message}", "OK");
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private void ValidateBaseInfo()
        {
            if (string.IsNullOrWhiteSpace(entryBirdName.Text))
            {
                throw new Exception("Не указано имя птицы");
            }

            if (ImageBirdFile == null || ImageBirdFile.Length == 0)
            {
                throw new Exception("Не выбрано изображение птицы");
            }
        }

        private List<List<SQLliteTableDopInfoMake>> ValidateAndPreparePhasesData()
        {
            if (phaseControlsList.Count == 0)
            {
                throw new Exception("Должна быть хотя бы одна фаза");
            }

            var allPhasesData = new List<List<SQLliteTableDopInfoMake>>();
            int previousEndDay = 0;

            for (int i = 0; i < phaseControlsList.Count; i++)
            {
                var phaseControls = phaseControlsList[i];
                var phaseDaysData = new List<SQLliteTableDopInfoMake>();

                // Проверка дня фазы
                int phaseEndDay = GetIntValue(phaseControls.DayToEntry.Text);
                if (phaseEndDay > MaxDays)
                {
                    throw new Exception($"Максимальное количество дней не может превышать {MaxDays}");
                }

                if (phaseEndDay <= previousEndDay)
                {
                    throw new Exception($"Фаза {i + 1}: День окончания ({phaseEndDay}) должен быть больше предыдущего ({previousEndDay})");
                }

                // Проверка температур
                float tempMin = GetFloatValue(phaseControls.TempMinEntry.Text);
                float tempMax = GetFloatValue(phaseControls.TempMaxEntry.Text);
                if (tempMin > tempMax)
                {
                    throw new Exception($"Фаза {i + 1}: Минимальная температура ({tempMin}) не может быть больше максимальной ({tempMax})");
                }

                // Проверка влажности
                int humidityMin = GetIntValue(phaseControls.HumidityMinEntry.Text);
                int humidityMax = GetIntValue(phaseControls.HumidityMaxEntry.Text);
                if (humidityMin > humidityMax)
                {
                    throw new Exception($"Фаза {i + 1}: Минимальная влажность ({humidityMin}) не может быть больше максимальной ({humidityMax})");
                }

                // Проверка поворотов
                byte? turnMin = (byte?)GetIntValue(phaseControls.TurnMinEntry.Text);
                byte? turnMax = (byte?)GetIntValue(phaseControls.TurnMaxEntry.Text);
                if (turnMin > turnMax)
                {
                    throw new Exception($"Фаза {i + 1}: Минимальное количество поворотов ({turnMin}) не может быть больше максимального ({turnMax})");
                }

                // Проверка охлаждения
                byte? coolingAmount = (byte?)GetIntValue(phaseControls.CoolingAmountEntry.Text);
                TimeSpan? coolingMin = null;
                TimeSpan? coolingMax = null;

                if (coolingAmount > 0)
                {
                    // Используем GetTimeSpanValue для преобразования
                    coolingMin = GetTimeSpanValue(phaseControls.CoolingMinEntry.Text);
                    coolingMax = GetTimeSpanValue(phaseControls.CoolingMaxEntry.Text);

                    if (coolingMin == null || coolingMax == null)
                    {
                        throw new Exception($"Фаза {i + 1}: Указано количество охлаждений, но не указано время");
                    }

                    if (coolingMin > coolingMax)
                    {
                        throw new Exception($"Фаза {i + 1}: Минимальное время охлаждения ({coolingMin.Value.TotalMinutes} мин) не может быть больше максимального ({coolingMax.Value.TotalMinutes} мин)");
                    }
                }

                // Создаем записи для каждого дня фазы
                int daysInPhase = phaseEndDay - previousEndDay;
                for (int dayInPhase = 1; dayInPhase <= daysInPhase; dayInPhase++)
                {
                    int absoluteDay = previousEndDay + dayInPhase;

                    phaseDaysData.Add(new SQLliteTableDopInfoMake
                    {
                        // IdMakeProgram будет установлен позже
                        Day = (byte)absoluteDay,
                        MinTemperature = tempMin,
                        MaxTemperature = tempMax,
                        MinHumidity = humidityMin,
                        MaxHumidity = humidityMax,
                        MinАmountTurn = turnMin,
                        MaxАmountTurn = turnMax,
                        АmountCooling = coolingAmount,
                        MinTimeCooling = coolingAmount > 0 ? coolingMin : null,
                        MaxTimeCooling = coolingAmount > 0 ? coolingMax : null
                    });
                }

                allPhasesData.Add(phaseDaysData);
                previousEndDay = phaseEndDay;
            }

            return allPhasesData;
        }

        private async Task<uint> SaveProgramData(List<List<SQLliteTableDopInfoMake>> allPhasesData)
        {
            var baseInfo = new SQLliteTableBaseInfoMake
            {
                IdBirdInMySQL = null,
                NameBird = entryBirdName.Text,
                Content = entryContent.Text,
                DaysUntilHatching = 0,
                DateTimeValue = DateTime.Now,
                ImageBirdFile = ImageBirdFile
            };

            // Сохраняем базовую информацию
            uint programId = await App.DatabaseMakePrograms.SaveBaseInfoAsync(baseInfo);

            // Сохраняем все доп. данные для всех фаз
            foreach (var phaseDaysData in allPhasesData)
            {
                foreach (var dayData in phaseDaysData)
                {
                    dayData.IdMakeProgram = programId;
                    await App.DatabaseMakePrograms.SaveDopInfoAsync(dayData);
                }
            }

            return programId;
        }

        private void ClearForm()
        {
            // Очистка полей птицы
            entryBirdName.Text = string.Empty;
            entryContent.Text = string.Empty;
            ImageBirdFile = null;
            imageFrame.IsVisible = false;
            selectedImage.Source = null;

            // Очистка фаз
            PhasesContainer.Children.Clear();
            phaseControlsList.Clear();
            AddFirstPhase();
        }

        private void OnCancelPhaseClicked(object sender, System.EventArgs e)
        {
            if (PhasesContainer.Children.Count > 1)
            {
                PhasesContainer.Children.RemoveAt(PhasesContainer.Children.Count - 1);
                phaseControlsList.RemoveAt(phaseControlsList.Count - 1);
                UpdateCancelButtonState();
            }
        }

        // Остальные методы остаются без изменений
        private void UpdateCancelButtonState()
        {
            if (btnCancelPhase != null)
            {
                btnCancelPhase.IsEnabled = PhasesContainer.Children.Count > 1;
                btnCancelPhase.BackgroundColor = btnCancelPhase.IsEnabled ? Colors.Red : Colors.Gray;
            }
        }

        private void SwitchCreateOrSelect_Toggled(object sender, ToggledEventArgs e)
        {
            UpdateBirdFieldsState(e.Value);
        }

        private void UpdateBirdFieldsState(bool isCreating)
        {
            idContainer.IsVisible = !isCreating;
            createContainer.IsVisible = isCreating;
        }

        private void SwitchPhaseCreateOrSelect_Toggled(object sender, ToggledEventArgs e)
        {
            UpdatePhaseFieldsState(e.Value);
        }

        private void UpdatePhaseFieldsState(bool isCreating)
        {
            PhasesContainer.IsVisible = isCreating;
            programParametersContainer.IsVisible = !isCreating;
            btnNextPhase.IsVisible = isCreating;
            btnCancelPhase.IsVisible = isCreating;
        }

        private void Entry_FocusedStoreOldValue(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
                previousValues[entry] = entry.Text;
        }

        private void ValidateEntryOnUnfocused(Entry entry, System.Func<string, bool> validateFunc)
        {
            if (isUpdating) return;

            isUpdating = true;
            try
            {
                if (!validateFunc(entry.Text))
                {
                    if (previousValues.TryGetValue(entry, out string oldValue))
                        entry.Text = oldValue;
                }
                else
                {
                    previousValues[entry] = entry.Text;
                }
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void ValidateCoolingMinMax(Entry minEntry, Entry maxEntry)
        {
            if (isUpdating) return;

            isUpdating = true;
            try
            {
                if (!uint.TryParse(minEntry.Text, out uint minVal))
                    minVal = 0;
                if (!uint.TryParse(maxEntry.Text, out uint maxVal))
                    maxVal = 0;

                SavePreviousValue(minEntry);
                SavePreviousValue(maxEntry);
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void SavePreviousValue(Entry entry)
        {
            if (entry != null)
            {
                previousValues[entry] = entry.Text;
            }
        }

        private bool ValidateIDToValue(string input) => int.TryParse(input, out int val) && val >= 0;
        private bool ValidateUInt(string input) => uint.TryParse(input, out _);
        private bool ValidateInt(string input) => int.TryParse(input, out _);
        private bool ValidateFloat(string input) => float.TryParse(input, out _);
        private bool ValidateTinyInt(string input) => byte.TryParse(input, out _);

        private int GetIntValue(string input) => int.TryParse(input, out int val) ? val : 0;
        private TimeSpan? GetTimeSpanValue(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            // Преобразуем минуты в TimeSpan
            if (int.TryParse(input, out int minutes))
            {
                return TimeSpan.FromMinutes(minutes);
            }

            return null;
        }
    }
}