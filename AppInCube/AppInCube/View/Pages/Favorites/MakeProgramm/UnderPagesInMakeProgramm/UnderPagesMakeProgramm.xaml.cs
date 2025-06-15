using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace AppInCube.View.Pages.Favorites.MakeProgramm.UnderPagesInMakeProgramm
{
    public partial class UnderPagesMakeProgramm : ContentPage
    {
        private bool isUpdating = false;
        private readonly Dictionary<Entry, string> previousValues = new();

        public UnderPagesMakeProgramm()
        {
            InitializeComponent();
            AddFirstPhase();
            UpdateCancelButtonState();
        }

        private void AddFirstPhase()
        {
            var firstPhaseStack = CreatePhase("1");
            PhasesContainer.Children.Add(firstPhaseStack);
        }

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
                Text = dayToValue ?? string.Empty
            };
            entryDayTo.Focused += Entry_FocusedStoreOldValue;
            entryDayTo.Unfocused += (s, e) => Entry_UnfocusedValidate(entryDayTo);
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

        private void Entry_UnfocusedValidate(Entry entry)
        {
            if (isUpdating) return;

            isUpdating = true;
            try
            {
                if (!int.TryParse(entry.Text, out int currentValue))
                {
                    RestorePreviousValue(entry);
                    return;
                }

                if (!ValidateDayToValue(entry, currentValue))
                {
                    RestorePreviousValue(entry);
                    return;
                }

                previousValues[entry] = entry.Text;
            }
            finally
            {
                isUpdating = false;
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
                    Application.Current?.MainPage?.DisplayAlert("Ошибка валидации", "Некорректное значение", "OK");
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
            BtnCancelPhase.IsEnabled = PhasesContainer.Children.Count > 1;
            BtnCancelPhase.BackgroundColor = BtnCancelPhase.IsEnabled ? Colors.Red : Colors.Gray;
        }
    }
}
