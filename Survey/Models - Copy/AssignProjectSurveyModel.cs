using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Survey.Models;

namespace Survey.Models
{

    public class AssignProjectSurveyModel
    {
        public IEnumerable<SelectListItem> lstProject { get; set; }
        public string ProjectName { get; set; }
        public string Customer { get; set; }
        public bool ClientPM { get; set; }
        public bool ClientPE { get; set; }
        public bool HelpDeskPM { get; set; }
        public bool HelpDeskPE { get; set; }
        public bool PEPM { get; set; }
        public bool PMPE { get; set; }
        public bool SalePM { get; set; }
        public DateTime AssignDate { get; set; }
        public int CreatedBy { get; set; }
    }



    public class UserModel
    {
        //public string Firstname { get; set; }
        //public string Lastname { get; set; }
        //public string EmailId { get; set; }
        //public string Username { get; set; }
        //public string Userrole { get; set; }
        //public string Password { get; set; }
        //public string Mobileno { get; set; }
        ////public int U_ID { get; set; }
        //public bool Status { get; set; }
        public List<SelectListItem> ddlUserStatus { get; set; }

        public int U_ID { get; set; }

        [Display(Name = "FirstName")]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Please enter only alphabets and numbers")]
        [StringLength(50, ErrorMessage = "Please limit your first name to 50 characters")]
        [Required(ErrorMessage = "FirstName is required")]
        public string U_FirstName { get; set; }


        [Display(Name = "LastName")]
        [Required(ErrorMessage = "LastName is required")]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Please enter only alphabets and numbers")]
        [StringLength(50, ErrorMessage = "Please limit your last name to 50 characters")]
        public string U_LastName { get; set; }

        [Display(Name = "Username")]
        [StringLength(50, ErrorMessage = "Please limit your user name to 30 characters")]
        [Required(ErrorMessage = "Username is required")]
        public string U_UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string U_Password { get; set; }

        [Display(Name = "Email ID")]
        [Required(ErrorMessage = "Email ID is required")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Please enter valid E-Mail Id")]
        public string U_Email { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile Number must be exactly 10 digits")]
        public string U_Mobile { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role is required")]
        public string U_Role { get; set; }
        public string userRole { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status is required")]
        public bool U_Status { get; set; }

        [Display(Name = "Profile Image")]
        [Required(ErrorMessage = "Status is required")]
        public string ProfileImage { get; set; }
    }

    

    public class UserName
    {
        public string U_FirstName { get; set; }
    }

    public class FeedbackViewModel
    {

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string FD_Name { get; set; }
        public int FD_ID { get; set; }
        public int U_ID { get; set; }
        public bool FD_Status { get; set; }
        public int FD_CreatedBy { get; set; }
        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime FD_Date { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Subject is required")]
        public string FD_Subject { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string FD_Description { get; set; }
        public string FD_Response { get; set; }
        public int FD_UpdatedBy { get; set; }

    }
    public class UserStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class AssignSurveyModel
    {
        public int SS_ID { get; set; }
        public int SS_AS_ID { get; set; }
        public int SS_P_ID { get; set; }
        public int U_ID { get; set; }
        public string P_Name { get; set; }
        public string UserName { get; set; }
        public string RatingTo { get; set; }
        public string SS_U_Role { get; set; }
        public string SS_status { get; set; }
        public string AS_Customer { get; set; }
        public string SS_Description { get; set; }
    }

    public class AssignSurveyMasterModel
    {
        public List<AssignSurveyModel> AssignSurveyLst { get; set; }
        public AssignSurveyModel AssignSurveyObjModel { get; set; }

        public List<TotalReportModel> LsttotalReport { get; set; }
        public List<TotalPMReportModel> LsttotalPMReport { get; set; }
        public List<TotalPEReportModel> LsttotalPEReport { get; set; }
        public UserModel objUserModel { get; set; }
        public FeedbackViewModel objFBModel { get; set; }
        public List<UserModel> LstUserModel { get; set; }
        public List<FeedbackViewModel> LstFbModel { get; set; }
        public List<SelectListItem> userRole { get; set; }
        public List<SelectListItem> ddlUserStatus { get; set; }




    }

    public class TotalReportModel
    {
        public int Total { get; set; }
        public string ProjectName { get; set; }
        public int PS_Id { get; set; }

    }

    public class TotalPMReportModel
    {
        public int TotalRating { get; set; }
        public string ProjectManagerName { get; set; }
        public int IDCOUNT { get; set; }
        public int PS_Id { get; set; }
    }

    public class TotalPEReportModel
    {
        public int TotalPERating { get; set; }
        public string ProjectEngineerName { get; set; }
        public int PE_IDCOUNT { get; set; }
        public int PS_Id { get; set; }
    }


}