using System.Windows;
using Hdd.View;
using Hdd.ViewModel;
using MvvmDialogs;

namespace Hdd.WpfApp
{
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var dialogFactory = new DialogFactory();
            var dialogService = new DialogService(dialogFactory, new DialogTypeLocator());
            var mainViewModel = new MainViewModel(dialogService);
            MainWindow = new MainWindow(mainViewModel);
            dialogFactory.Owner = MainWindow;
            MainWindow.Show();
        }
    }
}