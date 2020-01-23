namespace Hdd.Application.Core
{
    public class ApplicationCore : IApplicationCore
    {
        public IPersistence Persistence { get; set; }
        public ILogger Logger { get; set; }
    }
}
