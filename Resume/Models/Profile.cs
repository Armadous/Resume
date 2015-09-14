using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class Profile : Entity
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Summary { get; set; }

        public virtual string OwnerIdentity { get; set; }

        public virtual UserFile PorfilePicture { get; set; }

        public virtual int? PorfilePictureId { get; set; }
    }
}