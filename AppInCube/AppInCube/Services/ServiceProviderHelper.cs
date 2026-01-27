// Services/ServiceProviderHelper.cs
using Microsoft.Extensions.DependencyInjection;

namespace AppInCube.Services
{
    public static class ServiceProviderHelper
    {
        private static IServiceProvider _serviceProvider;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Console.WriteLine("✅ ServiceProviderHelper инициализирован");
        }

        public static T GetService<T>() where T : class
        {
            return _serviceProvider?.GetService<T>();
        }

        public static IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
    }
}