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
using System.IO;
using School_AppIn_Utils;
using Microsoft.AspNet.Identity;
using School_AppIn_Model;

namespace School_Appln.Areas.Core.Controllers
{
    public class StudentsController : BaseController
    {
        ApplicationDbContext appDbContext = new ApplicationDbContext();
        private StudentDbContext db = new StudentDbContext();

        // GET: Core/Students
        public async Task<ActionResult> Index()
        {
            //var gen = db.Genders.ToList();
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
            ViewBag.Country_Id = new SelectList(db.Country, "Id", "Name");
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
            var userId = LoggedInUser.Id;
            if (ModelState.IsValid)
            {
                string bodyHtml = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/EmailTemplates/WelcomeEmailTemplate.html")))
                {
                    bodyHtml = reader.ReadToEnd();
                }

                var user = UserManager.FindByName(student.Email_Id);
                if (user == null)
                {
                    user = new School_AppIn_Model.User { UserName = student.Email_Id, Email = student.Email_Id, EmailConfirmed = true };
                    var pwd = PasswordGenerator.GeneratePWD();
                    var result = await UserManager.CreateAsync(user, pwd);
                    if (result.Succeeded)
                    {
                        UserManager.AddToRole(user.Id, School_AppIn_Model.Common.Constants.ROLE_STUDENT);
                    }
                    appDbContext.SaveChanges();
                        var userMail = LoggedInUser;
                    var welcomeBodyHtml = PopoulateWelcomeEmailTemplate(bodyHtml, userMail, user.UserName, pwd.Trim());
                    School_AppIn_Utils.Utility.ApiTypes.EmailSend emailSend = Utility.Send(
                         apiKey: "41750a2d-38ba-4f35-a616-f0f776cc107e",
                         subject: string.Format("Welcome to {0} School ERP Portal", userMail.UserName),
                         from: LoggedInUser.UserName,
                         fromName: userMail.Email,
                         to: new List<string> { user.Email },
                         bodyText: "You can login to using the following credentials. Username :" + user.UserName + ", Password :" + pwd,
                         bodyHtml: welcomeBodyHtml);



                    student.Is_Active = true;
                    student.Created_By = userId;
                    student.Created_On = DateTime.Now;
                    db.Students.Add(student);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            ViewBag.Country_Id = new SelectList(db.Country, "Id", "Name");
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
            ViewBag.Country_Id = new SelectList(db.Country, "Id", "Name", student.Country_Id);
            ViewBag.State_Id = new SelectList(db.States.Where(s => s.CountryId == student.Country_Id), "Id", "Name", student.State_Id);
            ViewBag.City_Id = new SelectList(db.Cities.Where(c => c.StateId == student.State_Id), "Id", "Name", student.City_Id);
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
            ViewBag.Country_Id = new SelectList(db.Country, "Id", "Name", student.Country_Id);
            ViewBag.State_Id = new SelectList(db.States.Where(s => s.CountryId == student.Country_Id), "Id", "Name", student.State_Id);
            ViewBag.City_Id = new SelectList(db.Cities.Where(c => c.StateId == student.State_Id), "Id", "Name", student.City_Id);
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
        public JsonResult GetState(int id)
        {
            JsonResult result = new JsonResult();
            var statelist = (from s in db.States
                             where s.CountryId == id
                             select s).ToList();
            var selectlist = new SelectList(statelist, "Id", "Name");

            result.Data = selectlist;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public JsonResult GetCity(int id)
        {
            JsonResult result = new JsonResult();
            var dt = db.Cities.Where(y => y.StateId == id);
            List<SelectListItem> mydata = new List<SelectListItem>();
            foreach (var c in dt)
            {
                mydata.Add(new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
            }
            result.Data = mydata;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;

        }
        public string PopoulateWelcomeEmailTemplate(string bodyHtml, School_AppIn_Model.User user, string username, string pw)
        {


            var wBodyHtml = bodyHtml.Replace("{{USER-NAME}}", user.NickName)
            .Replace("{{USER-ADDRESS}}", (user.Email ?? String.Empty))
            .Replace("{{USERNAME}}", username)
            .Replace("{{PASSWORD}}", pw);

            return wBodyHtml;
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
