using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using School_AppIn_Model.DataAccessLayer;
using School_AppIn.Controllers;

namespace School_Appln.Areas.Ext.Controllers
{
    public class Fees_ConfigurationController : BaseController
    {
        private StudentDbContext db = new StudentDbContext();

        // GET: Ext/Fees_Configuration
        public async Task<ActionResult> Index()
        {
            var fees_Config = db.Fees_Config.Include(f => f.Class).Include(f => f.InvFrequencyCategory).Include(f => f.InvoiceFrequency);
            return View(await fees_Config.ToListAsync());
        }

        // GET: Ext/Fees_Configuration/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fees_Configuration fees_Configuration = await db.Fees_Config.FindAsync(id);
            if (fees_Configuration == null)
            {
                return HttpNotFound();
            }
            return View(fees_Configuration);
        }

        // GET: Ext/Fees_Configuration/Create
        public ActionResult Create()
        {
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name");
            ViewBag.FrequencyCategoryId = new SelectList(db.InvFrequencyCategories, "FrequencyCategoryId", "FrequencyCategoryCode");
            ViewBag.InvFrequencyId = new SelectList(db.InvoiceFrequencies, "InvFrequencyId", "InvFrequencyValue");
            return View();
        }

        // POST: Ext/Fees_Configuration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FeesId,Class_Id,FrequencyCategoryId,InvFrequencyId,FeesDescription,Fees,Created_By,Created_On,Updated_On,Updated_By,Is_Active,Is_Deleted")] Fees_Configuration fees_Configuration)
        {
            var userId = LoggedInUser.Id;
            if (string.IsNullOrEmpty(Request.Form["FrequencyCategoryId"]))
            {
                goto Fail;
            }
            if (string.IsNullOrEmpty(Request.Form["InvFrequencyId"]))
            {
                goto Fail;
            }
            if (ModelState.IsValid)
            {
                int cityId = int.Parse(Request.Form["InvFrequencyId"]);
                fees_Configuration.Is_Active = true;
                fees_Configuration.Created_By = userId;
                fees_Configuration.Created_On = DateTime.Now.Date;
                db.Fees_Config.Add(fees_Configuration);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            Fail:
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name", fees_Configuration.Class_Id);
            ViewBag.FrequencyCategoryId = new SelectList(db.InvFrequencyCategories, "FrequencyCategoryId", "FrequencyCategoryCode", fees_Configuration.FrequencyCategoryId);
            ViewBag.InvFrequencyId = new SelectList(db.InvoiceFrequencies, "InvFrequencyId", "InvFrequencyValue", fees_Configuration.InvFrequencyId);
            return View(fees_Configuration);
        }

        // GET: Ext/Fees_Configuration/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fees_Configuration fees_Configuration = await db.Fees_Config.FindAsync(id);
            if (fees_Configuration == null)
            {
                return HttpNotFound();
            }
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name", fees_Configuration.Class_Id);
            ViewBag.FrequencyCategoryId = new SelectList(db.InvFrequencyCategories, "FrequencyCategoryId", "FrequencyCategoryCode", fees_Configuration.FrequencyCategoryId);
            ViewBag.InvFrequencyId = new SelectList(db.InvoiceFrequencies, "InvFrequencyId", "InvFrequencyValue", fees_Configuration.InvFrequencyId);
            return View(fees_Configuration);
        }

        // POST: Ext/Fees_Configuration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FeesId,Class_Id,Class_Name,FrequencyCategoryId,InvFrequencyId,FeesDescription,Fees,Created_By,Created_On,Updated_On,Updated_By,Is_Active,Is_Deleted")] Fees_Configuration fees_Configuration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fees_Configuration).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name", fees_Configuration.Class_Id);
            ViewBag.FrequencyCategoryId = new SelectList(db.InvFrequencyCategories, "FrequencyCategoryId", "FrequencyCategoryCode", fees_Configuration.FrequencyCategoryId);
            ViewBag.InvFrequencyId = new SelectList(db.InvoiceFrequencies, "InvFrequencyId", "InvFrequencyValue", fees_Configuration.InvFrequencyId);
            return View(fees_Configuration);
        }

        // GET: Ext/Fees_Configuration/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fees_Configuration fees_Configuration = await db.Fees_Config.FindAsync(id);
            if (fees_Configuration == null)
            {
                return HttpNotFound();
            }
            return View(fees_Configuration);
        }

        // POST: Ext/Fees_Configuration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fees_Configuration fees_Configuration = await db.Fees_Config.FindAsync(id);
            db.Fees_Config.Remove(fees_Configuration);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public JsonResult GetPeriod(int InvFrequencyId)
        {
            JsonResult result = new JsonResult();
            var periodlist = (from s in db.InvFrequencyCategories
                              where s.InvFrequencyId == InvFrequencyId
                              select s).ToList();
            var selectlist = new SelectList(periodlist, "FrequencyCategoryId", "FrequencyForPeriod");

            result.Data = selectlist;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
