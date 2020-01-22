using DryIoc;

namespace Hdd.Application.Core
{
    public interface IModule
    {
        void Load(IRegistrator builder);
    }
}
