using System;
using System.Windows;
using Hdd.ModuleCore;

namespace Hdd.WpfApp
{
    public partial class App : Application
    {
        public App()
        {
            var types = ModuleLoadExtensions.GetLoadableTypes<IModule>();
            foreach (var type in types)
            {
                var module = (IModule)Activator.CreateInstance(type);
                module.Initialise();
            }
        }
    }
}
