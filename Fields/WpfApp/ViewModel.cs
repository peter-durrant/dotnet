using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
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
            var enumOptions = new EnumValues<Option> {Mask = Option.Option3 | Option.Option2};
            var enumConverter = new EnumConverter<Option>(enumOptions);
            var enumField = new Field<Option>("optionField", enumConverter);

            FieldGroup = new Model.FieldGroup("field group");
            FieldGroup.Fields.Add(intField);
            FieldGroup.Fields.Add(doubleField);
            FieldGroup.Fields.Add(boolField);
            FieldGroup.Fields.Add(enumField);

            LoadConfigurableFields();
        }

        public Field<int> Field { get; }
        public Model.FieldGroup FieldGroup { get; }

        public ObservableCollection<IField> Fields { get; private set; }


        public void LoadConfigurableFields()
        {
            Fields = new ObservableCollection<IField>();

            var serializer = new XmlSerializer(typeof(Page), "");
            using (var reader = new StreamReader("Page.xml"))
            {
                var page = (Page) serializer.Deserialize(reader);
                reader.Close();

                foreach (var field in page.Fields.Field)
                    Fields.Add(FieldBuilder.Build(field.id, field.type, field.Mask));

                foreach (var fieldGroup in page.Fields.FieldGroup)
                {
                    var fg = new Model.FieldGroup(fieldGroup.id);
                    foreach (var field in fieldGroup.Field)
                        fg.AddField(FieldBuilder.Build(field.id, field.type, field.Mask));
                    FieldGroup.AddFieldGroup(fg);
                }
            }
        }
    }
}