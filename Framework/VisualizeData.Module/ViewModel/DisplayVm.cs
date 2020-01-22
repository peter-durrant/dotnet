using System;
using Hdd.ViewModel.Core;
using Hdd.VisualizeData.Module.Model;

namespace Hdd.VisualizeData.Module.ViewModel
{
    public class DisplayVm : BaseVm
    {
        private readonly DisplayModel _model;

        public DisplayVm(DisplayModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }
    }
}
