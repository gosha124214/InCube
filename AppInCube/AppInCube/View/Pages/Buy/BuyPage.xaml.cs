using Plugin.LocalNotification;
#if WINDOWS
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder; // Правильное пространство имен!
#endif

namespace AppInCube.View.Pages.Buy
{
    public partial class BuyPage : ContentPage
    {
        private Random _random = new Random();

        public BuyPage()
        {
            InitializeComponent();
        }

        // УНИВЕРСАЛЬНЫЙ МЕТОД ДЛЯ ОТПРАВКИ УВЕДОМЛЕНИЙ С ОБРАБОТКОЙ WINDOWS ОШИБКИ
        private async Task<bool> SendNotificationSafeAsync(NotificationRequest request)
        {
            try
            {
#if WINDOWS
        // Для Windows используем Windows API
        SendWindowsNotification(
            request.Title ?? "Уведомление", 
            request.Description ?? "Сообщение");
        return true;
#else
                // Для других платформ используем Plugin.LocalNotification
                await LocalNotificationCenter.Current.Show(request);
                return true;
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка отправки уведомления: {ex.Message}");

                // Fallback: показываем алерт
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Уведомление",
                        request.Description ?? "Сообщение",
                        "OK");
                });
                return false;
            }
        }

        // 2. УВЕДОМЛЕНИЕ С ЗАДЕРЖКОЙ - ИСПРАВЛЕННОЕ
        private async void OnDelayedNotificationClicked(object sender, EventArgs e)
        {
            try
            {
                int delaySeconds = 5;
                var scheduleTime = DateTime.Now.AddSeconds(delaySeconds);

                await DisplayAlert("⏰ Запланировано",
                    $"Уведомление запланировано на:\n" +
                    $"{scheduleTime:HH:mm:ss}\n\n" +
                    $"Оно появится через {delaySeconds} секунд.", "OK");

#if WINDOWS
        // Для Windows - имитация задержки
        _ = Task.Run(async () =>
        {
            await Task.Delay(delaySeconds * 1000);
            
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SendWindowsNotification(
                    "⏰ Уведомление с задержкой",
                    $"🔹 Тип: Отложенное уведомление\n" +
                    $"🔹 Появилось через: {delaySeconds} секунд\n" +
                    $"🔹 Запланировано: {scheduleTime:HH:mm:ss}\n" +
                    $"🔹 Фактически: {DateTime.Now:HH:mm:ss}");
            });
        });
#else
                // Для других платформ используем Plugin.LocalNotification
                var request = new NotificationRequest
                {
                    NotificationId = _random.Next(1000, 9999),
                    Title = "⏰ Уведомление с задержкой",
                    Description = $"🔹 Тип: Отложенное уведомление\n" +
                                 $"🔹 Появится через: {delaySeconds} секунд\n" +
                                 $"🔹 Запланировано: {scheduleTime:HH:mm:ss}",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(delaySeconds)
                    }
                };

                await SendNotificationSafeAsync(request);
#endif
            }
            catch (Exception ex)
            {
                await DisplayAlert("❌ Ошибка", ex.Message, "OK");
            }
        }

#if WINDOWS
// Метод для отправки простых уведомлений на Windows
public static void SendWindowsNotification(string title, string message)
{
    try
    {
        var builder = new AppNotificationBuilder()
            .AddText(title)
            .AddText(message);

        var notification = builder.BuildNotification();
        AppNotificationManager.Default.Show(notification);
        
        Console.WriteLine($"📤 Windows notification sent: {title}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Windows notification error: {ex.Message}");
    }
}

