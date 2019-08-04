using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Hdd.Presentation.Core;

namespace WpfVirtualisation.ViewModel
{
    public class ButtonsViewModel : ViewModelBase
    {
        private bool _loadedData;

        public ButtonsViewModel()
        {
            var buttons = new List<ButtonViewModel>();

            ShowButtonsCommand = new RelayCommand(o =>
            {
                foreach (var buttonViewModel in buttons)
                {
                    Buttons.Add(buttonViewModel);
                }
            }, o => _loadedData);

            Task.Run(() =>
            {
                for (var i = 0; i < 100001; i++)
                {
                    buttons.Add(new ButtonViewModel($"Button {i}"));
                }

                _loadedData = true;
                CommandManager.InvalidateRequerySuggested();
            });
        }

        public RelayCommand ShowButtonsCommand { get; }

        public ObservableCollection<ButtonViewModel> Buttons { get; } = new ObservableCollection<ButtonViewModel>();
    }
}
