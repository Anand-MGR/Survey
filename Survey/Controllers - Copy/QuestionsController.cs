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

        CLPMModel objCLPM = new CLPMModel();
        CLPEModel objCLPE = new CLPEModel();
        HDPMModel objHDPM = new HDPMModel(); 
        HDPEModel objHDPE = new HDPEModel();
        PEPMModel objPEPM = new PEPMModel();
        PMPEModel objPMPE = new PMPEModel();
        List<ReviewModel> objRwviewlstmodel = new List<ReviewModel>();
        ProjectSurveyModel objProjectsurveymodel = new ProjectSurveyModel();
        QuestionsForm ObjquestionsForm = new QuestionsForm();
        ProjectMasterModel ObjProjectSurveyMaster = new ProjectMasterModel();
        #endregion

        public ActionResult CLPM(string Id) 
        {          
            objCLPM = GEtDataCLPM(Id);

            var QType = "CLPM";
            objCLPM.QuestionnaireLst = SuvEnt.Database.SqlQuery<QuestionnaireObjectModel>("usp_getquestionsfromdb @p0", QType).ToList();
            return View(objCLPM);
        }

        [HttpPost]
        public ActionResult CLPM(List<CLPMModel> ratings,string psId,string prId,string ratingFrom)
        {
           
            DataTable dt = new DataTable();
            dt.Columns.Add("Q_Id", typeof(int));
            dt.Columns.Add("PSD_Rating", typeof(int));

            for(int i =0; i<ratings.Count;i++)
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
            cmd.Parameters.AddWithValue("@PSD_Type", "CLPM");
            cmd.Parameters.AddWithValue("@PSD_CreatedBY", ratingFrom);
            cmd.Parameters.AddWithValue("@TypeTable", dt);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ThankYou","ProjectSurvey");
        
        }

        public ActionResult CLPE(string Id)
        {
           
            objCLPE = GEtDataCLPE(Id);

            var QType = "CLPE";
            objCLPE.QuestionnaireLst = SuvEnt.Database.SqlQuery<QuestionnaireObjectModel>("usp_getquestionsfromdb @p0", QType).ToList();
            return View(objCLPE);
            
        }

        [HttpPost]
        public ActionResult CLPE(List<CLPEModel> ratings, string psId, string prId, string ratingFrom)
        {
            
            DataTable dt = new DataTable();
            dt.Columns.Add("Q_Id", typeof(int));
            dt.Columns.Add("PSD_Rating", typeof(int));

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
            cmd.Parameters.AddWithValue("@PSD_Type", "CLPE");
            cmd.Parameters.AddWithValue("@PSD_CreatedBY", ratingFrom);
            cmd.Parameters.AddWithValue("@TypeTable", dt);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("ThankYou", "ProjectSurvey");

        }

        public ActionResult HDPM(string Id)
        {
            objHDPM = GEtDataHDPM(Id);

            var QType = "HDPM";
            objHDPM.QuestionnaireLst = SuvEnt.Database.SqlQuery<QuestionnaireObjectModel>("usp_getquestionsfromdb @p0", QType).ToList();
            return View(objHDPM);
        }

        [HttpPost]
        public ActionResult HDPM(List<HDPMModel> ratings, string psId, string prId, string ratingFrom)
        {
          
                
                DataTable dt = new DataTable();
                dt.Columns.Add("Q_Id", typeof(int));
                dt.Columns.Add("PSD_Rating", typeof(int));

                    for (int i = 0; i < ratings.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Q_Id"] = ratings[i].Question;
                        dr["PSD_Rating"] = ratings[i].Answer;
                        dt.Rows.Add(dr);
                    }



            SqlCommand cmd = new SqlCommand("usp_ProjectSurveyDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PS_ID",psId);
                cmd.Parameters.AddWithValue("@PR_ID", prId);
                cmd.Parameters.AddWithValue("@PSD_Type", "HDPM");
                cmd.Parameters.AddWithValue("@PSD_CreatedBY", ratingFrom);
                cmd.Parameters.AddWithValue("@TypeTable", dt);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            return RedirectToAction("ThankYou", "ProjectSurvey");
            //return Redirect("/ProjectSurvey/ThankYou");

        }

        public ActionResult HDPE(string Id)
        {
            objHDPE = GEtDataHDPE(Id);

            var QType = "HDPE";
            objHDPE.QuestionnaireLst = SuvEnt.Database.SqlQuery<QuestionnaireObjectModel>("usp_getquestionsfromdb @p0", QType).ToList();
            return View(objHDPE);
        }

        [HttpPost]
        public ActionResult HDPE(List<HDPEModel> ratings, string psId, string prId, string ratingFrom)
        {
           
                DataTable dt = new DataTable();
                dt.Columns.Add("Q_Id", typeof(int));
                dt.Columns.Add("PSD_Rating", typeof(int));

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
                cmd.Parameters.AddWithValue("@PSD_Type", "HDPE");
                cmd.Parameters.AddWithValue("@PSD_CreatedBY", ratingFrom);
                cmd.Parameters.AddWithValue("@TypeTable", dt);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            return RedirectToAction("ThankYou", "ProjectSurvey");

        }

        public ActionResult PEPM(string Id)
        {
            objPEPM = GEtDataPEPM(Id);


            var QType = "PEPM";
            objPEPM.QuestionnaireLst = SuvEnt.Database.SqlQuery<QuestionnaireObjectModel>("usp_getquestionsfromdb @p0", QType).ToList();
            return View(objPEPM);
        }

        [HttpPost]
        public ActionResult PEPM(List<PEPMModel> ratings, string psId, string prId, string ratingFrom)
        {
            
             
                DataTable dt = new DataTable();
                dt.Columns.Add("Q_Id", typeof(int));
                dt.Columns.Add("PSD_Rating", typeof(int));

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
                cmd.Parameters.AddWithValue("@PSD_Type", "PEPM");
                cmd.Parameters.AddWithValue("@PSD_CreatedBY", ratingFrom);
                cmd.Parameters.AddWithValue("@TypeTable", dt);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("ThankYou", "ProjectSurvey");
           

        }

        public ActionResult PMPE(string Id)
        {
            objPMPE = GEtDataPMPE(Id);

            var QType = "PMPE";
            objPMPE.QuestionnaireLst = SuvEnt.Database.SqlQuery<QuestionnaireObjectModel>("usp_getquestionsfromdb @p0", QType).ToList();

            return View(objPMPE);
        }

        [HttpPost]
        public ActionResult PMPE(List<PMPEModel> ratings, string psId, string prId, string ratingFrom)
        {
         
               
                DataTable dt = new DataTable();
                dt.Columns.Add("Q_Id", typeof(int));
                dt.Columns.Add("PSD_Rating", typeof(int));

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
                cmd.Parameters.AddWithValue("@PSD_Type", "PMPE");
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
            ObjCLModel.ClList = SuvEnt.Database.SqlQuery<GetClDataModel>("spGetClData @p0,@p1" ,Role, UserId).ToList();

            return View(ObjCLModel);
        }

        public List<ReviewModel> GetReviewDatas(int id)
        {
            var Role = Session["Role"].ToString();
            objRwviewlstmodel = SuvEnt.Database.SqlQuery<ReviewModel>("usp_GetReviewRating @p0,@p1", id, Role).ToList();
            return objRwviewlstmodel;
        }

        #region Getting Datas From DB

        public CLPMModel GEtDataCLPM(string Id)
        {
            string Type = "CLPM";
            objCLPM.RatingHeader = SuvEnt.Database.SqlQuery<RatingCommonmodel>("usp_GetDatas @p0,@p1", Type, Id).First();   
            return objCLPM;

        }

        public CLPEModel GEtDataCLPE(string Id)
        {
            var Type = "CLPE";
            objCLPE.RatingHeader = SuvEnt.Database.SqlQuery<RatingCommonmodel>("usp_GetDatas @p0,@p1", Type,Id).First();
            return objCLPE;

        }

        public HDPMModel GEtDataHDPM(string Id)
        {
            var Type = "HDPM";
            objHDPM.RatingHeader = SuvEnt.Database.SqlQuery<RatingCommonmodel>("usp_GetDatas @p0,@p1", Type,Id).First();
            return objHDPM;

        }

        public HDPEModel GEtDataHDPE(string Id)
        {
            var Type = "HDPE";
            objHDPE.RatingHeader = SuvEnt.Database.SqlQuery<RatingCommonmodel>("usp_GetDatas @p0,@p1", Type, Id).First();
            return objHDPE;

        }

        public PEPMModel GEtDataPEPM(string Id)
        {
            var Type = "PEPM";
            objPEPM.RatingHeader = SuvEnt.Database.SqlQuery<RatingCommonmodel>("usp_GetDatas @p0,@p1", Type,Id).First();
            return objPEPM;

        }

        public PMPEModel GEtDataPMPE(string Id)
        {
            var Type = "PMPE";
            objPMPE.RatingHeader = SuvEnt.Database.SqlQuery<RatingCommonmodel>("usp_GetDatas @p0,@p1", Type,Id).First();
            return objPMPE;

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


            return View("GetCLData",ObjCLModel);

        }

        #endregion

        public ActionResult SearchBasedOnProject(string Project)
        {
            if (Session["UserName"] == null)  
            {
                return RedirectToAction("Loginpage", "Login");
            }
            GetClDataListModel ObjCLModel = new GetClDataListModel();
            ObjCLModel.ClList = SuvEnt.Database.SqlQuery<GetClDataModel>("usp_SearchByProject @p0",Project).ToList();

            return PartialView("_PGetClData", ObjCLModel);
        }



        public ActionResult QuestionsForm()
        {
            //var Type = "PMPE";
            //objPMPE.RatingHeader = SuvEnt.Database.SqlQuery<RatingCommonmodel>("usp_GetDatas @p0,@p1", Type, Id).First();
            //ObjProjectSurveyMaster.LstUserModel = SuvEnt.Database.SqlQuery<UserModel>("usp_ListUserDashboardDetails").ToList();
            ObjProjectSurveyMaster.ddlQUserStatus = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetQtype").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();


            return View (ObjProjectSurveyMaster);

        }

        [HttpPost]

        public ActionResult QuestionsForm(QuestionsForm QuestionsForm)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            var UserId = Session["UserID"].ToString();
            SuvEnt.Database.ExecuteSqlCommand("usp_AddQuestions @p0,@p1,@p2,@p3,@p4"
          , QuestionsForm.Q_ID,QuestionsForm.Q_Question, QuestionsForm.Q_Type, QuestionsForm.Q_Status,UserId);

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



        #region "Search"

        [HttpPost]
        public ActionResult SearchRecords(string Value, string Fliterby)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Loginpage", "Login");
            }
            // depot = Session["WareHouse"].ToString();
            ObjProjectSurveyMaster.objQuestionsForm = SuvEnt.Database.SqlQuery<QuestionsForm>("usp_GetQuestionbysearch @p0,@p1", Value, Fliterby).ToList();
            return PartialView("_QuestionDashboard", ObjProjectSurveyMaster);
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

            return View("QuestionsForm",ObjProjectSurveyMaster);
        }

        #endregion
    }
}