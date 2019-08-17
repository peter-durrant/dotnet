using System.ComponentModel;

namespace Hdd.ModuleCore
{
    public interface IVm : INotifyPropertyChanged
    {
        string Name { get; }
    }
}
