﻿using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
using System.Web.Mvc;
using Survey.Models;
using Survey.SurveyEntity;
using System.Data;
using System.Configuration;
using System.Net;
using System.Net.Mail;

//using System.IO;
//using Newtonsoft.Json;
//using System.Data.SqlClient;

//using System.Net.NetworkInformation;


namespace Survey.Controllers
{
    public class ProjectSurveyController : Controller
    {
        #region "Declaration"

        ProjectSurveyEntities SuvEnt = new ProjectSurveyEntities();
        AssignProjectSurveyModel objProject = new AssignProjectSurveyModel();
        AssignSurveyMasterModel ObjSurveyMaster = new AssignSurveyMasterModel();
        List<AssignSurveyMasterModel> LstSurveyMaster = new List<AssignSurveyMasterModel>();
        ProjectSurveyModel objProjectsurveymodel = new ProjectSurveyModel();

        string Role, UserId;
        #endregion

        // GET: ProjectSurvey
        public ActionResult Index()
        {
            return View();
        } 

        #region "Assign Screen"

        public ActionResult AssignSurvey()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            objProject.lstProject = SuvEnt.P_Project.Where(P => P.P_IsActive == true).AsEnumerable()
            .Select(P => new SelectListItem
            {
                Value = P.P_ID.ToString(),
                Text = P.P_Name
            }).ToList();
            return View(objProject);
        }

        [HttpPost]
        public ActionResult AssignSurvey(AssignProjectSurveyModel ObjSurveyModel)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            string LoginID = Session["UserID"].ToString();
            //SuvEnt.Database.SqlQuery<IEnumerable<string>>("usp_AssignSurvey @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10"
            //   , ObjSurveyModel.ProjectName, ObjSurveyModel.Customer, ObjSurveyModel.ClientPM, ObjSurveyModel.ClientPE, ObjSurveyModel.HelpDeskPM, ObjSurveyModel.HelpDeskPE, ObjSurveyModel.PMPE
            //   , ObjSurveyModel.PEPM, ObjSurveyModel.SalePM, ObjSurveyModel.AssignDate, LoginID).ToList();

            SuvEnt.Database.ExecuteSqlCommand("usp_AssignSurvey @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10"
             , ObjSurveyModel.ProjectName, ObjSurveyModel.Customer, ObjSurveyModel.ClientPM, ObjSurveyModel.ClientPE, ObjSurveyModel.HelpDeskPM, ObjSurveyModel.HelpDeskPE, ObjSurveyModel.PMPE
             , ObjSurveyModel.PEPM, ObjSurveyModel.SalePM, ObjSurveyModel.AssignDate, LoginID);

            return View();           
        }

        #endregion

        #region 

        //public ActionResult GetAssign()
        //{
        //    ObjSurveyMaster.AssignSurveyLst = new List<AssignSurveyModel>();
        //    Role = Session["Role"].ToString();
        //    UserId = Session["UserID"].ToString();
        //    ObjSurveyMaster.AssignSurveyLst = SuvEnt.Database.SqlQuery<AssignSurveyModel>("usp_GetAssignment @p0,@p1,@p2,@p3", Role, UserId, 1, "1").ToList();          
        //    return View(ObjSurveyMaster);
        //}
        public ActionResult GetAssign()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ObjSurveyMaster.AssignSurveyLst = new List<AssignSurveyModel>();
            Role = Session["Role"].ToString();  // "ADMIN";// Session["Role"].ToString();
            UserId = Session["UserID"].ToString();  // "1";// Session["UserID"].ToString();
            ObjSurveyMaster.AssignSurveyLst = SuvEnt.Database.SqlQuery<AssignSurveyModel>("usp_GetAssignment @p0,@p1,@p2,@p3", Role, UserId, 1, "1").ToList();

            return View(ObjSurveyMaster);
        }
        #endregion

