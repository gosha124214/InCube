#if WINDOWS
    using AppInCube.Platforms.Windows.WinMyTools.LocalMessage;
    using Microsoft.Windows.AppNotifications;
    using AppInCube.Platforms.Windows;
#endif

using AppInCube.View;
using AppInCube.View.GeneralUnderPages;
using SQLite;
using System.IO;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite.Downloaded;
using AppInCube.Classes.SQLite.Partyes;
using AppInCube.Classes.SQLite.Maked;
using AppInCube.Services;
using Microsoft.Extensions.DependencyInjection;


namespace AppInCube
{
    public partial class App : Application
    {
        public static MenegerSQLlite DatabaseProgram { get; private set; }
        public static MenegerSQLliteParty DatabaseParty { get; private set; }
        public static MenegerSQLliteMake DatabaseMakePrograms { get; private set; }

        // Сервис для уведомлений
        private IMyNotificationService _notificationService;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

            // Создаем разные пути для каждой базы данных
            string mainDbPath = Path.Combine(FileSystem.AppDataDirectory, "programs.db");
            string partyDbPath = Path.Combine(FileSystem.AppDataDirectory, "parties.db");
            string makeProgramsDbPath = Path.Combine(FileSystem.AppDataDirectory, "makeProgramms.db");

            //File.Delete(makeProgramsDbPath); // этот метод нужен чтобы удалить файл устаревшего формата

            // Инициализация менеджеров с разными файлами
            DatabaseProgram = new MenegerSQLlite(mainDbPath);
            DatabaseParty = new MenegerSQLliteParty(partyDbPath);
            DatabaseMakePrograms = new MenegerSQLliteMake(makeProgramsDbPath);

            bool isFirstRun = Preferences.Get("IsFirstRun", true);

            if (isFirstRun)
            {
                MainPage = new NavigationPage(new WelcomePage());
            }
            else
            {
                MainPage = new NavigationPage(new AppShell());
            }
            // Инициализация уведомлений для Windows
#if WINDOWS
    InitializeWindowsNotifications();
#endif
        }

#if WINDOWS
private void InitializeWindowsNotifications()
{
    try
    {
        // Инициализируем менеджер уведомлений
        AppNotificationManager.Default.NotificationInvoked += OnNotificationInvoked;
        AppNotificationManager.Default.Register();
        
        Console.WriteLine("Windows: Уведомления инициализированы");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Windows: Ошибка инициализации уведомлений: {ex.Message}");
    }
}

private void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
{
    // Обработка клика по уведомлению
    Console.WriteLine($"Windows: Уведомление активировано: {args.Arguments}");
    
    MainThread.BeginInvokeOnMainThread(async () =>
    {
        if (Application.Current?.MainPage != null)
        {
            await Application.Current.MainPage.DisplayAlert("Уведомление", "Вы кликнули по уведомлению", "OK");
        }
    });
}
#endif
        // App.xaml.cs добавьте
        protected override void OnStart()
        {
            base.OnStart();

            // Для Windows проверяем пропущенные уведомления при запуске
#if WINDOWS
    CheckMissedNotificationsOnStart();
#endif
        }

#if WINDOWS
private async void CheckMissedNotificationsOnStart()
{
    try
    {
        Console.WriteLine("Windows: Проверяем пропущенные уведомления при запуске...");
        
        // Ждем инициализации сервисов
        await Task.Delay(2000);
        
        var notificationService = ServiceProviderHelper.GetService<IMyNotificationService>();
        if (notificationService != null)
        {
            var notifications = await notificationService.GetScheduledNotificationsAsync();
            var now = DateTime.Now;
            
            // Находим уведомления, которые должны были сработать пока приложение было закрыто
            var missedNotifications = notifications
                .Where(n => n.ScheduleTime <= now && n.ScheduleTime > now.AddHours(-24)) // За последние 24 часа
                .ToList();
                
            if (missedNotifications.Any())
            {
                // Показываем сводку пропущенных уведомлений
                string message = $"📅 Пропущенные уведомления ({missedNotifications.Count}):";
                
                foreach (var notification in missedNotifications.OrderBy(n => n.ScheduleTime))
                {
                    message += $"\n⏰ {notification.ScheduleTime:HH:mm}: {notification.Title}";
                }
                
                // Показываем как уведомление
                Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(async () =>
                {
                    if (Microsoft.Maui.Controls.Application.Current?.MainPage != null)
                    {
                        await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(
                            "🔔 Пропущенные уведомления", 
                            message, 
                            "OK");
                    }
                });
                
                // Удаляем пропущенные уведомления
                foreach (var notification in missedNotifications)
                {
                    await notificationService.CancelNotificationAsync(notification.Id);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Windows: Ошибка проверки пропущенных уведомлений: {ex.Message}");
    }
}
#endif
    }
}