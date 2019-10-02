using System;
using System.Reactive.Linq;
using System.Windows;

namespace Windowing
{
    public class WindowMonitor : IWindowMonitor
    {
        private readonly Window _window;

        public WindowMonitor(Window window)
        {
            _window = window ?? throw new ArgumentNullException(nameof(window));

            var isActiveObservable = Observable
                .FromEventPattern(
                    addHandler =>
                    {
                        _window.Activated += addHandler;
                        _window.Deactivated += addHandler;
                    },
                    removeHandler =>
                    {
                        _window.Activated -= removeHandler;
                        _window.Deactivated -= removeHandler;
                    })
                .Select(eventPattern => ((Window)eventPattern.Sender).IsActive);

            var windowStateObservable = Observable
                .FromEventPattern(
                    addHandler => _window.StateChanged += addHandler,
                    removeHandler => _window.StateChanged -= removeHandler)
                .Select(eventPattern => ((Window)eventPattern.Sender).WindowState);

            var changes = windowStateObservable.CombineLatest(isActiveObservable,
                    (windowState, isActive) => new WindowMonitorEventArgs(windowState, isActive))
                .Throttle(TimeSpan.FromMilliseconds(100));

            changes.Subscribe(windowMonitorEventArgs => Changed?.Invoke(this, windowMonitorEventArgs));
        }

        public event EventHandler<WindowMonitorEventArgs> Changed;
    }
}
