using System;

namespace Model
{
    public interface IField<T> : IField
    {
        T Value { get; set; }
    }

    public interface IField
    {
        string Id { get; }
        string RawValue { get; set; }
        bool HasValue { get; }
        Type FieldType { get; }
    }
}