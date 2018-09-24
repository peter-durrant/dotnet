using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Hdd.View.Converters
{
    public class ScaleConverter : MarkupExtension, IValueConverter
    {
        private ScaleConverter _scaleConverter;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double dimension))
            {
                return value;
            }

            if (!(parameter is string scaleString))
            {
                return value;
            }

            var scale = double.Parse(scaleString);

            return scale * dimension;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _scaleConverter ?? (_scaleConverter = new ScaleConverter());
        }
    }
}