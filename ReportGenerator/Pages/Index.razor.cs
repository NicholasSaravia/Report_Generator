using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Components.Forms;
using ReportGenerator.Helpers;
using ReportGenerator.Models;

namespace ReportGenerator.Pages
{
    public partial class Index
    {
        protected List<ClassRoom> classRooms = new List<ClassRoom>();
        private async void ParseData(InputFileChangeEventArgs e)
        {
            // add readonly files to a list
            foreach (var file in e.GetMultipleFiles())
            {
                
                var scores = await CsvHelper.ReadCsv<Score>(new ScoreMapping(), file);
                classRooms.Add(new ClassRoom()
                {
                    Name = Path.GetFileNameWithoutExtension(file.Name),
                    Scores = scores
                });
            }

            this.StateHasChanged();
        }
    }
}
