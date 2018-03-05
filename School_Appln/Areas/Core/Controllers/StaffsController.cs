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
    public class StaffsController : Controller
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
	}
}