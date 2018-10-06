using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hdd.WeakEvents.Demo.ViewModel
{
    public class LongLivedViewModel : INotifyPropertyChanged
    {
        private readonly List<string> _log = new List<string>();

        public string Log
        {
            get => string.Join(Environment.NewLine, _log);
            set
            {
                _log.Add(value);
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler EventOnLongLivedViewModel;

        public void RaiseEvent()
        {
            EventOnLongLivedViewModel?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}