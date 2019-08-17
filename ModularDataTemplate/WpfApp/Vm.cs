using System.ComponentModel;
using System.Runtime.CompilerServices;
using Hdd.ModuleCore;

namespace Hdd.WpfApp
{
    public class Vm : INotifyPropertyChanged
    {
        public IVm ViewModel1 { get;  }
        public IVm ViewModel2 { get;  }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Vm(IVm viewModel1, IVm viewModel2)
        {
            ViewModel1 = viewModel1;
            ViewModel2 = viewModel2;
        }
    }
}
