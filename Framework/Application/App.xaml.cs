using System;
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
        public App()
        {
            var container = new Container();
            container.Register<IModule, SqlPersistenceModule>();
            container.Register<IModule, VizualizeDataModule>();

            foreach (var module in container.ResolveMany<IModule>())
            {
                module.Load(container);
            }

            var persistence = container.Resolve<IPersistence>();

            Console.WriteLine(persistence.Name);

            Exit += (sender, args) => container.Dispose();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // noop
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
