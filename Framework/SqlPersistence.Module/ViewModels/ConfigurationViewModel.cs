﻿using Hdd.Application.Core;
using Prism.Mvvm;

namespace Hdd.SqlPersistence.Module.ViewModels
{
    public class ConfigurationViewModel : BindableBase, IConfiguration
    {
        public string Name => "SQL Configuration";
    }
}
