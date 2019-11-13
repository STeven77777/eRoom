using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace MetroOil.LoyaltyOps.Models.Reports
{
    public class ReportViewerModel
    {
        [DisplayNameLocalizedAttribute("ReportViewer", "RptId", "Report Name")]
        public string RptId { get; set; }
        public List<SelectListItem> RptNames { get; set; }
    }
}