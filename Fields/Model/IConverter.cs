using System.Globalization;

namespace Hdd.Model
{
    public interface IConverter<T> : IConverter
    {
        bool Convert(string valueStr, out T value);
        string Convert(T value);
    }

    public interface IConverter
    {
        void SetCulture(CultureInfo culture);
    }
}