using System.Windows;
using Hdd.Application.Core.Views;
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
