using Hdd.Application.Core;
using Prism.Ioc;
using Prism.Modularity;

namespace Hdd.SqlPersistence.Module
{
    public class SqlPersistenceModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IPersistence, SqlPersistence>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // noop
        }
    }
}
