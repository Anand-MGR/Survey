using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BMWC;
using Survey;
using static System.Windows.Forms.LinkLabel;
using Survey.Models;
using Survey.SurveyEntity;

namespace Survey
{
    public class MvcApplication : System.Web.HttpApplication
    {

        ProjectSurveyEntities SuvEnt = new ProjectSurveyEntities();
        private Timer _hourlyTimer;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
           FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimsIdentity.DefaultNameClaimType;

            #region 30 Days Once
            //TimeSpan reminderInterval = TimeSpan.FromDays(30);
            //DateTime now = DateTime.Now;
            //DateTime nextReminder = now.AddDays(30);
            //TimeSpan initialDelay = nextReminder - now;
            //_hourlyTimer = new Timer(SendRemainder, null, initialDelay, reminderInterval);
            #endregion

            #region Hourly Basis
            //DateTime now = DateTime.Now;
            //DateTime nextHour = now.AddHours(1).Date.AddHours(now.Hour + 1);
            //TimeSpan initialDelay = nextHour - now;
            //_hourlyTimer = new Timer(SendRemainder, null, initialDelay, TimeSpan.FromHours(1));
            #endregion

            #region Min Basis
            // Calculate the initial delay to start the timer
            DateTime now = DateTime.Now;
            DateTime nextTwoMinutes = now.AddMinutes(1);
            TimeSpan initialDelay = nextTwoMinutes - now;

            // Schedule the task to run every 2 minutes
            _hourlyTimer = new Timer(SendRemainder, null, initialDelay, TimeSpan.FromMinutes(1));
            #endregion
        }


        private void SendRemainder(object state)
        {
            GetClDataListModel ObjCLModel = new GetClDataListModel();
            ObjCLModel.surveyremainderlist = SuvEnt.Database.SqlQuery<ProjectSurveyRemainder>("Usp_GetRemainderRecords").ToList();


            foreach (var x in ObjCLModel.surveyremainderlist)
            {
                //var createdDate = x.PS_CreatedDate; 
                //DateTime currentDate = DateTime.Now;

                //TimeSpan difference = (TimeSpan)(currentDate - createdDate);
                //double differenceInDays = difference.TotalDays;
                //if (differenceInDays > 45)
                //{
                //    SuvEnt.Database.ExecuteSqlCommand("Usp_UpdateRemainderMailStatus @p0", x.PS_ID);
                //}
                //else
                {
                    ////Based On This Series We are Passing the Values to mail (string Name, string Link,string Project,string MailId,string recipientRole,string personsRole,string Qtype)
                    var Project = x.PS_ProjectName;
                    if (x.PS_Client_PM_Status == false)//CLPM
                    {
                        var Link = x.PS_Client_PM_link;
                        var MailId = x.PS_Client_Email;
                        var Name = x.PS_ClientName;
                        var recipientRole = "Client";
                        if (!string.IsNullOrEmpty(MailId)) { SendMail(Name, Link, Project, MailId, recipientRole, "Project Manager","CLPM"); }

                    }
                    if (x.PS_Client_PE_Status == false)//CLPE
                    {
                        var Link = x.PS_Client_PE_link;
                        var MailId = x.PS_Client_Email;
                        var Name = x.PS_ClientName;
                        var recipientRole = "Client";
                        if (!string.IsNullOrEmpty(MailId)) { SendMail(Name, Link, Project, MailId,recipientRole, "Project Engineer","CLPE"); }
                    }
                    if (x.PS_HD_PE_Status == false)//HDPE
                    {
                        var Link = x.PS_HD_PE_link;
                        var MailId = x.PS_HD_Email;
                        var Name = x.PS_HD_Name;
                        var recipientRole = "Help Desk";
                        if (!string.IsNullOrEmpty(MailId)) { SendMail(Name, Link, Project, MailId,recipientRole, "Project Engineer","HDPE"); }
                    }
                    if (x.PS_HD_PM_Status == false)//HDPM
                    {
                        var Link = x.PS_HD_PM_link;
                        var MailId = x.PS_HD_Email;
                        var Name = x.PS_HD_Name;
                        var recipientRole = "Help Desk";
                        if (!string.IsNullOrEmpty(MailId)) { SendMail(Name, Link, Project, MailId,recipientRole, "Project Manager","HDPM"); }
                    }
                    if (x.PS_PE_Status == false)//PEPM
                    {
                        var Link = x.PS_PE_link;
                        var MailId = x.PS_PE_Email;
                        var Name = x.PS_PE_Name;
                        var recipientRole = "Project Engineer";
                        if (!string.IsNullOrEmpty(MailId)) { SendMail(Name, Link, Project, MailId,recipientRole, "Project Manager","PEPM"); }
                    }
                    if (x.PS_PM_Status == false)//PMPE
                    {
                        var Link = x.PS_PM_link;
                        var MailId = x.PS_PM_Email;
                        var Name = x.PS_PM_Name;
                        var recipientRole = "Project Manager";
                        if (!string.IsNullOrEmpty(MailId)) { SendMail(Name, Link, Project, MailId,recipientRole, "Project Engineer","PMPE"); }
                    }
                    if (x.PS_SA_PE_Status == false)//SAPE
                    {
                        var Link = x.PS_SA_PE_link;
                        var MailId = x.PS_SA_Email;
                        var Name = x.PS_SA_Name;
                        var recipientRole = "Solution Architect";
                        if (!string.IsNullOrEmpty(MailId)) { SendMail(Name, Link, Project, MailId,recipientRole, "Project Engineer","SAPE"); }
                    }
                    if (x.PS_PM_SA_Status == false)//PMSA
                    {
                        var Link = x.PS_PM_SA_link;
                        var MailId = x.PS_PM_Email;
                        var Name = x.PS_PM_Name;
                        var recipientRole = "Project Manager";
                        if (!string.IsNullOrEmpty(MailId)) { SendMail(Name, Link, Project, MailId,recipientRole, "Solution Architect","PMSA"); }
                    }
                    if (x.PS_PE_SA_Status == false)//PESA
                    {
                        var Link = x.PS_PE_SA_link;
                        var MailId = x.PS_PE_Email;
                        var Name = x.PS_PE_Name;
                        var recipientRole = "Project Engineer";
                        if (!string.IsNullOrEmpty(MailId)) { SendMail(Name, Link, Project, MailId,recipientRole, "Solution Architect","PESA"); }
                    }
                }
            }            
        }

