using DryIoc;
using Hdd.Application.Core;

namespace Hdd.SqlPersistence.Module
{
    public class SqlPersistenceModule : IModule
    {
        public void Load(IRegistrator builder)
        {
            builder.Register<IPersistence, SqlPersistence>();
        }
    }
}
