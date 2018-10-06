using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Hdd.WeakEvents.Demo.ViewModel
{
    public sealed class ShortLivedViewModel : INotifyPropertyChanged
    {
        private readonly LongLivedViewModel _longLivedViewModel;

        public ShortLivedViewModel(LongLivedViewModel longLivedViewModel, bool useWeakEventManager)
        {
            _longLivedViewModel = longLivedViewModel ?? throw new ArgumentNullException(nameof(longLivedViewModel));

            if (!useWeakEventManager)
            {
                // potential memory leak - no garbage collection of ShortLivedViewModel due to subscription
                // to LongLivedViewModel_EventOnLongLivedViewModel
                longLivedViewModel.EventOnLongLivedViewModel += LongLivedViewModel_EventOnLongLivedViewModel;
            }
            else
            {
                // avoids memory leak
                WeakEventManager<LongLivedViewModel, EventArgs>.
                    AddHandler(
                        longLivedViewModel,
                        nameof(LongLivedViewModel.EventOnLongLivedViewModel),
                        LongLivedViewModel_EventOnLongLivedViewModel);
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