using System;
using System.Windows;

namespace Windowing
{
    public class NotificationManager : INotificationManager
    {
        private static int _index = 1;
        private readonly IWindowMonitor _windowMonitor;
        private readonly INotifications _notifications;

        public NotificationManager(IWindowMonitor windowMonitor, INotifications notifications)
        {
            _windowMonitor = windowMonitor ?? throw new ArgumentNullException(nameof(windowMonitor));
            _notifications = notifications ?? throw new ArgumentNullException(nameof(notifications));

            Subscribe();
        }

        public void Unsubscribe()
        {
            _windowMonitor.Changed -= WindowMonitorOnChanged;
        }

        private void Subscribe()
        {
            _windowMonitor.Changed += WindowMonitorOnChanged;
        }

        private void WindowMonitorOnChanged(object sender, WindowMonitorEventArgs e)
        {
            if (e.WindowState != WindowState.Minimized && e.IsActive)
            {
                while (_notifications.HasItems())
                {
                    var item = _notifications.GetItem();
                    Console.WriteLine($"{item.Title} - {item.Message}");
                }
            }

            Console.WriteLine($"{_index++} WindowState: {e.WindowState}, IsActive: {e.IsActive}");
        }
    }
}
