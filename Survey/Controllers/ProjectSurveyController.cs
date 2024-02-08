using System;
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
        ProjectMasterModel ObjProjectSurveyMaster = new ProjectMasterModel();

        ProjectMasterModel ObjprojectMaster = new ProjectMasterModel();
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
            model.objProjectsurveymodel.PS_Client_PM_link = DefaultURL + "RatingPage?QType=CLPM" + "&Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_Client_PM_Status = false;
            model.objProjectsurveymodel.PS_Client_PE_link = DefaultURL + "RatingPage?QType=CLPE" + "&Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_Client_PE_Status = false;
            model.objProjectsurveymodel.PS_PM_link = DefaultURL + "RatingPage?QType=PMPE" + "&Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_PM_Status = false;
            model.objProjectsurveymodel.PS_PE_link = DefaultURL + "RatingPage?QType=PEPM" + "&Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_PE_Status = false;
            model.objProjectsurveymodel.PS_HD_PM_link = DefaultURL + "RatingPage?QType=HDPM" + "&Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_HD_PM_Status = false;
            model.objProjectsurveymodel.PS_HD_PE_link = DefaultURL + "RatingPage?QType=HDPE" + "&Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_HD_PE_Status = false;
            model.objProjectsurveymodel.PS_SA_PM_link = DefaultURL + "RatingPage?QType=SAPM" + "&Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_SA_PM_Status = false;
            model.objProjectsurveymodel.PS_SA_PE_link = DefaultURL + "RatingPage?QType=SAPE" + "&Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_SA_PE_Status = false;
            model.objProjectsurveymodel.PS_PE_SA_link = DefaultURL + "RatingPage?QType=PESA" + "&Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_PE_SA_Status = false;
            model.objProjectsurveymodel.PS_PM_SA_link = DefaultURL + "RatingPage?QType=PMSA" + "&Id=" + GetRandomnumber();
            model.objProjectsurveymodel.PS_PM_SA_Status = false;

            model.objProjectsurveymodel.PS_Status = false;
            model.objProjectsurveymodel.PS_CreatedBY = UserId;

            SuvEnt.Database.ExecuteSqlCommand("usp_AssignProjectSurvey @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35"
              , model.objProjectsurveymodel.PS_ID, model.objProjectsurveymodel.PS_Date, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_ProjectCode, model.objProjectsurveymodel.PS_ClientName, model.objProjectsurveymodel.PS_Client_Email
             , model.objProjectsurveymodel.PS_Client_PM_link, model.objProjectsurveymodel.PS_Client_PM_Status, model.objProjectsurveymodel.PS_Client_PE_link, model.objProjectsurveymodel.PS_Client_PE_Status, model.objProjectsurveymodel.PS_PM_Name
             , model.objProjectsurveymodel.PS_PM_Email, model.objProjectsurveymodel.PS_PM_link, model.objProjectsurveymodel.PS_PM_Status, model.objProjectsurveymodel.PS_PE_Name, model.objProjectsurveymodel.PS_PE_Email
             , model.objProjectsurveymodel.PS_PE_link, model.objProjectsurveymodel.PS_PE_Status, model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_HD_Email, model.objProjectsurveymodel.PS_HD_PM_link
             , model.objProjectsurveymodel.PS_HD_PM_Status, model.objProjectsurveymodel.PS_HD_PE_link, model.objProjectsurveymodel.PS_HD_PE_Status, model.objProjectsurveymodel.PS_SA_Name, model.objProjectsurveymodel.PS_SA_Email,
             model.objProjectsurveymodel.PS_SA_PM_link, model.objProjectsurveymodel.PS_SA_PM_Status, model.objProjectsurveymodel.PS_SA_PE_link, model.objProjectsurveymodel.PS_SA_PE_Status, model.objProjectsurveymodel.PS_PE_SA_link, model.objProjectsurveymodel.PS_PE_SA_Status,
            model.objProjectsurveymodel.PS_PM_SA_link, model.objProjectsurveymodel.PS_PM_SA_Status, model.objProjectsurveymodel.PS_Status, model.objProjectsurveymodel.PS_CreatedBY);

            //Client will get 2 email, CLPM, CLPE,PMPE,PEPM,HD Seperate email.
            SendEmail(model.objProjectsurveymodel.PS_Client_Email, model.objProjectsurveymodel.PS_Client_PM_link, "Rating To CLPM - " + model.objProjectsurveymodel.PS_PM_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_ClientName, "Client", model.objProjectsurveymodel.PS_PM_Name, "Project Manager", "CLPM");
            SendEmail(model.objProjectsurveymodel.PS_Client_Email, model.objProjectsurveymodel.PS_Client_PE_link, "Rating To CLPE - " + model.objProjectsurveymodel.PS_PE_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_ClientName, "Client", model.objProjectsurveymodel.PS_PE_Name, "Project Engineer", "CLPE");

            //HD
            SendEmail(model.objProjectsurveymodel.PS_HD_Email, model.objProjectsurveymodel.PS_HD_PM_link, "Rating To HDPM - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_HD_Name, "Help Desk", model.objProjectsurveymodel.PS_PM_Name, "Project Manager", "HDPM");
            SendEmail(model.objProjectsurveymodel.PS_HD_Email, model.objProjectsurveymodel.PS_HD_PE_link, "Rating To HDPE - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_HD_Name, "Help Desk", model.objProjectsurveymodel.PS_PE_Name, "Project Engineer", "HDPE");

            //PE-PM
            SendEmail(model.objProjectsurveymodel.PS_PE_Email, model.objProjectsurveymodel.PS_PE_link, "Rating To PEPM - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_PE_Name, "Project Engineer", model.objProjectsurveymodel.PS_PM_Name, "Project Manager", "PEPM");
            SendEmail(model.objProjectsurveymodel.PS_PM_Email, model.objProjectsurveymodel.PS_PM_link, "Rating To PMPE - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_PM_Name, "Project Manager", model.objProjectsurveymodel.PS_PE_Name, "Project Engineer", "PMPE");

            //SA-PM
            // SendEmail(model.objProjectsurveymodel.PS_SA_Email, model.objProjectsurveymodel.PS_SA_PM_link, "Rating To SAPM - " + model.objProjectsurveymodel.PS_SA_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_SA_Name, "Solution Architect ", model.objProjectsurveymodel.PS_PM_Name, "Project Manager");
            SendEmail(model.objProjectsurveymodel.PS_SA_Email, model.objProjectsurveymodel.PS_SA_PE_link, "Rating To SAPE - " + model.objProjectsurveymodel.PS_SA_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_SA_Name, "Solution Architect ", model.objProjectsurveymodel.PS_PE_Name, "Project Engineer", "SAPM");

            //PE-PM
            SendEmail(model.objProjectsurveymodel.PS_PE_Email, model.objProjectsurveymodel.PS_PE_SA_link, "Rating To PESA - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_PE_Name, "Project Engineer", model.objProjectsurveymodel.PS_SA_Name, "Solution Architect", "PESA");
            SendEmail(model.objProjectsurveymodel.PS_PM_Email, model.objProjectsurveymodel.PS_PM_SA_link, "Rating To PMSA - " + model.objProjectsurveymodel.PS_HD_Name, model.objProjectsurveymodel.PS_ProjectName, model.objProjectsurveymodel.PS_PM_Name, "Project Manager", model.objProjectsurveymodel.PS_SA_Name, "Solution Architect", "PMSA");


            return RedirectToAction("GetCLData", "Questions");
        }

        public string GetRandomnumber()
        {
            Random generator = new Random();
            string r = generator.Next(100000, 9000000).ToString();
            return r;
        }

        public void SendEmail(string UserEmail, string link, string Subject, string project, string recipientName, string recipientRole, string personsName, string personsRole, string Qtype)
        {

            MailMessage mail = new MailMessage();
            mail.To.Add(UserEmail);
            mail.From = new MailAddress("noreply@SurveySpyder.com");
            //mail.From = new MailAddress("notifications@WeSucceed.com");
            mail.Body = "Use The below URL For Rating \n" + link + "";
            mail.Subject = project + " -  Project - How did we do?";
            mail.IsBodyHtml = true;

            ObjprojectMaster.lstEmailTemplate = SuvEnt.Database.SqlQuery<EmailTemplateModel>("usp_ListEmailMapDashboardDetails @p0", "").ToList();
            foreach (var x in ObjprojectMaster.lstEmailTemplate.Where(x => x.Q_type == Qtype))
            {
                string emailBody = x.Body_Content;
                emailBody = $"{x.Body_Content} {recipientName}";
                emailBody = emailBody.Replace(Environment.NewLine, "<br>");
                emailBody = emailBody.Replace("recipentName", recipientName);
                emailBody = emailBody.Replace("project", project);
                //emailBody = emailBody.Replace("[link]", link);
                emailBody = emailBody.Replace("link", link);
                emailBody = emailBody.Replace("recipientRole", recipientRole);
                emailBody = emailBody.Replace("personsName", personsName);
                emailBody = emailBody.Replace("personsRole", personsRole);
                mail.Body = emailBody;

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;


                SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");

                mail.IsBodyHtml = true;
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential("noreply@SurveySpyder.com", "SURapp_WSS123#");
                //SmtpServer.Credentials = new NetworkCredential("notifications@WeSucceed.com", "WSS*Notify#1");

                try
                {
                    SmtpServer.Send(mail);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

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
            ObjSurveyMaster.ProjectCount = ObjSurveyMaster.LsttotalReport.Count().ToString();
            ObjSurveyMaster.ProjectManagerCount = ObjSurveyMaster.LsttotalPMReport.Count().ToString();
            ObjSurveyMaster.ProjectEngineerCount = ObjSurveyMaster.LsttotalPEReport.Count().ToString();
            ObjSurveyMaster.SolutionArchitectCount = ObjSurveyMaster.LsttotalSAReport.Count().ToString();
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
            ObjSurveyMaster.ProjectCount = ObjSurveyMaster.LsttotalReport.Count().ToString();
            ObjSurveyMaster.ProjectManagerCount = ObjSurveyMaster.LsttotalPMReport.Count().ToString();
            ObjSurveyMaster.ProjectEngineerCount = ObjSurveyMaster.LsttotalPEReport.Count().ToString();
            ObjSurveyMaster.SolutionArchitectCount = ObjSurveyMaster.LsttotalSAReport.Count().ToString();
            return PartialView("_ProjectReport", ObjSurveyMaster);
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
        public ActionResult GetSuggestions(string input, string URid)
        {
            List<string> clientnameList = new List<string>();
            if (URid == "6")
            {
                var suggestions = SuvEnt.Database.SqlQuery<ProjectSurveyModel>("Usp_GetClientData @p0", input).ToList();

                foreach (var x in suggestions)
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
        public ActionResult GetClientEmail(string clientName, string URid)
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

        public ActionResult EmailTemplateDashboard()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ObjprojectMaster.lstEmailTemplate = SuvEnt.Database.SqlQuery<EmailTemplateModel>("usp_ListEmailDashboardDetails @p0", "").ToList();

            //ObjprojectMaster.ddlQUserStatus = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetQtype").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();

            return View(ObjprojectMaster);
        }

        public ActionResult EmailTemplateMappingDashboard()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ObjprojectMaster.lstEmailTemplate = SuvEnt.Database.SqlQuery<EmailTemplateModel>("usp_ListEmailMapDashboardDetails @p0", "").ToList();

            //ObjprojectMaster.ddlQUserStatus = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetQtype").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();

            return View(ObjprojectMaster);
        }

        public ActionResult EmailTemplateMapping()
        {
            EmailTemplateModel ObjETM = new EmailTemplateModel();

            ObjETM.ddlQUserStatus = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetQtype").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();
            ObjETM.ddlTemplateName = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetTemplateName").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();

            //  ObjETM.Body_Content = "";
            return View(ObjETM);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EmailTemplateMapping(EmailTemplateModel ETM)
        {
            string Createdby = Session["UserID"].ToString();
            SuvEnt.Database.ExecuteSqlCommand("Usp_InsertEmailTemplateMap @p0,@p1,@p2,@p3", ETM.TemplateName, ETM.Q_type, true, Createdby);
            return RedirectToAction("EmailTemplateDashboard");
        }

        public ActionResult EmailTemplate()
        {
            EmailTemplateModel ObjEmailTemplateModel = new EmailTemplateModel();
            //  ObjETM.Body_Content = "";
            return View(ObjEmailTemplateModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EmailTemplate(EmailTemplateModel ETM)
        {
            string Createdby = Session["UserID"].ToString();
            SuvEnt.Database.ExecuteSqlCommand("Usp_InsertEmailTemplate @p0,@p1,@p2,@p3,@p4", ETM.TemplateId, ETM.TemplateName, ETM.Body_Content, true, Createdby);
            return RedirectToAction("EmailTemplateDashboard");
        }


        public ActionResult EditEmailTemplate(int? TemplateId)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ViewData["Mode"] = "edit";
            EmailTemplateModel ObjEmailTemplateModel = new EmailTemplateModel();
            ObjEmailTemplateModel = SuvEnt.Database.SqlQuery<EmailTemplateModel>("usp_ListEmailDashboardDetails @p0", TemplateId).FirstOrDefault();
            //TempData["ObjProjectSurveyMaster"] = ObjProjectSurveyMaster;
            return View("EmailTemplate", ObjEmailTemplateModel);
            //return RedirectToAction("EmailTemplate", "ProjectSurvey");
        }


        public ActionResult EditEmailTemplateMapping(int? TemplateId)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ViewData["Mode"] = "edit";
            EmailTemplateModel ObjEmailTemplateModel = new EmailTemplateModel();
            ObjEmailTemplateModel = SuvEnt.Database.SqlQuery<EmailTemplateModel>("usp_ListEmailMapDashboardDetails @p0", TemplateId).FirstOrDefault();
            ObjEmailTemplateModel.ddlQUserStatus = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetQtype").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();
            ObjEmailTemplateModel.ddlTemplateName = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetTemplateName").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();
            return View("EmailTemplateMapping", ObjEmailTemplateModel);
        }
    }



}