        public void SendMail(string Name, string Link,string Project,string MailId,string recipientRole,string personsRole,string Qtype)
        {

            ProjectMasterModel ObjprojectMaster = new ProjectMasterModel();
            ObjprojectMaster.lstEmailTemplate = SuvEnt.Database.SqlQuery<EmailTemplateModel>("usp_ListEmailMapDashboardDetails @p0","").ToList();

            MailMessage mail = new MailMessage();
            //mail.To.Add(MailId);//Dont forget to enable this line while deployment
            mail.To.Add("Janagan.M@wesucceed.com");
            mail.From = new MailAddress("noreply@SurveySpyder.com");
            //mail.From = new MailAddress("notifications@WeSucceed.com");
            mail.Body = "Use The below URL For Rating \n" + Link + "";
            mail.Subject = Project + " -  Project - How did we do?";
            mail.IsBodyHtml = true;
            foreach (var x in ObjprojectMaster.lstEmailTemplate.Where(x => x.Q_type == Qtype))
            {
                string emailBody = x.Body_Content;
                emailBody = emailBody.Replace(Environment.NewLine, "<br>");
                emailBody = emailBody.Replace("recipentName", $"<strong>{Name}</strong>");
                emailBody = emailBody.Replace("project", $"<strong>{Project}</strong>");
                emailBody = emailBody.Replace("link", $"<a href='{Link}'>{Project}</a>");
                emailBody = emailBody.Replace("recipientRole", $"<strong>{recipientRole}</strong>");
                emailBody = emailBody.Replace("personsName", $"<strong>{Name}</strong>");
                emailBody = emailBody.Replace("personsRole", $"<strong>{personsRole}</strong>");
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
    }
}
