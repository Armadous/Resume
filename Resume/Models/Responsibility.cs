using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Responsibility : Entity
    {
        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Description { get; set; }

        [Required]
        public virtual double Percentage { get; set; }

        public virtual Position Position { get; set; }

        public virtual int PositionId { get; set; }

        public virtual ISet<Experience> Experiences { get; set; }

        public virtual IList<Tag> Tags { get; set; }

        public Responsibility()
        {
            Tags = new List<Tag>();
            Experiences = new HashSet<Experience>(); 
        }
    }
}