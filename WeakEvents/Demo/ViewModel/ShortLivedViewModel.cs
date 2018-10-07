using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Hdd.WeakEvents.Demo.ViewModel
{
    public sealed class ShortLivedViewModel : INotifyPropertyChanged
    {
        private readonly LongLivedViewModel _longLivedViewModel;

        public ShortLivedViewModel(LongLivedViewModel longLivedViewModel, EventPattern eventPattern)
        {
            _longLivedViewModel = longLivedViewModel ?? throw new ArgumentNullException(nameof(longLivedViewModel));

            switch (eventPattern)
            {
                case EventPattern.StrongReference:
                    // potential memory leak - no garbage collection of ShortLivedViewModel due to subscription
                    // to LongLivedViewModel_EventOnLongLivedViewModel
                    longLivedViewModel.EventOnLongLivedViewModel += LongLivedViewModel_EventOnLongLivedViewModel;
                    break;
                case EventPattern.GenericWeakEventManager:
                    // avoids memory leak
                    // generic weak event manager is less performant than a custom weak event manager
                    WeakEventManager<LongLivedViewModel, EventArgs>.AddHandler(
                        longLivedViewModel,
                        nameof(LongLivedViewModel.EventOnLongLivedViewModel),
                        LongLivedViewModel_EventOnLongLivedViewModel);
                    break;
                case EventPattern.CustomEventManager:
                    // avoids memory leak
                    // custom weak event manager is most performant
                    EventOnLongLivedViewModelWeakEventManager.AddHandler(
                        longLivedViewModel,
                        LongLivedViewModel_EventOnLongLivedViewModel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventPattern), eventPattern, null);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LongLivedViewModel_EventOnLongLivedViewModel(object sender, EventArgs e)
        {
            _longLivedViewModel.Log = "ShortLivedViewModel - handled event";
        }
    }
}