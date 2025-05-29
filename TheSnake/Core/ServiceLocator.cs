namespace TheSnake.Core
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new();

        public static void Register<T>(T service)
        {
            Services[typeof(T)] = service;
        }

        public static T Get<T>()
        {
            if (Services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }

            throw new InvalidOperationException($"Service not found: {typeof(T)}");
        }
    }
}