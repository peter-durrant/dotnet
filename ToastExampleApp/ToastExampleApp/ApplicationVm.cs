using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Hdd.ToastExampleApp
{
    public class ApplicationVm : INotifyPropertyChanged
    {
        private readonly IToastNotificationService _toastNotificationService;

        public ApplicationVm(IToastNotificationService toastNotificationService)
        {
            _toastNotificationService = toastNotificationService ??
                throw new ArgumentNullException(nameof(toastNotificationService));

            GenerateMessage = new RelayCommand(command => ShowMessage());
            GenerateError = new RelayCommand(command => ShowError());
        }

        public ICommand GenerateMessage { get; }
        public ICommand GenerateError { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ShowMessage()
        {
            _toastNotificationService.Notify("This is some information", false);
        }

        private void ShowError()
        {
            _toastNotificationService.Notify("This is an error", true);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
