using DryIoc;
using Hdd.Application.Core;
using Hdd.VisualizeData.Module.Model;
using Hdd.VisualizeData.Module.ViewModel;

namespace Hdd.VisualizeData.Module
{
    public class VizualizeDataModule : IModule
    {
        public void Load(IRegistrator builder)
        {
            builder.Register<DisplayModel>();
            builder.Register<DisplayVm>();
        }
    }
}
