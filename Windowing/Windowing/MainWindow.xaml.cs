using System.Windows;

namespace Windowing
{
    public partial class MainWindow : Window
    {
        private INotificationManager _notificationManager;

        public MainWindow()
        {
            InitializeComponent();

            var windowMonitor = new WindowMonitor(this);
            var notifications = new Notifications();

            for (var i = 1; i < 10; i++)
            {
                notifications.Add(new Notification($"{i}", $"Message {i}"));
            }

            _notificationManager = new NotificationManager(windowMonitor, notifications);
        }
    }
}
