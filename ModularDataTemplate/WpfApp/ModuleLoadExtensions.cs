using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hdd.WpfApp
{
    public static class ModuleLoadExtensions
    {
        public static IReadOnlyCollection<Type> GetLoadableTypes<T>()
        {
            var typesList = new List<Type>();
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (path == null)
                {
                    return typesList;
                }

                var files = Directory.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories);
                var assemblies = files.Select(Assembly.LoadFile).ToList();

                foreach (var loadedAssembly in assemblies)
                {
                    var types = loadedAssembly.GetTypes().Where(type =>
                        typeof(T).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract && type.IsPublic).ToList();
                    if (!types.Any())
                    {
                        continue;
                    }

                    foreach (var type in types)
                    {
                        typesList.Add(type);
                    }
                }
            }
            catch (ReflectionTypeLoadException)
            {
                return typesList;
            }

            return typesList;
        }
    }
}
