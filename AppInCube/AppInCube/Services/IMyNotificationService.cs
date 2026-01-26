using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppInCube.Services
{
    public interface IMyNotificationService
    {
        // Основные методы
        Task<bool> ScheduleExactNotificationAsync(string title, string message, DateTime scheduleTime);
        Task ShowNotificationNowAsync(string title, string message);

        // Разрешения
        Task<bool> CheckAndRequestPermissionsAsync();


        // Управление
        Task CancelAllNotificationsAsync();

        // Дополнительные методы
        Task<List<MessageModel>> GetScheduledNotificationsAsync();
        Task CancelNotificationAsync(int notificationId);
    }
}