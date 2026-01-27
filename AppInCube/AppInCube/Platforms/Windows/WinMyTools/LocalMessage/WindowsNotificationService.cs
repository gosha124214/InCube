// Platforms/Windows/WindowsNotificationService.cs
#if WINDOWS
using AppInCube.Services;
using System.Timers;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace AppInCube.Platforms.Windows.WinMyTools.LocalMessage
{
    public class WindowsNotificationService : NotificationServiceBase
    {
        private System.Timers.Timer _notificationTimer;
        private List<MessageModel> _pendingNotifications = new();
        private bool _isTimerRunning = false;
        private const string APP_ID = "AppInCube";

        public WindowsNotificationService()
        {
            Console.WriteLine("WindowsNotificationService: Конструктор вызван");
            InitializeNotificationTimer();
            _ = LoadPendingNotificationsAsync();
        }

        private async Task LoadPendingNotificationsAsync()
        {
            try
            {
                var savedNotifications = await base.GetScheduledNotificationsAsync();
                _pendingNotifications.Clear();
                _pendingNotifications.AddRange(savedNotifications);
                Console.WriteLine($"📋 Windows: Загружено {_pendingNotifications.Count} уведомлений из хранилища");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка загрузки уведомлений: {ex.Message}");
            }
        }

        private void InitializeNotificationTimer()
        {
            try
            {
                _notificationTimer = new System.Timers.Timer(30000); // 30 секунд
                _notificationTimer.Elapsed += CheckNotificationsTimerElapsed;
                _notificationTimer.AutoReset = true;
                _notificationTimer.Enabled = true;
                _isTimerRunning = true;
                Console.WriteLine("Windows: Таймер уведомлений запущен (30 секунд)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка инициализации таймера: {ex.Message}");
            }
        }

        private void CheckNotificationsTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (!_isTimerRunning) return;

            try
            {
                var now = DateTime.Now;
                var notificationsToShow = _pendingNotifications
                    .Where(n => n.ScheduleTime <= now && n.ScheduleTime > now.AddMinutes(-1))
                    .ToList();

                if (notificationsToShow.Any())
                {
                    Console.WriteLine($"🔔 Windows: Таймер: найдено {notificationsToShow.Count} уведомлений к показу");
                }

                foreach (var notification in notificationsToShow)
                {
                    Console.WriteLine($"🔔 Windows: Показываю системное Toast уведомление #{notification.Id}: '{notification.Title}'");

                    // ПОКАЗЫВАЕМ СИСТЕМНОЕ TOAST УВЕДОМЛЕНИЕ
                    ShowToastNotification(notification.Title, notification.Message);

                    _pendingNotifications.Remove(notification);

                    // Удаляем из хранилища асинхронно
                    Task.Run(async () => await RemoveNotificationFromStorage(notification.Id));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка в таймере: {ex.Message}");
            }
        }

        // ОСНОВНОЙ МЕТОД ДЛЯ ПОКАЗА TOAST УВЕДОМЛЕНИЙ
        private void ShowToastNotification(string title, string message)
        {
            try
            {
                // Используем Microsoft.Toolkit.Uwp.Notifications
                new ToastContentBuilder()
                    .AddArgument("action", "viewNotification")
                    .AddArgument("notificationId", Guid.NewGuid().ToString())
                    .AddText(title, AdaptiveTextStyle.Title)
                    .AddText(message, AdaptiveTextStyle.Body)
                    .AddAppLogoOverride(new Uri("ms-appx:///Assets/icon.png"), ToastGenericAppLogoCrop.Circle)
                    .AddAudio(new Uri("ms-winsoundevent:Notification.Default"), silent: false)
                    .Show(toast =>
                    {
                        toast.ExpirationTime = DateTime.Now.AddMinutes(5);
                    });

                Console.WriteLine($"✅ Windows: Toast уведомление показано: '{title}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка Toast уведомления: {ex.Message}");
                ShowFallbackNotification(title, message);
            }
        }

        private void ShowFallbackNotification(string title, string message)
        {
            try
            {
                // Простой fallback через старый API
                string toastXml = $@"<?xml version='1.0'?>
                <toast>
                    <visual>
                        <binding template='ToastGeneric'>
                            <text>{System.Security.SecurityElement.Escape(title)}</text>
                            <text>{System.Security.SecurityElement.Escape(message)}</text>
                        </binding>
                    </visual>
                </toast>";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(toastXml);

                ToastNotification toast = new ToastNotification(xmlDoc);
                ToastNotificationManager.CreateToastNotifier(APP_ID).Show(toast);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка fallback: {ex.Message}");
            }
        }

        protected override async Task<bool> PlatformScheduleExactNotificationAsync(string title, string message, DateTime scheduleTime, int notificationId)
        {
            try
            {
                var timeDifference = (scheduleTime - DateTime.Now).TotalSeconds;

                Console.WriteLine($"📅 Windows: Планирую уведомление #{notificationId}: '{title}' на {scheduleTime:HH:mm:ss} (через {timeDifference:F0} секунд)");

                // Добавляем в очередь
                var notification = new MessageModel
                {
                    Id = notificationId,
                    Title = title,
                    Message = message,
                    ScheduleTime = scheduleTime,
                    CreatedAt = DateTime.Now
                };

                _pendingNotifications.Add(notification);

                // Показываем ТОЛЬКО ПОДТВЕРЖДЕНИЕ создания
                if (timeDifference > 60) // Если больше минуты
                {
                    ShowConfirmationToast(title, scheduleTime);
                }
                else if (timeDifference <= 60 && timeDifference > 10) // Скоро (1 минута - 10 секунд)
                {
                    ShowQuickNotificationToast(title, timeDifference);
                }

                Console.WriteLine($"✅ Windows: Уведомление #{notificationId} запланировано");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка планирования: {ex.Message}");
                return false;
            }
        }

        private void ShowConfirmationToast(string title, DateTime scheduleTime)
        {
            try
            {
                new ToastContentBuilder()
                    .AddArgument("action", "confirmation")
                    .AddText("✅ Уведомление запланировано")
                    .AddText($"'{title}'")
                    .AddText($"Будет показано в {scheduleTime:HH:mm}")
                    .Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка подтверждения: {ex.Message}");
            }
        }

        private void ShowQuickNotificationToast(string title, double secondsLeft)
        {
            try
            {
                new ToastContentBuilder()
                    .AddArgument("action", "quickInfo")
                    .AddText("⏰ Скоро уведомление")
                    .AddText($"'{title}'")
                    .AddText($"Через {(int)secondsLeft} секунд")
                    .Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка быстрого уведомления: {ex.Message}");
            }
        }

        // Для запланированных Toast уведомлений (будущие)
        private void ScheduleToastNotification(string title, string message, DateTime scheduleTime, int notificationId)
        {
            try
            {
                // Создаем запланированный Toast
                var scheduledToast = new ScheduledToastNotification(
                    new ToastContentBuilder()
                        .AddArgument("action", "scheduledNotification")
                        .AddArgument("notificationId", notificationId.ToString())
                        .AddText(title)
                        .AddText(message)
                        .GetXml(),
                    scheduleTime)
                {
                    Id = notificationId.ToString(),
                    Tag = $"notification_{notificationId}"
                };

                // Добавляем в планировщик Windows
                ToastNotificationManager.CreateToastNotifier(APP_ID).AddToSchedule(scheduledToast);

                Console.WriteLine($"📅 Windows: Запланирован Toast #{notificationId} на {scheduleTime:HH:mm:ss}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка планирования Toast: {ex.Message}");
            }
        }

        protected override async Task PlatformShowNotificationNowAsync(string title, string message)
        {
            // Для немедленного показа (тест)
            ShowToastNotification(title, message);
            await Task.CompletedTask;
        }

        protected override Task<bool> PlatformCheckAndRequestPermissionsAsync()
        {
            // В Windows 10/11 проверяем настройки уведомлений
            bool hasPermission = true;

            try
            {
                // Можно проверить настройки системы
                // На Windows уведомления обычно включены по умолчанию
            }
            catch
            {
                hasPermission = true;
            }

            return Task.FromResult(hasPermission);
        }

        protected override Task PlatformCancelAllNotificationsAsync()
        {
            _pendingNotifications.Clear();

            // Очищаем все запланированные Toast уведомления
            ClearAllScheduledToasts();

            Console.WriteLine("Windows: Все уведомления очищены");
            return Task.CompletedTask;
        }

        private void ClearAllScheduledToasts()
        {
            try
            {
                var notifier = ToastNotificationManager.CreateToastNotifier(APP_ID);
                var scheduledToasts = notifier.GetScheduledToastNotifications();

                foreach (var toast in scheduledToasts)
                {
                    notifier.RemoveFromSchedule(toast);
                }

                // Очищаем историю
                ToastNotificationManager.History.Clear(APP_ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка очистки Toast: {ex.Message}");
            }
        }

        protected override Task PlatformCancelNotificationAsync(int notificationId)
        {
            var notification = _pendingNotifications.FirstOrDefault(n => n.Id == notificationId);
            if (notification != null)
            {
                _pendingNotifications.Remove(notification);
                Console.WriteLine($"Windows: Уведомление #{notificationId} удалено из очереди");
            }

            // Удаляем запланированный Toast
            RemoveScheduledToast(notificationId);

            return Task.CompletedTask;
        }

        private void RemoveScheduledToast(int notificationId)
        {
            try
            {
                var notifier = ToastNotificationManager.CreateToastNotifier(APP_ID);
                var scheduledToasts = notifier.GetScheduledToastNotifications();

                var toastToRemove = scheduledToasts.FirstOrDefault(t => t.Id == notificationId.ToString());
                if (toastToRemove != null)
                {
                    notifier.RemoveFromSchedule(toastToRemove);
                    Console.WriteLine($"🗑️ Windows: Запланированный Toast #{notificationId} удален");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка удаления Toast: {ex.Message}");
            }
        }

        // Метод для ручной проверки уведомлений
        public async Task ManualCheckNotifications()
        {
            try
            {
                Console.WriteLine("Windows: Ручная проверка уведомлений...");

                // Проверяем запланированные Toast уведомления
                CheckScheduledToasts();

                // Имитируем срабатывание таймера
                CheckNotificationsTimerElapsed(null, null);

                await LoadPendingNotificationsAsync();

                // Показываем результат
                ShowToastNotification("Проверка уведомлений",
                    $"Проверка завершена\n" +
                    $"В очереди: {_pendingNotifications.Count} уведомлений\n" +
                    $"Следующее: {GetNextNotificationTime() ?? "нет"}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка ручной проверки: {ex.Message}");
            }
        }

        private void CheckScheduledToasts()
        {
            try
            {
                var notifier = ToastNotificationManager.CreateToastNotifier(APP_ID);
                var scheduledToasts = notifier.GetScheduledToastNotifications();

                Console.WriteLine($"📋 Windows: Запланированных Toast: {scheduledToasts.Count}");

                foreach (var toast in scheduledToasts)
                {
                    Console.WriteLine($"   - ID: {toast.Id}, Время: {toast.DeliveryTime:HH:mm:ss}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Ошибка проверки Toast: {ex.Message}");
            }
        }

        private string? GetNextNotificationTime()
        {
            if (!_pendingNotifications.Any())
                return null;

            var next = _pendingNotifications.OrderBy(n => n.ScheduleTime).First();
            var timeLeft = next.ScheduleTime - DateTime.Now;

            if (timeLeft.TotalSeconds <= 0)
                return "сейчас";
            else if (timeLeft.TotalMinutes < 60)
                return $"через {(int)timeLeft.TotalMinutes} мин";
            else if (timeLeft.TotalHours < 24)
                return $"через {(int)timeLeft.TotalHours} ч";
            else
                return $"через {(int)timeLeft.TotalDays} дн";
        }
    }
}
#endif