using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resume.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is me.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "So you wana talk ay?";

            return View();
        }
    }
}