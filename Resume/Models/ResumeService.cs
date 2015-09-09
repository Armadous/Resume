using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class ResumeService
    {
        private readonly ISession session;
        public ResumeService(ISession session)
        {
            this.session = session;
        }
    }
}