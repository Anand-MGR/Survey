using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Survey.Controllers;
using Survey.Models;
using Survey.SurveyEntity;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

namespace Survey.Controllers
{
    public class UserController : Controller
    {

        #region "Declaration"

        ProjectSurveyEntities SuvEnt = new ProjectSurveyEntities();
        AssignProjectSurveyModel objProject = new AssignProjectSurveyModel();
        AssignSurveyMasterModel ObjSurveyMaster = new AssignSurveyMasterModel();
        List<AssignSurveyMasterModel> LstSurveyMaster = new List<AssignSurveyMasterModel>();
        UserModel objUserModel = new UserModel();

        #endregion
        public ActionResult UserDashboard()
        {
            if (Session["UserName"] == null)
            {
                 return RedirectToAction("Loginpage", "Login");
            }
            ObjSurveyMaster.LstUserModel = SuvEnt.Database.SqlQuery<UserModel>("usp_ListUserDashboardDetails").ToList();
            ObjSurveyMaster.ddlUserStatus = SuvEnt.Database.SqlQuery<UserStatus>("usp_GetUserstatusUST").Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            return View(ObjSurveyMaster);
        }

        // GET: User
        public ActionResult UserManagement()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ObjSurveyMaster.ddlUserStatus = SuvEnt.Database.SqlQuery<UserStatus>("usp_GetUserstatusUST").Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return View(ObjSurveyMaster);
        }

      
        [HttpPost]
        public ActionResult UserManagement(UserModel objUserModel)
        {
            if (Session["UserName"] == null)
            {
                 return RedirectToAction("Loginpage", "Login");
            }
            var UserId = Session["UserID"].ToString();

            ObjSurveyMaster.ddlUserStatus = SuvEnt.Database.SqlQuery<UserStatus>("usp_GetUserstatusUST").Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            var RoleName = ObjSurveyMaster.ddlUserStatus.Where(x => x.Value.Contains(objUserModel.U_Role))
                             .Select(x => new SelectListItem { Value = x.Text.ToString(), Text = x.Text })
                                          .FirstOrDefault();

            SuvEnt.Database.ExecuteSqlCommand("usp_AddUserManagement @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10"
            , objUserModel.U_ID, objUserModel.U_FirstName, objUserModel.U_LastName, objUserModel.U_UserName, objUserModel.U_Password, objUserModel.U_Email, objUserModel.U_Mobile, objUserModel.U_Role
             , objUserModel.U_Status, UserId, UserId);

            if (objUserModel.U_ID == 0)
            {
                SendEmail(objUserModel.U_Email, objUserModel.U_FirstName, objUserModel.U_LastName, objUserModel.U_Mobile, " Welcome to Proven IT ", RoleName.Value, objUserModel.U_UserName);

            }
            else if (objUserModel.U_Status == false)
            {
                SendEmail2(objUserModel.U_Email, objUserModel.U_FirstName, objUserModel.U_LastName, objUserModel.U_Mobile, " Welcome to Proven IT ", RoleName.Value, objUserModel.U_UserName);
                return RedirectToAction("UserDashboard", "User");
            }

            return RedirectToAction("UserDashboard", "User");
        }

