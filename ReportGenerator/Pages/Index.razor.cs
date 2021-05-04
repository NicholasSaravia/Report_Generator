using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace ReportGenerator.Pages
{
    public partial class Index
    {
        IList<IBrowserFile> files = new List<IBrowserFile>();
        private void UploadFiles(InputFileChangeEventArgs e)
        {
            // add readonly files to a list
            foreach (var file in e.GetMultipleFiles())
            {
                files.Add(file);
            }

            // parse data

        }

        private void ParseData()
        {

        }
    }
}
