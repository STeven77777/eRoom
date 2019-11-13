using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models.Reports
{
    public class RptDynControlModel
    {
        public int RptId { get; set; }
        public string RptName { get; set; }
        //public string RptSpName { get; set; }
        public string ControlLabelName { get; set; }
        public string ControlType { get; set; }
        public string ControlModelName { get; set; }
        public string ControlModelId { get; set; }
        public string ControlSeqNo { get; set; }
        public string ControlSpName { get; set; }
        public List<RptDynSelectSrcModel> DataSources { get; set; }

        // Convert to dynamic library model
        public string type { get { return ControlType; } }
        public string model { get { return ControlSeqNo; } }
        public string label { get { return ControlLabelName; } }
        public List<RptDynSelectSrcModel> options { get { return DataSources; } }

        public RptDynControlModel()
        {
            DataSources = new List<RptDynSelectSrcModel>();
        }
    }

    public class RptDynSelectSrcModel
    {
        public string TextName { get; set; }
        public string ValueName { get; set; }
    }
}