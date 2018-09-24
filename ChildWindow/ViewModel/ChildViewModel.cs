using Hdd.Presentation.Core;
using MvvmDialogs;

namespace Hdd.ViewModel
{
    public class ChildViewModel : ViewModelBase, IModalDialogViewModel
    {
        private bool? _dialogResult;

        public bool? DialogResult
        {
            get => _dialogResult;
            set
            {
                _dialogResult = value;
                OnPropertyChanged();
            }
        }
    }
}