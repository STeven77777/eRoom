using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models
{
    //public class LogChangeModelList
    //{
    //    public int RecordCount { get; set; }
    //    public List<LogChangeModel> RecordList { get; set; }
    //}
    public class LogChangeModel
    {
        public string FieldDesc { get; set; }
        public string OldVal { get; set; }
        public string NewVal { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByName { get; set; }
    }
}