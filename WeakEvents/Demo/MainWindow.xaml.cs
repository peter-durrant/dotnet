using System;
using System.Windows;
using Demo.ViewModel;

namespace Demo
{
    public partial class MainWindow : Window
    {
        private LongLivedViewModel _longLivedViewModel;
        private bool _testWeakEventManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToggleEventHandlingMechanism(object sender, RoutedEventArgs e)
        {
            _longLivedViewModel = new LongLivedViewModel();
            _testWeakEventManager = !_testWeakEventManager;

            DataContext = _longLivedViewModel;

            TestWeakEventManager(_longLivedViewModel);
        }

        private void TestWeakEventManager(LongLivedViewModel longLivedViewModel)
        {
            longLivedViewModel.Log = _testWeakEventManager ? "Using WeakEventManager" : "Using strong reference";
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, _testWeakEventManager);
            var weakReferenceShortLivedViewModel = new WeakReference(shortLivedViewModel);

            longLivedViewModel.Log = "Raise event on LongLivedViewModel";
            longLivedViewModel.RaiseEvent();

            longLivedViewModel.Log = "Clear reference to ShortLivedViewModel";

            shortLivedViewModel = null;

            longLivedViewModel.Log = "Force garbage collection";
            GC.Collect();

            longLivedViewModel.Log = $"ShortLivedViewModel.IsAlive = {weakReferenceShortLivedViewModel.IsAlive}";

            longLivedViewModel.Log = "Raise event on LongLivedViewModel";
            longLivedViewModel.RaiseEvent();
        }
    }
}