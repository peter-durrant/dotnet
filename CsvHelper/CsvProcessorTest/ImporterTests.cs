using System.IO;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Hdd.CsvProcessor;
using NUnit.Framework;

namespace CsvProcessorTest
{
    [TestFixture]
    public class ImporterTests
    {
        private class TestDataStructure
        {
            public string Name { get; set; }
            public double Value { get; set; }
        }

        private sealed class TestDataStructureClassMap : ClassMap<TestDataStructure>
        {
            public TestDataStructureClassMap()
            {
                Map(m => m.Name);
                Map(m => m.Value).TypeConverter<CurrencyToDoubleConverter>();
            }
        }

        [Test]
        public void ImportCsv_NoConversionRequired_ImportSuccess()
        {
            // arrange
            var testFile = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\TestData.csv");
            var uut = new Importer();

            // act
            var data = uut.ImportCsv<TestDataStructure>(testFile).ToArray();

            // assert
            Assert.AreEqual(4, data.Length);

            Assert.AreEqual("Alice", data[0].Name);
            Assert.AreEqual(100.0, data[0].Value);

            Assert.AreEqual("Bob", data[1].Name);
            Assert.AreEqual(20.0, data[1].Value);

            Assert.AreEqual("Charlotte", data[2].Name);
            Assert.AreEqual(45.0, data[2].Value);

            Assert.AreEqual("Dean", data[3].Name);
            Assert.AreEqual(89.7, data[3].Value);
        }

        [Test]
        public void ImportCsv_ConversionRequired_ConverterNotSpecified_ImportFailure()
        {
            // arrange
            var testFile = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\TestDataCurrency.csv");
            var uut = new Importer();

            // act
            Assert.Throws<TypeConverterException>(() => uut.ImportCsv<TestDataStructure>(testFile));
        }

        [Test]
        public void ImportCsv_ConversionRequired_ConverterSpecified_ImportSuccess()
        {
            // arrange
            var uut = new Importer();

            // act
            var testFile = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\TestDataCurrency.csv");
            var data = uut.ImportCsv<TestDataStructure, TestDataStructureClassMap>(testFile).ToArray();

            // assert
            Assert.AreEqual(4, data.Length);

            Assert.AreEqual("Alice", data[0].Name);
            Assert.AreEqual(100.0, data[0].Value);

            Assert.AreEqual("Bob", data[1].Name);
            Assert.AreEqual(20.0, data[1].Value);

            Assert.AreEqual("Charlotte", data[2].Name);
            Assert.AreEqual(45.0, data[2].Value);

            Assert.AreEqual("Dean", data[3].Name);
            Assert.AreEqual(89.7, data[3].Value);
        }
    }
}