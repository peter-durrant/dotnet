using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Hdd.Model.Converters
{
    public class EnumConverter<TEnum, T> : IConverter<TEnum>
        where TEnum : struct
        where T : IEnum<TEnum>
    {
        private readonly T _options;

        public EnumConverter(T options)
        {
            _options = options;
        }

        public IEnumerable<TEnum> Values => _options.Values;

        public bool Convert(string valueStr, out TEnum value)
        {
            var success = false;
            var convertedValue = default(TEnum);

            if (Values.Any())
            {
                success = Enum.TryParse(valueStr, out convertedValue);
                if (success)
                {
                    success = _options.Values.Contains(convertedValue);
                }
            }

            value = success ? convertedValue : default(TEnum);
            return success;
        }

        public string Convert(TEnum value)
        {
            return value.ToString();
        }

        public void SetCulture(CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}