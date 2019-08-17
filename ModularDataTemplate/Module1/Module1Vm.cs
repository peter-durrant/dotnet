using System.ComponentModel;
using Hdd.ModuleCore;

namespace Hdd.Module1
{
    public class Module1Vm : IVm
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; }

        public Module1Vm()
        {
            Name = "Name Property - Module1Vm";
        }
    }
}
