using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace Survey.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            #region Outlook
            string BodyContent = $"Shipment id: <b>hlo</b>";
            string signature = "<br><br><br> Best Regards, <br><b>-----Automatic Mail Generated From Web Application----</b><br>";
            MailMessage mail = new MailMessage();
            mail.To.Add("janagan.M@wesucceed.com");
            ////mail.To.Add("muruganandam.r@wesucceed.com");
            ////mail.To.Add("yogendra.pandey@wesucceed.com");
            mail.From = new MailAddress("janagan.M@wesucceed.com");
            mail.Body = BodyContent + "</br> Having some Issue please Look into this!" + $"{ signature}";
            mail.Subject = "Re:IFS Issue";
            SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
            SmtpServer.EnableSsl = true;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential("janagan.M@wesucceed.com", "Spidy@1997");
            try
            {
                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            #endregion
            return View();
        }
    }
}