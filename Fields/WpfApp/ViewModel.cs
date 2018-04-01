using Hdd.Model;
using Hdd.Model.Converters;
using Hdd.Presentation.Core;

namespace Hdd.WpfApp
{
    public class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            Field = new Field<int>("field", "100", new IntConverter());

            var intField = new Field<int, IntConverter>("intField");
            var doubleField = new Field<double, DoubleConverter>("doubleField");
            var boolField = new Field<bool, BoolConverter>("boolField");

            FieldGroup = new FieldGroup("field group");
            FieldGroup.Fields.Add(intField);
            FieldGroup.Fields.Add(doubleField);
            FieldGroup.Fields.Add(boolField);
        }

        public Field<int> Field { get; }
        public FieldGroup FieldGroup { get; }
    }
}