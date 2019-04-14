using System;
using System.Linq;

namespace Hdd.Model
{
    public class EnumValues<T> : IEnum<T>
        where T : struct, Enum
    {
        private readonly Array _values = Enum.GetValues(typeof(T));
        private T _value;

        public T[] Values => _values.Cast<object>().Where(value => Mask.HasFlag((T) value)).Cast<T>().ToArray();

        public T Value
        {
            get => _value;
            set => _value = Mask.HasFlag(value) ? value : default(T);
        }

        public T Mask { get; set; }
    }
}