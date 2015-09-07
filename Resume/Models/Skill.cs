using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Skill
    {
        public int SkillId { get; set; }

        public string OwnerIdentity { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Level { get; set; }

        //16x16
        [ForeignKey("SmallIconId")]
        [JsonIgnore]
        public virtual UserFile SmallIcon { get; set; }

        public int? SmallIconId { get; set; }

        //64x64
        [ForeignKey("MediumIconId")]
        [JsonIgnore]
        public virtual UserFile MediumIcon { get; set; }

        public int? MediumIconId { get; set; }

        //256x256
        [ForeignKey("LargeIconId")]
        [JsonIgnore]
        public virtual UserFile LargeIcon { get; set; }

        public int? LargeIconId { get; set; }

        public bool HasExperience { 
            get 
            {
                using (var db = new ResumeDb())
                {
                    return db.Experiences.Any(n => n.SkillId == SkillId);
                }
            } 
            private set {} }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}