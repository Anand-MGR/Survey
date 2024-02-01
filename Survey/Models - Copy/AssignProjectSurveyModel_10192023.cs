using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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