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

namespace School_Appln.Areas.Lab
{
    public class Student_Other_DetailsController : Controller
    {
        private StudentDbContext db = new StudentDbContext();

        // GET: Lab/Student_Other_Details
        public async Task<ActionResult> Index()
        {
            return View(await db.Student_Other_Details.ToListAsync());
        }

        // GET: Lab/Student_Other_Details/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Other_Details student_Other_Details = await db.Student_Other_Details.FindAsync(id);
            if (student_Other_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Other_Details);
        }

        // GET: Lab/Student_Other_Details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lab/Student_Other_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StudentDetail_Id,Student_Id,Identification_Mark1,Identification_Mark2,Is_Allergic,Allergy_Details,Father_Occupation_Id,Father_Annual_Income,Mother_Occupation_Id,Mother_Annual_Income,Caste,Religion,Languages_Known,Second_Language_Opted_Id,Birth_Certificate,Upload_Document1,UpLoad_Document2,Academic_Year,Is_Active,Is_Deleted,Created_On,Created_By,Deleted_On,Deleted_By")] Student_Other_Details student_Other_Details)
        {
            if (ModelState.IsValid)
            {
                db.Student_Other_Details.Add(student_Other_Details);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student_Other_Details);
        }

        // GET: Lab/Student_Other_Details/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Other_Details student_Other_Details = await db.Student_Other_Details.FindAsync(id);
            if (student_Other_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Other_Details);
        }

        // POST: Lab/Student_Other_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StudentDetail_Id,Student_Id,Identification_Mark1,Identification_Mark2,Is_Allergic,Allergy_Details,Father_Occupation_Id,Father_Annual_Income,Mother_Occupation_Id,Mother_Annual_Income,Caste,Religion,Languages_Known,Second_Language_Opted_Id,Birth_Certificate,Upload_Document1,UpLoad_Document2,Academic_Year,Is_Active,Is_Deleted,Created_On,Created_By,Deleted_On,Deleted_By")] Student_Other_Details student_Other_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Other_Details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student_Other_Details);
        }

        // GET: Lab/Student_Other_Details/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Other_Details student_Other_Details = await db.Student_Other_Details.FindAsync(id);
            if (student_Other_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Other_Details);
        }

        // POST: Lab/Student_Other_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Student_Other_Details student_Other_Details = await db.Student_Other_Details.FindAsync(id);
            db.Student_Other_Details.Remove(student_Other_Details);
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
