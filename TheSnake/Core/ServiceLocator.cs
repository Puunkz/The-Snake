using System;
using System.Collections.Generic;

namespace TheSnake.Core
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new();

        public static void Register<T>(T service)
        {
            services[typeof(T)] = service;
        }

        public static T Get<T>()
        {
            if (services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }

            throw new InvalidOperationException($"Service not found: {typeof(T)}");
        }
    }
}