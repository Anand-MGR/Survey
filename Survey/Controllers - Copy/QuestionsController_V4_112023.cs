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
            return View(objCLPM);
        }

        [HttpPost]
        public ActionResult CLPM(CLPMModel objModel)
        {
            var username = objModel.RatingFrom;
            DataTable dt = new DataTable();
            dt.Columns.Add("Q_Id", typeof(int));
            dt.Columns.Add("PSD_Rating", typeof(int));

                    DataRow dr1 = dt.NewRow();
                    dr1["Q_Id"] = objModel.Question1;
                    dr1["PSD_Rating"] = objModel.Answer1;
                    dt.Rows.Add(dr1);
                    DataRow dr2 = dt.NewRow();
                    dr2["Q_Id"] = objModel.Question2;
                    dr2["PSD_Rating"] = objModel.Answer2;
                    dt.Rows.Add(dr2);
                    DataRow dr3 = dt.NewRow();
                    dr3["Q_Id"] = objModel.Question3;
                    dr3["PSD_Rating"] = objModel.Answer3;
                    dt.Rows.Add(dr3);
                    DataRow dr4 = dt.NewRow();
                    dr4["Q_Id"] = objModel.Question4;
                    dr4["PSD_Rating"] = objModel.Answer4;
                    dt.Rows.Add(dr4);
                    DataRow dr5 = dt.NewRow();
                    dr5["Q_Id"] = objModel.Question5;
                    dr5["PSD_Rating"] = objModel.Answer5;
                    dt.Rows.Add(dr5);

            SqlCommand cmd = new SqlCommand("usp_ProjectSurveyDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PS_ID", objModel.PS_ID);
            cmd.Parameters.AddWithValue("@PR_ID", objModel.PR_ID);
            cmd.Parameters.AddWithValue("@PSD_Type", "CLPM");
            cmd.Parameters.AddWithValue("@PSD_CreatedBY", username);
            cmd.Parameters.AddWithValue("@TypeTable", dt);
           
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ThankYou", "ProjectSurvey");
            //return RedirectToAction("GetClData", "Questions");
        }

        public ActionResult CLPE(string Id)
        {
           
            objCLPE = GEtDataCLPE(Id);
            return View(objCLPE);
            
        }

        [HttpPost]
        public ActionResult CLPE(CLPEModel objModel)
        {
            var username = objModel.RatingFrom;
            DataTable dt = new DataTable();
            dt.Columns.Add("Q_Id", typeof(int));
            dt.Columns.Add("PSD_Rating", typeof(int));

                        DataRow dr1 = dt.NewRow();
                        dr1["Q_Id"] = objModel.Question6;
                        dr1["PSD_Rating"] = objModel.Answer6;
                        dt.Rows.Add(dr1);
                        DataRow dr2 = dt.NewRow();
                        dr2["Q_Id"] = objModel.Question7;
                        dr2["PSD_Rating"] = objModel.Answer7;
                        dt.Rows.Add(dr2);
                        DataRow dr3 = dt.NewRow();
                        dr3["Q_Id"] = objModel.Question8;
                        dr3["PSD_Rating"] = objModel.Answer8;
                        dt.Rows.Add(dr3);
                        DataRow dr4 = dt.NewRow();
                        dr4["Q_Id"] = objModel.Question9;
                        dr4["PSD_Rating"] = objModel.Answer9;
                        dt.Rows.Add(dr4);
                        DataRow dr5 = dt.NewRow();
                        dr5["Q_Id"] = objModel.Question10;
                        dr5["PSD_Rating"] = objModel.Answer10;
                        dt.Rows.Add(dr5);
                        DataRow dr6 = dt.NewRow();
                        dr6["Q_Id"] = objModel.Question11;
                        dr6["PSD_Rating"] = objModel.Answer11;
                        dt.Rows.Add(dr6);

            SqlCommand cmd = new SqlCommand("usp_ProjectSurveyDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PS_ID", objModel.PS_ID);
            cmd.Parameters.AddWithValue("@PR_ID", objModel.PR_ID);
            cmd.Parameters.AddWithValue("@PSD_Type", "CLPE");
            cmd.Parameters.AddWithValue("@PSD_CreatedBY", username);
            cmd.Parameters.AddWithValue("@TypeTable", dt);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("ThankYou", "ProjectSurvey");

        }

        public ActionResult HDPM(string Id)
        {
            objHDPM = GEtDataHDPM(Id);
            return View(objHDPM);
        }

        [HttpPost]
        public ActionResult HDPM(HDPMModel objModel)
        {
          
                var username = objModel.RatingFrom;
                DataTable dt = new DataTable();
                dt.Columns.Add("Q_Id", typeof(int));
                dt.Columns.Add("PSD_Rating", typeof(int));

                DataRow dr1 = dt.NewRow();
                dr1["Q_Id"] = objModel.Question12;
                dr1["PSD_Rating"] = objModel.Answer12;
                dt.Rows.Add(dr1);
                DataRow dr2 = dt.NewRow();
                dr2["Q_Id"] = objModel.Question13;
                dr2["PSD_Rating"] = objModel.Answer13;
                dt.Rows.Add(dr2);
                DataRow dr3 = dt.NewRow();
                dr3["Q_Id"] = objModel.Question14;
                dr3["PSD_Rating"] = objModel.Answer14;
                dt.Rows.Add(dr3);
                DataRow dr4 = dt.NewRow();
                dr4["Q_Id"] = objModel.Question15;
                dr4["PSD_Rating"] = objModel.Answer15;
                dt.Rows.Add(dr4);
                DataRow dr5 = dt.NewRow();
                dr5["Q_Id"] = objModel.Question16;
                dr5["PSD_Rating"] = objModel.Answer16;
                dt.Rows.Add(dr5);


                SqlCommand cmd = new SqlCommand("usp_ProjectSurveyDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PS_ID", objModel.PS_ID);
                cmd.Parameters.AddWithValue("@PR_ID", objModel.PR_ID);
                cmd.Parameters.AddWithValue("@PSD_Type", "HDPM");
                cmd.Parameters.AddWithValue("@PSD_CreatedBY", username);
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
            return View(objHDPE);
        }

        [HttpPost]
        public ActionResult HDPE(HDPEModel objModel)
        {
                var username = objModel.RatingFrom;
                DataTable dt = new DataTable();
                dt.Columns.Add("Q_Id", typeof(int));
                dt.Columns.Add("PSD_Rating", typeof(int));

                DataRow dr1 = dt.NewRow();
                dr1["Q_Id"] = objModel.Question17;
                dr1["PSD_Rating"] = objModel.Answer17;
                dt.Rows.Add(dr1);
                DataRow dr2 = dt.NewRow();
                dr2["Q_Id"] = objModel.Question18;
                dr2["PSD_Rating"] = objModel.Answer18;
                dt.Rows.Add(dr2);
                DataRow dr3 = dt.NewRow();
                dr3["Q_Id"] = objModel.Question19;
                dr3["PSD_Rating"] = objModel.Answer19;
                dt.Rows.Add(dr3);
                DataRow dr4 = dt.NewRow();
                dr4["Q_Id"] = objModel.Question20;
                dr4["PSD_Rating"] = objModel.Answer20;
                dt.Rows.Add(dr4);
                DataRow dr5 = dt.NewRow();
                dr5["Q_Id"] = objModel.Question21;
                dr5["PSD_Rating"] = objModel.Answer21;
                dt.Rows.Add(dr5);


                SqlCommand cmd = new SqlCommand("usp_ProjectSurveyDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PS_ID", objModel.PS_ID);
                cmd.Parameters.AddWithValue("@PR_ID", objModel.PR_ID);
                cmd.Parameters.AddWithValue("@PSD_Type", "HDPE");
                cmd.Parameters.AddWithValue("@PSD_CreatedBY", username);
                cmd.Parameters.AddWithValue("@TypeTable", dt);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return RedirectToAction("ThankYou", "ProjectSurvey");
    
        }

        public ActionResult PEPM(string Id)
        {
            objPEPM = GEtDataPEPM(Id);
            return View(objPEPM);
        }

        [HttpPost]
        public ActionResult PEPM(PEPMModel objModel)
        {
            
                var username = objModel.RatingFrom;
                DataTable dt = new DataTable();
                dt.Columns.Add("Q_Id", typeof(int));
                dt.Columns.Add("PSD_Rating", typeof(int));

                DataRow dr1 = dt.NewRow();
                dr1["Q_Id"] = objModel.Question22;
                dr1["PSD_Rating"] = objModel.Answer22;
                dt.Rows.Add(dr1);
                DataRow dr2 = dt.NewRow();
                dr2["Q_Id"] = objModel.Question23;
                dr2["PSD_Rating"] = objModel.Answer23;
                dt.Rows.Add(dr2);
                DataRow dr3 = dt.NewRow();
                dr3["Q_Id"] = objModel.Question24;
                dr3["PSD_Rating"] = objModel.Answer24;
                dt.Rows.Add(dr3);
                DataRow dr4 = dt.NewRow();
                dr4["Q_Id"] = objModel.Question25;
                dr4["PSD_Rating"] = objModel.Answer25;
                dt.Rows.Add(dr4);
                DataRow dr5 = dt.NewRow();
                dr5["Q_Id"] = objModel.Question26;
                dr5["PSD_Rating"] = objModel.Answer26;
                dt.Rows.Add(dr5);


                SqlCommand cmd = new SqlCommand("usp_ProjectSurveyDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PS_ID", objModel.PS_ID);
                cmd.Parameters.AddWithValue("@PR_ID", objModel.PR_ID);
                cmd.Parameters.AddWithValue("@PSD_Type", "PEPM");
                cmd.Parameters.AddWithValue("@PSD_CreatedBY", username);
                cmd.Parameters.AddWithValue("@TypeTable", dt);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("ThankYou", "ProjectSurvey");
           

        }

        public ActionResult PMPE(string Id)
        {
            objPMPE = GEtDataPMPE(Id);
            return View(objPMPE);
        }

        [HttpPost]
        public ActionResult PMPE(PMPEModel objModel)
        {
         
                var username = objModel.RatingFrom;
                DataTable dt = new DataTable();
                dt.Columns.Add("Q_Id", typeof(int));
                dt.Columns.Add("PSD_Rating", typeof(int));

                DataRow dr1 = dt.NewRow();
                dr1["Q_Id"] = objModel.Question27;
                dr1["PSD_Rating"] = objModel.Answer27;
                dt.Rows.Add(dr1);

                DataRow dr2 = dt.NewRow();
                dr2["Q_Id"] = objModel.Question28;
                dr2["PSD_Rating"] = objModel.Answer28;
                dt.Rows.Add(dr2);

                DataRow dr3 = dt.NewRow();
                dr3["Q_Id"] = objModel.Question29;
                dr3["PSD_Rating"] = objModel.Answer29;
                dt.Rows.Add(dr3);

                DataRow dr4 = dt.NewRow();
                dr4["Q_Id"] = objModel.Question30;
                dr4["PSD_Rating"] = objModel.Answer30;
                dt.Rows.Add(dr4);

                DataRow dr5 = dt.NewRow();
                dr5["Q_Id"] = objModel.Question31;
                dr5["PSD_Rating"] = objModel.Answer31;
                dt.Rows.Add(dr5);

                SqlCommand cmd = new SqlCommand("usp_ProjectSurveyDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PS_ID", objModel.PS_ID);
                cmd.Parameters.AddWithValue("@PR_ID", objModel.PR_ID);
                cmd.Parameters.AddWithValue("@PSD_Type", "PMPE");
                cmd.Parameters.AddWithValue("@PSD_CreatedBY", username);
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
            GetClDataListModel ObjCLModel = new GetClDataListModel();
            ObjCLModel.ClList = SuvEnt.Database.SqlQuery<GetClDataModel>("spGetClData").ToList();


            return View(ObjCLModel);
        }

        public List<ReviewModel> GetReviewDatas(int id)
        {
            objRwviewlstmodel = SuvEnt.Database.SqlQuery<ReviewModel>("usp_GetReviewRating @p0", id).ToList();
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

            ObjProjectSurveyMaster.objProjectsurveymodel = SuvEnt.Database.SqlQuery<ProjectSurveyModel>("Usp_DeleteGetCLData @p0", PS_ID).FirstOrDefault();

            GetClDataListModel ObjCLModel = new GetClDataListModel();
            ObjCLModel.ClList = SuvEnt.Database.SqlQuery<GetClDataModel>("spGetClData").ToList();


            return View("GetCLData",ObjCLModel);

        }

        #endregion

        public ActionResult SearchBasedOnProject(string Project)
        {
          
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

            var UserId = Session["UserID"].ToString();
            SuvEnt.Database.ExecuteSqlCommand("usp_AddQuestions @p0,@p1,@p2,@p3,@p4"
          , QuestionsForm.Q_ID,QuestionsForm.Q_Question, QuestionsForm.Q_Type, QuestionsForm.Q_Status,UserId);

            ObjProjectSurveyMaster.ddlQUserStatus = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetQtype").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();



            return View(ObjProjectSurveyMaster);

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
            // depot = Session["WareHouse"].ToString();
            ObjProjectSurveyMaster.objQuestionsForm = SuvEnt.Database.SqlQuery<QuestionsForm>("usp_GetQuestionbysearch @p0,@p1", Value, Fliterby).ToList();
            return PartialView("_QuestionDashboard", ObjProjectSurveyMaster);
        }

        #endregion

        #region "Edit"

        public ActionResult EditQuestion(int? Q_ID)
        {
            ViewData["Mode"] = "edit";
            ObjProjectSurveyMaster.QuestionsForm = SuvEnt.Database.SqlQuery<QuestionsForm>("usp_GetQuestionBasedID @p0", Q_ID).FirstOrDefault();
            ObjProjectSurveyMaster.ddlQUserStatus = SuvEnt.Database.SqlQuery<QUserStatus>("usp_GetQtype").Select(x => new SelectListItem { Value = x.value, Text = x.Name }).ToList();

            return View("QuestionsForm",ObjProjectSurveyMaster);
        }

        #endregion
    }
}