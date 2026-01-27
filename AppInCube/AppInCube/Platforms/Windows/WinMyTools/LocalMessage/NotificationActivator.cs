#if WINDOWS
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;

namespace AppInCube.Platforms.Windows
{
    public class NotificationActivator
    {
        public static void Initialize()
        {
            try
            {
                // Подписываемся на клики по Toast уведомлениям
                ToastNotificationManagerCompat.OnActivated += OnToastActivated;

                // Инициализируем менеджер
                ToastNotificationManagerCompat.History.Clear();

                Console.WriteLine("✅ Windows: Toast notification activator initialized");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Error initializing notification activator: {ex.Message}");
            }
        }

        private static void OnToastActivated(ToastNotificationActivatedEventArgsCompat args)
        {
            try
            {
                // Парсим аргументы из уведомления
                ToastArguments toastArgs = ToastArguments.Parse(args.Argument);

                Console.WriteLine($"🔔 Windows: Toast clicked - Action: {args.Argument}");

                // Обрабатываем разные действия
                if (toastArgs.Contains("action"))
                {
                    string action = toastArgs["action"];

                    switch (action)
                    {
                        case "viewNotification":
                            if (toastArgs.Contains("notificationId"))
                            {
                                string notificationId = toastArgs["notificationId"];
                                Console.WriteLine($"✅ Windows: Opening notification {notificationId}");
                                // Здесь можно открыть детали уведомления в приложении
                            }
                            break;

                        case "confirmation":
                            Console.WriteLine("✅ Windows: Confirmation toast clicked");
                            break;

                        case "scheduledNotification":
                            Console.WriteLine("✅ Windows: Scheduled notification clicked");
                            break;
                    }
                }

                // Можно активировать окно приложения
                ActivateAppWindow();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Error handling toast click: {ex.Message}");
            }
        }

        private static void ActivateAppWindow()
        {
            try
            {
                // Активируем окно приложения при клике на уведомление
                Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(() =>
                {
                    // Здесь можно обновить UI или показать страницу
                    Console.WriteLine("✅ Windows: App window activated from notification");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Windows: Error activating app window: {ex.Message}");
            }
        }
    }
}
#endif