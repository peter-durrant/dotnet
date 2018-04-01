using System;
using System.Globalization;
using Model;
using Model.Converters;
using NUnit.Framework;

namespace ModelTest
{
    [TestFixture]
    public class FieldTests
    {
        private static object[] FieldTypes =
        {
            new object[] {new Field<int>("id", new IntConverter()), typeof(int)},
            new object[] {new Field<double>("id", new DoubleConverter()), typeof(double)},
            new object[] {new Field<bool>("id", new BoolConverter()), typeof(bool)}
        };

        [TestCaseSource(nameof(FieldTypes))]
        public void Field_TypeIs_ConstructedType(IField field, Type expectedType)
        {
            Assert.AreEqual(expectedType, field.FieldType);
        }

        [Test]
        public void Field_Constructor_FieldHasId()
        {
            const string expectedFieldId = "id";
            var field = new Field<int, IntConverter>(expectedFieldId);
            Assert.AreEqual(expectedFieldId, field.Id);
        }

        [Test]
        public void Field_Constructor_WithConverter_FieldHasId()
        {
            const string expectedFieldId = "id";
            var field = new Field<int>(expectedFieldId, new IntConverter());
            Assert.AreEqual(expectedFieldId, field.Id);
        }

        [Test]
        public void Field_Constructor_WithConverter_HasNoValue()
        {
            var field = new Field<int>("id", new IntConverter());
            Assert.IsFalse(field.HasValue);
        }

        [Test]
        public void Field_Constructor_WithConverter_HasEmptyRawValue()
        {
            var field = new Field<int>("id", new IntConverter());
            Assert.AreEqual(string.Empty, field.RawValue);
        }

        [Test]
        public void Field_Constructor_WithConverter_SetValue_HasSameValue()
        {
            var field = new Field<int>("id", 10, new IntConverter());
            Assert.AreEqual(10, field.Value);
        }

        [Test]
        public void Field_Constructor_WithConverter_SetValue_HasValue()
        {
            var field = new Field<int>("id", 10, new IntConverter());
            Assert.IsTrue(field.HasValue);
        }

        [Test]
        public void Field_Constructor_WithConverter_SetValue_HasRawValue()
        {
            var field = new Field<int>("id", 10, new IntConverter());
            Assert.AreEqual("10", field.RawValue);
        }

        [Test]
        public void Field_Constructor_WithConverter_SetRawValue_HasSameValue()
        {
            var field = new Field<int>("id", "10", new IntConverter());
            Assert.AreEqual(10, field.Value);
        }

        [Test]
        public void Field_Constructor_WithConverter_SetRawValue_HasValue()
        {
            var field = new Field<int>("id", "10", new IntConverter());
            Assert.IsTrue(field.HasValue);
        }

        [Test]
        public void Field_Constructor_WithConverter_SetRawValue_HasRawValue()
        {
            var field = new Field<int>("id", "10", new IntConverter());
            Assert.AreEqual("10", field.RawValue);
        }

        [Test]
        public void Field_Constructor_WithConverterType_SetValue_HasSameValue()
        {
            var field = new Field<int, IntConverter>("id", 10);
            Assert.AreEqual(10, field.Value);
        }

        [Test]
        public void Field_Constructor_WithConverterType_SetValue_HasValue()
        {
            var field = new Field<int, IntConverter>("id", 10);
            Assert.IsTrue(field.HasValue);
        }

        [Test]
        public void Field_Constructor_WithConverterType_SetValue_HasRawValue()
        {
            var field = new Field<int, IntConverter>("id", 10);
            Assert.AreEqual("10", field.RawValue);
        }

        [Test]
        public void Field_Constructor_WithConverterType_SetRawValue_HasSameValue()
        {
            var field = new Field<int, IntConverter>("id", "10");
            Assert.AreEqual(10, field.Value);
        }

        [Test]
        public void Field_Constructor_WithConverterType_SetRawValue_HasValue()
        {
            var field = new Field<int, IntConverter>("id", "10");
            Assert.IsTrue(field.HasValue);
        }

