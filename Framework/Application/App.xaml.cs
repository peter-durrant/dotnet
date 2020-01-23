using System.Windows;
using Application.Views;
using DryIoc;
using Hdd.Application.Core;
using Hdd.SqlPersistence.Module;
using Hdd.VisualizeData.Module;
using Prism.DryIoc;
using Prism.Ioc;

namespace Application
{
    public partial class App : PrismApplication
    {
        private readonly IContainer _container;

        public App()
        {
            _container = new Container();

            Exit += (sender, args) => _container.Dispose();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _container.Register<IModule, SqlPersistenceModule>();
            _container.Register<IModule, VizualizeDataModule>();

            foreach (var module in _container.ResolveMany<IModule>())
            {
                module.Load(_container);
            }
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
