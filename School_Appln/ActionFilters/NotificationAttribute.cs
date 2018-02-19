using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using PrimeRealty.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace PrimeRealty.ActionFilters
{
    public class NotificationAttribute : ActionFilterAttribute
    {

        private PrimeRealtyEntities db = new PrimeRealtyEntities();

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                return;
            }

            try
            {

                var request = filterContext.HttpContext.Request;

                if (filterContext.RouteData.GetRequiredString("action") == "Create" && filterContext.RouteData.GetRequiredString("controller") == "Complaints")
                {

                    if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                    {


                        if (filterContext.HttpContext.User.IsInRole(Models.Common.Constants.ROLE_OWNER) || filterContext.HttpContext.User.IsInRole(Models.Common.Constants.ROLE_TENANT))
                        {

                            var createdComplaintId = (filterContext.Controller.ViewBag.CreatedComplaintId != null) ? filterContext.Controller.ViewBag.CreatedComplaintId : 0;
                            decimal complaintResidentId = (filterContext.Controller.ViewBag.ComplaintResidentId != null) ? filterContext.Controller.ViewBag.ComplaintResidentId : 0;

                            if (createdComplaintId != 0)
                            {
                                
                                var assocAdmins = db.Residents.Where(r => r.ResidentId == complaintResidentId).SingleOrDefault().Property.Association.Users;


                                foreach (var am in assocAdmins)
                                {
                                    Guid uKey = Utils.Utility.GenerateUniqueKey();
                                    Notification notification = new Notification
                                    {
                                        Timestamp = DateTime.Now,
                                        UserId = am.Id,
                                        UniqueKey = uKey,
                                        ObjectCategory = "Complaint",
                                        Action = "Create",
                                        ObjectId = createdComplaintId,
                                        Title = string.Format("{0}{1}", filterContext.HttpContext.User.Identity.Name, " has made a complaint"),
                                        NotificationMessage = "A new complaint has been made",
                                        Link = string.Format("/Core/Complaints/Details?id={0}&uniquekey={1}", createdComplaintId, uKey),
                                        IsRead = false,

                                    };

                                    db.Notifications.Add(notification);
                                }

                                db.SaveChanges();
                            }

                        }
                    }

                }

                if (filterContext.RouteData.GetRequiredString("action") == "Create" && filterContext.RouteData.GetRequiredString("controller") == "InvoiceMaintenances")
                {

                    if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                    {
                        var createdInvoiceId = (filterContext.Controller.ViewBag.CreatedInvoiceId != null) ? filterContext.Controller.ViewBag.CreatedInvoiceId : 0;
                        if (createdInvoiceId != 0)
                        {
                            decimal invoiceBlockAllotId = (filterContext.Controller.ViewBag.InvoiceBlockAllotId != null) ? filterContext.Controller.ViewBag.InvoiceBlockAllotId : 0;
                            var createdInvoiceNo = (filterContext.Controller.ViewBag.CreatedInvoiceNo != null) ? filterContext.Controller.ViewBag.CreatedInvoiceNo : 0;
                            var residents = db.Residents.Where(r => r.Property.BlockAllotId == invoiceBlockAllotId && (r.FromDate <= DateTime.Now && r.ToDate == null));

                            foreach (var res in residents)
                            {

                                Guid uKey = Utils.Utility.GenerateUniqueKey();
                                Notification notification = new Notification
                                {
                                    Timestamp = DateTime.Now,
                                    UserId = res.Users.First().Id,
                                    UniqueKey = uKey,
                                    ObjectCategory = "MaintenanceInvoice",
                                    Action = "Create",
                                    ObjectId = createdInvoiceId,
                                    Title = string.Format("Invoice {0} has been generated", createdInvoiceNo),
                                    NotificationMessage = "Maintenance invoice has been generated",
                                    Link = string.Format("/Ext/InvoiceMaintenances/Details?id={0}&uniquekey={1}", createdInvoiceId, uKey),
                                    IsRead = false,

                                };

                                db.Notifications.Add(notification);

                            }

                            db.SaveChanges();
                        }

                    }
                }

            }
            catch (Exception ex)
            {

            }
            base.OnActionExecuted(filterContext);

        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        



    }
}








     



