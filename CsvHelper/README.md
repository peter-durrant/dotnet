# CsvHelper

[CsvHelper](http://joshclose.github.io/CsvHelper/) is a library for reading and writing CSV files that implements [RFC 4180](https://tools.ietf.org/html/rfc4180).

This documentation is concerned with reading CSV data only.

## Wrapper

I demonstrate a basic wrapper around the **CsvHelper** library that will read all records from a CSV file matching a specified class definition. The wrapper in [Importer](./CsvProcessor/Importer.cs)
defines two methods to import the data.

The first method attempts a direct mapping from the CSV columns to the properties in the type `T`:

```c#
public IEnumerable<T> ImportCsv<T>(string filename)
```

The second method permits a custom mapping to be defined which can then include type conversion:

```c#
public IEnumerable<T> ImportCsv<T, TClassMap>(string filename) where TClassMap : ClassMap<T>
```

## Field Mapping

The CSV test files describe the column names and input records.

The test files [TestData.csv](./CsvpPocessorTest/Data/TestData.csv) and [TestDataCurrency.csv](./CsvpPocessorTest/Data/TestDataCurrency.csv) describe data that has columns called
* Name
* Value

The two files contain essentially the same data - a `Name` and a `Value`, where the `Value` in [TestData.csv](./CsvpPocessorTest/Data/TestData.csv) is represented as a double value, and the `Value` in [TestDataCurrency.csv](./CsvpPocessorTest/Data/TestDataCurrency.csv) as a currency (`£` character + `double` value).

### Automatic Mapping

Values are automatically converted into their respective types. In the unit tests in [ImporterTests](./CsvProcessorTest/ImporterTests.cs) the class `TestDataStructure` is the target data structure which matches the
column names in the source CSV file, so that automatic mapping can be used.

```c#
private class TestDataStructure
{
    public string Name { get; set; }
    public double Value { get; set; }
}
```

The test `ImportCsv_NoConversionRequired_ImportSuccess` demonstrates automatic mapping where no additional configuration is required except the specification of `TestDataStructure` as the target data structure.

### Field Mapping

The example field mapping in [ImporterTests](./CsvProcessorTest/ImporterTests.cs) is defined in the `TestDataStructureClassMap` class.

```c#
private sealed class TestDataStructureClassMap : ClassMap<TestDataStructure>
{
    public TestDataStructureClassMap()
    {
        Map(m => m.Name);
        Map(m => m.Value).TypeConverter<CurrencyToDoubleConverter>();
    }
}
```

If a mapping is defined, then all fields must be mapped. In `TestDataStructureClassMap` the mapping is defined so that Type Conversion can be done. But mapping is also useful if the CSV column names and target
object property names are not equivalent (e.g. `Map(m => m.Name).Name("LongName")` would map the CSV column `LongName` to the `TestDataStructure` property `Name`).

### Type Conversion

In the field mapping, a type conversion can be applied to convert the raw CSV data into the internal data type.

In the test `ImportCsv_ConversionRequired_ConverterNotSpecified_ImportFailure` in [ImporterTests](./CsvProcessorTest/ImporterTests.cs) the import fails because **CsvHelper** is trying to convert a value with a leading
`£` symbol into an internal `double` representation. This conversion cannot be done automatically, so a type conversion was applied to the `Value` field in the mapping `Map(m => m.Value).TypeConverter<CurrencyToDoubleConverter>()`.

The type converter [CurrencyToDoubleConverter](./CsvProcessor/CurrencyToDoubleConverter.cs) is designed to convert from an input `string` that permits a currency character into a `double` value. The parsing is done in the
`ConvertFromString` method. Since this code example is concerned with reading only, the corresponding `ConvertToString` for writing to a CSV file is not implemented.
