using System;
using System.Collections.Generic;

namespace Hdd.Model
{
    public interface IField<T> : IField
    {
        T Value { get; set; }
        IEnumerable<T> Values { get; }
    }

    public interface IField
    {
        string Id { get; }
        string RawValue { get; set; }
        bool HasValue { get; }
        Type FieldType { get; }
    }
}