﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace Hdd.Model.Converters
{
    public class IntConverter : IConverter<int>
    {
        public IEnumerable<int> Values { get; }

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