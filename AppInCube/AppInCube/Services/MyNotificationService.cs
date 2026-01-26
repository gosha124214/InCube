using Plugin.LocalNotification;
using System.Text.Json;

namespace AppInCube.Services
{
    public class MyNotificationService : IMyNotificationService
    {
        private int _counter = 1000;
        private const string SAVED_NOTIFICATIONS_KEY = "saved_notifications";

        public async Task<bool> ScheduleExactNotificationAsync(string title, string message, DateTime scheduleTime)
        {
            try
            {
                var request = new NotificationRequest
                {
                    NotificationId = ++_counter,
                    Title = title,
                    Description = message,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = scheduleTime
                    }
                };

                await LocalNotificationCenter.Current.Show(request);

                // Сохраняем для отображения в списке
                await SaveNotificationAsync(_counter, title, message, scheduleTime);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
                return false;
            }
        }

        public async Task ShowNotificationNowAsync(string title, string message)
        {
            var request = new NotificationRequest
            {
                NotificationId = ++_counter,
                Title = title,
                Description = message,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now
                }
            };

            await LocalNotificationCenter.Current.Show(request);
        }

        public async Task<bool> CheckAndRequestPermissionsAsync()
        {
            try
            {
                // Просто проверяем, включены ли уведомления
                return await LocalNotificationCenter.Current.AreNotificationsEnabled();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
                return false;
            }
        }


        public Task CancelAllNotificationsAsync()
        {
            LocalNotificationCenter.Current.CancelAll();
            Preferences.Remove(SAVED_NOTIFICATIONS_KEY);
            return Task.CompletedTask;
        }

        public async Task<List<MessageModel>> GetScheduledNotificationsAsync()
        {
            try
            {
                var json = Preferences.Get(SAVED_NOTIFICATIONS_KEY, "[]");
                return JsonSerializer.Deserialize<List<MessageModel>>(json) ?? new List<MessageModel>();
            }
            catch
            {
                return new List<MessageModel>();
            }
        }

        public Task CancelNotificationAsync(int notificationId)
        {
            LocalNotificationCenter.Current.Cancel(notificationId);
            RemoveNotificationFromStorage(notificationId);
            return Task.CompletedTask;
        }

        private async Task SaveNotificationAsync(int id, string title, string message, DateTime scheduleTime)
        {
            try
            {
                var notifications = await GetScheduledNotificationsAsync();

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
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка сохранения: {ex.Message}");
            }
        }

        private async void RemoveNotificationFromStorage(int notificationId)
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