using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hdd.CsvProcessor
{
    public class Importer : IImporter
    {
        public IEnumerable<T> ImportCsv<T>(string filename)
        {
            using (var textReader = File.OpenText(filename))
            {
                using (var csv = new CsvReader(textReader))
                {
                    return csv.GetRecords<T>().ToArray();
                }
            }
        }

        public IEnumerable<T> ImportCsv<T, TClassMap>(string filename) where TClassMap : ClassMap<T>
        {
            using (var textReader = File.OpenText(filename))
            {
                using (var csv = new CsvReader(textReader))
                {
                    csv.Configuration.RegisterClassMap<TClassMap>();
                    return csv.GetRecords<T>().ToArray();
                }
            }
        }
    }
}