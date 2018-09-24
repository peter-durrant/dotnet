using System;
using System.Windows;
using System.Windows.Controls;
using Hdd.ViewModel;
using MvvmDialogs;

namespace Hdd.View
{
    public partial class ChildWindow : Window, IWindow
    {
        public ChildWindow(Window owner, ChildViewModel viewModel)
        {
            base.Owner = owner;
            DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            InitializeComponent();
        }

        public new ContentControl Owner
        {
            get => base.Owner;
            set { }
        }
    }
}