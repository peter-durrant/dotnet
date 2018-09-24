using System;
using System.Windows.Input;
using Hdd.Presentation.Core;
using MvvmDialogs;

namespace Hdd.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            LaunchChildWindowCommand = new RelayCommand(ShowChildWindow);
        }

        public ICommand LaunchChildWindowCommand { get; }

        private void ShowChildWindow(object _)
        {
            var childViewModel = new ChildViewModel();
            _dialogService.ShowDialog(this, childViewModel);
        }
    }
}