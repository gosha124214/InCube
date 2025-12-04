using AppInCube.View;
using AppInCube.View.GeneralUnderPages;
using SQLite;
using System.IO;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite.Downloaded;
using AppInCube.Classes.SQLite.Partyes;
using AppInCube.Classes.SQLite.Maked;

#if WINDOWS
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
#endif

namespace AppInCube
{
    public partial class App : Application
    {
        public static MenegerSQLlite DatabaseProgram { get; private set; }
        public static MenegerSQLliteParty DatabaseParty { get; private set; }
        public static MenegerSQLliteMake DatabaseMakePrograms { get; private set; }

        public App()
        {
            InitializeComponent();

            // Инициализация баз данных
            InitializeDatabases();

            // Проверка первого запуска
            CheckFirstRun();

            // Инициализация уведомлений (для Windows)
#if WINDOWS
            InitializeWindowsNotifications();
#endif
        }

        private void InitializeDatabases()
        {
            string mainDbPath = Path.Combine(FileSystem.AppDataDirectory, "programs.db");
            string partyDbPath = Path.Combine(FileSystem.AppDataDirectory, "parties.db");
            string makeProgramsDbPath = Path.Combine(FileSystem.AppDataDirectory, "makeProgramms.db");

            //File.Delete(makeProgramsDbPath); // этот метод нужен чтобы удалить файл устаревшего формата

            DatabaseProgram = new MenegerSQLlite(mainDbPath);
            DatabaseParty = new MenegerSQLliteParty(partyDbPath);
            DatabaseMakePrograms = new MenegerSQLliteMake(makeProgramsDbPath);
        }

        private void CheckFirstRun()
        {
            bool isFirstRun = Preferences.Get("IsFirstRun", true);

            if (isFirstRun)
            {
                // Если это первый запуск, показываем страницу приветствия
                MainPage = new NavigationPage(new WelcomePage());
                // Помечаем, что первый запуск уже был
                Preferences.Set("IsFirstRun", false);
            }
            else
            {
                // Если это не первый запуск, показываем главную страницу
                MainPage = new NavigationPage(new AppShell());
            }
        }

#if WINDOWS
private void InitializeWindowsNotifications()
{
    try
    {
        Console.WriteLine("=== Windows Notifications Initialization ===");
        
        // 1. Проверяем аргументы командной строки
        var args = Environment.GetCommandLineArgs();
        Console.WriteLine($"Command line args: {string.Join(", ", args)}");
        
        // 2. Если запущено из уведомления, обрабатываем сразу
        if (args.Contains("-ToastActivated"))
        {
            Console.WriteLine("App launched from notification!");
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(1000);
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync("//buy");
                }
            });
        }
        
        // 3. Пробуем зарегистрировать активатор
        try
        {
            var manager = AppNotificationManager.Default;
            manager.NotificationInvoked += OnNotificationInvoked;
            
            // ВАЖНО: Этот вызов может выбросить исключение
            manager.Register();
            Console.WriteLine("✅ Notification activator registered successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to register activator: {ex.Message}");
            Console.WriteLine("ℹ️ Notifications will show but clicks won't be handled");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Windows notifications error: {ex.Message}");
    }
}

// Упрощенный обработчик
private void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
{
    Console.WriteLine("🔔 Notification clicked!");
    
    // Просто открываем приложение
    MainThread.BeginInvokeOnMainThread(async () =>
    {
        try
        {
            // Даем время на инициализацию
            await Task.Delay(500);
            
            // Если приложение не запущено, запускаем
            if (Current?.MainPage == null)
            {
                MainPage = new NavigationPage(new AppShell());
                await Task.Delay(500);
            }
            
            // Переходим на страницу buy
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//buy");
                Console.WriteLine("✅ Navigated to Buy page");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error handling notification: {ex.Message}");
        }
    });
}

// Простой метод для тестирования
public static void SendTestNotification()
{
    try
    {
        var builder = new AppNotificationBuilder()
            .AddText("AppInCube Test")
            .AddText("Click this notification to open the app")
            .AddButton(new AppNotificationButton("Open Buy")
                .AddArgument("action", "open_buy"));
        
        var notification = builder.BuildNotification();
        AppNotificationManager.Default.Show(notification);
        
        Console.WriteLine("📤 Test notification sent");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error sending notification: {ex.Message}");
    }
}
#endif

        // Добавьте эти методы для обработки жизненного цикла приложения
        protected override void OnStart()
        {
            base.OnStart();
            Console.WriteLine("🚀 Приложение запущено");
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            Console.WriteLine("💤 Приложение ушло в сон");
        }

        protected override void OnResume()
        {
            base.OnResume();
            Console.WriteLine("🔁 Приложение возобновлено");
        }
    }
}