#region 

        public ActionResult ProjectSurveyAssign()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }

            var objProjectsurveymodel = TempData["ObjProjectSurveyMaster"];

            if (objProjectsurveymodel != null)
            {
                ViewData["Mode"] = "edit";
            }
            return View(objProjectsurveymodel);
        }


        [HttpPost]
        public ActionResult ProjectSurveyAssign(ProjectMasterModel model)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            var UserId = Session["UserID"].ToString();
            //string DefaultURL = ConfigurationManager.AppSettings["URL"];
            //string DefaultURL = "www.survey.com/Questions/";
            string DefaultURL = "http://localhost:53598/Questions/";
            model.objProjectsurveymodel.PS_ID = model.objProjectsurveymodel.PS_ID;
            model.objProjectsurveymodel.PS_Date = DateTime.Now;
            model.objProjectsurveymodel.PS_Client_PM_link = DefaultURL +"CLPM?Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_Client_PM_Status = false;
            model.objProjectsurveymodel.PS_Client_PE_link = DefaultURL + "CLPE?Id=" + GetRandomnumber(); 
            model.objProjectsurveymodel.PS_Client_PE_Status = false;
            model.objProjectsurveymodel.PS_PM_link = DefaultURL + "PMPE?Id=" + GetRandomnumber(); 
            model.objProjectsurveymodel.PS_PM_Status = false;
            model.objProjectsurveymodel.PS_PE_link = DefaultURL + "PEPM?Id=" + GetRandomnumber(); 
            model.objProjectsurveymodel.PS_PE_Status = false;
            model.objProjectsurveymodel.PS_HD_PM_link = DefaultURL + "HDPM?Id=" + GetRandomnumber(); 
            model.objProjectsurveymodel.PS_HD_PM_Status = false;
            model.objProjectsurveymodel.PS_HD_PE_link = DefaultURL + "HDPE?Id=" + GetRandomnumber(); 
            model.objProjectsurveymodel.PS_HD_PE_Status = false;
            model.objProjectsurveymodel.PS_SA_PM_link = DefaultURL + "SAPM?Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_SA_PM_Status = false;
            model.objProjectsurveymodel.PS_SA_PE_link = DefaultURL + "SAPE?Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_SA_PE_Status = false;
            model.objProjectsurveymodel.PS_PE_SA_link = DefaultURL + "PESA?Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_PE_SA_Status = false;
            model.objProjectsurveymodel.PS_PM_SA_link = DefaultURL + "PMSA?Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_PM_SA_Status = false;

            model.objProjectsurveymodel.PS_Status = false;
            model.objProjectsurveymodel.PS_CreatedBY = UserId;

            SuvEnt.Database.ExecuteSqlCommand("usp_AssignProjectSurvey @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35"
              ,model.objProjectsurveymodel.PS_ID, model.objProjectsurveymodel.PS_Date, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_ProjectCode, model.objProjectsurveymodel.PS_ClientName, model.objProjectsurveymodel.PS_Client_Email
             , model.objProjectsurveymodel.PS_Client_PM_link, model.objProjectsurveymodel.PS_Client_PM_Status , model.objProjectsurveymodel.PS_Client_PE_link, model.objProjectsurveymodel.PS_Client_PE_Status, model.objProjectsurveymodel.PS_PM_Name
             , model.objProjectsurveymodel.PS_PM_Email, model.objProjectsurveymodel.PS_PM_link, model.objProjectsurveymodel.PS_PM_Status, model.objProjectsurveymodel.PS_PE_Name, model.objProjectsurveymodel.PS_PE_Email
             , model.objProjectsurveymodel.PS_PE_link, model.objProjectsurveymodel.PS_PE_Status, model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_HD_Email, model.objProjectsurveymodel.PS_HD_PM_link
             , model.objProjectsurveymodel.PS_HD_PM_Status, model.objProjectsurveymodel.PS_HD_PE_link, model.objProjectsurveymodel.PS_HD_PE_Status, model.objProjectsurveymodel.PS_SA_Name, model.objProjectsurveymodel.PS_SA_Email,
             model.objProjectsurveymodel.PS_SA_PM_link, model.objProjectsurveymodel.PS_SA_PM_Status,model.objProjectsurveymodel.PS_SA_PE_link, model.objProjectsurveymodel.PS_SA_PE_Status, model.objProjectsurveymodel.PS_PE_SA_link, model.objProjectsurveymodel.PS_PE_SA_Status,
            model.objProjectsurveymodel.PS_PM_SA_link, model.objProjectsurveymodel.PS_PM_SA_Status, model.objProjectsurveymodel.PS_Status, model.objProjectsurveymodel.PS_CreatedBY);

            //Client will get 2 email, CLPM, CLPE,PMPE,PEPM,HD Seperate email.
            SendEmail(model.objProjectsurveymodel.PS_Client_Email, model.objProjectsurveymodel.PS_Client_PM_link, "Rating To CLPM - " + model.objProjectsurveymodel.PS_PM_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_ClientName, "Client", model.objProjectsurveymodel.PS_PM_Name, "Project Manager");
            SendEmail(model.objProjectsurveymodel.PS_Client_Email, model.objProjectsurveymodel.PS_Client_PE_link, "Rating To CLPE - " + model.objProjectsurveymodel.PS_PE_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_ClientName, "Client", model.objProjectsurveymodel.PS_PE_Name, "Project Engineer");

            //HD
            SendEmail(model.objProjectsurveymodel.PS_HD_Email, model.objProjectsurveymodel.PS_HD_PM_link, "Rating To HDPM - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_HD_Name, "Help Desk", model.objProjectsurveymodel.PS_PM_Name, "Project Manager");
            SendEmail(model.objProjectsurveymodel.PS_HD_Email, model.objProjectsurveymodel.PS_HD_PE_link, "Rating To HDPE - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_HD_Name, "Help Desk", model.objProjectsurveymodel.PS_PE_Name, "Project Engineer");

            //PE-PM
            SendEmail(model.objProjectsurveymodel.PS_PE_Email, model.objProjectsurveymodel.PS_PE_link, "Rating To PEPM - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_PE_Name, "Project Engineer", model.objProjectsurveymodel.PS_PM_Name, "Project Manager");
            SendEmail(model.objProjectsurveymodel.PS_PM_Email, model.objProjectsurveymodel.PS_PM_link, "Rating To PMPE - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_PM_Name, "Project Manager", model.objProjectsurveymodel.PS_PE_Name, "Project Engineer");

            //SA-PM
           // SendEmail(model.objProjectsurveymodel.PS_SA_Email, model.objProjectsurveymodel.PS_SA_PM_link, "Rating To SAPM - " + model.objProjectsurveymodel.PS_SA_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_SA_Name, "Solution Architect ", model.objProjectsurveymodel.PS_PM_Name, "Project Manager");
            SendEmail(model.objProjectsurveymodel.PS_SA_Email, model.objProjectsurveymodel.PS_SA_PE_link, "Rating To SAPE - " + model.objProjectsurveymodel.PS_SA_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_SA_Name, "Solution Architect ", model.objProjectsurveymodel.PS_PE_Name, "Project Engineer");

            //PE-PM
            SendEmail(model.objProjectsurveymodel.PS_PE_Email, model.objProjectsurveymodel.PS_PE_SA_link, "Rating To PESA - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_PE_Name, "Project Engineer", model.objProjectsurveymodel.PS_SA_Name, "Solution Architect");
            SendEmail(model.objProjectsurveymodel.PS_PM_Email, model.objProjectsurveymodel.PS_PM_SA_link, "Rating To PMSA - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_PM_Name, "Project Manager", model.objProjectsurveymodel.PS_SA_Name, "Solution Architect");


            return RedirectToAction("GetCLData","Questions");
        }

        public string GetRandomnumber()
        {
            Random generator = new Random();
            string r = generator.Next(100000, 9000000).ToString();
            return r;
        }

        public void SendEmail(string UserEmail, string link, string Subject, string project, string recipientName, string recipientRole, string personsName, string personsRole)
        {
         
            MailMessage mail = new MailMessage();
            mail.To.Add(UserEmail);          
           // mail.From = new MailAddress("noreply@SurveySpyder.com");          
            mail.From = new MailAddress("notifications@WeSucceed.com");          
            mail.Body = "Use The below URL For Rating \n" + link + "";         
            mail.Subject = project + " -  Project - How did we do?";
            mail.IsBodyHtml = true;
            string emailBody = $"<p>Hi {recipientName},<br><br>" +
                $"Thank you for your participation on the {project} project.<br><br>" +
                "As part of our continuing efforts to provide the best experience for everyone involved, we are asking you to answer just a few questions about your experience and how we could be better.<br><br>" +
                $"These questions are related to you as the {recipientRole} and {personsName} as the {personsRole}.<br><br>" +
                $"Please use the following link to answer a few questions about your experience:<br><br>" +
                $"<a href='{link}'>{project} Survey</a><br><br>" +
                "Thank you again for your time.<br>" +
                "Please feel free to contact me for any reason.<br><br>" +
                "<b style='color: orange;font-family: Arial, Helvetica, sans-serif; font-size:20px;'>Jacob Janik</b><br>" +
                "Project Manager Lead<br>" +
                "<b>Proven IT</b><br><br>" +
                "<img src='~/images/smartphone_254638.png' style='width:10px;height:10px;'> <span style='color: black;'>(708) 407 - 2947</span><br>" +
                "<img src='~/ images / pin_3177361.png' style='width:10px;height:10px;'> <span style='color: black;'>18450 Crossing Dr, Tinley Park, IL 60487</span><br>" +
                $"<img src='link_logo_url' style='width:10px;height:10px;'> <a href='http://www.provenit.com' style='color: blue;'>www.provenit.com</a></p>";

            mail.Body = emailBody;
 
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
      

            SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
       
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = false;
           // SmtpServer.Credentials = new NetworkCredential("noreply@SurveySpyder.com", "SURapp_WSS123#");
            SmtpServer.Credentials = new NetworkCredential("notifications@WeSucceed.com", "WSS*Notify#1");
          
            try
            {
                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
       
     
        }
        #endregion
        

        public ActionResult ProjectReport()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ObjSurveyMaster.LsttotalReport = SuvEnt.Database.SqlQuery<TotalReportModel>("usp_GetTotalReportForProject").ToList();
            ObjSurveyMaster.LsttotalPMReport = SuvEnt.Database.SqlQuery<TotalPMReportModel>("usp_GetProjectManagerReport").ToList();
            ObjSurveyMaster.LsttotalPEReport = SuvEnt.Database.SqlQuery<TotalPEReportModel>("usp_GetProjectEngineerReport").ToList();
            ObjSurveyMaster.LsttotalSAReport = SuvEnt.Database.SqlQuery<TotalSAReportModel>("usp_GetSAReport").ToList();
            return View(ObjSurveyMaster);
            //return View(ObjSurveyMaster);
        }

        [HttpPost]
        public ActionResult ProjectReport(string selectedQuadrant, string selectedyear)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }

            string[] months = selectedQuadrant.Split('-');
            string startMonth = months[0];
            string endMonth = months[1];
            //var obj = new AssignSurveyMasterModel();
            LstSurveyMaster.Clear();
            ObjSurveyMaster.LsttotalReport = SuvEnt.Database.SqlQuery<TotalReportModel>("usp_GetTotalReportForProject @p0,@p1,@p2", selectedyear, startMonth, endMonth).ToList();
            ObjSurveyMaster.LsttotalPMReport = SuvEnt.Database.SqlQuery<TotalPMReportModel>("usp_GetProjectManagerReport @p0,@p1,@p2", selectedyear, startMonth, endMonth).ToList();
            ObjSurveyMaster.LsttotalPEReport = SuvEnt.Database.SqlQuery<TotalPEReportModel>("usp_GetProjectEngineerReport @p0,@p1,@p2", selectedyear, startMonth, endMonth).ToList();
            ObjSurveyMaster.LsttotalSAReport = SuvEnt.Database.SqlQuery<TotalSAReportModel>("usp_GetSAReport @p0,@p1,@p2", selectedyear, startMonth, endMonth).ToList();
            return PartialView("_ProjectReport",ObjSurveyMaster);
        }

        public ActionResult ProjectReport2()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            return View();
        }

        #region "Auto Suggestion"
        public ActionResult GetSuggestions(string input , string URid)
       {
            List<string> clientnameList = new List<string>();
            if (URid =="6")
            {
            var suggestions = SuvEnt.Database.SqlQuery<ProjectSurveyModel>("Usp_GetClientData @p0", input).ToList();
                
                foreach(var x in suggestions)
                {
                    clientnameList.Add(x.ClientName);
                }
 
                //var suggestions = SuvEnt.ClientDetails.Where(u => u.ClientName.Contains(input)).Select(u => u.ClientName).ToList();
                return Json(clientnameList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var suggestions = SuvEnt.U_User.Where(u => u.U_Role == URid && u.U_FirstName.Contains(input)).Select(u => u.U_FirstName).ToList();
                return Json(suggestions, JsonRequestBehavior.AllowGet);
            }
             
        }

        // This action method will return the client email based on the client name.
        public ActionResult GetClientEmail(string clientName , string URid)
        {
            List<string> clientnameList = new List<string>();

            if (URid == "6")
            {
                var suggestions = SuvEnt.Database.SqlQuery<ProjectSurveyModel>("Usp_GetClientemail @p0", clientName).ToList();

                foreach (var x in suggestions)
                {
                    clientnameList.Add(x.ClientName);
                }

                //var suggestions = SuvEnt.ClientDetails.Where(u => u.ClientName.Contains(input)).Select(u => u.ClientName).ToList();
                return Json(clientnameList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var client = SuvEnt.U_User.Where(x => x.U_FirstName == clientName).Select(x => x.U_Email).FirstOrDefault();
                return Json(client, JsonRequestBehavior.AllowGet);
            }
          
        }
        #endregion

        [HttpPost]
        public JsonResult SearchAssetForPartsPickOrder(string Name2, string URid)
        {
            List<UserModel> ObjList = new List<UserModel>();
            ObjList = SuvEnt.Database.SqlQuery<UserModel>("usp_GetUserBasedOnUserrole @p0, @p1", URid, Name2).ToList();
            List<string> AssetIds = new List<string>();
            if (Session["AssetIds"] != null)
            {
                AssetIds = (List<string>)Session["AssetIds"];

                foreach (var o in AssetIds)
                {
                    var asset = ObjList.Single(r => r.U_FirstName == o);
                    ObjList.Remove(asset);
                }
            }

            var Name = (from N in ObjList
                            // where N.A_AssetNo.StartsWith(AssetNo)
                        select new { N.U_FirstName });
            return Json(Name, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Thankyou()
        {
            return View();
        }


        //[HttpPost]
        //public JsonResult Index(string Name2, string URid)
        //{

        //    List<UserModel> LstUserModel = new List<UserModel>();
        //    List<ProjectSurveyModel> objPSM = new List<ProjectSurveyModel>();
        //    ProjectSurveyModel testPSM = new ProjectSurveyModel();

        //    LstUserModel = SuvEnt.Database.SqlQuery<UserModel>("usp_GetUserBasedOnUserrole @p0, @p1", URid, Name2).ToList();

        //    var names = (from N in LstUserModel
        //                 where N.U_FirstName.IndexOf(Name2, StringComparison.OrdinalIgnoreCase) >= 0
        //                 select new { PS_ClientName = N.U_FirstName }).ToList();


        //    return Json(names, JsonRequestBehavior.AllowGet);
        //}

    }



}
