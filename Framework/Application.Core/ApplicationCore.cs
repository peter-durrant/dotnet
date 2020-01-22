using System.Collections.Generic;

namespace Hdd.Application.Core
{
    public class ApplicationCore : IApplicationCore
    {
        public IList<IModule> Modules { get; set; }
        public IPersistence Persistence { get; set; }
        public ILogger Logger { get; set; }
    }
}
