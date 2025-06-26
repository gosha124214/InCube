using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AppInCube.Classes.SQLite.Maked;


namespace AppInCube.View.Pages.Favorites.MakeProgramm.UnderPagesInMakeProgramm
{
    public partial class UnderPagesMakeProgramm : ContentPage
    {
        private bool isUpdating = false;
        private readonly Dictionary<Entry, string> previousValues = new();
        public byte[] ImageBirdFile { get; set; } // Массив байтов для хранения изображения

        public UnderPagesMakeProgramm()
        {
            InitializeComponent();
            AddBirdFields();
            AddFirstPhase();
            UpdateCancelButtonState();
        }

        private void AddBirdFields()
        {
            // Изначально показываем только idContainer
            UpdateBirdFieldsState(switchCreateOrSelect.IsToggled);
            // Устанавливаем начальные значения для ID
            entryBirdId.Text = "0";
            entryProgramId.Text = "0";

            // Сохраняем начальные значения
            previousValues[entryBirdId] = entryBirdId.Text;
            previousValues[entryProgramId] = entryProgramId.Text;

            // Добавляем обработчики для проверки ID
            entryBirdId.Unfocused += (s, e) => ValidateEntryOnUnfocused(entryBirdId, ValidateIDToValue);
            entryProgramId.Unfocused += (s, e) => ValidateEntryOnUnfocused(entryProgramId, ValidateIDToValue);
        }

        private void OnSelectImageClicked(object sender, System.EventArgs e)
        {
            SelectImage();
        }

