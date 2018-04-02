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
    public class MarksController : BaseController
	{
		ApplicationDbContext appDbContext = new ApplicationDbContext();
		private StudentDbContext db = new StudentDbContext();
		// GET: Core/Marks
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult Index1()
		{
			return View();
		}

		//you can also use list of objects
		//(ex: public ActionResult AddCar(IEnumerable<Cars> CarsListFromTable))
		public ActionResult AddCar(List<string[]> dataListFromTable)
		{
			var dataListTable = dataListFromTable;
			return Json("Response, Data Received Successfully");
		}
		public JsonResult GetCar()
		{
			var jsonData = new[]
						 {
						 new[] {" ", "Kia", "Nissan",
						 "Toyota", "Honda", "Mazda", "Ford"},
						 new[] {"2012", "10", "11",
						 "12", "13", "15", "16"},
						 new[] {"2013", "10", "11",
						 "12", "13", "15", "16"},
						 new[] {"2014", "10", "11",
						 "12", "13", "15", "16"},
						 new[] {"2015", "10", "11",
						 "12", "13", "15", "16"},
						 new[] {"2016", "10", "11",
						 "12", "13", "15", "16"}
					};

			return Json(jsonData, JsonRequestBehavior.AllowGet);
		}

	
		public ActionResult tableData(List<string[]>  data)
		{
			return View();
		}


		public ActionResult Create()
		{
			
				var result = (from skills in db.Blood_Group select skills).ToList();
				if (result != null)
				{
					ViewBag.mySkills = result.Select(N => new SelectListItem { Text = N.Name, Value = N.Id.ToString() });
				}

			string str = "1001,1003,1005"; //query database and get the selected value

			List<string> selectedList = str.Split(',').ToList();

			//DDLGetInitData() method get the DropDownList Init data
			//according to the selected value to set the default selected value.
			List<SelectListItem> ddlitemlist = db.Blood_Group.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = selectedList.Contains(c.Id.ToString()) ? true : false }).ToList();

			ViewBag.ddlitemlist = ddlitemlist; //using ViewBag to bind DropDownList
			return View();

			//return View();
			//List<SelectListItem> items = new List<SelectListItem>();
			//items.Add(new SelectListItem
			//{
			//	Text = "dd",
			//	Value = "1"
			//});

			//items.Add(new SelectListItem
			//{
			//	Text = "dw",
			//	Value = "2"
			//});

			//items.Add(new SelectListItem
			//{
			//	Text = "dl",
			//	Value = "3"
			//});

			//List<SelectListItem> selectedItems = items.ToList();
			//	//.Where(p => fruit.FruitIds.Contains(int.Parse(p.Value))).ToList();

			//ViewBag.Message = "Selected Fruits:";
			//foreach (var selectedItem in selectedItems)
			//{
			//	selectedItem.Selected = true;
			//	ViewBag.Message += "\\n" + selectedItem.Text;
			//}
			return View();
		}
			
			
		}
	

	public class Gate
	{
		public string PreprationRequired { get; set; }
		public List<CheckBoxes> lstPreprationRequired { get; set; }
		public string[] CategoryIds { get; set; }
	}

	public class CheckBoxes
	{
		public int ID { get; set; }
		public string Value { get; set; }
		public string Text { get; set; }
		public bool Checked { get; set; }
	}
}