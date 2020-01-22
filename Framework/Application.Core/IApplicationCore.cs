using System.Collections.Generic;

namespace Hdd.Application.Core
{
    public interface IApplicationCore
    {
        IList<IModule> Modules { get; set; }
        IPersistence Persistence { get; set; }
        ILogger Logger { get; set; }
    }
}