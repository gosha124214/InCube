// AppShell.xaml.cs
using AppInCube.Services;

namespace AppInCube
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Инициализируем ServiceProviderHelper
            InitializeServiceProvider();
        }

        private void InitializeServiceProvider()
        {
            try
            {
                // Ждем инициализации Handler
                Dispatcher.Dispatch(() =>
                {
                    if (Handler?.MauiContext?.Services != null)
                    {
                        ServiceProviderHelper.Initialize(Handler.MauiContext.Services);
                        Console.WriteLine("ServiceProviderHelper инициализирован");
                    }
                    else
                    {
                        Console.WriteLine("Handler или MauiContext еще не готовы");
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации ServiceProviderHelper: {ex.Message}");
            }
        }
    }
}