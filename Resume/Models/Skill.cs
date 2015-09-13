using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Skill : Entity
    {
        public virtual string OwnerIdentity { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Description { get; set; }

        [Required]
        public virtual float Level { get; set; }

        //16x16
        [ForeignKey("SmallIconId")]
        [JsonIgnore]
        public virtual UserFile SmallIcon { get; set; }

        public virtual int? SmallIconId { get; set; }

        //64x64
        [ForeignKey("MediumIconId")]
        [JsonIgnore]
        public virtual UserFile MediumIcon { get; set; }

        public virtual int? MediumIconId { get; set; }

        //256x256
        [ForeignKey("LargeIconId")]
        [JsonIgnore]
        public virtual UserFile LargeIcon { get; set; }

        public virtual int? LargeIconId { get; set; }

        public virtual bool HasExperience { 
            get 
            {
                return Experiences.Any();
            } 
            protected set {} }

        public virtual IList<Tag> Tags { get; set; }

        public virtual IList<Experience> Experiences { get; set; }

        public Skill()
        {
            Tags = new List<Tag>();
            Experiences = new List<Experience>();
        }
    }
}