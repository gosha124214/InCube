using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AppInCube.Classes.SQLite.Maked;
using System.Linq;

namespace AppInCube.View.Pages.Favorites.MakeProgramm.UnderPagesInMakeProgramm
{
    public partial class UnderPagesMakeProgramm : ContentPage
    {
        private bool isUpdating = false;
        private readonly Dictionary<Entry, string> previousValues = new();
        public byte[] ImageBirdFile { get; set; }

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
            controls.TempMinEntry = CreateValidatedEntry("0", ValidateFloat);
            controls.TempMaxEntry = CreateValidatedEntry("0", ValidateFloat);
            tempStack.Children.Add(new Label { Text = "Температура", FontSize = 24 });
            tempStack.Children.Add(new Label { Text = "Мин:", FontSize = 24 });
            tempStack.Children.Add(controls.TempMinEntry);
            tempStack.Children.Add(new Label { Text = "Макс:", FontSize = 24 });
            tempStack.Children.Add(controls.TempMaxEntry);
            phase.Children.Add(tempStack);

            // Влажность
            var humidityStack = new HorizontalStackLayout { Spacing = 10 };
            controls.HumidityMinEntry = CreateValidatedEntry("0", ValidateInt);
            controls.HumidityMaxEntry = CreateValidatedEntry("0", ValidateInt);
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
            controls.CoolingMinEntry = CreateValidatedEntry("0", ValidateUInt);
            controls.CoolingMaxEntry = CreateValidatedEntry("0", ValidateUInt);
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
                var dopInfoList = new List<SQLliteTableDopInfoMake>();

                int previousEndDay = 0; // Последний день предыдущей фазы

                // Собираем данные по фазам
                for (int i = 0; i < phaseControlsList.Count; i++)
                {
                    var phaseControls = phaseControlsList[i];
                    int phaseEndDay = GetIntValue(phaseControls.DayToEntry.Text);

                    // Количество дней в текущей фазе
                    int daysInPhase = phaseEndDay - previousEndDay;

                    if (daysInPhase <= 0)
                    {
                        throw new Exception($"Фаза {i + 1}: Конечный день должен быть больше чем {previousEndDay}");
                    }

                    // Создаем записи для каждого дня фазы
                    for (int dayInPhase = 1; dayInPhase <= daysInPhase; dayInPhase++)
                    {
                        int absoluteDay = previousEndDay + dayInPhase;

                        dopInfoList.Add(new SQLliteTableDopInfoMake
                        {
                            IdMakeProgram = programId,
                            Day = (byte)absoluteDay,
                            MinTemperature = GetFloatValue(phaseControls.TempMinEntry.Text),
                            MaxTemperature = GetFloatValue(phaseControls.TempMaxEntry.Text),
                            MinHumidity = GetIntValue(phaseControls.HumidityMinEntry.Text),
                            MaxHumidity = GetIntValue(phaseControls.HumidityMaxEntry.Text),
                            MinАmountTurn = (byte)GetIntValue(phaseControls.TurnMinEntry.Text),
                            MaxАmountTurn = (byte)GetIntValue(phaseControls.TurnMaxEntry.Text),
                            АmountCooling = (byte)GetIntValue(phaseControls.CoolingAmountEntry.Text),
                            MinTimeCooling = GetTimeSpanValue(phaseControls.CoolingMinEntry.Text),
                            MaxTimeCooling = GetTimeSpanValue(phaseControls.CoolingMaxEntry.Text)
                        });
                    }

                    previousEndDay = phaseEndDay; // Запоминаем последний день текущей фазы
                }

                // Сохраняем все доп. данные
                foreach (var dopInfo in dopInfoList)
                {
                    await App.DatabaseMakePrograms.SaveDopInfoAsync(dopInfo);
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

        private void ValidateDayToEntry_Unfocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                if (!int.TryParse(entry.Text, out int currentValue) || currentValue < 1)
                {
                    if (previousValues.TryGetValue(entry, out string oldValue))
                        entry.Text = oldValue;
                    DisplayAlert("Ошибка", "Значение 'День По:' должно быть не меньше 1", "ОК");
                }
                else
                {
                    previousValues[entry] = entry.Text;
                }
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
        private float GetFloatValue(string input) => float.TryParse(input, out float val) ? val : 0;
        private TimeSpan GetTimeSpanValue(string input) => TimeSpan.TryParse(input, out TimeSpan val) ? val : TimeSpan.Zero;
    }
}