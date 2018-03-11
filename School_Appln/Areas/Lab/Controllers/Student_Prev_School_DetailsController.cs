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

namespace School_Appln.Areas.Lab.Controllers
{
    public class Student_Prev_School_DetailsController : Controller
    {
        private StudentDbContext db = new StudentDbContext();

        // GET: Lab/Student_Prev_School_Details
        public async Task<ActionResult> Index()
        {
            return View(await db.Student_Prev_School_Details.ToListAsync());
        }

        // GET: Lab/Student_Prev_School_Details/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Prev_School_Details student_Prev_School_Details = await db.Student_Prev_School_Details.FindAsync(id);
            if (student_Prev_School_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Prev_School_Details);
        }

        // GET: Lab/Student_Prev_School_Details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lab/Student_Prev_School_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Student_PrevSchool_Id,Student_Id,School_Id,Other_School_Name,Other_School_Address,Academic_Year,Is_Active,Is_Deleted,Created_On,Created_By,Updated_On,Updated_By,From_Year,To_Year,Leaving_Reason,Upload_Document1,Upload_Document2,Comments")] Student_Prev_School_Details student_Prev_School_Details)
        {
            if (ModelState.IsValid)
            {
                db.Student_Prev_School_Details.Add(student_Prev_School_Details);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student_Prev_School_Details);
        }

        // GET: Lab/Student_Prev_School_Details/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Prev_School_Details student_Prev_School_Details = await db.Student_Prev_School_Details.FindAsync(id);
            if (student_Prev_School_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Prev_School_Details);
        }

        // POST: Lab/Student_Prev_School_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Student_PrevSchool_Id,Student_Id,School_Id,Other_School_Name,Other_School_Address,Academic_Year,Is_Active,Is_Deleted,Created_On,Created_By,Updated_On,Updated_By,From_Year,To_Year,Leaving_Reason,Upload_Document1,Upload_Document2,Comments")] Student_Prev_School_Details student_Prev_School_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Prev_School_Details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student_Prev_School_Details);
        }

        // GET: Lab/Student_Prev_School_Details/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Prev_School_Details student_Prev_School_Details = await db.Student_Prev_School_Details.FindAsync(id);
            if (student_Prev_School_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Prev_School_Details);
        }

        // POST: Lab/Student_Prev_School_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student_Prev_School_Details student_Prev_School_Details = await db.Student_Prev_School_Details.FindAsync(id);
            db.Student_Prev_School_Details.Remove(student_Prev_School_Details);
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
