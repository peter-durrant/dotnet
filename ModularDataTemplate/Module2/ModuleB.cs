using System;
using System.Windows;
using Hdd.ModuleCore;

namespace Hdd.Module2
{
    public class ModuleB : IModule
    {
        public void Initialise()
        {
            var rd = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/Hdd.Module2;component/Module2Dictionary.xaml", UriKind.Absolute)
            };
            Application.Current.Resources.MergedDictionaries.Add(rd);
            Console.WriteLine("Hdd.Module2.Initialise");
        }
    }
}
