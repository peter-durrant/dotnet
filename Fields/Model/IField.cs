﻿namespace Hdd.Model
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
    }
}