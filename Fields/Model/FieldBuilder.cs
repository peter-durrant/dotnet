using System;
using System.Reflection;
using Hdd.Model.Converters;

namespace Hdd.Model
{
    public static class FieldBuilder
    {
        public static IField Build(string id, string type, string[] mask)
        {
            switch (type)
            {
                case "int":
                    return new Field<int, IntConverter>(id);

                case "double":
                    return new Field<double, DoubleConverter>(id);

                case "bool":
                    return new Field<bool, BoolConverter>(id);

                default:
                    // enum

                    // find named enum type in current assembly
                    var assemblyQualifiedName = Assembly.GetExecutingAssembly().FullName;
                    var fieldTemplateParameterType = Type.GetType($"{type}, {assemblyQualifiedName}");

                    // set up mask
                    var enumMask = typeof(EnumValues<>);
                    var enumMaskTemplateParameter = enumMask.MakeGenericType(fieldTemplateParameterType);
                    dynamic maskValues = Activator.CreateInstance(fieldTemplateParameterType);
                    if (mask != null)
                    {
                        // use mask to limit range of enum values
                        foreach (var val in mask)
                            maskValues |= (dynamic) Enum.Parse(fieldTemplateParameterType, val);
                    }
                    else
                    {
                        // use all enum values
                        var values = Enum.GetValues(fieldTemplateParameterType);
                        foreach (var val in values) maskValues |= (dynamic) val;
                    }

                    dynamic enumMaskObject = Activator.CreateInstance(enumMaskTemplateParameter);
                    enumMaskObject.Mask = maskValues;

                    // set up enum converter
                    var enumConverter = typeof(EnumConverter<>);
                    var enumConverterEnumTemplateParameter =
                        enumConverter.MakeGenericType(fieldTemplateParameterType);
                    var enumConverterObject =
                        Activator.CreateInstance(enumConverterEnumTemplateParameter, enumMaskObject);

                    // create field
                    var fieldType = typeof(Field<>);
                    var fieldTemplateParameter = fieldType.MakeGenericType(fieldTemplateParameterType);
                    return (IField) Activator.CreateInstance(fieldTemplateParameter, id, enumConverterObject);
            }
        }
    }
}