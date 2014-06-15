using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Responsibility
    {
        public int ResponsibilityId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Percentage { get; set; }

        public virtual Position Position { get; set; }

        public virtual int PositionId { get; set; }

        public virtual ICollection<Experience> Experiences { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public Responsibility()
        {
            Tags = new HashSet<Tag>();
        }
    }
}