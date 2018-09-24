using System;
using System.Windows;
using Hdd.ViewModel;

namespace Hdd.View
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            DataContext = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));

            InitializeComponent();
        }
    }
}