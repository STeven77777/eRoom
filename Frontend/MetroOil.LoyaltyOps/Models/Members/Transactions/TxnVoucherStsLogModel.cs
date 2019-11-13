using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroOil.LoyaltyOps.Helpers;

namespace MetroOil.LoyaltyOps.Models.Members
{
    public class TxnVoucherStsLogModel
    {
        public string Ids { get; set; }
        public string Remark { get; set; }
        public string Sts { get; set; }
        public string StatusName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedDateStr
        {
            get
            {
                if (CreatedDate != null)
                    return CreatedDate.Value.ToString(Enums.DATE_FORMAT);
                return string.Empty;
            }
        }
        public string CreatedByName { get; set; }
    }
}