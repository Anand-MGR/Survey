using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Survey.SurveyEntity;
using Survey.Models;
namespace Survey.Controllers
{
    public class QuestionnaireController : Controller
    {
        ProjectSurveyEntities SuvEnt = new ProjectSurveyEntities();        
        Listwholemodelvalues objlistwholemodel = new Listwholemodelvalues();
        List<QuestionnaireModel> lstquestionaremodel = new List<QuestionnaireModel>();
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-28Q085U;Initial Catalog=ProjectSurvey;User ID=sa;Password=SHRIram@007");


        #region 

        QuestionMasterModel QusModel = new QuestionMasterModel();
        public ActionResult Questionnaire(int id)
        {
            if (Session["UserName"] == null)
            {
                 return RedirectToAction("Loginpage", "Login");
            }
            QusModel.AnswerQuestionLst = GetQuestion(id);
            return View(QusModel);
        }

        public List<AnswerQuestion> GetQuestion(int id)
        {
            string UserID = Session["UserID"].ToString();
            List<AnswerQuestion> objLst = new List<AnswerQuestion>();
            objLst = SuvEnt.Database.SqlQuery<AnswerQuestion>("usp_GetQuestionServey @p0", id).ToList();
            return objLst;
        }

        #endregion

        public ActionResult SubmitRating()
        {


            return View();
        }


        // GET: Questionnaire
        //    public ActionResult Index()
        //    {
        //        return View();
        //    }

        //    public ActionResult Dashboard()
        //    {
        //        QuestionnaireModel objQstModel = new QuestionnaireModel();
        //        var Query = "select * from Q_Questionnaire where Q_Type = '" + "CLPM" + "'";

        //        SqlDataReader dr;
        //        SqlCommand cmd = new SqlCommand();
        //        objQstModel.QuestionnaireLst = new List<QuestionnaireObjectModel>();

        //        try
        //        {
        //            con.Open();
        //            cmd.Connection = con;
        //            cmd.CommandText = Query;
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                objQstModel.QuestionnaireLst.Add(new QuestionnaireObjectModel()
        //                {

        //                    QuestionID = (int)dr["Q_ID"],
        //                    QuestionType = dr["Q_Type"].ToString(),
        //                    QuestionSequence = (int)dr["Q_QuestionSequence"],
        //                    // QuestionStatus =dr["Q_Status"],
        //                    Q_Questions = dr["Q_Question"].ToString()

        //                });

        //            }
        //            con.Close();

        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        return View(objQstModel);
        //    }


        //    public List<Q_Questionnaire> GetQuestionnaire()
        //    {
        //        List<Q_Questionnaire> objQuestionnaireLst = new List<Q_Questionnaire>();
        //        objQuestionnaireLst = SuvEnt.Q_Questionnaire.Where(Q => Q.Q_Status == true).Select(Q => Q).ToList();
        //        return objQuestionnaireLst;
        //    }



        //    public ActionResult GetQuestions(string selectedquestion)
        //    {
        //        QuestionnaireModel objQstModel = new QuestionnaireModel();
        //        var Query = "select * from Q_Questionnaire where Q_Type = '" + "CLPM" + "'";

        //        SqlDataReader dr;
        //        SqlCommand cmd = new SqlCommand();

        //        objQstModel.QuestionnaireLst = new List<QuestionnaireObjectModel>();


        //        try
        //        {
        //            con.Open();
        //            cmd.Connection = con;
        //            cmd.CommandText = Query;
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                objQstModel.QuestionnaireLst.Add(new QuestionnaireObjectModel()
        //                {

        //                    QuestionID = (int)dr["Q_ID"],
        //                    QuestionType = dr["Q_Type"].ToString(),
        //                    QuestionSequence = (int)dr["Q_QuestionSequence"],
        //                    // QuestionStatus =dr["Q_Status"],
        //                    Q_Questions = dr["Q_Question"].ToString()

        //                });

        //            }

        //            con.Close();

        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }

        //        return View("GetQuestions", objQstModel);

        //    }



    }
}