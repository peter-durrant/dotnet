using System;
using Hdd.Application.Core;

namespace Hdd.VisualizeData.Module.Model
{
    public class DisplayModel
    {
        private readonly IPersistence _persistence;

        public DisplayModel(IPersistence persistence)
        {
            _persistence = persistence ?? throw new ArgumentNullException(nameof(persistence));
        }
    }
}
