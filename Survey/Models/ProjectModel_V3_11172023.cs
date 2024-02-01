using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey.Models
{
    public class ProjectMasterModel
    {
        public ProjectSurveyModel objProjectsurveymodel { get; set; }
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
        [Required(ErrorMessage = "Email Id Required.")]
        //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Please enter valid email address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string PS_Client_Email { get; set; }
        public string PS_Client_PM_link { get; set; }
        public bool PS_Client_PM_Status { get; set; }
        public string PS_Client_PE_link { get; set; }
        public bool PS_Client_PE_Status { get; set; }
        [Display(Name = "Project Manager Name")]
        [Required(ErrorMessage = "Project Manager Name Required.")]
        public string PS_PM_Name { get; set; }
        [Display(Name = "Project Manager Email")]
        [Required(ErrorMessage = "Email Id Required.")]
        // [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Please enter valid email address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string PS_PM_Email { get; set; }
        public string PS_PM_link { get; set; }
        public bool PS_PM_Status { get; set; }
        [Display(Name = "Project Engineer Name")]
        [Required(ErrorMessage = "Project Engineer Name Required.")]
        public string PS_PE_Name { get; set; }
        [Display(Name = "Project Engineer Email")]
        [Required(ErrorMessage = "Email Id Required.")]
        //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Please enter valid email address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string PS_PE_Email { get; set; }
        public string PS_PE_link { get; set; }
        public bool PS_PE_Status { get; set; }
        [Display(Name = "Help Desk Person Name")]
        [Required(ErrorMessage = "Help Desk Person Name Required.")]
        public string PS_HD_Name { get; set; }
        [Display(Name = "Help Desk Person Email")]
        [Required(ErrorMessage = "Email Id Required.")]
        //   [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Please enter valid email address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string PS_HD_Email { get; set; }
        public string PS_HD_PM_link { get; set; }
        public bool PS_HD_PM_Status { get; set; }
        public string PS_HD_PE_link { get; set; }
        public bool PS_HD_PE_Status { get; set; }
        public bool PS_Status { get; set; }
        public string PS_CreatedBY { get; set; }
        public DateTime PS_CreatedDate { get; set; }

        public string ClientName { get; set; }

    }
}