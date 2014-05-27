using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class UserFile
    {
        public int UserFileId { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public string LocalFileName { get; set; }

        public Guid FileGuid { get; set; }
    }
}