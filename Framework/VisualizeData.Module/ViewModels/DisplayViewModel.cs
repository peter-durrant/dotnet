using System;
using Hdd.VisualizeData.Module.Model;
using Prism.Mvvm;

namespace Hdd.VisualizeData.Module.ViewModels
{
    public class DisplayViewModel : BindableBase
    {
        private readonly DisplayModel _model;

        public DisplayViewModel(DisplayModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }
    }
}
