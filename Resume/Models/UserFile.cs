using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class UserFile : Entity
    {
        public virtual string FileName { get; set; }

        public virtual string ContentType { get; set; }

        public virtual string LocalFileName { get; set; }

        public virtual Guid FileGuid { get; set; }
    }
}