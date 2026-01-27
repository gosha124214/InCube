using Microsoft.Extensions.Logging;
using AppInCube.Services;
using AppInCube.View.Pages.Buy;
using Plugin.LocalNotification;

namespace AppInCube
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // ✅ Используем LocalNotification только для Android/iOS
#if ANDROID
            builder.UseLocalNotification();
#endif

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