        public void SendEmail2(string U_Email, string U_FirstName, string U_LastName, string U_Mobile, string subject, string Userrole, string Username)
        {
            //UserEmail = "devp1@wesucceed.com";

            MailMessage mail = new MailMessage();
            mail.To.Add(U_Email);
            mail.From = new MailAddress("wesucceed.dev1@gmail.com");
            //mail.Body = "Use The below URL For Rating \n" + link + "";
            //mail.Subject = project + " - " + Subject;
            mail.Subject = "Account Deactivation Notice";
            mail.IsBodyHtml = true;
            string emailBody = $"<p>Dear {U_FirstName + " " + U_LastName},<br><br>" +
             $"I hope this email finds you well.This is to inform you that your user account  will be deactivated..<br><br>" +
             $"The decision to deactivate your account has been made based on terms of service..<br><br>" +
             $"Please note that you will no longer be able to access your account and associated services after the deactivation date.<br><br>" +
             $"If you believe this deactivation is in error or if you have any concerns, please reach out to our support team as soon as possible.<br><br>" +
             $"We appreciate your understanding and cooperation in this matter.If you wish to reactivate your account, please contact our support team, and they will assist you through the necessary steps.<br><br>" +
             "Thank you for your cooperation.<br><br>" +
             "Best regards,.<br><br>" +
             "<b style='color: orange;font-family: Arial, Helvetica, sans-serif; font-size:20px;'>Jacob Janik</b><br>" +
             "Project Manager Lead<br>" +
             "<b>Proven IT</b><br><br>" +
             "<img src='~/images/smartphone_254638.png' style='width:10px;height:10px;'> <span style='color: black;'>(708) 407 - 2947</span><br>" +
             "<img src='~/ images / pin_3177361.png' style='width:10px;height:10px;'> <span style='color: black;'>18450 Crossing Dr, Tinley Park, IL 60487</span><br>" +
             $"<img src='link_logo_url' style='width:10px;height:10px;'> <a href='http://www.provenit.com' style='color: blue;'>www.provenit.com</a></p>";


            mail.Body = emailBody;

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.EnableSsl = true;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new NetworkCredential("wesucceed.dev1@gmail.com", "pvwvrkcnupmlpxlj"); // Use Token 

            try
            {
                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SendEmail(string U_Email, string U_FirstName, string U_LastName, string U_Mobile, string subject, string Userrole, string Username)
        {
            //UserEmail = "devp1@wesucceed.com";

            MailMessage mail = new MailMessage();
            mail.To.Add(U_Email);
            mail.From = new MailAddress("wesucceed.dev1@gmail.com");
            //mail.Body = "Use The below URL For Rating \n" + link + "";
            //mail.Subject = project + " - " + Subject;
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            string emailBody = $"<p>Dear {U_FirstName + " " + U_LastName},<br><br>" +
                $"We are thrilled to welcome you to ProvenIT!.<br><br>" +
                $"Your registration as a <b>{Userrole} </b> has been successfully completed, and we are excited to have you as part of our community.<br><br>" +
                $" Your account details are as follows:<br>" +
                $"Username: {Username}<br>" +
                $"Role: {Userrole}<br>" +
                $"Email: {U_Email}<br><br>" +
                "We encourage you to explore our platform and take full advantage of the resources and tools available to you..<br>" +
                "If you have any questions or need assistance, please don't hesitate to reach out to our support team.<br>" +
                "Thank you for joining us. We look forward to seeing you thrive and succeed within our community!.<br>" +
                "Best regards,.<br><br>" +
                "<b style='color: orange;font-family: Arial, Helvetica, sans-serif; font-size:20px;'>Jacob Janik</b><br>" +
                "Project Manager Lead<br>" +
                "<b>Proven IT</b><br><br>" +
                "<img src='~/images/smartphone_254638.png' style='width:10px;height:10px;'> <span style='color: black;'>(708) 407 - 2947</span><br>" +
                "<img src='~/ images / pin_3177361.png' style='width:10px;height:10px;'> <span style='color: black;'>18450 Crossing Dr, Tinley Park, IL 60487</span><br>" +
                $"<img src='link_logo_url' style='width:10px;height:10px;'> <a href='http://www.provenit.com' style='color: blue;'>www.provenit.com</a></p>";

            mail.Body = emailBody;

            //string emailBody = $"Hi {recipientName},\n\n" +
            //    $"Thank you for your participation on the {project} project.\n\n" +
            //    "As part of our continuing efforts to provide the best experience for everyone involved, we are asking you to answer just a few questions about your experience and how we could be better.\n\n" +
            //    $"These questions are related to you as the {recipientRole} and {personsName} as the {personsRole}.\n\n" +
            //    $"Please use the following link to answer a few questions about your experience:\n\n" +
            //    $"{project} Survey ({link})\n\n" +
            //    "Thank you again for your time.\n" +
            //    "Please feel free to contact me for any reason.\n\n" +
            //    "Jacob Janik\n" +
            //    "Project Manager Lead \n" +
            //    "Proven IT\n\n" +
            //    ". (708) 407 - 2947\n" +
            //    ". 18450 Crossing Dr, Tinley Park, IL 60487\n" +
            //    ". www.provenit.com\n";

            //mail.Body = emailBody;

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.EnableSsl = true;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new NetworkCredential("wesucceed.dev1@gmail.com", "pvwvrkcnupmlpxlj"); // Use Token 

            try
            {
                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #region "Search"

        [HttpPost]
        public ActionResult SearchRecords(string Value)
        {
            if (Session["UserName"] == null)
            {
                 return RedirectToAction("Loginpage", "Login");
            }
            // depot = Session["WareHouse"].ToString();
            ObjSurveyMaster.LstUserModel = SuvEnt.Database.SqlQuery<UserModel>("usp_GetUserDetails @p0", Value).ToList();
            return PartialView("_UserDashboard", ObjSurveyMaster);
        }

        #endregion

        #region "Edit"

        public ActionResult EditUser(int? U_ID)
        {
            if (Session["UserName"] == null)
            {
                 return RedirectToAction("Loginpage", "Login");
            }
            ViewData["Mode"] = "edit";
            ObjSurveyMaster.objUserModel = SuvEnt.Database.SqlQuery<UserModel>("usp_GetUserDetailsBasedID @p0", U_ID).FirstOrDefault();
            ObjSurveyMaster.ddlUserStatus = SuvEnt.Database.SqlQuery<UserStatus>("usp_GetUserstatusUST").Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            return View("UserManagement", ObjSurveyMaster);
        }

        #endregion
    }
}
