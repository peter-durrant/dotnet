using System.Collections.Generic;
using System.Globalization;

namespace Hdd.Model.Converters
{
    public class DoubleConverter : IConverter<double>
    {
        private CultureInfo _culture = CultureInfo.InvariantCulture;

        public DoubleConverter()
        {
        }

        public DoubleConverter(CultureInfo culture)
        {
            _culture = culture;
        }

        public IEnumerable<double> Values { get; }

        public bool Convert(string valueStr, out double value)
        {
            const NumberStyles numberStyles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
            var success = double.TryParse(valueStr, numberStyles, _culture, out var convertedValue);
            value = success ? convertedValue : default(double);
            return success;
        }

        public string Convert(double value)
        {
            return value.ToString(_culture);
        }

        public void SetCulture(CultureInfo culture)
        {
            _culture = culture;
        }
    }
}