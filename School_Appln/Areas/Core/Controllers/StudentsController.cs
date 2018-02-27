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

namespace School_Appln.Areas.Core.Controllers
{
    public class StudentsController : Controller
    {
        private StudentDbContext db = new StudentDbContext();

        // GET: Core/Students
        public async Task<ActionResult> Index()
        {
            var students = db.Students.Include(s => s.BloodGroup).Include(s => s.FClass).Include(s => s.FSection);///.Include(s => s.Gender);
            return View(await students.ToListAsync());
        }

        // GET: Core/Students/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Core/Students/Create
        public ActionResult Create()
        {
            ViewBag.Blood_Group_Id = new SelectList(db.Blood_Group, "Id", "Name");
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name");
            ViewBag.Section_Id = new SelectList(db.Sections, "Section_Id", "Section_Name");
            ViewBag.Gender_Id = new SelectList(db.Genders, "Id", "Name");
            return View();
        }

        // POST: Core/Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Student_Id,Roll_No,First_Name,Middle_Name,Last_Name,Gender_Id,DOB,Enrollment_Date,Father_Name,Mother_Name,Blood_Group_Id,Address_Line1,Address_Line2,City_Id,State_Id,Country_Id,Phone_No1,Phone_No2,LandLine,Email_Id,Academic_Year,Created_By,Created_On,Updated_On,Updated_By,Is_Active,Is_Deleted,Pincode,Photo,Aadhar_No,Class_Id,Section_Id,Is_HostelStudent,Is_FeesDueRemaining,Fees_Due_Amount")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Blood_Group_Id = new SelectList(db.Blood_Group, "Id", "Name", student.Blood_Group_Id);
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name", student.Class_Id);
            ViewBag.Section_Id = new SelectList(db.Sections, "Section_Id", "Section_Name", student.Section_Id);
            ViewBag.Gender_Id = new SelectList(db.Genders, "Id", "Name", student.Gender_Id);
            return View(student);
        }

        // GET: Core/Students/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.Blood_Group_Id = new SelectList(db.Blood_Group, "Id", "Name", student.Blood_Group_Id);
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name", student.Class_Id);
            ViewBag.Section_Id = new SelectList(db.Sections, "Section_Id", "Section_Name", student.Section_Id);
            ViewBag.Gender_Id = new SelectList(db.Genders, "Id", "Name", student.Gender_Id);
            return View(student);
        }

        // POST: Core/Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Student_Id,Roll_No,First_Name,Middle_Name,Last_Name,Gender_Id,DOB,Enrollment_Date,Father_Name,Mother_Name,Blood_Group_Id,Address_Line1,Address_Line2,City_Id,State_Id,Country_Id,Phone_No1,Phone_No2,LandLine,Email_Id,Academic_Year,Created_By,Created_On,Updated_On,Updated_By,Is_Active,Is_Deleted,Pincode,Photo,Aadhar_No,Class_Id,Section_Id,Is_HostelStudent,Is_FeesDueRemaining,Fees_Due_Amount")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Blood_Group_Id = new SelectList(db.Blood_Group, "Id", "Name", student.Blood_Group_Id);
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name", student.Class_Id);
            ViewBag.Section_Id = new SelectList(db.Sections, "Section_Id", "Section_Name", student.Section_Id);
            ViewBag.Gender_Id = new SelectList(db.Genders, "Id", "Name", student.Gender_Id);
            return View(student);
        }

        // GET: Core/Students/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Core/Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Student student = await db.Students.FindAsync(id);
            db.Students.Remove(student);
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
