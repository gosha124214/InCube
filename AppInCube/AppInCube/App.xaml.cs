using AppInCube.View;
using AppInCube.View.GeneralUnderPages;
using SQLite;
using System.IO;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite.Downloaded;
using AppInCube.Classes.SQLite.Partyes;

namespace AppInCube
{
    public partial class App : Application
    {
        public static MenegerSQLlite DatabaseProgram { get; private set; }
        public static MenegerSQLliteParty DatabaseParty { get; private set; }

        public App()
        {
            InitializeComponent();

            // Создаем разные пути для каждой базы данных
            string mainDbPath = Path.Combine(FileSystem.AppDataDirectory, "programs.db");
            string partyDbPath = Path.Combine(FileSystem.AppDataDirectory, "parties.db");

              //File.Delete(partyDbPath); // этот метод нужен чтобы удалить файл устаревшего формата


            // Инициализация менеджеров с разными файлами
            DatabaseProgram = new MenegerSQLlite(mainDbPath);
            DatabaseParty = new MenegerSQLliteParty(partyDbPath); // Инициализация менеджера для партий

            bool isFirstRun = Preferences.Get("IsFirstRun", true);

            if (isFirstRun)
            {
                // Если это первый запуск, показываем страницу приветствия
                MainPage = new NavigationPage(new WelcomePage());
            }
            else
            {
                // Если это не первый запуск, показываем главную страницу
                MainPage = new NavigationPage(new AppShell());
            }
        }
    }
}
