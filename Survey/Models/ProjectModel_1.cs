﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey.Models
{
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
        public string PS_ID { get; set; }
        public DateTime PS_Date { get; set; }
        [Display(Name = "Project Name")]
        public string PS_ProjectName { get; set; }
        [Display(Name = "Project Code")]
        public string PS_ProjectCode { get; set; }
        [Display(Name = "Client Name")]
        public string PS_ClientName { get; set; }
        [Display(Name = "Client Email")]
        public string PS_Client_Email { get; set; }
        public string PS_Client_PM_link { get; set; }
        public bool PS_Client_PM_Status { get; set; }
        public string PS_Client_PE_link { get; set; }
        public bool PS_Client_PE_Status { get; set; }
        [Display(Name = "Project Manager Name")]
        public string PS_PM_Name { get; set; }
        [Display(Name = "Project Manager Email")]
        public string PS_PM_Email { get; set; }
        public string PS_PM_link { get; set; }
        public bool PS_PM_Status { get; set; }
        [Display(Name = "Project Engineer Name")]
        public string PS_PE_Name { get; set; }
        [Display(Name = "Project Engineer Email")]
        public string PS_PE_Email { get; set; }
        public string PS_PE_link { get; set; }
        public bool PS_PE_Status { get; set; }
        [Display(Name = "Help Desk Person Name")]
        public string PS_HD_Name { get; set; }
        [Display(Name = "Help Desk Person Email")]
        public string PS_HD_Email { get; set; }
        public string PS_HD_PM_link { get; set; }
        public bool PS_HD_PM_Status { get; set; }
        public string PS_HD_PE_link { get; set; }
        public bool PS_HD_PE_Status { get; set; }
        public bool PS_Status { get; set; }
        public string PS_CreatedBY { get; set; }
        public DateTime PS_CreatedDate { get; set; }

    }
}