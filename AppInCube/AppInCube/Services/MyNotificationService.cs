// Services/MyNotificationService.cs
#if ANDROID
using AppInCube.Platforms.Android.AndroidMyTools.LocalMessage;
#elif WINDOWS
using AppInCube.Platforms.Windows.WinMyTools.LocalMessage;
#endif

namespace AppInCube.Services
{
    public class MyNotificationService : IMyNotificationService
    {
        private readonly IMyNotificationService _platformService;

        public MyNotificationService()
        {
            // Выбираем платформенную реализацию
#if ANDROID
            _platformService = new AndroidNotificationService();
#elif WINDOWS
            _platformService = new WindowsNotificationService();
#else
            throw new PlatformNotSupportedException("Текущая платформа не поддерживается");
#endif
        }

        public Task<bool> ScheduleExactNotificationAsync(string title, string message, DateTime scheduleTime)
            => _platformService.ScheduleExactNotificationAsync(title, message, scheduleTime);

        public Task ShowNotificationNowAsync(string title, string message)
            => _platformService.ShowNotificationNowAsync(title, message);

        public Task<bool> CheckAndRequestPermissionsAsync()
            => _platformService.CheckAndRequestPermissionsAsync();

        public Task CancelAllNotificationsAsync()
            => _platformService.CancelAllNotificationsAsync();

        public Task<List<MessageModel>> GetScheduledNotificationsAsync()
            => _platformService.GetScheduledNotificationsAsync();

        public Task CancelNotificationAsync(int notificationId)
            => _platformService.CancelNotificationAsync(notificationId);
    }
}