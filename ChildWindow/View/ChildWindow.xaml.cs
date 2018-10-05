using System;
using System.Windows;
using Hdd.ViewModel;

namespace Hdd.View
{
    public partial class ChildWindow : Window
    {
        public ChildWindow(Window owner, ChildViewModel viewModel)
        {
            Owner = owner;
            DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            InitializeComponent();
        }
    }
}