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
			var staffs = db.Staffs.Include(s => s.BloodGroup).Include(s => s.Gender).Where(s => s.Is_Deleted == false);
			return View(await staffs.ToListAsync());
		}

		// GET: Core/Staffs
		public ActionResult RedirectToAddStaff()
		{
			return RedirectToAction("Create");
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
						return RedirectToAction("StaffSalaryDetails");
					//return RedirectToAction("CreateStaffOtherDetails");
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

		// GET: Core/Staffs/Delete/5
		public async Task<ActionResult> Delete(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			//if (staff == null)
			//{
			//	return HttpNotFound();
			//}

			
			
				using (StudentDbContext db = new StudentDbContext())
				{
					Staff staff = await db.Staffs.FindAsync(id);
					if (staff != null)
					{
						staff.Is_Deleted = true;
						db.Entry(staff).CurrentValues.SetValues(staff);
						db.Entry(staff).State = EntityState.Modified;
						await db.SaveChangesAsync();
					}
				}			
			
				using (StudentDbContext db = new StudentDbContext())
				{
					Staff_Educational_Details staffEducationalDetails = await db.Staff_Educational_Details.FindAsync(id);
					if (staffEducationalDetails != null)
					{
						staffEducationalDetails.Is_Deleted = true;
						db.Entry(staffEducationalDetails).CurrentValues.SetValues(staffEducationalDetails);
						db.Entry(staffEducationalDetails).State = EntityState.Modified;
						await db.SaveChangesAsync();
					}
				}
			
			
				using (StudentDbContext db = new StudentDbContext())
				{
					Staff_Salary_Detail staffSalaryDetail = await db.Staff_Salary_Details.FindAsync(id);
					if (staffSalaryDetail != null)
					{
						staffSalaryDetail.Is_Deleted = true;
						db.Entry(staffSalaryDetail).CurrentValues.SetValues(staffSalaryDetail);
						db.Entry(staffSalaryDetail).State = EntityState.Modified;
						await db.SaveChangesAsync();
					}
				}			
			
				using (StudentDbContext db = new StudentDbContext())
				{
					Staff_Exp_Details stafExpDetails = await db.Staff_Exp_Details.FindAsync(id);
					if (stafExpDetails != null)
					{
						stafExpDetails.Is_Deleted = true;
						db.Entry(stafExpDetails).CurrentValues.SetValues(stafExpDetails);
						db.Entry(stafExpDetails).State = EntityState.Modified;
						await db.SaveChangesAsync();
					}
				}

			return RedirectToAction("Index"); 
		}



		//Save & Continue
		public ActionResult CreateStaffOtherDetails()
		{
			//string ForOtherDetailStaffId = TempData.Peek("Staff_Id").ToString();
			return View();
		}

		#region Add Educational Qualification Details 
		[HttpPost]
		public JsonResult AutoCompleteQualification(string qual_Name)
		{

			var qualificationList = (from qual in db.Qualifications
									 where qual.Qualification_Name.StartsWith(qual_Name)
									 select new
									 {
										 label = qual.Qualification_Name,
										 val = qual.Qualification_Id
									 }).ToList();

			return Json(qualificationList, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult AutoCompleteSpecialization(string specialization_Name)
		{
			var specializationList = (from specialization in db.Specializations
									  where specialization.Specialization_Name.StartsWith(specialization_Name)
									  select new
									  {
										  label = specialization.Specialization_Name,
										  val = specialization.Specialization_Id
									  }).ToList();

			return Json(specializationList, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult AutoCompleteInstitution(string institution_Name)
		{
			var institutionList = (from institution in db.Institutions
								   where institution.Institution_Name.StartsWith(institution_Name)
								   select new
								   {
									   label = institution.Institution_Name,
									   val = institution.Institution_Id
								   }).ToList();

			return Json(institutionList, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public async Task<ActionResult> AddEducationalQualificationDetails(string staffId, string qualification_Id, string specialization_Id, string institution_Id, string passingYear, string MediumOfInstruction)
		{
			var userId = LoggedInUser.Id;
			JsonResult result = new JsonResult();
			try
			{
				//var schoolid = Convert.ToInt32(schoolId);
				var staff_Id = Convert.ToInt32(staffId);
				var qualificationId = Convert.ToInt32(qualification_Id);
				var specializationId = Convert.ToInt32(specialization_Id);
				var institutionId = Convert.ToInt32(institution_Id);
				var passing_Year = Convert.ToInt64(passingYear);
				var Medium_Of_Instruction = MediumOfInstruction;
				//   , string FrmYear, string ToYear, string Leaving, string Comments

				var CheckStaffQualDetails = db.Staff_Educational_Details.Where(a => a.Staff_Id == staff_Id && a.Qualification_Id == qualificationId).Count();

				if (CheckStaffQualDetails == 0)
				{
					Staff_Educational_Details addStaffEduQualDetails = new Staff_Educational_Details();
					addStaffEduQualDetails.Staff_Id = staff_Id;
					addStaffEduQualDetails.Qualification_Id = qualificationId;
					addStaffEduQualDetails.Specialization_Id = specializationId;
					addStaffEduQualDetails.Institution_Id = institutionId;
					addStaffEduQualDetails.Is_Deleted = false;
					addStaffEduQualDetails.Year_Of_Passing = passing_Year;
					addStaffEduQualDetails.Medium_Of_Instruction = Medium_Of_Instruction;
					addStaffEduQualDetails.Academic_Year = (DateTime.Now.Month < 3) ? DateTime.Now.Year - 1 : DateTime.Now.Year;
					//addStaffWorkExpDetails.Comments = Comments;
					addStaffEduQualDetails.Created_On = DateTime.Now;
					addStaffEduQualDetails.Created_By = userId;
					addStaffEduQualDetails.Is_Active = true;

					db.Staff_Educational_Details.Add(addStaffEduQualDetails);
					await db.SaveChangesAsync();
				}
				else
				{

				}
			}
			catch (Exception ex)
			{
				result.Data = new { Result = "ERROR", Message = " This property already available in this block " };
			}

			return RedirectToAction("CreateStaffOtherDetails");
		}

		public JsonResult GetEducationalQualificationDetails(string sidx, string sord, int page, int rows, string staffId)
		{
			var refStaffId = Convert.ToInt64(staffId);
			int pageIndex = Convert.ToInt32(page) - 1;
			int pageSize = rows;
			var EduQualList = (from stEduQualDetail in db.Staff_Educational_Details
							   join st in db.Staffs on stEduQualDetail.Staff_Id equals st.Staff_Id
							   join institution in db.Institutions on stEduQualDetail.Institution_Id equals institution.Institution_Id
							   join specialization in db.Specializations on stEduQualDetail.Specialization_Id equals specialization.Specialization_Id
							   join qualification in db.Qualifications on stEduQualDetail.Qualification_Id equals qualification.Qualification_Id
							   where stEduQualDetail.Staff_Id == refStaffId && stEduQualDetail.Is_Deleted != true
							   select new { stEduQualDetail.Staff_Id, stEduQualDetail.Qualification_Id, stEduQualDetail.Specialization_Id, stEduQualDetail.Institution_Id, stEduQualDetail.Academic_Year, qualification.Qualification_Name, specialization.Specialization_Name, institution.Institution_Name, stEduQualDetail.Year_Of_Passing, stEduQualDetail.Medium_Of_Instruction, stEduQualDetail.Created_By, stEduQualDetail.Created_On }
							  ).ToList();
			int totalRecords = EduQualList.Count();
			var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
			if (sord.ToUpper() == "DESC")
			{
				EduQualList = EduQualList.OrderByDescending(s => s.Staff_Id).ToList();
				EduQualList = EduQualList.Skip(pageIndex * pageSize).Take(pageSize).ToList();
			}
			else
			{
				EduQualList = EduQualList.OrderBy(s => s.Staff_Id).ToList();
				EduQualList = EduQualList.Skip(pageIndex * pageSize).Take(pageSize).ToList();
			}
			var jsonData = new
			{
				total = totalPages,
				page,
				records = totalRecords,
				rows = EduQualList
			};

			return Json(jsonData, JsonRequestBehavior.AllowGet);

		}

		public async Task<ActionResult> DeleteEduQualDetails(string staffId, string qual_Id)
		{
			var userId = LoggedInUser.Id;
			JsonResult result = new JsonResult();
			try
			{
				var staff_Id = Convert.ToInt32(staffId);
				var qualId = int.Parse(qual_Id);
				Staff_Educational_Details deleteEduQualDetails = db.Staff_Educational_Details.Where(a => a.Staff_Id == staff_Id && a.Qualification_Id == qualId).FirstOrDefault();
				db.Entry(deleteEduQualDetails).CurrentValues.SetValues(deleteEduQualDetails);
				deleteEduQualDetails.Is_Deleted = true;
				db.Entry(deleteEduQualDetails).State = EntityState.Modified;
				await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				result.Data = new { Result = "ERROR", Message = " This property already available in this block " };
			}

			return RedirectToAction("CreateStaffOtherDetails");
		}
		#endregion

		#region Add Previous Work Experience Details
		[HttpPost]
		public JsonResult AutoCompleteDesignation(string DesignationName)
		{
			var designationList = (from designation in db.Designations
								   where designation.Designation_Name.StartsWith(DesignationName)
								   select new
								   {
									   label = designation.Designation_Name,
									   val = designation.Designation_Id
								   }).ToList();

			return Json(designationList, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public async Task<ActionResult> AddPrevWorkExpDetails(string schoolId, string Staff_Id, string Name, string Address, string Designation, string FrmYear, string ToYear, string Subjects_Handled, string Designation_Id)
		{
			var userId = LoggedInUser.Id;
			JsonResult result = new JsonResult();
			try
			{
				var schoolid = Convert.ToInt32(schoolId);
				var staffId = Convert.ToInt32(Staff_Id);
				var designation = Convert.ToInt32(Designation_Id);
				var frmYear = Convert.ToInt32(FrmYear);
				var toYear = Convert.ToInt32(ToYear);
				//   , string FrmYear, string ToYear, string Leaving, string Comments

				var CheckStudent = db.Staff_Exp_Details.Where(a => a.Staff_Id == staffId && a.School_Id == schoolid).Count();

				if (CheckStudent == 0)
				{
					Staff_Exp_Details addStaffWorkExpDetails = new Staff_Exp_Details();
					addStaffWorkExpDetails.Staff_Id = staffId;
					addStaffWorkExpDetails.School_Id = schoolid;
					addStaffWorkExpDetails.Designation_Id = Convert.ToInt32(Designation_Id);
					addStaffWorkExpDetails.From_Year = frmYear;
					addStaffWorkExpDetails.Is_Deleted = false;
					addStaffWorkExpDetails.To_Year = toYear;
					addStaffWorkExpDetails.Subject_Id = Subjects_Handled;
					addStaffWorkExpDetails.Academic_Year = (DateTime.Now.Month < 3) ? DateTime.Now.Year - 1 : DateTime.Now.Year;
					//addStaffWorkExpDetails.Comments = Comments;
					addStaffWorkExpDetails.Created_On = DateTime.Now;
					addStaffWorkExpDetails.Created_By = userId;
					addStaffWorkExpDetails.Is_Active = true;

					db.Staff_Exp_Details.Add(addStaffWorkExpDetails);
					await db.SaveChangesAsync();
				}
				else
				{

				}
			}
			catch (Exception ex)
			{
				result.Data = new { Result = "ERROR", Message = " This property already available in this block " };
			}

			return RedirectToAction("CreateStaffOtherDetails");
		}

		public JsonResult GetPrevWorkExpDetails(string sidx, string sord, int page, int rows, string staffId)
		{
			var refStaffId = Convert.ToInt64(staffId);
			int pageIndex = Convert.ToInt32(page) - 1;
			int pageSize = rows;
			var PrevWorkExpList = (from stexpdetail in db.Staff_Exp_Details
								   join st in db.Staffs on stexpdetail.Staff_Id equals st.Staff_Id
								   join schl in db.Schools on stexpdetail.School_Id equals schl.Id
								   join desg in db.Designations on stexpdetail.Designation_Id equals desg.Designation_Id
								   where stexpdetail.Staff_Id == refStaffId && stexpdetail.Is_Deleted != true
								   select new { stexpdetail.Staff_Id, stexpdetail.School_Id, stexpdetail.Academic_Year, schl.Name, desg.Designation_Name, stexpdetail.From_Year, stexpdetail.To_Year, stexpdetail.Subject_Id, stexpdetail.Created_By, stexpdetail.Created_On }
							  ).ToList();
			int totalRecords = PrevWorkExpList.Count();
			var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
			if (sord.ToUpper() == "DESC")
			{
				PrevWorkExpList = PrevWorkExpList.OrderByDescending(s => s.Staff_Id).ToList();
				PrevWorkExpList = PrevWorkExpList.Skip(pageIndex * pageSize).Take(pageSize).ToList();
			}
			else
			{
				PrevWorkExpList = PrevWorkExpList.OrderBy(s => s.Staff_Id).ToList();
				PrevWorkExpList = PrevWorkExpList.Skip(pageIndex * pageSize).Take(pageSize).ToList();
			}
			var jsonData = new
			{
				total = totalPages,
				page,
				records = totalRecords,
				rows = PrevWorkExpList
			};

			return Json(jsonData, JsonRequestBehavior.AllowGet);

		}

		public async Task<ActionResult> DeleteWorkExpDetails(string schoolId, string StaffId)
		{
			var userId = LoggedInUser.Id;
			JsonResult result = new JsonResult();
			try
			{
				var school_Id = Convert.ToInt32(schoolId);
				var staff_Id = Convert.ToInt32(StaffId);
				Staff_Exp_Details deleteWorkExpDetails = db.Staff_Exp_Details.Where(a => a.School_Id == school_Id && a.Staff_Id == staff_Id).FirstOrDefault();
				db.Entry(deleteWorkExpDetails).CurrentValues.SetValues(deleteWorkExpDetails);
				deleteWorkExpDetails.Is_Deleted = true;
				db.Entry(deleteWorkExpDetails).State = EntityState.Modified;
				await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				result.Data = new { Result = "ERROR", Message = " This property already available in this block " };
			}

			return RedirectToAction("CreateStaffOtherDetails");
		}
		#endregion

		#region Edit Staff Details 
		// GET: Core/Staffs/Edit/5
		public async Task<ActionResult> Edit(long? id)

		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Staff staff = await db.Staffs.FindAsync(id);
			if (staff == null)
			{
				return HttpNotFound();
			}
			ViewBag.Selected_Country = new SelectList(db.Country, "Id", "Name", staff.Country_Id);
			ViewBag.Selected_State = new SelectList(db.States.Where(s => s.Country_Id == staff.Country_Id), "Id", "Name", staff.State_Id);
			ViewBag.Selected_City = new SelectList(db.Cities.Where(c => c.State_Id == staff.State_Id), "Id", "Name", staff.City_Id);
			ViewBag.Blood_Grp_Id = new SelectList(db.Blood_Group, "Id", "Name", staff.Blood_Group_Id);
			ViewBag.Staff_Id = new SelectList(db.Staff_Type, "Staff_Type_Id", "Staff_Type_Name", staff.Staff_Type_Id);
			ViewBag.Gender_Selected = new SelectList(db.Genders, "Id", "Name", staff.Gender_Id);

			return View(staff);
		}

		// POST: Core/Staffs/Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Staff_Id,Employee_Id,Staff_Type_Id,First_Name,Middle_Name,Last_Name,Gender_Id,DOB,Date_Of_Joining,Aadhar_Number,Father_Name,Mother_Name,Blood_Group_Id,Address_Line1,Address_Line2,City_Id,State_Id,Country_Id,Mobile_No,Alt_Mobile_No,LandLine,Email_Id,Academic_Year,Created_By,Created_On,Updated_On,Updated_By,Is_Active,Is_Deleted,Pincode,Photo,City_Id,State_Id,Country_Id,Experience_in_Years,Is_Married")] Staff staff, HttpPostedFileBase imageUpload, string command)
		{
			var userId = LoggedInUser.Id;

			int cityId = int.Parse(Request.Form["City_Id"]);
			int countryId = int.Parse(Request.Form["Country_Id"]);
			int stateId = int.Parse(Request.Form["State_Id"]);
			int staffTypeId = int.Parse(Request.Form["Staff_Type_Id"]);
			Staff existingStaff = db.Staffs.Find(staff.Staff_Id);
			if (ModelState.IsValid)
			{
				existingStaff.City_Id = cityId;
				existingStaff.State_Id = stateId;
				existingStaff.Country_Id = countryId;
				existingStaff.Staff_Type_Id = staffTypeId;
				existingStaff.Aadhar_Number = staff.Aadhar_Number;
				existingStaff.Address_Line1 = staff.Address_Line1;
				existingStaff.Address_Line2 = staff.Address_Line2;
				existingStaff.Alt_Mobile_No = staff.Alt_Mobile_No;
				existingStaff.Blood_Group_Id = staff.Blood_Group_Id;
				existingStaff.Date_Of_Joining = staff.Date_Of_Joining;
				existingStaff.DOB = staff.DOB;
				existingStaff.Email_Id = staff.Email_Id;
				existingStaff.Employee_Id = staff.Employee_Id;
				existingStaff.Experience_in_Years = staff.Experience_in_Years;
				existingStaff.Father_Name = staff.Father_Name;
				existingStaff.First_Name = staff.First_Name;
				existingStaff.Gender_Id = staff.Gender_Id;
				existingStaff.Last_Name = staff.Last_Name;
				existingStaff.Middle_Name = staff.Middle_Name;
				existingStaff.Mobile_No = staff.Mobile_No;
				existingStaff.PinCode = staff.PinCode;
				existingStaff.Staff_Type_Id = staff.Staff_Type_Id;
				existingStaff.Is_Married = staff.Is_Married;			
				existingStaff.Updated_By = userId;
				existingStaff.Updated_On = DateTime.Now;
				existingStaff.Is_Active = true;
				existingStaff.Is_Deleted = false;
				db.Entry(existingStaff).CurrentValues.SetValues(existingStaff);
				db.Entry(existingStaff).State = EntityState.Modified;
				await db.SaveChangesAsync();
				//return RedirectToAction("Index");
				switch (command)
				{
					case "Edit & Back To List":
						return RedirectToAction("Index");
					case "Edit & Continue":
						TempData["Staff_Id"] = staff.Staff_Id;
						return RedirectToAction("EditStaffSalaryDetails");
						//return RedirectToAction("EditStaffOtherDetails");
					default:
						return RedirectToAction("Index");
				}
			}

			ViewBag.Selected_Country = new SelectList(db.Country, "Id", "Name", staff.Country_Id);
			ViewBag.Selected_State = new SelectList(db.States.Where(s => s.Country_Id == staff.Country_Id), "Id", "Name", staff.State_Id);
			ViewBag.Selected_City = new SelectList(db.Cities.Where(c => c.State_Id == staff.State_Id), "Id", "Name", staff.City_Id);
			ViewBag.Blood_Grp_Id = new SelectList(db.Blood_Group, "Id", "Name", staff.Blood_Group_Id);
			ViewBag.Staff_Id = new SelectList(db.Staff_Type, "Staff_Type_Id", "Staff_Type_Name", staff.Staff_Type_Id);
			ViewBag.Gender_Selected = new SelectList(db.Genders, "Id", "Name", staff.Gender_Id);
			return View(staff);

		}

		//GET : Core/Staffs/EditStaffOtherDetails
		public ActionResult EditStaffOtherDetails()
		{
			//string ForOtherDetailStaffId = TempData.Peek("Staff_Id").ToString();
			return View();
		}
		#endregion

		#region Staff Salary Details
		//GET : Core/Staffs/StaffSalaryDetails
		public ActionResult StaffSalaryDetails()
		{
			//string ForOtherDetailStaffId = TempData.Peek("Staff_Id").ToString();
			return View();
		}

		// POST: Core/Staffs/StaffSalaryDetails
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> StaffSalaryDetails([Bind(Include = "Basic,DA,Medical,Conveyance,HRA,LTA,Other,Provident_Fund,ESIC,Professional_Tax,Academic_Year,Created_By,Created_On,Updated_On,Updated_By,Is_Active,Is_Deleted,Gross_Salary,Net_Salary")] Staff_Salary_Detail staff_Salary_Detail, HttpPostedFileBase imageUpload, string command)
		{
			var userId = LoggedInUser.Id;


			if (ModelState.IsValid)
			{

				//Staff_Salary_Detail staffSalaryDetail = new Staff_Salary_Detail();

				staff_Salary_Detail.Staff_Id =Convert.ToInt32(TempData.Peek("Staff_Id").ToString());
				//staffSalaryDetail.Staff_Type_Id = staff_Salary_Detail.Staff_Type_Id;
				staff_Salary_Detail.Basic = staff_Salary_Detail.Basic;
				staff_Salary_Detail.DA = staff_Salary_Detail.DA;
				staff_Salary_Detail.Medical = staff_Salary_Detail.Medical;
				staff_Salary_Detail.Conveyance = staff_Salary_Detail.Conveyance;
				staff_Salary_Detail.HRA = staff_Salary_Detail.HRA;
				staff_Salary_Detail.LTA = staff_Salary_Detail.LTA;
				staff_Salary_Detail.Other = staff_Salary_Detail.Other;
				staff_Salary_Detail.Provident_Fund = staff_Salary_Detail.Provident_Fund;
				staff_Salary_Detail.ESIC = staff_Salary_Detail.ESIC;
				staff_Salary_Detail.Professional_Tax = staff_Salary_Detail.Professional_Tax;
				staff_Salary_Detail.Is_Active = true;
				staff_Salary_Detail.Is_Deleted = false;
				staff_Salary_Detail.Created_By = userId;
				staff_Salary_Detail.Created_On = DateTime.Now;

				db.Staff_Salary_Details.Add(staff_Salary_Detail);
				await db.SaveChangesAsync();
			}
			//return RedirectToAction("Index");
			switch (command)
			{
				//case "Save & Back To List":
				//    return RedirectToAction("Index");
				case "Save & Continue":
					TempData["Staff_Id"] = staff_Salary_Detail.Staff_Id;
					//return RedirectToAction("StaffSalaryDetails");
					return RedirectToAction("CreateStaffOtherDetails");
				default:
					return RedirectToAction("Index");
			}


			//return View(staff_Salary_Detail);
		}

		// GET: Core/Staffs/EditStaffSalaryDetails/5
		public async Task<ActionResult> EditStaffSalaryDetails()
		{
			int staff_Id = Convert.ToInt32(TempData["Staff_Id"]);
			//var Staff_Salary_Id = Convert.ToInt32(db.Staff_Salary_Details.fi);

			if (staff_Id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Staff_Salary_Detail staffSalaryDetails = await db.Staff_Salary_Details.Where(x => x.Staff_Id == staff_Id).FirstOrDefaultAsync();
			//Staff_Salary_Detail staffSalaryDetails = await db.Staff_Salary_Details.FindAsync(Staff_Salary_Id);
			if (staffSalaryDetails == null)
			{
				return HttpNotFound();
			}
			
			return View(staffSalaryDetails);
		}

		// POST: Core/Staffs/EditStaffSalaryDetails
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditStaffSalaryDetails([Bind(Include = "Staff_Salary_Id,Staff_Id,Basic,DA,Medical,Conveyance,HRA,LTA,Other,Provident_Fund,ESIC,Professional_Tax,Academic_Year,Created_By,Created_On,Updated_On,Updated_By,Is_Active,Is_Deleted,Gross_Salary,Net_Salary")] Staff_Salary_Detail staff_Salary_Detail, HttpPostedFileBase imageUpload, string command)
		{
			var userId = LoggedInUser.Id;

			Staff_Salary_Detail existingStaffSalaryDetail = db.Staff_Salary_Details.Find(staff_Salary_Detail.Staff_Salary_Id);
			if (ModelState.IsValid)
			{

				existingStaffSalaryDetail.Basic = staff_Salary_Detail.Basic;
				existingStaffSalaryDetail.Conveyance = staff_Salary_Detail.Conveyance;
				existingStaffSalaryDetail.DA = staff_Salary_Detail.DA;
				existingStaffSalaryDetail.HRA = staff_Salary_Detail.HRA;
				existingStaffSalaryDetail.LTA = staff_Salary_Detail.LTA;
				existingStaffSalaryDetail.Medical = staff_Salary_Detail.Medical;
				existingStaffSalaryDetail.Other = staff_Salary_Detail.Other;
				existingStaffSalaryDetail.Professional_Tax = staff_Salary_Detail.Professional_Tax;
				existingStaffSalaryDetail.Provident_Fund = staff_Salary_Detail.Provident_Fund;
				existingStaffSalaryDetail.ESIC = staff_Salary_Detail.ESIC;
				existingStaffSalaryDetail.Updated_By = userId;
				existingStaffSalaryDetail.Updated_On = DateTime.Now;
				existingStaffSalaryDetail.Is_Active = true;
				existingStaffSalaryDetail.Is_Deleted = false;
				db.Entry(existingStaffSalaryDetail).CurrentValues.SetValues(existingStaffSalaryDetail);
				db.Entry(existingStaffSalaryDetail).State = EntityState.Modified;
				await db.SaveChangesAsync();
				//return RedirectToAction("Index");
				switch (command)
				{
					case "Edit & Back To List":
						return RedirectToAction("Index");
					case "Edit & Continue":
						TempData["Staff_Id"] = staff_Salary_Detail.Staff_Id;
						//return RedirectToAction("EditStaffSalaryDetails");
					   return RedirectToAction("EditStaffOtherDetails");
					default:
						return RedirectToAction("Index");
				}
			}

			return View(existingStaffSalaryDetail);

			
		}


		#endregion
	}

}