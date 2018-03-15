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
    public class StaffsController : BaseController
	{
		ApplicationDbContext appDbContext = new ApplicationDbContext();
		private StudentDbContext db = new StudentDbContext();
		// GET: Core/Staffs
		public async Task<ActionResult> Index()
		{
			var staffs = db.Staffs.Include(s => s.BloodGroup).Include(s => s.Gender); ;
			return View(await staffs.ToListAsync());
		}

		// GET: Core/Students/Create
		public ActionResult Create()
		{
			ViewBag.Country_Id = new SelectList(db.Country, "Id", "Name");
			ViewBag.Blood_Group_Id = new SelectList(db.Blood_Group, "Id", "Name");
			ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_Name");
			ViewBag.Section_Id = new SelectList(db.Sections, "Section_Id", "Section_Name");
			ViewBag.Gender_Id = new SelectList(db.Genders, "Id", "Name");
			ViewBag.Staff_Id = new SelectList(db.Staff_Type, "Staff_Type_Id", "Staff_Type_Name");
				return View();
		}

		// POST: Core/Students/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Employee_Id,Staff_Type_Id,First_Name,Middle_Name,Last_Name,Gender_Id,DOB,Date_Of_Joining,Aadhar_Number,Father_Name,Mother_Name,Blood_Group_Id,Address_Line1,Address_Line2,City_Id,State_Id,Country_Id,Mobile_No,Alt_Mobile_No,LandLine,Email_Id,Academic_Year,Created_By,Created_On,Updated_On,Updated_By,Is_Active,Is_Deleted,Pincode,Photo,City_Id,State_Id,Country_Id,Experience_in_Years,Is_Married")] Staff staff, HttpPostedFileBase imageUpload, string command)
		{
			var userId = LoggedInUser.Id;

			if (string.IsNullOrEmpty(Request.Form["Country_Id"]))
			{
				goto Fail;
			}
			if (string.IsNullOrEmpty(Request.Form["State_Id"]))
			{
				goto Fail;
			}
			if (string.IsNullOrEmpty(Request.Form["City_Id"]))
			{
				goto Fail;
			}
			if (string.IsNullOrEmpty(Request.Form["Staff_Type_Id"]))
			{
				goto Fail;
			}

			if (ModelState.IsValid)
			{

				int cityId = int.Parse(Request.Form["City_Id"]);
				int countryId = int.Parse(Request.Form["Country_Id"]);
				int stateId = int.Parse(Request.Form["State_Id"]);
				int staffTypeId = int.Parse(Request.Form["Staff_Type_Id"]);



				string bodyHtml = string.Empty;
				using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/EmailTemplates/WelcomeEmailTemplate.html")))
				{
					bodyHtml = reader.ReadToEnd();
				}

				var user = UserManager.FindByName(staff.Email_Id);
				if (user == null)
				{
					user = new School_AppIn_Model.User { UserName = staff.Email_Id, Email = staff.Email_Id, EmailConfirmed = true };
					var pwd = PasswordGenerator.GeneratePWD();
					var result = await UserManager.CreateAsync(user, pwd);
					if (result.Succeeded)
					{
						UserManager.AddToRole(user.Id, School_AppIn_Model.Common.Constants.ROLE_STUDENT);
					}
					appDbContext.SaveChanges();
					//    var userMail = LoggedInUser;
					//var welcomeBodyHtml = PopoulateWelcomeEmailTemplate(bodyHtml, userMail, user.UserName, pwd.Trim());
					//School_AppIn_Utils.Utility.ApiTypes.EmailSend emailSend = Utility.Send(
					//     apiKey: "41750a2d-38ba-4f35-a616-f0f776cc107e",
					//     subject: string.Format("Welcome to {0} School ERP Portal", userMail.UserName),
					//     from: LoggedInUser.UserName,
					//     fromName: userMail.Email,
					//     to: new List<string> { user.Email },
					//     bodyText: "You can login to using the following credentials. Username :" + user.UserName + ", Password :" + pwd,
					//     bodyHtml: welcomeBodyHtml);

					if (imageUpload != null && imageUpload.ContentLength > 0)
					{
						byte[] imageData = null;
						using (var binaryReader = new BinaryReader(imageUpload.InputStream))
						{
							imageData = binaryReader.ReadBytes(imageUpload.ContentLength);
						}
						staff.Photo = imageData;
					}

					staff.City_Id = cityId;
					staff.State_Id = stateId;
					staff.Country_Id = countryId;
					staff.Staff_Type_Id = staffTypeId;
					staff.Is_Active = true;
					staff.Is_Deleted = false;
					staff.Created_By = userId;
					staff.Created_On = DateTime.Now;
					db.Staffs.Add(staff);
					await db.SaveChangesAsync();
				}
				//return RedirectToAction("Index");
				switch (command)
				{
					//case "Save & Back To List":
					//    return RedirectToAction("Index");
					case "Save & Continue":
						TempData["Staff_Id"] = staff.Staff_Id;
						return RedirectToAction("CreateStudentOther");
					default:
						return RedirectToAction("Index");
				}
			}
			Fail:
			ViewBag.Country_Id = new SelectList(db.Country, "Id", "Name");
			ViewBag.Blood_Group_Id = new SelectList(db.Blood_Group, "Id", "Name", staff.Blood_Group_Id);
			ViewBag.Gender_Id = new SelectList(db.Genders, "Id", "Name", staff.Gender_Id);
			ViewBag.Staff_Id = new SelectList(db.Staff_Type, "Staff_Type_Id", "Staff_Type_Name");
			return View(staff);
		}
	}
}