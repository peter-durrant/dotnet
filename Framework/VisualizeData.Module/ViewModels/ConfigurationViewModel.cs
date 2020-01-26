using Hdd.Application.Core;
using Prism.Mvvm;

namespace Hdd.VisualizeData.Module.ViewModels
{
    public class ConfigurationViewModel : BindableBase, IConfiguration
    {
        public string Name => "Vizualisation";
    }
}
