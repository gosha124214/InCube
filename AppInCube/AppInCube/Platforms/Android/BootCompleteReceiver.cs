using Android.App;
using Android.Content;

#if ANDROID
namespace AppInCube.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] {
        Intent.ActionBootCompleted,
        Intent.ActionLockedBootCompleted
        // ActionQuickbootPoweron удаляем - он не стандартный
    })]
    public class BootCompleteReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            // Логируем событие перезагрузки
            Console.WriteLine("📱 Телефон перезагружен/включен");
            Console.WriteLine($"Действие: {intent.Action}");

            // Plugin.LocalNotification сам восстанавливает уведомления
            // Но можно добавить свою логику восстановления здесь

            // Например, можно запустить сервис для восстановления:
            // RestoreNotifications(context);
        }

        private void RestoreNotifications(Context context)
        {
            try
            {
                // Ваша логика восстановления уведомлений
                // Например, восстановить из базы данных или настроек
                Console.WriteLine("🔄 Восстанавливаем уведомления...");

                // В реальном приложении здесь бы вы:
                // 1. Загрузили сохраненные уведомления из Preferences
                // 2. Перепланировали их через NotificationService

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка восстановления: {ex.Message}");
            }
        }
    }
}
#endif