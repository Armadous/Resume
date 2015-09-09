using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Position
    {
        public int PositionId { get; set; }

        public string OwnerIdentity { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Company { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Responsibility> Responsibilities { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public Position()
        {
            Responsibilities = new HashSet<Responsibility>();
        }
    }
}