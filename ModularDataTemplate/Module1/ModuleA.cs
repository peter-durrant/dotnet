using System;
using System.Windows;
using Hdd.ModuleCore;

namespace Hdd.Module1
{
    public class ModuleA : IModule
    {
        public void Initialise()
        {
            var rd = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/Hdd.Module1;component/Module1Dictionary.xaml", UriKind.Absolute)
            };
            Application.Current.Resources.MergedDictionaries.Add(rd);
            Console.WriteLine("Hdd.Module1.Initialise");
        }
    }
}
