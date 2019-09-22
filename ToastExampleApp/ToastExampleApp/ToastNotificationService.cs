using System;
using System.Windows;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Hdd.ToastExampleApp
{
    public sealed class ToastNotificationService : IDisposable, IToastNotificationService
    {
        private readonly Notifier _notifier;

        public ToastNotificationService()
        {
            _notifier = new Notifier(config =>
            {
                config.PositionProvider =
                    new WindowPositionProvider(Application.Current.MainWindow, Corner.BottomRight, 20, 20);

                config.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(TimeSpan.FromSeconds(4),
                    MaximumNotificationCount.FromCount(5));

                config.Dispatcher = Dispatcher.CurrentDispatcher;

                config.DisplayOptions.TopMost = true;
            });
        }

        public void Dispose()
        {
            _notifier.Dispose();
        }

        public void Notify(string message, bool isError)
        {
            if (isError)
            {
                _notifier.ShowError(message);
            }
            else
            {
                _notifier.ShowInformation(message);
            }
        }
    }
}