        [Test]
        public void Field_Constructor_WithConverterType_SetRawValue_HasRawValue()
        {
            var field = new Field<int, IntConverter>("id", "10");
            Assert.AreEqual("10", field.RawValue);
        }

        [Test]
        public void Field_SetValue_HasNewValue()
        {
            const int expectedValue = 99;
            var field = new Field<int, IntConverter>("id", 10) {Value = expectedValue};
            Assert.AreEqual(expectedValue, field.Value);
        }

        [Test]
        public void Field_SetValue_HasNewRawValue()
        {
            const string expectedRawValue = "99";
            var field = new Field<int, IntConverter>("id", 10) {Value = 99};
            Assert.AreEqual(expectedRawValue, field.RawValue);
        }

        [Test]
        public void Field_SetRawValue_HasNewValue()
        {
            const int expectedValue = 99;
            var field = new Field<int, IntConverter>("id", 10) {RawValue = "99"};
            Assert.AreEqual(expectedValue, field.Value);
        }

        [Test]
        public void Field_SetRawValue_HasNewRawValue()
        {
            const string expectedRawValue = "99";
            var field = new Field<int, IntConverter>("id", 10) {RawValue = expectedRawValue};
            Assert.AreEqual(expectedRawValue, field.RawValue);
        }

        [Test]
        public void Field_SetInvalidRawValue_DoubleString_HasNoValue()
        {
            var field = new Field<int, IntConverter>("id", "10.0");
            Assert.IsFalse(field.HasValue);
        }

        [Test]
        public void Field_SetInvalidRawValue_DoubleString_HasRawValue()
        {
            const string expectedRawValue = "10.0";
            var field = new Field<int, IntConverter>("id", expectedRawValue);
            Assert.AreEqual(expectedRawValue, field.RawValue);
        }

        [Test]
        public void Field_SetInvalidRawValue_AlphaString_HasNoValue()
        {
            var field = new Field<int, IntConverter>("id", "a");
            Assert.IsFalse(field.HasValue);
        }

        [Test]
        public void Field_SetInvalidRawValue_AlphaString_HasRawValue()
        {
            const string expectedRawValue = "a";
            var field = new Field<int, IntConverter>("id", expectedRawValue);
            Assert.AreEqual(expectedRawValue, field.RawValue);
        }

        [Test]
        public void Field_SetValue_InvariantCulture_HasSameValue()
        {
            const double expectedValue = 1.2;
            var field = new Field<double>("id", "1.2", new DoubleConverter());
            Assert.AreEqual(expectedValue, field.Value);
        }

        [Test]
        public void Field_SetValue_CommaDecimalPoint_InvariantCulture_HasNoValue()
        {
            var field = new Field<double>("id", "1,2", new DoubleConverter());
            Assert.IsFalse(field.HasValue);
        }

        [Test]
        public void Field_SetValue_CommaDecimalPoint_CommaCulture_HasSameValue()
        {
            const double expectedValue = 1.2;
            var field = new Field<double>("id", "1,2", new DoubleConverter(new CultureInfo("DE")));
            Assert.AreEqual(expectedValue, field.Value);
        }

        [Test]
        public void Field_SetValue_DotDecimalPoint_CommaCulture_HasNoValue()
        {
            var field = new Field<double>("id", "1.2", new DoubleConverter(new CultureInfo("DE")));
            Assert.IsFalse(field.HasValue);
        }

        [Test]
        public void Field_SetValue_InvariantCulture_HasDecimalPoint_RawValue()
        {
            const string expectedRawValue = "1.2";
            var field = new Field<double>("id", expectedRawValue, new DoubleConverter());
            Assert.AreEqual(expectedRawValue, field.RawValue);
        }

        [Test]
        public void Field_SetValue_CommaDecimalPoint_CommaCulture_HasCommaDecimalPoint_RawValue()
        {
            const string expectedRawValue = "1,2";
            var field = new Field<double>("id", expectedRawValue, new DoubleConverter(new CultureInfo("DE")));
            Assert.AreEqual(expectedRawValue, field.RawValue);
        }

