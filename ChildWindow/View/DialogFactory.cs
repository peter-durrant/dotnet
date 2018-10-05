using System;
using System.Windows;
using Hdd.ViewModel;
using MvvmDialogs;
using MvvmDialogs.DialogFactories;

namespace Hdd.View
{
    public class DialogFactory : IDialogFactory
    {
        private readonly ReflectionDialogFactory _reflectionDialogFactory;

        public DialogFactory()
        {
            _reflectionDialogFactory = new ReflectionDialogFactory();
        }

        public Window Owner { private get; set; }

        public IWindow Create(Type dialogType)
        {
            if (dialogType != typeof(ChildWindow))
            {
                return _reflectionDialogFactory.Create(dialogType);
            }

            var childViewModel = new ChildViewModel();
            var childWindow = new ChildWindow(Owner, childViewModel);
            return new WindowWrapper(childWindow);
        }
    }
}