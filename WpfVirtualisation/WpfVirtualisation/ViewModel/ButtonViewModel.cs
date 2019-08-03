using System;
using Hdd.Presentation.Core;

namespace WpfVirtualisation.ViewModel
{
    public class ButtonViewModel : ViewModelBase
    {
        public ButtonViewModel(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public string Title { get; }
    }
}
