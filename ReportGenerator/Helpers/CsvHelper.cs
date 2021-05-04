using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace ReportGenerator.Helpers
{
    public class CsvHelper
    {
        public static List<T> ReadCsv<T>(ICsvMapping<T> mapping, string path)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            CsvParser<T> csvParser = new CsvParser<T>(csvParserOptions, mapping);
            var results = csvParser.ReadFromFile(path, encoding: Encoding.ASCII);

            return results.Select(x => x.Result).ToList();
        }
    }
}
