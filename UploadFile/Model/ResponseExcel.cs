using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFile.Model
{
    public class ResponseExcel
    {
        public string Name { get; set; }
        public MemoryStream Stream { get; set; }
        public string ContentType { get; set; }
        public string LinkFile { get; set; }


        public ResponseExcel(string name, MemoryStream stream, string contentType, string linkFile)
        {
            this.Name = name;
            this.Stream = stream;
            this.ContentType = contentType;
            this.LinkFile = linkFile;
        }
    }      
}
