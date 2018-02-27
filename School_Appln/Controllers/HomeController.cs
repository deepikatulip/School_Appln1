using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School_AppIn_Model;
using School_AppIn_Model.DataAccessLayer;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace School_Appln.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {

            if (Request.IsAuthenticated)
            {
                StudentDbContext db = new StudentDbContext();
                var user = Request.GetOwinContext().GetUserManager<UserManager>().FindByName(User.Identity.Name);
                return RedirectToAction("RedirectToLocal", "Account", new { returnUrl = "", userId = user.Id });
            }
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