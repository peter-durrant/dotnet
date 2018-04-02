using System.Collections.Generic;
using System.Globalization;

namespace Hdd.Model
{
    public interface IConverter<T> : IConverter
    {
        IEnumerable<T> Values { get; }
        bool Convert(string valueStr, out T value);
        string Convert(T value);
    }

    public interface IConverter
    {
        void SetCulture(CultureInfo culture);
    }
}