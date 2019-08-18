using System.Reflection;
using Hdd.ModuleCore;

namespace Hdd.Module2
{
    public class ModuleB : IModule
    {
        public void Initialise()
        {
            ResourceDictionaryManager.MergeDictionary(Assembly.GetExecutingAssembly().GetName().Name,
                "Module2Dictionary.xaml");
        }
    }
}
