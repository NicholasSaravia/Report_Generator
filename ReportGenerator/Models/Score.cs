using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace ReportGenerator.Models
{
    public class Score
    {
        public string StudentName { get; set; }
        public double Grade { get; set; }

        /// <summary>
        /// Rounds down
        /// </summary>
        public double GradeRounded => Math.Round(Grade, 0, MidpointRounding.ToNegativeInfinity);
        /// <summary>
        /// Not equal to zero
        /// </summary>
        public bool GradeUsable => GradeRounded != 0;
    }

    public class CsvReportGradeMapping : CsvMapping<Score>
    {
        public CsvReportGradeMapping()
            : base()
        {
            MapProperty(0, x => x.StudentName);
            MapProperty(1, x => x.Grade);
        }
    }
}
