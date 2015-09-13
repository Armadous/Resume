using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class ImportLinkedInViewModel
    {
        public ImportLinkedInViewModel() 
        {
            Positions = new List<Position>();
        }

        public IList<Position> Positions;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Summary { get; set; }
    }
}