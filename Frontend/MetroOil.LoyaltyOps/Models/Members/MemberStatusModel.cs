using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MetroOil.LoyaltyOps.Models
{
    public class MemberStatusModel
    {
        public string AcctNo { get { return Helpers.Helper.Decrypt(AcctNoHash); } }
        public string AcctNoHash { get; set; }
        public string Sts { get { return StsNew;  } }
        public string ReasonCd { get { return StsNewReason; } }
        public string Descp { get { return StsNewRemarks; } }

        public string StsNewRemarks { get; set; }
        public string StsNew { get; set; }
        public string StsNewReason { get; set; }
    }
}