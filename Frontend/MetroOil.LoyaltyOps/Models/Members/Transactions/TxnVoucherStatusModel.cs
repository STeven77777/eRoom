using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models.Members
{
    public class TxnVoucherStatusModel
    {
        public string AcctNo { get { return Helpers.Helper.Decrypt(AcctNoHash); } }
        public string AcctNoHash { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherSts { get { return StsNew; } }
        public string Remark { get { return StsNewRemarks; } }
        public string RedeemedBusnLocation { get; set; }
        public string RedeemedUserId { get; set; }

        public string StsNewRemarks { get; set; }
        public string StsNew { get; set; }
    }
}