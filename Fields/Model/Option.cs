using System;

namespace Hdd.Model
{
    [Flags]
    public enum Option
    {
        Option1 = 1 << 0,
        Option2 = 1 << 1,
        Option3 = 1 << 2,
        Option4 = 1 << 3
    }
}