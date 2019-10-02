using System;
using System.Windows;

namespace Windowing
{
    public class WindowMonitorEventArgs : EventArgs
    {
        public WindowMonitorEventArgs(WindowState windowState, bool isActive)
        {
            WindowState = windowState;
            IsActive = isActive;
        }

        public WindowState WindowState { get; }
        public bool IsActive { get; }
    }
}
