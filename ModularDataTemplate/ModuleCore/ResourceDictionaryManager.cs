using System;
using System.Windows;

namespace Hdd.ModuleCore
{
    public static class ResourceDictionaryManager
    {
        public static void MergeDictionary(string assemblyName, string resourceDictionaryPath)
        {
            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri(
                    $"pack://application:,,,/{assemblyName};component/{resourceDictionaryPath}",
                    UriKind.Absolute)
            };
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
