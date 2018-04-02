using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hdd.Model
{
    public class Field<T> : IField<T>, INotifyPropertyChanged
    {
        private readonly IConverter<T> _converter;
        private string _rawValue = string.Empty;
        private T _value;

        public Field(string id, IConverter<T> converter)
        {
            Id = id;
            _converter = converter;
            _value = default(T);
            HasValue = false;

            OnPropertyChanged(nameof(HasValue));
            OnPropertyChanged(nameof(Value));
            OnPropertyChanged(nameof(RawValue));
        }

        public Field(string id, T value, IConverter<T> converter)
        {
            Id = id;
            _converter = converter;
            SetValue(value);
            OnPropertyChanged(nameof(HasValue));
            OnPropertyChanged(nameof(Value));
            OnPropertyChanged(nameof(RawValue));
        }

        public Field(string id, string valueStr, IConverter<T> converter)
        {
            Id = id;
            _converter = converter;
            SetRawValue(valueStr);
            OnPropertyChanged(nameof(HasValue));
            OnPropertyChanged(nameof(Value));
            OnPropertyChanged(nameof(RawValue));
        }

        public Type FieldType => typeof(T);

        public T Value
        {
            get => _value;
            set
            {
                SetValue(value);
                OnPropertyChanged(nameof(HasValue));
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(RawValue));
            }
        }

        public IEnumerable<T> Values => _converter.Values;

        public string Id { get; }

        public string RawValue
        {
            get => _rawValue;
            set
            {
                SetRawValue(value);
                OnPropertyChanged(nameof(HasValue));
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(RawValue));
            }
        }

        public bool HasValue { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetValue(T value)
        {
            _value = value;
            HasValue = true;
            _rawValue = _converter.Convert(_value);
        }

        private void SetRawValue(string value)
        {
            _rawValue = value;
            HasValue = _converter.Convert(_rawValue, out var convertedValue);
            _value = convertedValue;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public sealed class Field<T, TConverter> : Field<T> where TConverter : IConverter<T>, new()
    {
        public Field(string id) : base(id, new TConverter())
        {
            RawValue = string.Empty;
        }

        public Field(string id, T value) : base(id, value, new TConverter())
        {
        }

        public Field(string id, string valueStr) : base(id, valueStr, new TConverter())
        {
        }
    }
}