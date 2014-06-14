using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Experience
    {
        public int ExperienceId { get; set; }

        public int Value { get; set; }

        public int ResponsibilityId { get; set; }

        public virtual Responsibility Responsibility { get; set; }

        public int SkillId { get; set; }

        public virtual Skill Skill { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public Experience()
        {
            Tags = new HashSet<Tag>();
        }
    }
}