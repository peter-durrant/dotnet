using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Hdd.Ef.Model;
using Hdd.EfData;

namespace Hdd.Ef.ViewModel
{
    public class PartsViewModel : INotifyPropertyChanged
    {
        public PartsViewModel(DataModel dataModel)
        {
            PartCollection = new ObservableCollection<Part>(dataModel.PartCollection());
        }

        public ObservableCollection<Part> PartCollection { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
