using System.Windows;
using Hdd.Module1;
using Hdd.Module2;

namespace Hdd.WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Vm(new Module1Vm(), new Module2Vm());
        }
    }
}
