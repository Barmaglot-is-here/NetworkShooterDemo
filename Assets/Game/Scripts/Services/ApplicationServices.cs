using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Services
{
    public static class ApplicationServices
    {
        private static readonly Dictionary<Type, object> _services;

        static ApplicationServices()
        {
            _services = new();
        }

        public static void Add<T>(T service) where T : class
        {
            var type = typeof(T);

            _services.Add(type, service);
        }

        public static T Get<T>() where T : class
        {
            var type = typeof(T);

            return (T)_services[type];
        }
    }
}