        [Test]
        public void Field_PropertyChanged_SetValue_PropertyChanged_Three_Fired()
        {
            const int expectedPropertyChangedFiredCount = 3;
            var converter = new IntConverter();
            var field = new Field<int>("id", converter);

            var propertyChangedFiredCount = 0;
            field.PropertyChanged += (sender, args) =>
            {
                Console.WriteLine(args.PropertyName);
                propertyChangedFiredCount++;
            };

            field.Value = 4;

            Assert.AreEqual(expectedPropertyChangedFiredCount, propertyChangedFiredCount);
        }

        [Test]
        public void Field_PropertyChanged_SetValue_PropertyChanged_Value_Fired()
        {
            const int expectedValue = 4;
            var converter = new IntConverter();
            var field = new Field<int>("id", converter);

            var propertyChangedFired = false;
            field.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(Field<int>.Value))
                {
                    return;
                }

                Assert.AreEqual(expectedValue, ((Field<int>) sender).Value);
                propertyChangedFired = true;
            };

            field.Value = 4;

            Assert.IsTrue(propertyChangedFired);
        }

        [Test]
        public void Field_PropertyChanged_SetValue_PropertyChanged_HasValue_Fired()
        {
            var converter = new IntConverter();
            var field = new Field<int>("id", converter);

            var propertyChangedFired = false;
            field.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(Field<int>.HasValue))
                {
                    return;
                }

                Assert.IsTrue(((Field<int>) sender).HasValue);
                propertyChangedFired = true;
            };

            field.Value = 4;

            Assert.IsTrue(propertyChangedFired);
        }

        [Test]
        public void Field_PropertyChanged_SetValue_PropertyChanged_RawValue_Fired()
        {
            const string expectedValue = "4";
            var converter = new IntConverter();
            var field = new Field<int>("id", converter);

            var propertyChangedFired = false;
            field.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(Field<int>.RawValue))
                {
                    return;
                }

                Assert.AreEqual(expectedValue, ((Field<int>) sender).RawValue);
                propertyChangedFired = true;
            };

            field.Value = 4;

            Assert.IsTrue(propertyChangedFired);
        }

        [Test]
        public void Field_PropertyChanged_SetRawValue_PropertyChanged_Three_Fired()
        {
            const int expectedPropertyChangedFiredCount = 3;
            var converter = new IntConverter();
            var field = new Field<int>("id", converter);

            var propertyChangedFiredCount = 0;
            field.PropertyChanged += (sender, args) =>
            {
                Console.WriteLine(args.PropertyName);
                propertyChangedFiredCount++;
            };

            field.RawValue = "4";

            Assert.AreEqual(expectedPropertyChangedFiredCount, propertyChangedFiredCount);
        }

        [Test]
        public void Field_PropertyChanged_SetRawValue_PropertyChanged_Value_Fired()
        {
            const int expectedValue = 4;
            var converter = new IntConverter();
            var field = new Field<int>("id", converter);

            var propertyChangedFired = false;
            field.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(Field<int>.Value))
                {
                    return;
                }

                Assert.AreEqual(expectedValue, ((Field<int>) sender).Value);
                propertyChangedFired = true;
            };

            field.RawValue = "4";

            Assert.IsTrue(propertyChangedFired);
        }

        [Test]
        public void Field_PropertyChanged_SetRawValue_PropertyChanged_HasValue_Fired()
        {
            var converter = new IntConverter();
            var field = new Field<int>("id", converter);

            var propertyChangedFired = false;
            field.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(Field<int>.HasValue))
                {
                    return;
                }

                Assert.IsTrue(((Field<int>) sender).HasValue);
                propertyChangedFired = true;
            };

            field.RawValue = "4";

            Assert.IsTrue(propertyChangedFired);
        }

        [Test]
        public void Field_PropertyChanged_SetRawValue_PropertyChanged_RawValue_Fired()
        {
            const string expectedValue = "4";
            var converter = new IntConverter();
            var field = new Field<int>("id", converter);

            var propertyChangedFired = false;
            field.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(Field<int>.RawValue))
                {
                    return;
                }

                Assert.AreEqual(expectedValue, ((Field<int>) sender).RawValue);
                propertyChangedFired = true;
            };

            field.RawValue = "4";

            Assert.IsTrue(propertyChangedFired);
        }
    }
}