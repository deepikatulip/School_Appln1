﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using School_AppIn_Model;
using System.Collections.Generic;
using System.IO;
using School_AppIn_Utils;

namespace School_AppIn.Controllers
{

    public class AccountController : BaseController

    {
        ApplicationDbContext appDbContext = new ApplicationDbContext();

        // GET: /Account/Login
		//
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string role)
        {
            ViewBag.ReturnUrl = returnUrl;
            switch (role)
            {
                case "SuperAdmin":
                    return RedirectToAction("SuperAdminLogin", returnUrl);
                case "Parent":
                    return RedirectToAction("ParentStaffLogin", returnUrl);
                case "Staff":
                    return RedirectToAction("ParentStaffLogin", returnUrl);
                default:
                    return new RedirectResult("~/Home/Index");
            }

        }

        [AllowAnonymous]
        public ActionResult ParentStaffLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
         


        //Demo Login
        [AllowAnonymous]
        public ActionResult SuperAdminLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl, string command)
        {
            if (!ModelState.IsValid)
            {
                return View(loginView(command), model);
            }

            var user = UserManager.FindByName(model.Email);

            if (user != null && user.Disabled)
            {
                ModelState.AddModelError("", "Your account is disabled!");
                return View(loginView(command), model);
            }

            if (user != null && !user.TermsAgreed && !model.TermsAgreed)
            {
                ModelState.AddModelError("", "Terms must be agreed before signing in");
                return View(loginView(command), model);
            }
            if (user != null && !user.TermsAgreed && model.TermsAgreed)
            {
                user.TermsAgreed = model.TermsAgreed;
                UserManager.Update(user);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password.Trim(), model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    // var user = UserManager.FindByName(model.Email);
                    return RedirectToLocal(returnUrl, user.Id.ToString());
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(loginView(command), model);
            }
        }
        private string loginView(string command)
        {

            if (string.IsNullOrEmpty(command))
            {
                return "Login";
            }
            else
            {
                return command;
            }

        }



        #region Create User
        [Authorize]
        public ActionResult CreateUser()
        {
            var RoleList = appDbContext.Roles.Where(a=>a.Name != "SuperAdmin").ToList();
            ViewBag.RolesId = new SelectList(RoleList, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser(UserRegisterViewModel model, FormCollection frmFields)
        {


            if (string.IsNullOrEmpty(Request.Form["RolesId"]))
            {
                goto Fail;
            }
            var RoleId = Request.Form["RolesId"]; 

            if (ModelState.IsValid)
            {
                string bodyHtml = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/EmailTemplates/WelcomeEmailTemplate.html")))
                {
                    bodyHtml = reader.ReadToEnd();
                }
                var user = new User { NickName = model.Name, UserName = model.Email, Email = model.Email, TermsAgreed = true };
                var pwd = PasswordGenerator.GeneratePWD();
                var result = await UserManager.CreateAsync(user, pwd);
                var UserRole = appDbContext.Roles.Where(rl => rl.Id == RoleId).Single().Name;
                if (result.Succeeded)
                {
                    switch (UserRole)
                    {
                        case "Admin":
                            UserManager.AddToRole(user.Id, School_AppIn_Model.Common.Constants.ROLE_ADMIN);
                            break;
                        case "Staff":
                            UserManager.AddToRole(user.Id, School_AppIn_Model.Common.Constants.ROLE_STAFF);
                            break;
                        case "Student":
                            UserManager.AddToRole(user.Id, School_AppIn_Model.Common.Constants.ROLE_STUDENT);
                            break;
                        case "Parent":
                            UserManager.AddToRole(user.Id, School_AppIn_Model.Common.Constants.ROLE_PARENT);
                            break;
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
                    return RedirectToLocal("", user.Id);
                }
                AddErrors(result);
            }
            Fail:
            return View(model);
        }

        public string PopoulateWelcomeEmailTemplate(string bodyHtml, User user, string username, string pw)
        {


            var wBodyHtml = bodyHtml.Replace("{{USER-NAME}}", user.NickName)
            .Replace("{{USER-ADDRESS}}", (user.Email ?? String.Empty))
            .Replace("{{USERNAME}}", username)
            .Replace("{{PASSWORD}}", pw);

            return wBodyHtml;
        }






        #endregion







        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                   // return RedirectToLocal(model.ReturnUrl, null);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }


        [Authorize]
        public ActionResult SiteAdmin()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SiteAdmin(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password.Trim());
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, School_AppIn_Model.Common.Constants.ROLE_ADMIN);
                    return RedirectToLocal("", user.Id);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();

        }

     

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
       
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                  //  return RedirectToLocal(returnUrl, AuthenticationManager.AuthenticationResponseGrant.Identity.GetUserId());
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        //return RedirectToLocal(returnUrl, null);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }



        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        

        public ActionResult RedirectToLocal(string returnUrl, string userId)
        {


            if (UserManager.IsInRole(userId, School_AppIn_Model.Common.Constants.ROLE_SUPERADMIN))
            {
                return RedirectToAction("SuperAdminHome", "Protected");
            }
            if (UserManager.IsInRole(userId, School_AppIn_Model.Common.Constants.ROLE_ADMIN))
            {
                return RedirectToAction("AdminHome", "Protected");
            }
            if (UserManager.IsInRole(userId, School_AppIn_Model.Common.Constants.ROLE_PARENT))
            {
                return RedirectToAction("ParentHome", "Protected");
            }
            if (UserManager.IsInRole(userId, School_AppIn_Model.Common.Constants.ROLE_STAFF))
            {
                return RedirectToAction("StaffHome", "Protected");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}