using System;
using System.Windows;
using Hdd.WeakEvents.Demo.ViewModel;

namespace Hdd.WeakEvents.Demo
{
    public partial class MainWindow : Window
    {
        private LongLivedViewModel _longLivedViewModel;
        private EventPattern _testWeakEventManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToggleEventHandlingMechanism(object sender, RoutedEventArgs e)
        {
            _longLivedViewModel = new LongLivedViewModel();
            _testWeakEventManager = (EventPattern) ((int)(_testWeakEventManager + 1) % 3);

            DataContext = _longLivedViewModel;

            TestWeakEventManager(_longLivedViewModel);
        }

        private void TestWeakEventManager(LongLivedViewModel longLivedViewModel)
        {
            switch (_testWeakEventManager)
            {
                case EventPattern.StrongReference:
                    longLivedViewModel.Log = "Using strong reference";
                    break;
                case EventPattern.GenericWeakEventManager:
                    longLivedViewModel.Log = "Using generic WeakEventManager";
                    break;
                case EventPattern.CustomEventManager:
                    longLivedViewModel.Log = "Using custom WeakEventManager";
                    break;
            }

            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, _testWeakEventManager);
            var weakReferenceShortLivedViewModel = new WeakReference(shortLivedViewModel);

            longLivedViewModel.Log = "Raise event on LongLivedViewModel";
            // expect shortLivedViewModel to handle event
            longLivedViewModel.RaiseEvent();

            longLivedViewModel.Log = "Clear reference to ShortLivedViewModel";

            shortLivedViewModel = null;

            longLivedViewModel.Log = "Force garbage collection";
            GC.Collect();

            longLivedViewModel.Log = $"ShortLivedViewModel.IsAlive = {weakReferenceShortLivedViewModel.IsAlive}";

            longLivedViewModel.Log = "Raise event on LongLivedViewModel";
            // shortLivedViewModel will only handle the event if not using the WeakEventManager
            longLivedViewModel.RaiseEvent();
        }
    }
}