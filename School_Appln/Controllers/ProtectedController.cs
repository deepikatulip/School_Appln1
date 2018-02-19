﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using School_AppIn_Models;
using Microsoft.AspNet.Identity;

namespace School_Appln.Controllers
{
    public class ProtectedController : Controller
    {
        // GET: Protected
        public ActionResult Index()
        {
            return View();
        }

        // GET: Protected
        [Authorize]
        public async Task<ActionResult> AdminHome()
        {
            return View();
        }

        // GET: Protected
        [Authorize]
        public async Task<ActionResult> SuperAdminHome()
        {
            return View();
        }
    }
}