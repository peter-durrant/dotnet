using System;
using Hdd.Application.Core;
using Hdd.Application.Core.Regions;
using Hdd.SqlPersistence.Module.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Hdd.SqlPersistence.Module
{
    public class SqlPersistenceModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public SqlPersistenceModule(IRegionManager regionManager)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IPersistence, SqlPersistence>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ConfigurationRegion, typeof(ConfigurationView));
        }
    }
}
