using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Position
    {
        public int PositionId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Company { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual ICollection<Responsibility> Responsibilities { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public Position()
        {
            Responsibilities = new HashSet<Responsibility>();
        }
    }
}