using System;

namespace Windowing
{
    public interface IWindowMonitor
    {
        event EventHandler<WindowMonitorEventArgs> Changed;
    }
}
