using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Models
{
    public class ProjectMasterModel
    {
        public ProjectSurveyModel objProjectsurveymodel { get; set; }
        public List<SelectListItem> ddlQUserStatus { get; set; }
        public List<SelectListItem> ddlAnsType { get; set; }
        public QuestionsForm QuestionsForm { get; set; }
        public EmailTemplateModel ObjEmailTemplateModel { get; set; }
        public QuestionsTypeForm QuestionsTypeForm { get; set; }
        public List<QuestionsTypeForm> LstQuestiontypeFrom { get; set; }
        public List<QuestionsForm> objQuestionsForm { get; set; }

        public List<EmailTemplateModel> lstEmailTemplate { get; set; }

    }

    public class ProjectModel
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public bool Projectstatus { get; set; }
        public int ProjectCreatedBy { get; set; }
    }

    public class ProjectSurveyModel
    {
        public int PS_ID { get; set; }
        public DateTime PS_Date { get; set; }
        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "Project Name Required.")]
        public string PS_ProjectName { get; set; }
        [Display(Name = "Project Code")]
        [Required(ErrorMessage = "Project Code Required.")]
        public string PS_ProjectCode { get; set; }

        [Display(Name = "Client Name")]
        [Required(ErrorMessage = "Client Name Required.")]
        public string PS_ClientName { get; set; }
        [Display(Name = "Client Email")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Please enter valid E-Mail Id")]
        public string PS_Client_Email { get; set; }
        public string PS_Client_PM_link { get; set; }
        public bool PS_Client_PM_Status { get; set; }
        public string PS_Client_PE_link { get; set; }
        public bool PS_Client_PE_Status { get; set; }

        [Display(Name = "Project Manager Name")]
        [Required(ErrorMessage = "Project Manager Name Required.")]
        public string PS_PM_Name { get; set; }
        [Display(Name = "Project Manager Email")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Please enter valid E-Mail Id")]
        public string PS_PM_Email { get; set; }
        public string PS_PM_link { get; set; }
        public bool PS_PM_Status { get; set; }
        [Display(Name = "Project Engineer Name")]
        [Required(ErrorMessage = "Project Engineer Name Required.")]
        public string PS_PE_Name { get; set; }
        [Display(Name = "Project Engineer Email")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Please enter valid E-Mail Id")]
        public string PS_PE_Email { get; set; }
        public string PS_PE_link { get; set; }
        public bool PS_PE_Status { get; set; }
        [Display(Name = "Help Desk Person Name")]
        [Required(ErrorMessage = "Help Desk Person Name Required.")]
        public string PS_HD_Name { get; set; }
        [Display(Name = "Help Desk Person Email")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Please enter valid E-Mail Id")]
        public string PS_HD_Email { get; set; }
        public string PS_HD_PM_link { get; set; }
        public bool PS_HD_PM_Status { get; set; }
        public string PS_HD_PE_link { get; set; }
        public bool PS_HD_PE_Status { get; set; }
        public bool PS_Status { get; set; }
        public string PS_CreatedBY { get; set; }
        public string ClientName { get; set; }
        public DateTime PS_CreatedDate { get; set; }
        [Display(Name = "Solution Architect Name")]
        [Required(ErrorMessage = "Help Desk Person Name Required.")]
        public string PS_SA_Name { get; set; }
        [Display(Name = "Solution Architect Email")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Please enter valid E-Mail Id")]
        public string PS_SA_Email { get; set; }
        public string PS_SA_PM_link { get; set; }
        public string PS_SA_PE_link { get; set; }
        public string PS_PE_SA_link { get; set; }
        public string PS_PM_SA_link { get; set; }
        public bool PS_SA_PM_Status { get; set; }
        public bool PS_SA_PE_Status { get; set; }
        public bool PS_PE_SA_Status { get; set; }
        public bool PS_PM_SA_Status { get; set; }


    }



    public class QuestionsForm
    {
        public List<SelectListItem> ddlQUserStatus { get; set; }
        public List<SelectListItem> ddlAnsType { get; set; }
        public int Q_ID { get; set; }
        [Display(Name = "Question Type")]
        [Required(ErrorMessage = "Type is required")]
        public string Q_Type { get; set; }

        [Display(Name = "Question")]
        [Required(ErrorMessage = "Question is required")]
        public string Q_Question { get; set; }



        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status is required")]
        public bool Q_Status { get; set; }
        public int QuestionSequence { get; set; }
        public int Rating { get; set; }


        public int QuestionID { get; set; }

        public string QuestionType { get; set; }

        public int QuestionStatus { get; set; }

        [Display(Name = "Answer Type")]
        [Required(ErrorMessage = "Type is required")]
        public string Q_Ans_Type { get; set; }
        public string Ans_Options1 { get; set; }
        public string Ans_Options2 { get; set; }
        public string Ans_Options3 { get; set; }
        public string Ans_Options4 { get; set; }
        public string Ans_Options5 { get; set; }

    }

    public class QuestionsTypeForm
    {
        [Display(Name = "Question Type")]
        [Required(ErrorMessage = "Question Type is required")]
        public string Q_QuestionType { get; set; }

        public bool QuestionTypeStatus { get; set; }

        public int QT_ID { get; set; }
    }
    public class QUserStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string value { get; set; }


    }


    public class AnsType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int value { get; set; }


    }


    public class EmailTemplateModel
    {

        public int TemplateId { get; set; }
        public int? TemplateMapId { get; set; }
        public string TemplateName { get; set; }
        public string Q_type { get; set; }

        public string Body_Content { get; set; }

        public bool TemplateActiveStatus { get; set; }
        public bool TemplateMapActiveStatus { get; set; }
        public List<SelectListItem> ddlQUserStatus { get; set; }
        public List<SelectListItem> ddlTemplateName { get; set; }
    }
}