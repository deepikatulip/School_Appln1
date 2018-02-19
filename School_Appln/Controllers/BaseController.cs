using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Drawing;
using System.IO;
using School_AppIn_Model;
//using School_AppIn_Model.DataAccessLayer;
using School_Appln;

namespace School_AppIn.Controllers
{
    public class BaseController : Controller
    {

        private School_AppIn_Model.DataAccessLayer.StudentDbContext db = new School_AppIn_Model.DataAccessLayer.StudentDbContext();



        public readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BaseController()
            : base()
        {
        }


        public UserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public BaseController(UserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        public User LoggedInUser
        {

            get
            {

                return UserManager.FindByName(User.Identity.Name);

            }

        }



        protected override void OnException(ExceptionContext filterContext)
        {

            Exception exception = filterContext.Exception;
            //filterContext.ExceptionHandled = true;

            //Logging the Exception
            logger.Error(string.Format("{0}.{1}", filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"].ToString()), exception);

            var Result = this.View("Error", new HandleErrorInfo(exception,
                filterContext.RouteData.Values["controller"].ToString(),
                filterContext.RouteData.Values["action"].ToString()));

            filterContext.Result = Result;

            base.OnException(filterContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }


        public byte[] ResizeImageToStandard(byte[] image)
        {

            return ResizeImageTo(image, 200, 206);


        }

        public byte[] ResizeImageTo(byte[] image, int width, int height)
        {

            if (image != null)
            {

                MemoryStream msi = new MemoryStream(image);
                Image inImage = Image.FromStream(msi);
                Image returnImage = inImage.GetThumbnailImage(width, height, null, IntPtr.Zero);

                MemoryStream mso = new MemoryStream();
                returnImage.Save(mso, System.Drawing.Imaging.ImageFormat.Png);
                return mso.ToArray();


            }
            else
            {
                return image;
            }
        }


        private UserManager _userManager;
        private ApplicationSignInManager _signInManager;



    }
}