using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace ReportGenerator.Helpers
{
    public class CsvHelper
    {
        public static async Task<List<T>> ReadCsv<T>(ICsvMapping<T> mapping, IBrowserFile file)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            CsvParser<T> csvParser = new CsvParser<T>(csvParserOptions, mapping);

            using var reader = new StreamReader(file.OpenReadStream());
            var line = await reader.ReadToEndAsync();

            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new string[]{"\r","\n"});
            var results = csvParser.ReadFromString(csvReaderOptions, line);
            return results.Select(x => x.Result).ToList();
        }
    }
}
