using System;
using System.Globalization;

namespace Model.Converters
{
    public class IntConverter : IConverter<int>
    {
        public bool Convert(string valueStr, out int value)
        {
            var success = int.TryParse(valueStr, out var convertedValue);
            value = success ? convertedValue : default(int);
            return success;
        }

        public string Convert(int value)
        {
            return value.ToString();
        }

        public void SetCulture(CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}