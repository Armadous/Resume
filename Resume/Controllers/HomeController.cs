using NHibernate;
using NHibernate.Linq;
using Resume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resume.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession db;
        public HomeController(ISession session)
        {
            db = session;
        }   

        public ActionResult Index()
        {
            ViewBag.User = "DemoUser";
            return View();
        }

        public ActionResult Dashboard(string user)
        {
            ViewBag.User = user;
            var profile = db.Query<Profile>().SingleOrDefault(p => p.OwnerIdentity == User.Identity.Name) ?? new Profile();
            return View(profile);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}