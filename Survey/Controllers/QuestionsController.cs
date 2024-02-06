using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Survey.Models;
using Survey.SurveyEntity;
using System.Linq;
using System.Configuration;

namespace Survey.Controllers
{
    public class QuestionsController : Controller
    {
        #region Declaration
        Listwholemodelvalues objtotal = new Listwholemodelvalues();
        //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-E2G75GE;Initial Catalog=ProjectSurvey;User ID=sa;Password=Janagan@515");  
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectiondetails"].ConnectionString);

        ProjectSurveyEntities SuvEnt = new ProjectSurveyEntities();

        RatingPageModel objRatingPageModel = new RatingPageModel();

        List<ReviewModel> objRwviewlstmodel = new List<ReviewModel>();
        ProjectSurveyModel objProjectsurveymodel = new ProjectSurveyModel();
        QuestionsForm ObjquestionsForm = new QuestionsForm();
        ProjectMasterModel ObjProjectSurveyMaster = new ProjectMasterModel();
        #endregion


        public ActionResult RatingPage(string QType, string Id)
        {
            objRatingPageModel = GEtDataRatingPage(Id, QType);
            //var QType = "RatingPage";
            objRatingPageModel.QuestionnaireLst = SuvEnt.Database.SqlQuery<QuestionnaireObjectModel>("usp_getquestionsfromdb @p0", QType).ToList();
            objRatingPageModel.objTotalQtype = SuvEnt.Database.SqlQuery<TotalQtype>("Usp_GetQTypes @p0", QType).First();
            return View(objRatingPageModel);
        }

        [HttpPost]
        public ActionResult RatingPage(List<RatingPageModel> ratings, string psId, string prId, string QType, string ratingFrom)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Q_Id", typeof(int));
            dt.Columns.Add("PSD_Rating", typeof(int));
            dt.Columns.Add("PSD_Yesorno", typeof(string));
            dt.Columns.Add("PSD_Mcq", typeof(string));

            for (int i = 0; i < ratings.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Q_Id"] = ratings[i].Question;
                dr["PSD_Rating"] = ratings[i].Answer;
                dt.Rows.Add(dr);
            }


            SqlCommand cmd = new SqlCommand("usp_ProjectSurveyDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PS_ID", psId);
            cmd.Parameters.AddWithValue("@PR_ID", prId);
            cmd.Parameters.AddWithValue("@PSD_Type", QType);
            cmd.Parameters.AddWithValue("@PSD_CreatedBY", ratingFrom);
            cmd.Parameters.AddWithValue("@TypeTable", dt);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ThankYou", "ProjectSurvey");

        }


        public ActionResult ReviewRating(int id)
        {
            ReviewSummaryModel objSummary = new ReviewSummaryModel();
            objSummary.LstReview = new List<ReviewModel>();
            objSummary.LstReview = GetReviewDatas(id);
            objSummary.Final = new List<string>();
            objSummary.ProjectName = objSummary.LstReview.Select(o => o.PS_ProjectName).FirstOrDefault();
            objSummary.PS_Date = objSummary.LstReview.Select(o => o.PS_Date).FirstOrDefault();
            objSummary.PS_Status = objSummary.LstReview.Select(o => o.PS_Status).FirstOrDefault();
            string Id = "";

            if (objSummary.LstReview.Count > 0)
            {
                foreach (var i in objSummary.LstReview)
                {
                    Id = "star" + i.PSD_Rating + "-" + i.Q_Id;
                    objSummary.Final.Add(Id);

                }

            }

            return View(objSummary);

        }

        public ActionResult GetCLData()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            GetClDataListModel ObjCLModel = new GetClDataListModel();
            var UserId = Session["UserID"].ToString();
            var Role = Session["Role"].ToString();
            ObjCLModel.ClList = SuvEnt.Database.SqlQuery<GetClDataModel>("spGetClData @p0,@p1", Role, UserId).ToList();

