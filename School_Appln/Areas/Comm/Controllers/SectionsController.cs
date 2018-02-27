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
    public class SectionsController : BaseController
    {
        private StudentDbContext db = new StudentDbContext();

        // GET: Comm/Sections
        public async Task<ActionResult> Index()
        {
            var sections = db.Sections.Where(a=>a.Is_Deleted!=true).Include(s => s.Class).Include(s => s.Created).Include(s => s.Updated);
            return View(await sections.ToListAsync());
        }

        // GET: Comm/Sections/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: Comm/Sections/Create
        public ActionResult Create()
        {
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name");
            return View();
        }

        // POST: Comm/Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Section_Id,Class_Id,Section_Name,Academic_Year,Is_Active,Created_On,Created_By")] Section section)
        {
            var userId = LoggedInUser.Id;
            if (ModelState.IsValid)
            {
                section.Is_Active = true;
                section.Created_By = userId;
                section.Created_On = DateTime.Now;
                db.Sections.Add(section);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name", section.Class_Id);
            return View(section);
        }

        // GET: Comm/Sections/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name", section.Class_Id);
            return View(section);
        }

        // POST: Comm/Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Section_Id,Class_Id,Section_Name,Academic_Year,Is_Active,Created_On,Created_By,Updated_On,Updated_By,Is_Deleted")] Section section)
        {
            var userId = LoggedInUser.Id;
            Section existingSection = db.Sections.Find(section.Section_Id);

            if (ModelState.IsValid)
            {
                db.Entry(existingSection).CurrentValues.SetValues(section);
                existingSection.Updated_By = userId;
                existingSection.Updated_On = DateTime.Now;
                db.Entry(existingSection).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name", section.Class_Id);
            return View(section);
        }

        // GET: Comm/Sections/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Comm/Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Section section = await db.Sections.FindAsync(id);
            db.Entry(section).CurrentValues.SetValues(section);
            section.Is_Deleted = true;
            db.Entry(section).State = EntityState.Modified;
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
