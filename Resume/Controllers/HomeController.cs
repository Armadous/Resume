using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resume.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Headers()
        {
            var headers = "";
            foreach(var header in Request.Headers.Keys)
            {
                headers += string.Format("{0}:{1},", header, Request.Headers[header.ToString()]);
            }

            return Content(headers);
        }

        public ActionResult Index()
        {
            return View();
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