// Метод для отложенных уведомлений (имитация)
public static async Task SendDelayedWindowsNotification(string title, string message, int delaySeconds)
{
    try
    {
        await Task.Delay(delaySeconds * 1000);
        
        var builder = new AppNotificationBuilder()
            .AddText(title)
            .AddText($"{message}\nЗадержка: {delaySeconds} секунд");

        var notification = builder.BuildNotification();
        AppNotificationManager.Default.Show(notification);
        
        Console.WriteLine($"📤 Delayed Windows notification sent after {delaySeconds}s");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Delayed notification error: {ex.Message}");
    }
}
#endif
        private void TestWindowsNotification_Clicked(object sender, EventArgs e)
        {
#if WINDOWS
    try
    {
        App.SendTestNotification();
        DisplayAlert("✅", "Windows notification sent!\nClick on it to test activation", "OK");
    }
    catch (Exception ex)
    {
        DisplayAlert("❌", $"Error: {ex.Message}", "OK");
    }
#endif
        }
        // 1. ПРОСТОЕ УВЕДОМЛЕНИЕ - ИСПРАВЛЕННОЕ
        private async void OnSimpleNotificationClicked(object sender, EventArgs e)
        {
            try
            {
                var request = new NotificationRequest
                {
                    NotificationId = _random.Next(1000, 9999),
                    Title = "✅ Простое уведомление",
                    Description = "Это уведомление вызвано кнопкой 'Простое уведомление'\n" +
                                 "Оно появляется сразу после нажатия",
                    CategoryType = NotificationCategoryType.Status
                };

                // Убираем ReturningData для Windows
#if WINDOWS
                request.ReturningData = null;
#endif

                bool success = await SendNotificationSafeAsync(request);

                if (success)
                {
                    await DisplayAlert("✅ Готово",
#if WINDOWS
                        "Уведомление отправлено (Windows: работает без активации по клику)",
#else
                        "Простое уведомление отправлено!",
#endif
                        "OK");
                }
                else
                {
                    await DisplayAlert("⚠️ Внимание", "Не удалось отправить уведомление", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("❌ Ошибка", ex.Message, "OK");
            }
        }

        private CancellationTokenSource _repeatCancellationToken;

        // 3. ПОВТОРЯЮЩЕЕСЯ УВЕДОМЛЕНИЕ С ВОЗМОЖНОСТЬЮ ОСТАНОВКИ
        private async void OnRepeatingNotificationClicked(object sender, EventArgs e)
        {
            try
            {
                // Запрашиваем подтверждение
                var result = await DisplayAlert("🔄 Повторяющееся уведомление",
                    "Уведомление будет приходить каждые 10 секунд.\n\n" +
                    "Начать повторение? (Можно будет остановить)", "✅ Да", "❌ Нет");

                if (!result) return;

#if WINDOWS
        // Для Windows запускаем повторение
        await StartRepeatingNotifications();
#else
                // Для других платформ
                var request = new NotificationRequest
                {
                    NotificationId = _random.Next(1000, 9999),
                    Title = "🔄 Повторяющееся уведомление",
                    Description = "📋 Будет повторяться каждые 10 секунд",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(10),
                        NotifyRepeatInterval = TimeSpan.FromSeconds(10),
                        RepeatType = NotificationRepeat.TimeInterval
                    }
                };

                bool success = await SendNotificationSafeAsync(request);

                if (success)
                {
                    await DisplayAlert("🔄 Запущено",
                        "Повторяющееся уведомление запущено!\n" +
                        "Остановите в настройках устройства.", "OK");
                }
#endif
            }
            catch (Exception ex)
            {
                await DisplayAlert("❌ Ошибка", ex.Message, "OK");
            }
        }

        // Кнопка для остановки повторения
        private async void OnStopRepeatingClicked(object sender, EventArgs e)
        {
            try
            {
                if (_repeatCancellationToken != null)
                {
                    _repeatCancellationToken.Cancel();
                    _repeatCancellationToken = null;

                    await DisplayAlert("⏹️ Остановлено",
                        "Повторяющиеся уведомления остановлены.", "OK");
                }
                else
                {
                    await DisplayAlert("ℹ️ Информация",
                        "Нет активных повторяющихся уведомлений.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("❌ Ошибка", ex.Message, "OK");
            }
        }

#if WINDOWS
private async Task StartRepeatingNotifications()
{
    try
    {
        // Отменяем предыдущее повторение если есть
        _repeatCancellationToken?.Cancel();
        _repeatCancellationToken = new CancellationTokenSource();
        
        var token = _repeatCancellationToken.Token;
        int count = 0;
        
        _ = Task.Run(async () =>
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(10000, token); // 10 секунд
                    if (token.IsCancellationRequested) break;
                    
                    count++;
                    
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        SendWindowsNotification(
                            $"🔄 Повторение #{count}",
                            $"⏰ {DateTime.Now:HH:mm:ss}\n" +
                            $"📊 Счетчик: {count}\n" +
                            $"🔄 Каждые 10 секунд");
                    });
                    
                    Console.WriteLine($"✅ Repeating #{count}");
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Repeating error: {ex.Message}");
                    break;
                }
            }
            
            // Финальное сообщение если не отменено пользователем
            if (!token.IsCancellationRequested)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    SendWindowsNotification("🔄 Завершено",
                        $"Показано {count} уведомлений");
                });
            }
        }, token);
        
        await DisplayAlert("🔄 Запущено",
            $"Повторяющиеся уведомления запущены.\n" +
            $"Они будут приходить каждые 10 секунд.\n\n" +
            $"Нажмите 'Остановить повторение' чтобы отменить.", "OK");
    }
    catch (Exception ex)
    {
        await DisplayAlert("❌ Ошибка", ex.Message, "OK");
    }
}
#endif
        // 4. УВЕДОМЛЕНИЕ С ПОДРОБНЫМ ОПИСАНИЕМ - ИСПРАВЛЕННОЕ
        private async void OnLongNotificationClicked(object sender, EventArgs e)
        {
            try
            {
                var request = new NotificationRequest
                {
                    NotificationId = _random.Next(1000, 9999),
                    Title = "📄 Уведомление с подробным описанием",
                    Description = "🎯 Это тестовое уведомление с подробным описанием\n\n" +
                                 "🔸 Источник: Кнопка 'С подробным текстом'\n" +
                                 "🔸 Цель: Демонстрация длинных уведомлений\n" +
                                 "🔸 Платформа: .NET MAUI\n" +
                                 "🔸 Библиотека: Plugin.LocalNotification v12\n" +
                                 "🔸 Время: " + DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy") + "\n\n" +
                                 "ℹ️ Это уведомление показывает, как можно передавать " +
                                 "подробную информацию пользователю через системные уведомления.",
                    CategoryType = NotificationCategoryType.Promo
                };

#if WINDOWS
                request.ReturningData = null;
#endif

                bool success = await SendNotificationSafeAsync(request);

                if (success)
                {
                    await DisplayAlert("📄 Отправлено",
                        "Уведомление с подробным описанием отправлено!\n\n" +
                        "На некоторых платформах текст может быть сокращён, " +
                        "но при разворачивании уведомления будет виден полный текст.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("❌ Ошибка", $"Ошибка отправки:\n{ex.Message}", "OK");
            }
        }

        // 5. ТЕСТ ВСЕХ УВЕДОМЛЕНИЙ - ИСПРАВЛЕННОЕ
        private async void OnTestAllClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await DisplayAlert("⚡ Тест",
                    "Запустить тест 3 уведомлений?", "✅ Да", "❌ Нет");

                if (!result) return;

                // Тест 1: Простое
                var request1 = new NotificationRequest
                {
                    NotificationId = _random.Next(1000, 9999),
                    Title = "🔹 Тест 1: Простое",
                    Description = "Первое тестовое уведомление"
                };
                bool success1 = await SendNotificationSafeAsync(request1);

                if (success1) await Task.Delay(1000);

                // Тест 2: С задержкой
                var request2 = new NotificationRequest
                {
                    NotificationId = _random.Next(1000, 9999),
                    Title = "🔹 Тест 2: С задержкой",
                    Description = "Второе уведомление (через 2 сек)",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(2)
                    }
                };
                bool success2 = await SendNotificationSafeAsync(request2);

                if (success2) await Task.Delay(1000);

                // Тест 3: Финальное
                var request3 = new NotificationRequest
                {
                    NotificationId = _random.Next(1000, 9999),
                    Title = "🔹 Тест 3: Завершение",
                    Description = "Тест завершён успешно!"
                };
                bool success3 = await SendNotificationSafeAsync(request3);

                if (success1 && success2 && success3)
                {
                    await DisplayAlert("🎉 Готово", "3 тестовых уведомления отправлены!", "OK");
                }
                else
                {
                    await DisplayAlert("⚠️ Частично", "Не все уведомления удалось отправить", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("❌ Ошибка", ex.Message, "OK");
            }
        }

        // 6. ЗАПРОС РАЗРЕШЕНИЙ
        private async void OnRequestPermissionClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await DisplayAlert("🔐 Разрешения на уведомления",
                    "Для работы уведомлений необходимо разрешение.\n\n" +
                    "Разрешить приложению показывать уведомления?\n\n" +
                    "ℹ️ На Android/iOS появится системный диалог",
                    "✅ Да, запросить", "❌ Нет, позже");

                if (!result) return;

                var status = LocalNotificationCenter.Current.RequestNotificationPermission();

                string statusText = $"Статус: {status}";
                string statusString = status.ToString();

                if (statusString.Contains("Granted") || statusString.Contains("Allowed") || statusString.Contains("Authorized"))
                {
                    statusText = "✅ Разрешение получено!";
                }
                else if (statusString.Contains("Denied") || statusString.Contains("Rejected") || statusString.Contains("Disabled"))
                {
                    statusText = "❌ Разрешение отклонено";
                }
                else if (statusString.Contains("NotDetermined") || statusString.Contains("Unknown") || statusString.Contains("NotSet"))
                {
                    statusText = "⚠️ Решение не принято";
                }

                await DisplayAlert("📋 Статус разрешений",
                    $"{statusText}\n\n" +
                    "Что это значит:\n" +
                    "• ✅ Разрешено - уведомления будут работать\n" +
                    "• ❌ Отклонено - проверьте настройки устройства\n" +
                    "• ⚠️ Не решено - разрешите при следующем запросе", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("❌ Ошибка", $"Ошибка запроса разрешений:\n{ex.Message}", "OK");
            }
        }

        // 7. ОЧИСТКА
        private async void OnClearAllClicked(object sender, EventArgs e)
        {
            try
            {
                bool confirm = await DisplayAlert("🗑️ Очистка",
                    "Удалить все уведомления?", "✅ Да", "❌ Нет");

                if (confirm)
                {
                    LocalNotificationCenter.Current.CancelAll();
                    await DisplayAlert("🧹 Готово", "Все уведомления удалены", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("❌ Ошибка", ex.Message, "OK");
            }
        }
    }
}