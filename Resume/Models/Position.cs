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

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual string Company { get; set; }

        [DisplayName("Start Date")]
        public virtual DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public virtual DateTime? EndDate { get; set; }

        public virtual IList<Responsibility> Responsibilities { get; set; }

        public virtual IList<Tag> Tags { get; set; }

        public Position()
        {
            Responsibilities = new List<Responsibility>();
            Tags = new List<Tag>();
        }
    }
}