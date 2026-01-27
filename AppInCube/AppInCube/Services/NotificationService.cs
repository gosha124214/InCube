// Services/NotificationServiceBase.cs
using System.Text.Json;

namespace AppInCube.Services
{
    public abstract class NotificationServiceBase : IMyNotificationService
    {
        protected int _counter = 1000;
        protected const string SAVED_NOTIFICATIONS_KEY = "saved_notifications";

        // Абстрактные методы для платформенной реализации
        protected abstract Task<bool> PlatformScheduleExactNotificationAsync(string title, string message, DateTime scheduleTime, int notificationId);
        protected abstract Task PlatformShowNotificationNowAsync(string title, string message);
        protected abstract Task<bool> PlatformCheckAndRequestPermissionsAsync();
        protected abstract Task PlatformCancelAllNotificationsAsync();
        protected abstract Task PlatformCancelNotificationAsync(int notificationId);

        // Виртуальный метод, чтобы Windows мог его переопределить
        public virtual async Task<List<MessageModel>> GetScheduledNotificationsAsync()
        {
            try
            {
                var json = Preferences.Get(SAVED_NOTIFICATIONS_KEY, "[]");
                var notifications = JsonSerializer.Deserialize<List<MessageModel>>(json) ?? new List<MessageModel>();

                return notifications
                    .Where(n => n.ScheduleTime > DateTime.Now)
                    .OrderBy(n => n.ScheduleTime)
                    .ToList();
            }
            catch
            {
                return new List<MessageModel>();
            }
        }

        // Общая реализация интерфейса
        public async Task<bool> ScheduleExactNotificationAsync(string title, string message, DateTime scheduleTime)
        {
            try
            {
                var notificationId = ++_counter;

                // Вызываем платформенную реализацию
                var result = await PlatformScheduleExactNotificationAsync(title, message, scheduleTime, notificationId);

                if (result)
                {
                    // Сохраняем для отображения в списке
                    await SaveNotificationAsync(notificationId, title, message, scheduleTime);

                    // Логируем
#if WINDOWS
                    Console.WriteLine($"📅 Windows: Уведомление #{notificationId} сохранено");
#endif
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
                return false;
            }
        }

        public async Task ShowNotificationNowAsync(string title, string message)
        {
            try
            {
                await PlatformShowNotificationNowAsync(title, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }
        }

        public async Task<bool> CheckAndRequestPermissionsAsync()
        {
            try
            {
                return await PlatformCheckAndRequestPermissionsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
                return false;
            }
        }

        public async Task CancelAllNotificationsAsync()
        {
            try
            {
                await PlatformCancelAllNotificationsAsync();
                Preferences.Remove(SAVED_NOTIFICATIONS_KEY);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }
        }

        public async Task CancelNotificationAsync(int notificationId)
        {
            try
            {
                await PlatformCancelNotificationAsync(notificationId);
                await RemoveNotificationFromStorage(notificationId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }
        }

        // Общие вспомогательные методы
        protected async Task SaveNotificationAsync(int id, string title, string message, DateTime scheduleTime)
        {
            try
            {
                var notifications = await GetScheduledNotificationsAsync();

                if (!notifications.Any(n => n.Id == id))
                {
                    notifications.Add(new MessageModel
                    {
                        Id = id,
                        Title = title,
                        Message = message,
                        ScheduleTime = scheduleTime,
                        CreatedAt = DateTime.Now
                    });

                    var json = JsonSerializer.Serialize(notifications);
                    Preferences.Set(SAVED_NOTIFICATIONS_KEY, json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка сохранения: {ex.Message}");
            }
        }

        protected async Task RemoveNotificationFromStorage(int notificationId)
        {
            try
            {
                var notifications = await GetScheduledNotificationsAsync();
                notifications.RemoveAll(n => n.Id == notificationId);

                var json = JsonSerializer.Serialize(notifications);
                Preferences.Set(SAVED_NOTIFICATIONS_KEY, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка удаления: {ex.Message}");
            }
        }
    }
}