            return View(ObjCLModel);
        }

        public List<ReviewModel> GetReviewDatas(int id)
        {
            var Role = Session["Role"].ToString();
            objRwviewlstmodel = SuvEnt.Database.SqlQuery<ReviewModel>("usp_GetReviewRating @p0,@p1", id, Role).ToList();
            return objRwviewlstmodel;
        }


        public ActionResult ReviewRatingpage(string Value)
        {
            string[] Details = Value.Split(',');
            var PS_Id = Details[0];
            var Qtype = Details[1];

            ReviewSummaryModel objSummary = new ReviewSummaryModel();
            objSummary.LstReview = new List<ReviewModel>();
            objSummary.LstReview = GetReviewDatas(Convert.ToInt16(PS_Id));
            objSummary.LstReview = objSummary.LstReview.Where(x => x.PRType == Qtype).ToList();
            objSummary.Final = new List<string>();
            objSummary.ProjectName = objSummary.LstReview.Select(o => o.PS_ProjectName).FirstOrDefault();
            objSummary.PS_Date = objSummary.LstReview.Select(o => o.PS_Date).FirstOrDefault();
            objSummary.PS_Status = objSummary.LstReview.Select(o => o.PS_Status).FirstOrDefault();
            string Id = "";

            if (objSummary.LstReview.Count > 0)
            {
                foreach (var i in objSummary.LstReview)
                {
                    Id = "star" + i.PSD_Rating + "-" + i.Q_Id;
                    objSummary.Final.Add(Id);

                }

            }

            return View(objSummary);



            //var Role = Session["Role"].ToString();
            //objRwviewlstmodel = SuvEnt.Database.SqlQuery<ReviewModel>("usp_GetReviewRating @p0,@p1", PS_Id, Role).ToList();
            //return View ("ReviewRating",objRwviewlstmodel);
            //return View();

        }
        #region Getting Datas From DB


        public RatingPageModel GEtDataRatingPage(string Id, string Type)
        {

            objRatingPageModel.RatingHeader = SuvEnt.Database.SqlQuery<RatingCommonmodel>("usp_GetDatas @p0,@p1", Type, Id).First();
            return objRatingPageModel;

        }

        #endregion

        #region "Edit"

        public ActionResult EditCldata(int? PS_ID)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ViewData["Modes"] = "edit";
            ObjProjectSurveyMaster.objProjectsurveymodel = SuvEnt.Database.SqlQuery<ProjectSurveyModel>("Usp_GetCLDatabasedID @p0", PS_ID).FirstOrDefault();
            TempData["ObjProjectSurveyMaster"] = ObjProjectSurveyMaster;
            return RedirectToAction("ProjectSurveyAssign", "ProjectSurvey");
        }

        #endregion

        #region "Delete"

        public ActionResult DeleteGetCLData(int? PS_ID)
        {
            ViewData["Mode"] = "edit";

            var UserId = Session["UserID"].ToString();
            var Role = Session["Role"].ToString();
            ObjProjectSurveyMaster.objProjectsurveymodel = SuvEnt.Database.SqlQuery<ProjectSurveyModel>("Usp_DeleteGetCLData @p0", PS_ID).FirstOrDefault();

            GetClDataListModel ObjCLModel = new GetClDataListModel();
            ObjCLModel.ClList = SuvEnt.Database.SqlQuery<GetClDataModel>("spGetClData @p0,@p1", Role, UserId).ToList();


            return View("GetCLData", ObjCLModel);

        }

        #endregion

        public ActionResult SearchBasedOnProject(string Value, string Filterby)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            GetClDataListModel ObjCLModel = new GetClDataListModel();
            ObjCLModel.ClList = SuvEnt.Database.SqlQuery<GetClDataModel>("usp_SearchByProject @p0,@p1", Value, Filterby).ToList();

            return PartialView("_PGetClData", ObjCLModel);
        }



        public ActionResult QuestionsForm()
        {
            //var Type = "PMPE";
            //objPMPE.RatingHeader = SuvEnt.Database.SqlQuery<RatingCommonmodel>("usp_GetDatas @p0,@p1", Type, Id).First();
            //ObjProjectSurveyMaster.LstUserModel = SuvEnt.Database.SqlQuery<UserModel>("usp_ListUserDashboardDetails").ToList();
            ObjProjectSurveyMaster.ddlQUserStatus = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetQtype").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();
            ObjProjectSurveyMaster.ddlAnsType = SuvEnt.Database.SqlQuery<AnsType>("usp_GetAnstype").Select(x => new SelectListItem { Value = x.value.ToString(), Text = x.Name }).ToList();


            return View(ObjProjectSurveyMaster);

        }

        [HttpPost]

        public ActionResult QuestionsForm(QuestionsForm QuestionsForm)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            var UserId = Session["UserID"].ToString();
            SuvEnt.Database.ExecuteSqlCommand("usp_AddQuestions @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10"
          , QuestionsForm.Q_ID, QuestionsForm.Q_Question, QuestionsForm.Q_Type, QuestionsForm.Q_Status, QuestionsForm.Q_Ans_Type, UserId, QuestionsForm.Ans_Options1, QuestionsForm.Ans_Options2
          , QuestionsForm.Ans_Options3, QuestionsForm.Ans_Options4, QuestionsForm.Ans_Options5);

            ObjProjectSurveyMaster.ddlQUserStatus = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetQtype").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();


            return RedirectToAction("QuestionDashboard", "Questions");

        }

        public ActionResult QuestionDashboard()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ObjProjectSurveyMaster.objQuestionsForm = SuvEnt.Database.SqlQuery<QuestionsForm>("usp_ListQusDashboardDetails").ToList();
            ObjProjectSurveyMaster.ddlQUserStatus = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetQtype").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();

            return View(ObjProjectSurveyMaster);
        }

        public ActionResult QuestionsTypeDashboard()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ObjProjectSurveyMaster.LstQuestiontypeFrom = SuvEnt.Database.SqlQuery<QuestionsTypeForm>("usp_ListQuestionsTypeDetails @p0", "").ToList();
            return View(ObjProjectSurveyMaster);
        }
        public ActionResult QuestionsTypeForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuestionsTypeForm(QuestionsTypeForm QuestionsTypeForm)
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            SuvEnt.Database.ExecuteSqlCommand("usp_AddQuestionType @p0,@p1,@p2"

          , QuestionsTypeForm.Q_QuestionType, QuestionsTypeForm.QuestionTypeStatus, QuestionsTypeForm.QT_ID);

            return RedirectToAction("QuestionsTypeDashboard", "Questions");
        }

        #region "Search"
        [HttpPost]
        public ActionResult SearchRecordsQuestions(string Value)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            // depot = Session["WareHouse"].ToString();
            ObjProjectSurveyMaster.LstQuestiontypeFrom = SuvEnt.Database.SqlQuery<QuestionsTypeForm>("Usp_GetQuestionsBasedonType @p0", Value).ToList();
            return PartialView("_QuestionsTypeDashboard", ObjProjectSurveyMaster);
        }

        #endregion

        #region "Edit"

        public ActionResult EditQuestion(int? Q_ID)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ViewData["Mode"] = "edit";
            ObjProjectSurveyMaster.QuestionsForm = SuvEnt.Database.SqlQuery<QuestionsForm>("usp_GetQuestionBasedID @p0", Q_ID).FirstOrDefault();
            ObjProjectSurveyMaster.ddlQUserStatus = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetQtype").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();
            ObjProjectSurveyMaster.ddlAnsType = SuvEnt.Database.SqlQuery<AnsType>("usp_GetAnstype").Select(x => new SelectListItem { Value = x.value.ToString(), Text = x.Name }).ToList();

            return View("QuestionsForm", ObjProjectSurveyMaster);
        }


        public ActionResult EditQuestionsTypeForm(int? UR_ID)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            ViewData["Mode"] = "edit";
            ObjProjectSurveyMaster.QuestionsTypeForm = SuvEnt.Database.SqlQuery<QuestionsTypeForm>("usp_ListQuestionsTypeDetails @p0", UR_ID).FirstOrDefault();

            return View("QuestionsTypeForm", ObjProjectSurveyMaster);
        }

        #endregion
    }
}