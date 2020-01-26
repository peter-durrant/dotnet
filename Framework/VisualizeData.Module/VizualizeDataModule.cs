using System;
using Hdd.Application.Core.Regions;
using Hdd.VisualizeData.Module.Model;
using Hdd.VisualizeData.Module.ViewModels;
using Hdd.VisualizeData.Module.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Hdd.VisualizeData.Module
{
    public class VizualizeDataModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public VizualizeDataModule(IRegionManager regionManager)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<DisplayModel>();
            containerRegistry.Register<DisplayViewModel>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ConfigurationRegion, typeof(ConfigurationView));
        }
    }
}
