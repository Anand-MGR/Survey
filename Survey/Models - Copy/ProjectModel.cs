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
        public QuestionsForm QuestionsForm { get; set; }
        public List<QuestionsForm> objQuestionsForm { get; set; }
      
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

    }



    public class QuestionsForm
    {
        public List<SelectListItem> ddlQUserStatus { get; set; }
        public int Q_ID { get; set; }
        [Display(Name = "Type")]
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
    }

   
    public class QUserStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string value { get; set; }

    }
}