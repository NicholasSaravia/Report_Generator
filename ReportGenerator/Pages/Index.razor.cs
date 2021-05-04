using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using ReportGenerator.Models;

namespace ReportGenerator.Pages
{
    public partial class Index
    {
        List<ClassRoom> classRooms = new List<ClassRoom>();
        private async void ParseData(InputFileChangeEventArgs e)
        {
            // add readonly files to a list
            foreach (var file in e.GetMultipleFiles())
            {
                classRooms.Add(new ClassRoom()
                {
                    Name = Path.GetFileNameWithoutExtension(file.Name),
                });
            }



        }

        private void ParseDataFromFile()
        {

        }
    }
}
