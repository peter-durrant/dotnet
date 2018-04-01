using System.Collections.Generic;

namespace Model
{
    public interface IFieldGroup
    {
        List<IFieldGroup> FieldGroups { get; }
        List<IField> Fields { get; }
        string Id { get; }
    }
}