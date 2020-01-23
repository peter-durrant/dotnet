namespace Hdd.Application.Core
{
    public interface IApplicationCore
    {
        IPersistence Persistence { get; set; }
        ILogger Logger { get; set; }
    }
}
