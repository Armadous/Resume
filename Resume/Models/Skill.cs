using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Skill
    {
        public int SkillId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Level { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}