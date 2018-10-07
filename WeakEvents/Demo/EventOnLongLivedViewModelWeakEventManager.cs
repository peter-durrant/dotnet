using System;
using System.Windows;
using Hdd.WeakEvents.Demo.ViewModel;

namespace Hdd.WeakEvents.Demo
{
    /// <inheritdoc />
    /// <summary>
    ///     Derived from the pattern described at
    ///     https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/weak-event-patterns
    ///     The reason described in the article for writing a customer WeakEventManager is for efficiency. In this demo, it
    ///     appears to be less efficient than the generic weak event manager, which does not require boiler-plate code like
    ///     this.
    /// </summary>
    public class EventOnLongLivedViewModelWeakEventManager : WeakEventManager
    {
        private EventOnLongLivedViewModelWeakEventManager()
        {
        }

        private static EventOnLongLivedViewModelWeakEventManager CurrentManager
        {
            get
            {
                var managerType = typeof(EventOnLongLivedViewModelWeakEventManager);
                var manager =
                    (EventOnLongLivedViewModelWeakEventManager) GetCurrentManager(managerType);

                if (manager != null)
                {
                    return manager;
                }

                manager = new EventOnLongLivedViewModelWeakEventManager();
                SetCurrentManager(managerType, manager);

                return manager;
            }
        }

        public static void AddHandler(LongLivedViewModel source, EventHandler handler)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            _ = handler ?? throw new ArgumentNullException(nameof(handler));

            CurrentManager.ProtectedAddHandler(source, handler);
        }

        public static void RemoveHandler(LongLivedViewModel source, EventHandler handler)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            _ = handler ?? throw new ArgumentNullException(nameof(handler));

            CurrentManager.ProtectedRemoveHandler(source, handler);
        }

        protected override ListenerList NewListenerList()
        {
            return new ListenerList();
        }

        protected override void StartListening(object source)
        {
            var typedSource = (LongLivedViewModel) source;
            typedSource.EventOnLongLivedViewModel += OnSomeEvent;
        }

        protected override void StopListening(object source)
        {
            var typedSource = (LongLivedViewModel) source;
            typedSource.EventOnLongLivedViewModel -= OnSomeEvent;
        }

        private void OnSomeEvent(object sender, EventArgs e)
        {
            DeliverEvent(sender, e);
        }
    }
}