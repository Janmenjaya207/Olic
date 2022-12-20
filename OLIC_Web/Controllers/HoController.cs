using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLIC_Web.Controllers
{
    public class HoController : Controller
    {
        // GET: Ho
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dashboard()
        {
            return View();
        }
        public ActionResult ApplicationList()
        {
            return View();
        }
        
    }
}