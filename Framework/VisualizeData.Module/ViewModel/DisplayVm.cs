using System;
using Hdd.VisualizeData.Module.Model;
using Prism.Mvvm;

namespace Hdd.VisualizeData.Module.ViewModel
{
    public class DisplayVm : BindableBase
    {
        private readonly DisplayModel _model;

        public DisplayVm(DisplayModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }
    }
}
