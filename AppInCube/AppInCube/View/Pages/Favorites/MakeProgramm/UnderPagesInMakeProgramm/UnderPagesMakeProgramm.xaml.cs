using Microsoft.Maui.Controls;
using System.Text.RegularExpressions;

namespace AppInCube.View.Pages.Favorites.MakeProgramm.UnderPagesInMakeProgramm
{
    public partial class UnderPagesMakeProgramm : ContentPage
    {
        private static readonly Regex uintRegex = new Regex(@"^\d*$");

        public UnderPagesMakeProgramm()
        {
            InitializeComponent();
            AddFirstPhase();
            UpdateCancelButtonState();
        }

        private void AddFirstPhase()
        {
            var firstPhaseStack = CreatePhase("1", true);
            PhasesContainer.Children.Add(firstPhaseStack);
        }

        private VerticalStackLayout CreatePhase(string dayFromValue = null, bool isFirstPhase = false)
        {
            var phaseStack = new VerticalStackLayout { Spacing = 10 };

            // День
            var dayStack = new HorizontalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };
            dayStack.Children.Add(new Label { Text = "День ", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            dayStack.Children.Add(new Label { Text = "С:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });

            var entryDayFrom = new Entry
            {
                Keyboard = Keyboard.Numeric,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsReadOnly = isFirstPhase,
                Text = dayFromValue ?? string.Empty
            };
            entryDayFrom.TextChanged += OnDayFromTextChanged_Validated;
            dayStack.Children.Add(entryDayFrom);

            dayStack.Children.Add(new Label { Text = " По: ", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });

            var entryDayTo = new Entry
            {
                Keyboard = Keyboard.Numeric,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = dayFromValue ?? string.Empty // Инициализируем значением из "С:" той же фазы
            };
            entryDayTo.TextChanged += OnDayToTextChanged_Validated;
            dayStack.Children.Add(entryDayTo);

            phaseStack.Children.Add(dayStack);

            // Температура
            var tempStack = new HorizontalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };
            tempStack.Children.Add(new Label { Text = "Температура ", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            tempStack.Children.Add(new Label { Text = "Мин:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            tempStack.Children.Add(new Entry { Placeholder = "float", Keyboard = Keyboard.Numeric, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand });
            tempStack.Children.Add(new Label { Text = " Макс:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            tempStack.Children.Add(new Entry { Placeholder = "float", Keyboard = Keyboard.Numeric, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand });
            phaseStack.Children.Add(tempStack);

            // Влажность
            var humidityStack = new HorizontalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };
            humidityStack.Children.Add(new Label { Text = "Влажность ", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            humidityStack.Children.Add(new Label { Text = "Мин:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            humidityStack.Children.Add(new Entry { Placeholder = "int", Keyboard = Keyboard.Numeric, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand });
            humidityStack.Children.Add(new Label { Text = " Макс:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            humidityStack.Children.Add(new Entry { Placeholder = "int", Keyboard = Keyboard.Numeric, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand });
            phaseStack.Children.Add(humidityStack);

            // Повороты
            var turnsStack = new HorizontalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };
            turnsStack.Children.Add(new Label { Text = "Повороты ", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            turnsStack.Children.Add(new Label { Text = "Мин:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            turnsStack.Children.Add(new Entry { Placeholder = "tinyint", Keyboard = Keyboard.Numeric, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand });
            turnsStack.Children.Add(new Label { Text = " Макс:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            turnsStack.Children.Add(new Entry { Placeholder = "tinyint", Keyboard = Keyboard.Numeric, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand });
            phaseStack.Children.Add(turnsStack);

            // Охлаждение
            var coolingStack = new HorizontalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };
            coolingStack.Children.Add(new Label { Text = "Охлаждение", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            coolingStack.Children.Add(new Label { Text = "Колл:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            coolingStack.Children.Add(new Entry { Placeholder = "tinyint", Keyboard = Keyboard.Numeric, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, WidthRequest = 80 });
            coolingStack.Children.Add(new Label { Text = "Мин:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            coolingStack.Children.Add(new Entry { Placeholder = "uint", Keyboard = Keyboard.Numeric, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, WidthRequest = 80 });
            coolingStack.Children.Add(new Label { Text = "Макс:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, FontSize = 24 });
            coolingStack.Children.Add(new Entry { Placeholder = "uint", Keyboard = Keyboard.Numeric, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, WidthRequest = 80 });
            phaseStack.Children.Add(coolingStack);

            return phaseStack;
        }

        private void OnDayFromTextChanged_Validated(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entryDayFrom)
            {
                // Проверка на uint: пустое значение не меняется, недопустимые символы игнорируем
                if (!IsValidUintInput(e.NewTextValue))
                {
                    entryDayFrom.Text = e.OldTextValue;
                    return;
                }

                int phaseIndex = GetPhaseIndexByDayFrom(entryDayFrom);
                if (phaseIndex > 0)
                {
                    var prevPhase = PhasesContainer.Children[phaseIndex - 1] as VerticalStackLayout;
                    if (prevPhase != null)
                    {
                        SetDayToInPhase(prevPhase, entryDayFrom.Text);
                    }
                }
            }
        }

        private void OnDayToTextChanged_Validated(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entryDayTo)
            {
                // Проверка на uint
                if (!IsValidUintInput(e.NewTextValue))
                {
                    entryDayTo.Text = e.OldTextValue;
                    return;
                }

                int phaseIndex = GetPhaseIndexByDayTo(entryDayTo);
                if (phaseIndex != -1 && phaseIndex < PhasesContainer.Children.Count - 1)
                {
                    var nextPhase = PhasesContainer.Children[phaseIndex + 1] as VerticalStackLayout;
                    if (nextPhase != null)
                    {
                        SetDayFromInPhase(nextPhase, entryDayTo.Text);
                    }
                }
            }
        }

        private bool IsValidUintInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false; // Не пустое значение требуется для сохранения

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            // Можно добавить проверку диапазона uint, если нужно
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
                        if (child is HorizontalStackLayout dayStack)
                        {
                            if (dayStack.Children.Count >= 5 &&
                                dayStack.Children[4] == entryDayTo)
                            {
                                return i;
                            }
                        }
                    }
                }
            }
            return -1;
        }

        private int GetPhaseIndexByDayFrom(Entry entryDayFrom)
        {
            for (int i = 0; i < PhasesContainer.Children.Count; i++)
            {
                if (PhasesContainer.Children[i] is VerticalStackLayout phaseStack)
                {
                    foreach (var child in phaseStack.Children)
                    {
                        if (child is HorizontalStackLayout dayStack)
                        {
                            if (dayStack.Children.Count >= 5 &&
                                dayStack.Children[2] == entryDayFrom)
                            {
                                return i;
                            }
                        }
                    }
                }
            }
            return -1;
        }

        private void SetDayFromInPhase(VerticalStackLayout phaseStack, string value)
        {
            foreach (var child in phaseStack.Children)
            {
                if (child is HorizontalStackLayout dayStack)
                {
                    if (dayStack.Children.Count >= 5 &&
                        dayStack.Children[2] is Entry entryDayFrom)
                    {
                        if (entryDayFrom.Text != value)
                        {
                            entryDayFrom.Text = value;
                        }
                        break;
                    }
                }
            }
        }

        private void SetDayToInPhase(VerticalStackLayout phaseStack, string value)
        {
            foreach (var child in phaseStack.Children)
            {
                if (child is HorizontalStackLayout dayStack)
                {
                    if (dayStack.Children.Count >= 5 &&
                        dayStack.Children[4] is Entry entryDayTo)
                    {
                        if (entryDayTo.Text != value)
                        {
                            entryDayTo.Text = value;
                        }
                        break;
                    }
                }
            }
        }

        private void UpdateCancelButtonState()
        {
            BtnCancelPhase.IsEnabled = PhasesContainer.Children.Count > 1;
            BtnCancelPhase.BackgroundColor = BtnCancelPhase.IsEnabled ? Colors.Red : Colors.Gray;
        }

        // Добавляем метод для обработки нажатия кнопки "Следующая фаза"
        private void OnNextPhaseClicked(object sender, EventArgs e)
        {
            // Проверка, заполнено ли поле "По:" в последней фазе
            if (!IsLastPhaseDayToFilled())
            {
                DisplayAlert("Ошибка", "Пожалуйста, заполните поле 'По:' в текущей фазе.", "OK");
                return;
            }

            string lastDayTo = GetLastPhaseDayToValue();

            var newPhase = CreatePhase(lastDayTo);
            PhasesContainer.Children.Add(newPhase);

            UpdateCancelButtonState();
        }

        // Добавляем метод для обработки нажатия кнопки "Отменить следующую фазу"
        private void OnCancelPhaseClicked(object sender, EventArgs e)
        {
            // Удаляем последнюю фазу, если их больше одной
            if (PhasesContainer.Children.Count > 1)
            {
                PhasesContainer.Children.RemoveAt(PhasesContainer.Children.Count - 1);
                UpdateCancelButtonState();
            }
        }

        private bool IsLastPhaseDayToFilled()
        {
            if (PhasesContainer.Children.Count == 0)
                return true;

            var lastPhase = PhasesContainer.Children[PhasesContainer.Children.Count - 1] as VerticalStackLayout;
            if (lastPhase == null)
                return true;

            foreach (var child in lastPhase.Children)
            {
                if (child is HorizontalStackLayout dayStack)
                {
                    if (dayStack.Children.Count >= 5 &&
                        dayStack.Children[4] is Entry entryDayTo)
                    {
                        return !string.IsNullOrWhiteSpace(entryDayTo.Text);
                    }
                }
            }
            return false;
        }

        private string GetLastPhaseDayToValue()
        {
            if (PhasesContainer.Children.Count == 0)
                return string.Empty;

            var lastPhase = PhasesContainer.Children[PhasesContainer.Children.Count - 1] as VerticalStackLayout;
            if (lastPhase == null)
                return string.Empty;

            foreach (var child in lastPhase.Children)
            {
                if (child is HorizontalStackLayout dayStack)
                {
                    if (dayStack.Children.Count >= 5 &&
                        dayStack.Children[4] is Entry entryDayTo)
                    {
                        return entryDayTo.Text ?? string.Empty;
                    }
                }
            }
            return string.Empty;
        }
    }
}
