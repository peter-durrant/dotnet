using System.ComponentModel;
using Hdd.ModuleCore;

namespace Hdd.Module2
{
    public class Module2Vm : IVm
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; }

        public Module2Vm()
        {
            Name = "Name Property - Module2Vm";
        }
    }
}
