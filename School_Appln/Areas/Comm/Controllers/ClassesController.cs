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

namespace School_Appln.Areas.Comm.Controllers
{
    public class ClassesController : BaseController
    {
        private StudentDbContext db = new StudentDbContext();

        
        public async Task<ActionResult> Index()
        {
            return View(await db.Classes.Where(a=>a.Is_Deleted!=true).ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.FindAsync(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Class_Id,Class_Name,Academic_Year")] Class @class)
        {
            var userId = LoggedInUser.Id;
            if (ModelState.IsValid)
            {
                @class.Is_Active = true;
                @class.Created_By = userId;
                @class.Created_On = DateTime.Now;
                db.Classes.Add(@class);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(@class);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.FindAsync(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Class_Id,Class_Name,Academic_Year,Is_Active,Is_Deleted,Created_On,Created_By,Updated_On,Updated_By")] Class @class)
        {
            var userId = LoggedInUser.Id;
            Class existingClass = db.Classes.Find(@class.Class_Id);

            if (ModelState.IsValid)
            {
                db.Entry(existingClass).CurrentValues.SetValues(@class);
                existingClass.Updated_By = userId;
                existingClass.Updated_On = DateTime.Now;
                db.Entry(existingClass).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@class);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.FindAsync(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Class @class = await db.Classes.FindAsync(id);
            db.Entry(@class).CurrentValues.SetValues(@class);
            @class.Is_Deleted = true;
            db.Entry(@class).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
