// Platforms/Android/AndroidNotificationService.cs
#if ANDROID
using AppInCube.Services;
using Plugin.LocalNotification;

namespace AppInCube.Platforms.Android.AndroidMyTools.LocalMessage
{
    public class AndroidNotificationService : NotificationServiceBase
    {
        protected override async Task<bool> PlatformScheduleExactNotificationAsync(string title, string message, DateTime scheduleTime, int notificationId)
        {
            var request = new NotificationRequest
            {
                NotificationId = notificationId,
                Title = title,
                Description = message,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = scheduleTime
                }
            };

            await LocalNotificationCenter.Current.Show(request);
            return true;
        }

        protected override async Task PlatformShowNotificationNowAsync(string title, string message)
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

        protected override async Task<bool> PlatformCheckAndRequestPermissionsAsync()
        {
            return await LocalNotificationCenter.Current.AreNotificationsEnabled();
        }

        protected override Task PlatformCancelAllNotificationsAsync()
        {
            LocalNotificationCenter.Current.CancelAll();
            return Task.CompletedTask;
        }

        protected override Task PlatformCancelNotificationAsync(int notificationId)
        {
            LocalNotificationCenter.Current.Cancel(notificationId);
            return Task.CompletedTask;
        }
    }
}
#endif