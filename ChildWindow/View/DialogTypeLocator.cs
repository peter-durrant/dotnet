using System;
using System.ComponentModel;
using Hdd.ViewModel;
using MvvmDialogs.DialogTypeLocators;

namespace Hdd.View
{
    public class DialogTypeLocator : IDialogTypeLocator
    {
        public Type Locate(INotifyPropertyChanged viewModel)
        {
            if (viewModel.GetType() == typeof(ChildViewModel))
            {
                return typeof(ChildWindow);
            }

            throw new ArgumentException("Unrecognised view model", nameof(viewModel));
        }
    }
}