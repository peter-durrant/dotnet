using System;
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
            var enumOptions = new Options {Mask = Options.Option.Option3 | Options.Option.Option2};
            var enumConverter = new EnumConverter<Options.Option, Options>(enumOptions);
            var enumField = new Field<Options.Option>("optionField", enumConverter);

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

                foreach (var field in page.Fields.Field) Fields.Add(Build(field));

                foreach (var fieldGroup in page.Fields.FieldGroup)
                {
                    var fg = new Model.FieldGroup(fieldGroup.id);
                    foreach (var field in fieldGroup.Field) fg.AddField(Build(field));
                    FieldGroup.AddFieldGroup(fg);
                }
            }
        }

        private IField Build(Field field)
        {
            switch (field.type)
            {
                case "int":
                    return new Field<int, IntConverter>(field.id);

                case "double":
                    return new Field<double, DoubleConverter>(field.id);

                case "bool":
                    return new Field<bool, BoolConverter>(field.id);

                case "enum":
                    throw new NotImplementedException("todo");

                default:
                    throw new InvalidOperationException("Invalid field type");
            }
        }
    }
}