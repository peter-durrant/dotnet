using System;

namespace Hdd.Model
{
    public interface IEnum<T>
    {
        T Mask { get; set; }
        T[] Values { get; }
    }
}