using System.Collections.Generic;
using CsvHelper.Configuration;

namespace Hdd.CsvProcessor
{
    public interface IImporter
    {
        IEnumerable<T> ImportCsv<T>(string filename);
        IEnumerable<T> ImportCsv<T, TClassMap>(string filename) where TClassMap : ClassMap<T>;
    }

}