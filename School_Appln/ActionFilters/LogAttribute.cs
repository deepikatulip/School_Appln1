using System.Web.Mvc;
using PrimeRealty.Models;



namespace PrimeRealty.ActionFilters
{
    public class LogAttribute : ActionFilterAttribute
    {
        private PrimeRealtyEntities db = new PrimeRealtyEntities();

 
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
           
            var request = filterContext.HttpContext.Request;
            base.OnActionExecuted(filterContext);

        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        


    }
}
