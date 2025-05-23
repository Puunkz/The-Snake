namespace The_Snake.Core;

public static class ServiceLocator
{
   private static Dictionary<Type, object> _services = new();
   public static void Register<T>(T service)
   { 
       _services[typeof(T)] = service!;
   }
   
   public static T Get<T>()
   {
       if (_services.TryGetValue(typeof(T), out var service))
       {
           return (T)service;
       }
       throw new Exception($"Service of type {typeof(T)} not registered.");
   }
   
   public static void Clear()
   {
       _services.Clear();
   }
}