using System.Windows;

namespace Hdd.ToastExampleApp
{
    public partial class App : Application
    {
        private readonly ToastNotificationService _toastNotificationService;

        public App()
        {
            _toastNotificationService = new ToastNotificationService();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var viewModel = new ApplicationVm(_toastNotificationService);
            var mainWindow = new MainWindow {DataContext = viewModel};
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _toastNotificationService.Dispose();
            base.OnExit(e);
        }
    }
}
