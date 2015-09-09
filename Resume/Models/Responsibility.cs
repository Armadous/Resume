using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Responsibility : Entity
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual double Percentage { get; set; }

        public virtual Position Position { get; set; }

        public virtual int PositionId { get; set; }

        public virtual IList<Experience> Experiences { get; set; }

        public virtual IList<Tag> Tags { get; set; }

        public Responsibility()
        {
            Tags = new List<Tag>();
            Experiences = new List<Experience>(); 
        }
    }
}