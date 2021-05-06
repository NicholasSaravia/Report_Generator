using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorDownloadFile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using ReportGenerator.Helpers;
using ReportGenerator.Models;

namespace ReportGenerator.Pages
{
    public partial class Index : ComponentBase
    {
        public List<ClassRoom> ClassRooms = new List<ClassRoom>();
        public ClassRoom HighestPerformingCLass => ClassRooms.OrderByDescending(x => x.ClassAverage).ToList()[0];
        [Inject] IBlazorDownloadFileService BlazorDownloadFileService { get; set; }
        public List<Score> ClassScores = new List<Score>();


        private async void ParseData(InputFileChangeEventArgs e)
        {
            // add readonly files to a list
            foreach (var file in e.GetMultipleFiles())
            {
                var scores = await CsvHelper.ReadCsv<Score>(new ScoreMapping(), file);
                ClassRooms.Add(new ClassRoom()
                {
                    Name = Path.GetFileNameWithoutExtension(file.Name),
                    Scores = scores
                });
            }

            this.StateHasChanged();
        }

        private async void SaveToTextFile()
        {
            await using var memoryStream = new MemoryStream();
            await using StreamWriter sw = new StreamWriter(memoryStream);

            // add winner
            await sw.WriteLineAsync($"{HighestPerformingCLass.Name} was the highest performing class!!");
            await sw.WriteLineAsync("");

            // add averages
            await sw.WriteLineAsync($"------- Class Averages -------");
            await sw.WriteLineAsync($"");

            foreach (var classRoom in ClassRooms.OrderByDescending(x => x.ClassAverage))
            {
                await sw.WriteLineAsync($"{classRoom.Name} scored an average of {classRoom.ClassAverage}");
                await sw.WriteLineAsync(
                    $"A. {classRoom.StudentsUsedToCalculateReport}/{classRoom.StudentsWithinClass} students where used to calculate this report");

                if (classRoom.NameOfStudentsNotUsedOnTest.Any())
                    await sw.WriteLineAsync("B. Students discarded from consideration");

                for (int i = 0; i < classRoom.NameOfStudentsNotUsedOnTest.Length; i++)
                {
                    await sw.WriteLineAsync($"    {i + 1}. {classRoom.NameOfStudentsNotUsedOnTest[i]}");
                }

                await sw.WriteLineAsync("");
            }

            await sw.FlushAsync();

            await BlazorDownloadFileService.DownloadFile("report.txt",
                memoryStream.ToArray(),
                contentType: "text/plain");

        }
        
        private string IsWinner(double average)
        {
            return average == HighestPerformingCLass.ClassAverage ? "report-layout__winner-card " : "thick-border-inverse";
        }

        private void ShowClassScores(ClassRoom classRoom)
        {
            ClassScores = classRoom.Scores.OrderBy(x => x.Grade).ToList();
            this.StateHasChanged();
            Js.InvokeVoidAsync("ScrollToScores");
        }
    }
}
