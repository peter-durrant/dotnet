using Hdd.VisualizeData.Module.Model;
using Hdd.VisualizeData.Module.ViewModel;
using Prism.Ioc;
using Prism.Modularity;

namespace Hdd.VisualizeData.Module
{
    public class VizualizeDataModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<DisplayModel>();
            containerRegistry.Register<DisplayVm>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // noop
        }
    }
}
