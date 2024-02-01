using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Models
{
    public class Listwholemodelvalues
    {
        public List<AssignProjectSurveyModel> objassignprojectsurveymodel { get; set; }
        public List<LoginModel> objloginmodel { get; set; }
        public List<QuestionnaireModel> objquestionnaireModels { get; set; }
        public List<CLPMModel> objlistCLPM { get; set; }
        public List<CLPEModel> objlistCLPE { get; set; }

        public List<ReviewModel> objlistreviewmodel { get; set; }
        public ReviewModel objReviewmodel { get; set; }


    }

    public class QuestionnaireModel
    {
        public List<QuestionnaireObjectModel> QuestionnaireLst { get; set; }
    }

    public class QuestionnaireObjectModel
    {
        public int QuestionID { get; set; }

        public string QuestionType { get; set; }

        public int QuestionSequence { get; set; }
        public string Q_Questions { get; set; }

        public int QuestionStatus { get; set; }

        public string Q_Ans_Type { get; set; }
        public string Ans_Options1 { get; set; }
        public string Ans_Options2 { get; set; }
        public string Ans_Options3 { get; set; }
        public string Ans_Options4 { get; set; }
        public string Ans_Options5 { get; set; }

        public int Rating { get; set; }
    }

    #region 

    public class QuestionMasterModel
    {
        public AnswerQuestion AnswerQuestionObj { get; set; }
        public List<AnswerQuestion> AnswerQuestionLst { get; set; }
    }

    public class AnswerQuestion
    {
        public int SSD_ID { get; set; }
        public string Q_Question { get; set; }
        public string Answer { get; set; }
    }

    #endregion

    #region 
    //created by pavithra 13-02-2023

    public class GetClDataListModel
    {
        public GetClDataModel SearchBy { get; set; }
        public List<GetClDataModel> ClList { get; set; }
    }
    public class GetClDataModel
    {
        public int PS_ID { get; set; }
        public DateTime PS_Date { get; set; }

        public string PS_ProjectName { get; set; }
        public string PS_ProjectCode { get; set; }
        public string PS_ClientName { get; set; }

        public bool PS_Client_PM_Status { get; set; }
        public bool PS_Client_PE_Status { get; set; }

        public string PS_PM_Name { get; set; }

        public bool PS_PM_Status { get; set; }
        public string PS_PE_Name { get; set; }
        public bool PS_PE_Status { get; set; }
        public string PS_HD_Name { get; set; }
        public string PS_SA_Name { get; set; }
        public bool PS_HD_PM_Status { get; set; }
        public bool PS_SA_PM_Status { get; set; }
        public bool PS_SA_PE_Status { get; set; }
        public bool PS_PE_SA_Status { get; set; }
        public bool PS_PM_SA_Status { get; set; }
        public bool PS_HD_PE_Status { get; set; }
        public bool PS_Status { get; set; }

    }


    #endregion
    public class CLPMModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        //public string Question1 { get; set; }
        //public string Answer1 { get; set; }
        //public string Question2 { get; set; }
        //public string Answer2 { get; set; }
        //public string Question3 { get; set; }
        //public string Answer3 { get; set; }
        //public string Question4 { get; set; }
        //public string Answer4 { get; set; }
        //public string Question5 { get; set; }
        //public string Answer5 { get; set; }

        public string PS_ID { get; set; }
        public string PR_ID { get; set; }
        public string RatingFrom { get; set; }

        //created on 08-02-2023 for getting values of CLPM Created by Janagan
        public RatingCommonmodel RatingHeader { get; set; }
        public QuestionnaireModel QuestionModel { get; set; }
        public List<QuestionnaireObjectModel> QuestionnaireLst { get; set; }



    }

    public class CLPEModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        //public string Question7 { get; set; }
        //public string Answer7 { get; set; }
        //public string Question8 { get; set; }
        //public string Answer8 { get; set; }
        //public string Question9 { get; set; }
        //public string Answer9 { get; set; }
        //public string Question10 { get; set; }
        //public string Answer10 { get; set; }
        //public string Question11 { get; set; }
        //public string Answer11 { get; set; }
        public string PS_ID { get; set; }
        public string PR_ID { get; set; }
        public string RatingFrom { get; set; }


        public RatingCommonmodel RatingHeader { get; set; }
        public QuestionnaireModel QuestionModel { get; set; }
        public List<QuestionnaireObjectModel> QuestionnaireLst { get; set; }
    }

    public class HDPMModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        //public string Question13 { get; set; }
        //public string Answer13 { get; set; }
        //public string Question14 { get; set; }
        //public string Answer14 { get; set; }
        //public string Question15 { get; set; }
        //public string Answer15 { get; set; }
        //public string Question16 { get; set; }
        //public string Answer16 { get; set; }
        public string PS_ID { get; set; }
        public string PR_ID { get; set; }
        public string RatingFrom { get; set; }

        public RatingCommonmodel RatingHeader { get; set; }
        public QuestionnaireModel QuestionModel { get; set; }
        public List<QuestionnaireObjectModel> QuestionnaireLst { get; set; }
    }

    public class HDPEModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        //public string Question18 { get; set; }
        //public string Answer18 { get; set; }
        //public string Question19 { get; set; }
        //public string Answer19 { get; set; }
        //public string Question20 { get; set; }
        //public string Answer20 { get; set; }
        //public string Question21 { get; set; }
        //public string Answer21 { get; set; }
        public string PS_ID { get; set; }
        public string PR_ID { get; set; }
        public string RatingFrom { get; set; }


        public RatingCommonmodel RatingHeader { get; set; }

        public QuestionnaireModel QuestionModel { get; set; }
        public List<QuestionnaireObjectModel> QuestionnaireLst { get; set; }
    }

    public class PEPMModel
    {

        public string Question { get; set; }
        public string Answer { get; set; }
        //public string Question23 { get; set; }
        //public string Answer23 { get; set; }
        //public string Question24 { get; set; }
        //public string Answer24 { get; set; }
        //public string Question25 { get; set; }
        //public string Answer25 { get; set; }
        //public string Question26 { get; set; }
        //public string Answer26 { get; set; }
        public string RatingFrom { get; set; }

        public string PS_ID { get; set; }
        public string PR_ID { get; set; }
        public RatingCommonmodel RatingHeader { get; set; }
        public QuestionnaireModel QuestionModel { get; set; }
        public List<QuestionnaireObjectModel> QuestionnaireLst { get; set; }
    }

    public class PMPEModel
    {

        public string Question { get; set; }
        public string Answer { get; set; }
        //public string Question28 { get; set; }
        //public string Answer28 { get; set; }
        //public string Question29 { get; set; }
        //public string Answer29 { get; set; }
        //public string Question30 { get; set; }
        //public string Answer30 { get; set; }
        //public string Question31 { get; set; }
        //public string Answer31 { get; set; }
        public string RatingFrom { get; set; }


        public string PS_ID { get; set; }
        public string PR_ID { get; set; }
        public RatingCommonmodel RatingHeader { get; set; }
        public QuestionnaireModel QuestionModel { get; set; }
        public List<QuestionnaireObjectModel> QuestionnaireLst { get; set; }
    }
    public class SAPMModel
    {

        public string Question { get; set; }
        public string Answer { get; set; }

        public string RatingFrom { get; set; }


        public string PS_ID { get; set; }
        public string PR_ID { get; set; }
        public RatingCommonmodel RatingHeader { get; set; }
        public QuestionnaireModel QuestionModel { get; set; }
        public List<QuestionnaireObjectModel> QuestionnaireLst { get; set; }
    }

    public class ReviewSummaryModel
    {
        public bool IsOpen { get; set; }
        public DateTime PS_Date { get; set; }
        public List<string> Final { get; set; }
        public List<ReviewModel> LstReview { get; set; }
        public string ProjectName { get; set; }
        public bool PS_Status { get; set; }

        //public QuestionnaireModel QuestionModel { get; set; }
        //public List<QuestionnaireObjectModel> QuestionnaireLst { get; set; }
    }

    public class ReviewModel
    {
        public string PRType { get; set; }
        public int PS_ID { get; set; }
        public int PR_ID { get; set; }
        public string PR_From { get; set; }
        public string PR_RatingTo { get; set; }
        public int Q_Id { get; set; }
        public int PSD_Rating { get; set; }
        public string PS_ProjectName { get; set; }
        public string PSD_Yesorno { get; set; }
        public string PSD_Mcq { get; set; }
        public DateTime PSD_CreatedDate { get; set; }
        public string Q_Question { get; set; }
        //public string PS_ClientName { get; set;}
        //public string PS_HD_Name { get; set; }
        //public string PS_PE_Name { get; set; }
        //public string PS_PM_Name { get; set; }
        public DateTime PS_Date { get; set; }
        public bool PS_Status { get; set; }
        public string Q_Ans_Type { get; set; }

        public string Ans_Options1 { get; set; }
        public string Ans_Options2 { get; set; }
        public string Ans_Options3 { get; set; }
        public string Ans_Options4 { get; set; }
        public string Ans_Options5 { get; set; }


    }

    public class RatingCommonmodel
    {
        public int PS_ID { get; set; }
        public string PS_ProjectName { get; set; }
        public string PR_RatingTo { get; set; }
        public string PRType { get; set; }
        public int PR_ID { get; set; }

        //public string PS_ProjectName { get; set; }
        public string PS_ProjectCode { get; set; }
        public string RatingTo { get; set; }
        public string RatingFrom { get; set; }
        public bool SurveyStatus { get; set; }

        //public bool PS_Client_PM_Status { get; set; }
        //public bool PS_HD_PM_Status { get; set; }
        //public bool PS_Client_PE_Status { get; set; }
        //public bool PS_HD_PE_Status { get; set; }
        //public bool PS_PE_Status { get; set; }
        //public bool PS_PM_Status { get; set; }

    }
}