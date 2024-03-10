using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class HelloWorldController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Hello World";
            return View();
        }
    }
}