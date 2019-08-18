using System.Reflection;
using Hdd.ModuleCore;

namespace Hdd.Module1
{
    public class ModuleA : IModule
    {
        public void Initialise()
        {
            ResourceDictionaryManager.MergeDictionary(Assembly.GetExecutingAssembly().GetName().Name,
                "Module1Dictionary.xaml");
        }
    }
}
