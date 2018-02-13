using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School_AppIn_Model;
using School_AppIn_Model.DataAccessLayer;

namespace School_Appln.Controllers
{
    public class HomeController : Controller
    {
        StudentDbContext db = new StudentDbContext();
       
        public ActionResult Index()
        {
            var List = db.Classes.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}