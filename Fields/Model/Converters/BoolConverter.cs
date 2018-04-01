using System.Globalization;

namespace Hdd.Model.Converters
{
    public class BoolConverter : IConverter<bool>
    {
        private CultureInfo _culture = CultureInfo.InvariantCulture;

        public BoolConverter()
        {
        }

        public BoolConverter(CultureInfo culture)
        {
            _culture = culture;
        }

        public bool Convert(string valueStr, out bool value)
        {
            var success = bool.TryParse(valueStr, out var convertedValue);
            value = success && convertedValue;
            return success;
        }

        public string Convert(bool value)
        {
            return value.ToString(_culture);
        }

        public void SetCulture(CultureInfo culture)
        {
            _culture = culture;
        }
    }
}