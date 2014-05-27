using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        //64x64
        public UserFile SmallIcon { get; set; }

        //128x128
        public UserFile MediumIcon { get; set; }

        //256x256
        public UserFile LargeIcon { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}