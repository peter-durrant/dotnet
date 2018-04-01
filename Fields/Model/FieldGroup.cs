using System.Collections.Generic;

namespace Model
{
    public class FieldGroup : IFieldGroup
    {
        public FieldGroup(string id)
        {
            Id = id;
        }

        public string Id { get; }

        public List<IField> Fields { get; } = new List<IField>();

        public List<IFieldGroup> FieldGroups { get; } = new List<IFieldGroup>();

        public void AddField(IField field)
        {
            Fields.Add(field);
        }

        public void AddFieldGroup(IFieldGroup fieldGroup)
        {
            FieldGroups.Add(fieldGroup);
        }
    }
}