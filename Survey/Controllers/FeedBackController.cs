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

namespace Survey.Controllers
{
    public class FeedBackController : Controller
    {
        #region "Declaration"

        ProjectSurveyEntities SuvEnt = new ProjectSurveyEntities();
        AssignProjectSurveyModel objProject = new AssignProjectSurveyModel();
        AssignSurveyMasterModel ObjSurveyMaster = new AssignSurveyMasterModel();
        List<AssignSurveyMasterModel> LstSurveyMaster = new List<AssignSurveyMasterModel>();
        FeedbackViewModel objFBModel = new FeedbackViewModel();

        #endregion
        // GET: FeedBack
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FeedBackDashboard()
        {
            if (Session["UserName"] == null)
            {
                 return RedirectToAction("Loginpage", "Login");
            }
            var UserId = Session["UserID"].ToString();

            ObjSurveyMaster.LstFbModel = SuvEnt.Database.SqlQuery<FeedbackViewModel>("usp_ListFbDetails @p0", UserId).ToList();

            return View(ObjSurveyMaster);
        }

        public ActionResult Thankyoupage()
        {
            return View();
        }

        public ActionResult FeedBack()
        {
            if (Session["UserName"] == null)
            {
                 return RedirectToAction("Loginpage", "Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult FeedBack(FeedbackViewModel objFBModel)
        {
            if (Session["UserName"] == null)
            {
                 return RedirectToAction("Loginpage", "Login");
            }

            var UserId = Session["UserID"].ToString();
            if (objFBModel.FD_Name == null)
            {
                var Username = Session["UserName"].ToString();
                objFBModel.FD_Name = Username;
            }

            SuvEnt.Database.ExecuteSqlCommand("usp_AddFeedback @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8"
           , objFBModel.FD_ID, objFBModel.FD_Name, objFBModel.FD_Date, objFBModel.FD_Subject, objFBModel.FD_Description, objFBModel.FD_Response, objFBModel.FD_Status, UserId, UserId);

            return RedirectToAction("Thankyoupage", "FeedBack");
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
            ObjSurveyMaster.LstFbModel = SuvEnt.Database.SqlQuery<FeedbackViewModel>("usp_GetFBDetails @p0", Value).ToList();
            return PartialView("_FeedBack", ObjSurveyMaster);
        }

        #endregion

        #region "Edit"

        public ActionResult EditFeedback(int? FD_ID)
        {
            if (Session["UserName"] == null)
            {
                 return RedirectToAction("Loginpage", "Login");
            }

            ViewData["Mode"] = "edit";
                       
            ObjSurveyMaster.objFBModel = SuvEnt.Database.SqlQuery<FeedbackViewModel>("usp_GetFeedBackDetailsBasedID @p0", FD_ID).FirstOrDefault();
            //ObjSurveyMaster.ddlUserStatus = SuvEnt.Database.SqlQuery<UserStatus>("usp_GetUserstatusUST").Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            return View("FeedBack", ObjSurveyMaster);
        }

        #endregion
    }
}