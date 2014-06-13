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
        [ForeignKey("SmallIconId")]
        public virtual UserFile SmallIcon { get; set; }

        public int? SmallIconId { get; set; }

        //128x128
        [ForeignKey("MediumIconId")]
        public virtual UserFile MediumIcon { get; set; }

        [ForeignKey("MediumIcon")]
        public int? MediumIconId { get; set; }

        //256x256
        [ForeignKey("LargeIconId")]
        public virtual UserFile LargeIcon { get; set; }

        public int? LargeIconId { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}