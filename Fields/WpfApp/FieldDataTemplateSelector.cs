using System;
using System.Windows;
using System.Windows.Controls;
using Model;

namespace WpfApp
{
    public class FieldDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultFieldTemplate { get; set; }
        public DataTemplate BoolFieldTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!(item is IField field))
            {
                throw new InvalidOperationException($"Unrecognised type {item.GetType().Name} expected {nameof(IField)}");
            }

            if (field is IField<bool>)
            {
                return BoolFieldTemplate;
            }

            return DefaultFieldTemplate;
        }
    }
}