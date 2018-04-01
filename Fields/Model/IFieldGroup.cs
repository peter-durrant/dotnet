using System.Collections.Generic;

namespace Hdd.Model
{
    public interface IFieldGroup
    {
        List<IFieldGroup> FieldGroups { get; }
        List<IField> Fields { get; }
        string Id { get; }
    }
}