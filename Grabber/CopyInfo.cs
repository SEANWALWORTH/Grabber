using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabber
{
    class CopyInfo
    {
        public string TargetPath { get; set; }
        public List<FileInfo> FilesImage { get; set; }
        public List<FileInfo> FilesVideo { get; set; }
    }
}
