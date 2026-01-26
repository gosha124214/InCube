using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using AppInCube.Services;
using AppInCube.View.Pages.Buy;

namespace AppInCube
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // ✅ Регистрируем IMyNotificationService
            builder.Services.AddSingleton<IMyNotificationService, MyNotificationService>();
            builder.Services.AddTransient<BuyPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}