        private async Task SelectImage()
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
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            ImageBirdFile = memoryStream.ToArray(); // Сохраняем изображение в массив байтов
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось выбрать изображение: {ex.Message}", "OK");
            }
        }

        private void AddFirstPhase()
        {
            // Создаем первую фазу и добавляем ее в PhasesContainer
            var firstPhase = CreatePhase("1");
            PhasesContainer.Children.Add(firstPhase);
        }

        private void OnNextPhaseClicked(object sender, System.EventArgs e)
        {
            // Проверяем, заполнено ли поле "День По:" в последней фазе
            if (!IsLastPhaseDayToFilled())
            {
                DisplayAlert("Ошибка", "Пожалуйста, заполните поле 'День По:' в текущей фазе.", "ОК");
                return;
            }

            // Получаем текущее значение "День По:" из последней фазы
            string lastValue = GetLastPhaseDayToValue();
            if (!int.TryParse(lastValue, out int lastDay))
                lastDay = 0; // Если значение не удалось получить, устанавливаем 0

            // Увеличиваем значение на 1
            lastDay++;

            // Создаем новую фазу с увеличенным значением "День По:"
            var newPhase = CreatePhase(lastDay.ToString());

            // Добавляем новую фазу в PhasesContainer
            PhasesContainer.Children.Add(newPhase);
            UpdateCancelButtonState();
        }

        private string GetLastPhaseDayToValue()
        {
            if (PhasesContainer.Children.Count == 0) return string.Empty;
            var lastPhase = PhasesContainer.Children[^1] as VerticalStackLayout;
            if (lastPhase == null) return string.Empty;
            foreach (var child in lastPhase.Children)
            {
                if (child is HorizontalStackLayout hStack &&
                    hStack.Children.Count > 1 &&
                    hStack.Children[1] is Entry entry)
                {
                    return entry.Text ?? string.Empty;
                }
            }
            return string.Empty;
        }
        private async void OnProgramReadyClicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            try
            {
                // 1. Собираем основную информацию
                var baseInfo = new SQLliteTableBaseInfoMake
                {
                    IdBirdInMySQL = null,
                    NameBird = entryBirdName.Text,
                    Content = entryContent.Text,
                    DaysUntilHatching = 0,
                    DateTimeValue = DateTime.Now,
                    ImageBirdFile = ImageBirdFile
                };

                // Сохраняем базовую информацию и получаем ID созданной программы
                uint programId = await App.DatabaseMakePrograms.SaveBaseInfoAsync(baseInfo);

                // 2. Собираем данные по фазам
                foreach (var phaseData in GetPhaseDataFromUI(programId))
                {
                    await App.DatabaseMakePrograms.SaveDopInfoAsync(phaseData);
                }

                await DisplayAlert("Успех", "Программа сохранена!", "OK");
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

        private List<SQLliteTableDopInfoMake> GetPhaseDataFromUI(uint programId)
        {
            var phasesData = new List<SQLliteTableDopInfoMake>();

            foreach (var child in PhasesContainer.Children)
            {
                if (child is VerticalStackLayout phase)
                {
                    var controls = GetControlsFromPhase(phase);
                    int days = GetIntValue(controls.DayEntry); // Получаем количество дней для текущей фазы

                    for (int day = 1; day <= days; day++)
                    {
                        phasesData.Add(new SQLliteTableDopInfoMake
                        {
                            IdMakeProgram = programId,
                            Day = (byte)day, // Устанавливаем текущий день
                            MinTemperature = GetFloatValue(controls.MinTempEntry),
                            MaxTemperature = GetFloatValue(controls.MaxTempEntry),
                            MinHumidity = GetIntValue(controls.MinHumidityEntry),
                            MaxHumidity = GetIntValue(controls.MaxHumidityEntry),
                            MinАmountTurn = (byte)GetIntValue(controls.MinTurnEntry),
                            MaxАmountTurn = (byte)GetIntValue(controls.MaxTurnEntry),
                            АmountCooling = (byte)GetIntValue(controls.CoolingEntry),
                            MinTimeCooling = GetTimeSpanValue(controls.MinTimeCoolingEntry),
                            MaxTimeCooling = GetTimeSpanValue(controls.MaxTimeCoolingEntry)
                        });
                    }
                }
            }

            return phasesData;
        }

      

        private (Entry DayEntry,
                 Entry MinTempEntry,
                 Entry MaxTempEntry,
                 Entry MinHumidityEntry,
                 Entry MaxHumidityEntry,
                 Entry MinTurnEntry,
                 Entry MaxTurnEntry,
                 Entry CoolingEntry,
                 Entry MinTimeCoolingEntry,
                 Entry MaxTimeCoolingEntry) 
          GetControlsFromPhase(VerticalStackLayout phase)
        {
            Entry dayEntry = null;
            Entry minTempEntry = null;
            Entry maxTempEntry = null;
            Entry minHumidityEntry = null;
            Entry maxHumidityEntry = null;
            Entry minTurnEntry = null;
            Entry maxTurnEntry = null;
            Entry coolingEntry = null;
            Entry minTimeCoolingEntry = null;
            Entry maxTimeCoolingEntry = null;

            foreach (var child in phase.Children)
            {
                if (child is HorizontalStackLayout hStack)
                {
                    foreach (var element in hStack.Children)
                    {
                        if (element is Label lbl)
                        {
                            if (lbl.Text == "День По:" && hStack.Children.IndexOf(element) + 1 < hStack.Children.Count)
                            {
                                dayEntry = hStack.Children[hStack.Children.IndexOf(element) + 1] as Entry;
                            }
                            else if (lbl.Text == "Температура" && hStack.Children.IndexOf(element) + 1 < hStack.Children.Count)
                            {
                                minTempEntry = hStack.Children[hStack.Children.IndexOf(element) + 1] as Entry;
                                maxTempEntry = hStack.Children[hStack.Children.IndexOf(element) + 2] as Entry;
                            }
                            else if (lbl.Text == "Влажность" && hStack.Children.IndexOf(element) + 1 < hStack.Children.Count)
                            {
                                minHumidityEntry = hStack.Children[hStack.Children.IndexOf(element) + 1] as Entry;
                                maxHumidityEntry = hStack.Children[hStack.Children.IndexOf(element) + 2] as Entry;
                            }
                            else if (lbl.Text == "Повороты" && hStack.Children.IndexOf(element) + 1 < hStack.Children.Count)
                            {
                                minTurnEntry = hStack.Children[hStack.Children.IndexOf(element) + 1] as Entry;
                                maxTurnEntry = hStack.Children[hStack.Children.IndexOf(element) + 2] as Entry;
                            }
                            else if (lbl.Text == "Охлаждение" && hStack.Children.IndexOf(element) + 1 < hStack.Children.Count)
                            {
                                coolingEntry = hStack.Children[hStack.Children.IndexOf(element) + 1] as Entry;
                            }
                            else if (lbl.Text == "Мин:" && hStack.Children.IndexOf(element) + 1 < hStack.Children.Count)
                            {
                                minTimeCoolingEntry = hStack.Children[hStack.Children.IndexOf(element) + 1] as Entry;
                            }
                            else if (lbl.Text == "Макс:" && hStack.Children.IndexOf(element) + 1 < hStack.Children.Count)
                            {
                                maxTimeCoolingEntry = hStack.Children[hStack.Children.IndexOf(element) + 1] as Entry;
                            }
                        }
                    }
                }
            }

            return (dayEntry, minTempEntry, maxTempEntry, minHumidityEntry, maxHumidityEntry,
                    minTurnEntry, maxTurnEntry, coolingEntry, minTimeCoolingEntry, maxTimeCoolingEntry);
        }

        private int GetIntValue(Entry entry) => entry != null && int.TryParse(entry.Text, out int val) ? val : 0;
        private float GetFloatValue(Entry entry) => entry != null && float.TryParse(entry.Text, out float val) ? val : 0;
        private TimeSpan GetTimeSpanValue(Entry entry) => entry != null && TimeSpan.TryParse(entry.Text, out TimeSpan val) ? val : TimeSpan.Zero;




        private VerticalStackLayout CreatePhase(string dayToValue = null)
        {
            var phaseStack = new VerticalStackLayout { Spacing = 10 };

            // День По:
            var dayStack = new HorizontalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };
            dayStack.Children.Add(new Label { Text = "День По:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            var entryDayTo = new Entry
            {
                Keyboard = Keyboard.Numeric,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = dayToValue ?? "0" // Устанавливаем значение "День По:" по умолчанию 0
            };
            entryDayTo.Focused += Entry_FocusedStoreOldValue;
            entryDayTo.Unfocused += ValidateDayToEntry_Unfocused; // Используем отдельный метод для проверки "День По:"
            dayStack.Children.Add(entryDayTo);
            phaseStack.Children.Add(dayStack);

            // Температура
            var tempStack = CreateMinMaxStack("Температура", ValidateFloat);
            phaseStack.Children.Add(tempStack);

            // Влажность
            var humidityStack = CreateMinMaxStack("Влажность", ValidateInt);
            phaseStack.Children.Add(humidityStack);

            // Повороты
            var turnsStack = CreateMinMaxStack("Повороты", ValidateTinyInt);
            phaseStack.Children.Add(turnsStack);

            // Охлаждение
            var coolingStack = new HorizontalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };
            coolingStack.Children.Add(new Label { Text = "Охлаждение", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            coolingStack.Children.Add(new Label { Text = "Колл:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            coolingStack.Children.Add(CreateValidatedEntry("0", ValidateTinyInt));
            coolingStack.Children.Add(new Label { Text = "Мин:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            var coolingMinEntry = CreateValidatedEntry("0", ValidateUInt);
            coolingStack.Children.Add(coolingMinEntry);
            coolingStack.Children.Add(new Label { Text = "Макс:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            var coolingMaxEntry = CreateValidatedEntry("0", ValidateUInt);
            coolingStack.Children.Add(coolingMaxEntry);
            phaseStack.Children.Add(coolingStack);

            // Добавляем валидацию для охлаждения
            coolingMinEntry.Unfocused += (s, e) => ValidateCoolingMinMax(coolingMinEntry, coolingMaxEntry);
            coolingMaxEntry.Unfocused += (s, e) => ValidateCoolingMinMax(coolingMaxEntry, coolingMinEntry);

            return phaseStack;
        }

        private HorizontalStackLayout CreateMinMaxStack(string title, System.Func<string, bool> validateFunc)
        {
            var stack = new HorizontalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };
            stack.Children.Add(new Label { Text = title, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            stack.Children.Add(new Label { Text = "Мин:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            var minEntry = CreateValidatedEntry("0", validateFunc);
            stack.Children.Add(minEntry);
            stack.Children.Add(new Label { Text = "Макс:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            var maxEntry = CreateValidatedEntry("0", validateFunc);
            stack.Children.Add(maxEntry);

            // Добавляем валидацию для мин и макс
            minEntry.Unfocused += (s, e) => ValidateMinMax(minEntry, maxEntry);
            maxEntry.Unfocused += (s, e) => ValidateMaxMin(maxEntry, minEntry);

            return stack;
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

        private void Entry_FocusedStoreOldValue(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
                previousValues[entry] = entry.Text;
        }

        private void Entry_UnfocusedValidate(Entry entry, System.Func<string, bool> validateFunc)
        {
            if (isUpdating) return;

            isUpdating = true;
            try
            {
                // Проверяем, является ли значение пустым или не является положительным целым числом
                if (!uint.TryParse(entry.Text, out uint currentValue))
                {
                    // Если значение некорректно, откатываем на предыдущее значение
                    RestorePreviousValue(entry);
                    DisplayAlert("Ошибка", "ID должен быть положительным целым числом или 0.", "ОК");
                    return;
                }

                previousValues[entry] = entry.Text;
            }
            finally
            {
                isUpdating = false;
            }
        }

        private bool ValidateIDToValue(string input)
        {
            if (!int.TryParse(input, out int currentValue))
            {
                return false;
            }

            // Проверка, что значение >= 0
            if (currentValue < 0)
            {
                return false;
            }

            return true;
        }

        private void ValidateDayToEntry_Unfocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                // Попробуем преобразовать текст в целое число
                if (!int.TryParse(entry.Text, out int currentValue) ||
                    !ValidateDayToValue(entry, currentValue))
                {
                    // Если преобразование не удалось или валидация не прошла, откатываем значение
                    RestorePreviousValue(entry);
                }
                else
                {
                    previousValues[entry] = entry.Text; // Сохраняем текущее значение
                }
            }
        }

        private bool ValidateDayToValue(Entry entry, int currentValue)
        {
            int phaseIndex = GetPhaseIndexByDayTo(entry);
            if (phaseIndex == -1) return true; // Не нашли - пропускаем

            // Проверка, что значение больше предыдущего, если он есть
            if (phaseIndex > 0)
            {
                var prevPhase = PhasesContainer.Children[phaseIndex - 1] as VerticalStackLayout;
                int prevVal = GetDayToValueFromPhase(prevPhase);
                if (currentValue <= prevVal)
                {
                    DisplayAlert("Ошибка", $"Значение 'День По:' должно быть больше предыдущей фазы ({prevVal})", "ОК");
                    return false;
                }
            }

            // Проверка, что значение меньше следующего, если он есть
            if (phaseIndex < PhasesContainer.Children.Count - 1)
            {
                var nextPhase = PhasesContainer.Children[phaseIndex + 1] as VerticalStackLayout;
                int nextVal = GetDayToValueFromPhase(nextPhase);
                if (nextVal != 0 && currentValue >= nextVal)
                {
                    DisplayAlert("Ошибка", $"Значение 'День По:' должно быть меньше следующей фазы ({nextVal})", "ОК");
                    return false;
                }
            }

            // Значение должно быть >= 1
            if (currentValue < 1)
            {
                DisplayAlert("Ошибка", "Значение 'День По:' должно быть не меньше 1", "ОК");
                return false;
            }

            return true;
        }

        private int GetPhaseIndexByDayTo(Entry entryDayTo)
        {
            for (int i = 0; i < PhasesContainer.Children.Count; i++)
            {
                if (PhasesContainer.Children[i] is VerticalStackLayout phaseStack)
                {
                    foreach (var child in phaseStack.Children)
                    {
                        if (child is HorizontalStackLayout hStack &&
                            hStack.Children.Count > 1 &&
                            hStack.Children[1] == entryDayTo)
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        private int GetDayToValueFromPhase(VerticalStackLayout phase)
        {
            if (phase == null) return 0;
            foreach (var child in phase.Children)
            {
                if (child is HorizontalStackLayout hStack &&
                    hStack.Children.Count > 1 &&
                    hStack.Children[1] is Entry entry &&
                    int.TryParse(entry.Text, out int val))
                {
                    return val;
                }
            }
            return 0;
        }

        private void RestorePreviousValue(Entry entry)
        {
            if (previousValues.TryGetValue(entry, out string oldValue))
            {
                entry.Text = oldValue;
            }
        }

        private void ValidateEntryOnUnfocused(Entry entry, System.Func<string, bool> validateFunc)
        {
            if (isUpdating) return;

            isUpdating = true;
            try
            {
                if (!validateFunc(entry.Text))
                {
                    RestorePreviousValue(entry);

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

        private bool ValidateUInt(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            return uint.TryParse(input, out _);
        }

        private bool ValidateInt(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            return int.TryParse(input, out _);
        }

        private bool ValidateFloat(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            return float.TryParse(input, out _);
        }

        private bool ValidateTinyInt(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            return byte.TryParse(input, out _);
        }

        private void ValidateMinMax(Entry minEntry, Entry maxEntry)
        {
            if (isUpdating) return;

            isUpdating = true;
            try
            {
                if (!float.TryParse(minEntry.Text, out float minVal))
                    minVal = 0;
                if (!float.TryParse(maxEntry.Text, out float maxVal))
                    maxVal = 0;

                // Убираем проверку на больше/меньше
                SavePreviousValue(minEntry);
                SavePreviousValue(maxEntry);
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

                // Убираем проверку на больше/меньше
                SavePreviousValue(minEntry);
                SavePreviousValue(maxEntry);
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void ValidateMaxMin(Entry maxEntry, Entry minEntry)
        {
            if (isUpdating) return;

            isUpdating = true;
            try
            {
                if (!float.TryParse(maxEntry.Text, out float maxVal))
                    maxVal = 0;
                if (!float.TryParse(minEntry.Text, out float minVal))
                    minVal = 0;

                // Убираем проверку на больше/меньше
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

        private bool IsLastPhaseDayToFilled()
        {
            if (PhasesContainer.Children.Count == 0) return true;
            var lastPhase = PhasesContainer.Children[^1] as VerticalStackLayout;
            if (lastPhase == null) return true;
            foreach (var child in lastPhase.Children)
            {
                if (child is HorizontalStackLayout hStack &&
                    hStack.Children.Count > 1 &&
                    hStack.Children[1] is Entry entry)
                {
                    if (string.IsNullOrWhiteSpace(entry.Text)) return false;
                }
            }
            return true;
        }

        private void OnCancelPhaseClicked(object sender, System.EventArgs e)
        {
            if (PhasesContainer.Children.Count > 1)
            {
                PhasesContainer.Children.RemoveAt(PhasesContainer.Children.Count - 1);
                UpdateCancelButtonState();
            }
        }

        private void UpdateCancelButtonState()
        {
            if (btnCancelPhase != null) // Проверяем, что кнопка инициализирована
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
            idContainer.IsVisible = !isCreating; // Если "Создать птицу", скрываем ID
            createContainer.IsVisible = isCreating; // Если "Создать птицу", показываем остальные поля
        }

        private void SwitchPhaseCreateOrSelect_Toggled(object sender, ToggledEventArgs e)
        {
            UpdatePhaseFieldsState(e.Value);
        }

        private void UpdatePhaseFieldsState(bool isCreating)
        {
            PhasesContainer.IsVisible = isCreating; // Если "Создать программу", скрываем фазы
            programParametersContainer.IsVisible = !isCreating; // Если "Выбрать программу", показываем поле ID программы

            btnNextPhase.IsVisible = isCreating;
            btnCancelPhase.IsVisible = isCreating;
        }
    }
}
