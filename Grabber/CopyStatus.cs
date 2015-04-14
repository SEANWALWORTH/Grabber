using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabber
{
    class CopyStatus
    {
        public int ImageCurrent { get; set; }
        public int VideoCurrent { get; set; }
        public int ImageTotal { get; set; }
        public int VideoTotal { get; set; }
        public string Filename { get; set; }
        public bool IsImage { get; set; }
    }
}