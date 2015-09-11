using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
    }
}