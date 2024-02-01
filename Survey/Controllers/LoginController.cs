using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;
using Survey.Models;
using Survey.SurveyEntity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Identity.Web.OWIN;
using System.IO;


namespace Survey.Controllers
{
    public class LoginController : Controller
    {
        #region "Declaration"

        ProjectSurveyEntities ent = new ProjectSurveyEntities();
        LoginModel ObjLogin = new LoginModel();
        ChangePassword ObjChangePassword = new ChangePassword();
        string UserName;
        #endregion
        

        [HttpGet]
        public ActionResult LoginPage()
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                UserName = ClaimsPrincipal.Current.FindFirst("preferred_username").Value;

                ObjLogin = ent.Database.SqlQuery<LoginModel>("usp_LoginSSO @p0", UserName).Select(x => x).FirstOrDefault();

                if (ObjLogin != null)
                {
                    Session["UserID"] = ObjLogin.UserID;
                    Session["UserName"] = UserName;
                    Session["FirstName"] = ObjLogin.FirstName;
                    Session["Role"] = ObjLogin.UserRole;
                    Session["RoleName"] = ObjLogin.UR_Role;
                    Session["Image"] = ObjLogin.Images;
                   
                    return RedirectToAction("GetClData", "Questions");

                    // return Json(new { success = true, redirectTo = Url.Action("GetClData", "Questions") });
                }
                else
                {
                    Session.Clear();
                    Session.RemoveAll();
                    HttpContext.GetOwinContext().Authentication.SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);

                    TempData["ErrorMessage"] = "Invalid username or password.";
                    ViewBag.ErrorMessage = TempData["ErrorMessage"];
                    return RedirectToAction("LoginPage", "Login");
                }
            }
            //return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword ObjChangePassword)
        {
            if(ObjChangePassword.CFNewPassword == ObjChangePassword.NewPassword)
            {
               
                string Username = Session["UserName"].ToString();
          
                ObjChangePassword = ent.Database.SqlQuery<ChangePassword>("Usp_ChangePassword @p0, @p1, @p2", Username, ObjChangePassword.OldPassword, ObjChangePassword.NewPassword).Select(x => x).FirstOrDefault();         
               

                if(ObjChangePassword != null)
                {
                    TempData["ErrorMessage"] = "Password Changed Successfully";
                    ViewBag.ErrorMessage2 = TempData["ErrorMessage"];
                    return View();
                }
                else
                {
                    TempData["ErrorMessage"] = "Old Password is incorrect ";
                    ViewBag.ErrorMessage = TempData["ErrorMessage"];
                    return View();
                }

            }
            else
            {
                TempData["ErrorMessage"] = "New Password and Confirm New Password is not same.";
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                return View();
            }
          
        }


        [HttpGet]
        public ActionResult LoginPage2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPage(LoginModel objLoginModel)
        {                   

            //ObjLogin = ent.Database.SqlQuery<LoginModel>("usp_Login @p0, @p1", objLoginModel.UserName, objLoginModel.Password).FirstOrDefault();
            ObjLogin = ent.Database.SqlQuery<LoginModel>("usp_Login @p0, @p1", objLoginModel.UserName, objLoginModel.Password).Select(x => x).FirstOrDefault();

            if (ObjLogin != null)
            {
                Session["UserID"] = ObjLogin.UserID;
                Session["UserName"] = objLoginModel.UserName;
                Session["FirstName"] = ObjLogin.FirstName;
                Session["Role"] = ObjLogin.UserRole;
                Session["RoleName"] = ObjLogin.UR_Role;
                Session["Image"] = ObjLogin.Images;

                //if (ObjLogin.UserRole == "1")
                //{
                //    return RedirectToAction("GetClData", "Questions");
                //}

                //else
                //{
                //    return RedirectToAction("FeedbackDashboard", "Feedback");
                //}
                return RedirectToAction("GetClData", "Questions");

                // return Json(new { success = true, redirectTo = Url.Action("GetClData", "Questions") });
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid username or password.";
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                return View();
            }


            //else
            //{
            //        TempData["ErrorMessage"] = "Invalid username or password.";
            //    return View(objLoginModel);
            //    // return Json(new { success = false, errorMessage = "Invalid username or password." });
            //}
        }


        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            HttpContext.GetOwinContext().Authentication.SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
            return RedirectToAction("LoginPage", "Login");
        }
        public void SignIn()
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
                     new Microsoft.Owin.Security.AuthenticationProperties { RedirectUri = "https://surveyspyder.azurewebsites.net/Login/LoginPage" },
                     OpenIdConnectAuthenticationDefaults.AuthenticationType
                 );

                // new Microsoft.Owin.Security.AuthenticationProperties { RedirectUri = "http://localhost:53598/Login/LoginPage" },
            }
            else
            {               
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var username = claimsIdentity.FindFirst(ClaimTypes.Name);
       
                if (username != null)
                {
                    Session["UserName"] = username.Value;
                }
            }
        }


        public void SignOut()
        {
            Session.Clear();
            Session.RemoveAll();
            // Send an OpenID Connect sign-out request.
            HttpContext.GetOwinContext().Authentication.SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
        }


    }
}