using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Experience : Entity
    {
        [Required]
        public virtual int Value { get; set; }

        [Required]
        public virtual int ResponsibilityId { get; set; }

        public virtual Responsibility Responsibility { get; set; }

        [Required]
        public virtual int SkillId { get; set; }

        public virtual Skill Skill { get; set; }

        public virtual IList<Tag> Tags { get; set; }

        public Experience()
        {
            Tags = new List<Tag>();
        }
    }
}