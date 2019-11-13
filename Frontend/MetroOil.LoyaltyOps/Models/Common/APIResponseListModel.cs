using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models
{
    public class APIResponseListModel<T>
    {
        public int ResponseCode { get; set; }
        public string ResponseDesc { get; set; }
        public APIResultModel<T> Result { get; set; }
        public APIResponseListModel()
        {
            Result = new APIResultModel<T>();
        }
    }

    public class APIResultModel<T>
    {
        public int RecordCount { get; set; }
        public List<T> RecordList { get; set; }
        //public List<object> Rows { get; set; }

        public APIResultModel()
        {
            RecordList = new List<T>();
            //Rows = new List<object>();
        }
    }
}