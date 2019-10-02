using System;

namespace Windowing
{
    public class Notification
    {
        public Notification(string title, string message)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public string Title { get; }
        public string Message { get; }
    }
}
