using System;
using System.Collections.Generic;

namespace UIManagement
{
    public static class UIManager
    {
        private static readonly Dictionary<Type, UIScreen> _screens;

        static UIManager()
        {
            _screens = new();
        }

        public static void Register(UIScreen screen)
        {
            var type = screen.GetType();

            _screens.Add(type, screen);
        }

        public static void Show<T>() where T : UIScreen
        {
            var type = typeof(T);

            _screens[type].Show();
        }

        public static void Hide<T>() where T : UIScreen
        {
            var type = typeof(T);

            _screens[type].Hide();
        }
    }
}
