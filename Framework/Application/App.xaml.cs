using System;
using System.Windows;
using Application.Views;
using Hdd.Application.Core;
using Hdd.SqlPersistence.Module;
using Hdd.VisualizeData.Module;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;

namespace Application
{
    public partial class App : PrismApplication
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = new ModuleCatalog();
            moduleCatalog.AddModule(
                new ModuleInfo
                {
                    ModuleName = typeof(SqlPersistenceModule).Name,
                    ModuleType = typeof(SqlPersistenceModule).AssemblyQualifiedName
                });
            moduleCatalog.AddModule(
                new ModuleInfo
                {
                    ModuleName = typeof(VizualizeDataModule).Name,
                    ModuleType = typeof(VizualizeDataModule).AssemblyQualifiedName
                });
            return moduleCatalog;
        }

        protected override void OnInitialized()
        {
            var persistence = Container.Resolve<IPersistence>();
            Console.WriteLine(persistence.Name);

            base.OnInitialized();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // noop
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }
    }
}
