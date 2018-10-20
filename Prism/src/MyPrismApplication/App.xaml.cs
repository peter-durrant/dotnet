using System.Windows;
using Hdd.MyPrismApplication.Views;
using Prism.Ioc;

namespace Hdd.MyPrismApplication
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}