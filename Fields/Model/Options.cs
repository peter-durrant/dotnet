using System;
using System.Linq;

namespace Hdd.Model
{
    public class Options : IEnum<Options.Option>
    {
        [Flags]
        public enum Option
        {
            Option1 = 1 << 0,
            Option2 = 1 << 1,
            Option3 = 1 << 2,
            Option4 = 1 << 3
        }

        private readonly Array _values = Enum.GetValues(typeof(Option));
        private Option _value;

        public Options.Option[] Values => _values.Cast<object>().Where(value => Mask.HasFlag((Option) value)).Cast<Option>().ToArray();

        public Option Value
        {
            get => _value;
            set => _value = Mask.HasFlag(value) ? value : default(Options.Option);
        }

        public Option Mask { get; set; }
    }
}