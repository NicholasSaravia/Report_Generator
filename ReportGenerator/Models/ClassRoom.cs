using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportGenerator.Models
{
    public class ClassRoom
    {
        public string Name { get; set; }
        public List<Score> Scores { get; set; }

        public double ClassAverage =>
            Math.Truncate(Scores
                .Where(x => x.GradeUsable)
                .Select(x => x.GradeRounded)
                .Average() * 100) / 100;

        public int StudentsWithinClass => Scores.Count();
        public int StudentsUsedToCalculateReport => Scores?.Where(x => x.GradeUsable).Count() ?? 0;
        public string[] NameOfStudentsNotUsedOnTest => Scores.Where(x => !x.GradeUsable).Select(y => y.StudentName).ToArray();

    }
}
