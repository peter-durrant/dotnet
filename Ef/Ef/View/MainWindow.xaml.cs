using System;
using System.Windows;
using Hdd.Ef.Model;
using Hdd.Ef.ViewModel;

namespace Hdd.Ef.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var dataModel = new DataModel("../../../../database.db");
            DataContext = new PartsViewModel(dataModel);
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            (DataContext as IDisposable)?.Dispose();
        }
    }
}
