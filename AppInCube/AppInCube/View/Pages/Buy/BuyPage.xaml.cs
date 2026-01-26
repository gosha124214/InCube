using AppInCube.Services;
using Plugin.LocalNotification;
using System.Collections.ObjectModel;



namespace AppInCube.View.Pages.Buy
{
    public partial class BuyPage : ContentPage
    {
        // ✅ Используем IMyNotificationService
        private readonly IMyNotificationService _notificationService;
        private ObservableCollection<MessageModel> _messages;
        private bool _permissionsGranted = false;

        // ✅ Конструктор принимает IMyNotificationService
        public BuyPage(IMyNotificationService notificationService)
        {
            InitializeComponent();
            _notificationService = notificationService;

            _messages = new ObservableCollection<MessageModel>();
            NotificationsListView.ItemsSource = _messages;

            DatePicker.Date = DateTime.Today;
            TimePicker.Time = DateTime.Now.AddMinutes(5).TimeOfDay;

            CheckPermissionsOnStart();
            LoadExistingNotificationsAsync();
        }

        private async void CheckPermissionsOnStart()
        {
            try
            {
                LoadingIndicator.IsVisible = true;
                _permissionsGranted = await _notificationService.CheckAndRequestPermissionsAsync();

                if (!_permissionsGranted)
                {
                    await DisplayAlert("Внимание",
                        "Для точных уведомлений нужны разрешения.\n" +
                        "Включите уведомления и точные алармы в настройках телефона.",
                        "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка разрешений: {ex.Message}");
            }
            finally
            {
                LoadingIndicator.IsVisible = false;
            }
        }

        private async Task LoadExistingNotificationsAsync()
        {
            try
            {
                var notifications = await _notificationService.GetScheduledNotificationsAsync();
                _messages.Clear();

                foreach (var notification in notifications
                    .Where(n => n.ScheduleTime > DateTime.Now)
                    .OrderBy(n => n.ScheduleTime))
                {
                    _messages.Add(notification);
                }

                UpdateStatistics();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки: {ex.Message}");
            }
        }

        private async void OnAddNotificationClicked(object sender, EventArgs e)
        {
            try
            {
                if (!_permissionsGranted)
                {
                    var result = await DisplayAlert("Требуются разрешения",
                        "Для создания точных уведомлений нужны разрешения.\n" +
                        "Запросить сейчас?", "Да", "Нет");

                    if (result)
                    {
                        _permissionsGranted = await _notificationService.CheckAndRequestPermissionsAsync();
                        if (!_permissionsGranted) return;
                    }
                    else return;
                }

                if (string.IsNullOrWhiteSpace(TitleEntry.Text))
                {
                    await DisplayAlert("Ошибка", "Введите название", "OK");
                    return;
                }

                var scheduleTime = DatePicker.Date.Add(TimePicker.Time);

                if (scheduleTime <= DateTime.Now)
                {
                    var result = await DisplayAlert("Внимание",
                        "Время уже прошло. Запланировать на завтра?", "Да", "Нет");
                    if (result) scheduleTime = scheduleTime.AddDays(1);
                    else return;
                }

                AddingIndicator.IsVisible = true;
                AddButton.IsEnabled = false;

                var success = await _notificationService.ScheduleExactNotificationAsync(
                    TitleEntry.Text.Trim(),
                    MessageEntry.Text?.Trim() ?? "",
                    scheduleTime);

                if (success)
                {
                    await LoadExistingNotificationsAsync();

                    await DisplayAlert("✅ Успешно",
                        $"Уведомление запланировано на:\n" +
                        $"{scheduleTime:dd.MM.yyyy HH:mm}\n\n" +
                        "Работает после закрытия приложения и перезагрузки.",
                        "OK");

                    TitleEntry.Text = "";
                    MessageEntry.Text = "";
                    DatePicker.Date = DateTime.Today;
                    TimePicker.Time = scheduleTime.AddMinutes(5).TimeOfDay;
                }
                else
                {
                    await DisplayAlert("❌ Ошибка", "Не удалось запланировать", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("❌ Ошибка", ex.Message, "OK");
            }
            finally
            {
                AddingIndicator.IsVisible = false;
                AddButton.IsEnabled = true;
            }
        }

        private async void OnTestNotificationClicked(object sender, EventArgs e)
        {
            var testTime = DateTime.Now.AddSeconds(10);

            var success = await _notificationService.ScheduleExactNotificationAsync(
                "✅ Тестовое уведомление",
                $"Сработает в {testTime:HH:mm:ss}\n" +
                "Закройте приложение для проверки!",
                testTime);

            if (success)
            {
                await DisplayAlert("Тест запущен",
                    "Уведомление придет через 10 секунд.\n" +
                    "Закройте приложение для проверки работы в фоне.",
                    "OK");
            }
        }

        private async void OnAddTestNotificationsClicked(object sender, EventArgs e)
        {
            try
            {
                var testData = new[]
                {
                    new { Title = "☕ Утренний кофе", Message = "Время для кофе-брейка", Delay = TimeSpan.FromMinutes(2) },
                    new { Title = "🍽️ Обед", Message = "Пора обедать", Delay = TimeSpan.FromMinutes(5) },
                    new { Title = "📅 Встреча", Message = "Еженедельный созвон", Delay = TimeSpan.FromHours(1) },
                    new { Title = "🏋️ Тренировка", Message = "Время спорта", Delay = TimeSpan.FromHours(2) },
                    new { Title = "📚 Чтение", Message = "30 минут чтения", Delay = TimeSpan.FromDays(1).Add(TimeSpan.FromHours(20)) }
                };

                AddingIndicator.IsVisible = true;
                AddButton.IsEnabled = false;

                foreach (var test in testData)
                {
                    var scheduleTime = DateTime.Now.Add(test.Delay);

                    var success = await _notificationService.ScheduleExactNotificationAsync(
                        test.Title,
                        test.Message,
                        scheduleTime);

                    if (success)
                    {
                        var message = new MessageModel
                        {
                            Id = new Random().Next(1000, 9999),
                            Title = test.Title,
                            Message = test.Message,
                            ScheduleTime = scheduleTime,
                            CreatedAt = DateTime.Now
                        };

                        _messages.Add(message);
                    }
                }

                SortMessages();
                UpdateStatistics();

                await DisplayAlert("✅ Тест", "Добавлено 5 тестовых уведомлений", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("❌ Ошибка", $"Не удалось добавить тестовые уведомления: {ex.Message}", "OK");
            }
            finally
            {
                AddingIndicator.IsVisible = false;
                AddButton.IsEnabled = true;
            }
        }

        private async void OnCheckPermissionsClicked(object sender, EventArgs e)
        {
            try
            {
                LoadingIndicator.IsVisible = true;
                _permissionsGranted = await _notificationService.CheckAndRequestPermissionsAsync();

                var status = _permissionsGranted ? "✅ Все разрешения получены" : "❌ Разрешения не получены";
                await DisplayAlert("Статус разрешений", status, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось проверить разрешения: {ex.Message}", "OK");
            }
            finally
            {
                LoadingIndicator.IsVisible = false;
            }
        }

        private async void OnClearAllClicked(object sender, EventArgs e)
        {
            if (!_messages.Any())
            {
                await DisplayAlert("Информация", "Нет уведомлений для удаления", "OK");
                return;
            }

            var count = _messages.Count;
            var confirm = await DisplayAlert("Очистка всех уведомлений",
                $"Вы уверены, что хотите удалить ВСЕ уведомления ({count})?",
                "Да, удалить все", "Отмена");

            if (confirm)
            {
                try
                {
                    await _notificationService.CancelAllNotificationsAsync();
                    _messages.Clear();
                    UpdateStatistics();

                    await DisplayAlert("✅ Очищено", $"Все {count} уведомлений удалены", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("❌ Ошибка", $"Не удалось очистить: {ex.Message}", "OK");
                }
            }
        }

        private void SortMessages()
        {
            var sorted = _messages.OrderBy(m => m.ScheduleTime).ToList();
            _messages.Clear();
            foreach (var item in sorted)
            {
                _messages.Add(item);
            }
        }

        private void UpdateStatistics()
        {
            TotalCountLabel.Text = $"Всего: {_messages.Count}";

            var nextMessage = _messages
                .Where(m => m.ScheduleTime > DateTime.Now)
                .OrderBy(m => m.ScheduleTime)
                .FirstOrDefault();

            if (nextMessage != null)
            {
                NextTimeLabel.Text = $"Следующее: {nextMessage.ScheduleTime:HH:mm}";
            }
            else
            {
                NextTimeLabel.Text = "Следующее: --:--";
            }
        }

        private async void OnDeleteNotificationClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is MessageModel message)
            {
                var confirm = await DisplayAlert("Удаление",
                    $"Удалить уведомление:\n\"{message.Title}\"?", "Да", "Нет");

                if (confirm)
                {
                    _messages.Remove(message);
                    UpdateStatistics();
                }
            }
        }
    }
}