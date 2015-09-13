using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Position : Entity
    {
        public virtual string OwnerIdentity { get; set; }

        [Required]
        public virtual string Title { get; set; }

        [Required]
        public virtual string Description { get; set; }

        [Required]
        public virtual string Company { get; set; }

        [DisplayName("Start Date")]
        [Required]
        public virtual DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public virtual DateTime? EndDate { get; set; }

        public virtual ISet<Responsibility> Responsibilities { get; set; }

        public virtual IList<Tag> Tags { get; set; }

        public Position()
        {
            Responsibilities = new HashSet<Responsibility>();
            Tags = new List<Tag>();
        }
    }
}