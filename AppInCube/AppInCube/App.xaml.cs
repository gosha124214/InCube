using AppInCube.View;
using AppInCube.View.GeneralUnderPages;
using SQLite;
using System.IO;
using Microsoft.Maui.Controls;
using AppInCube.Classes.SQLite;

namespace AppInCube
{
    public partial class App : Application
    {
        public static MenegerSQLlite Database { get; private set; }

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "programs.db");
            Database = new MenegerSQLlite(dbPath);

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