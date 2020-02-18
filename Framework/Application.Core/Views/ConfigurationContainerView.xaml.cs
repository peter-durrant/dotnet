using System.Windows.Controls;
using Hdd.Application.Core.ViewModels;

namespace Hdd.Application.Core.Views
{
    public partial class ConfigurationContainerView : TabItem
    {
        public ConfigurationContainerView(ConfigurationContainerViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
