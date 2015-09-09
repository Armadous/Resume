using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Tag : Entity
    {
        public virtual string Name { get; set; }
    }
}