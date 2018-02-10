using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Hdd.CsvProcessor
{
    public class CurrencyToDoubleConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return double.Parse(text, NumberStyles.Currency | NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture);
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            throw new System.NotSupportedException();
        }
    }
}