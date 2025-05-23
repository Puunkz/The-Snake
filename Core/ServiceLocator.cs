using System;
using System.Collections.Generic;

namespace The_Snake.Core;

public static class ServiceLocator
{
   private static Dictionary<Type, object> services = new();
   public static void Register<T>(T service)
   {
        services[typeof(T)] = service!;
   }
   
   public static T Get<T>()
   {
       if (services.TryGetValue(typeof(T), out var service))
       {
           return (T)service;
       }
       throw new Exception($"Service of type {typeof(T)} not registered.");
   }
   
   public static void clear()
   {
       services.Clear();
   }
}