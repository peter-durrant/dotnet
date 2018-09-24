using System;
using System.Windows;
using Hdd.ViewModel;
using MvvmDialogs;
using MvvmDialogs.DialogFactories;

namespace Hdd.View
{
    public class DialogFactory : IDialogFactory
    {
        public Window Owner { get; set; }

        public IWindow Create(Type dialogType)
        {
            if (dialogType != typeof(ChildWindow))
            {
                throw new NotSupportedException();
            }

            var childViewModel = new ChildViewModel();
            var childWindow = new ChildWindow(Owner, childViewModel);
            return childWindow;
        }
    }
}