using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.BusnLocation
{
    public class TxnPointSearchModel
    {
        public int TxnInd { get; set; }// 1 -Post , 2- Online/Unpost
        public string BusnLocation { get; set; }
        public string BusnLocationNoHash { get; set; }
        [DisplayNameLocalizedAttribute("BusnLocationTxn", "TxnId", "Transaction No.")]
        public string TxnId { get; set; }
        [DisplayNameLocalizedAttribute("BusnLocationTxn", "BusnLocationNo", "Transaction Date & Time")]
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        //[DisplayNameLocalizedAttribute("Txn", "BusnLocationNo", "Business Location No.")]
        //public string BusnLocationNo { get; set; }
        //public IEnumerable<SelectListItem> BusnLocationNos { get; set; }
        [DisplayNameLocalizedAttribute("BusnLocationTxn", "TermId", "Terminal No. / Pump Attendant ID")]
        public string TermId { get; set; }

        [DisplayNameLocalizedAttribute("BusnLocationTxn", "AcctNo", "Member Account No.")]
        public string AcctNo { get; set; }

        [DisplayNameLocalizedAttribute("BusnLocationTxn", "CardNo", "Card No.")]
        public string CardNo { get; set; }
        [DisplayNameLocalizedAttribute("BusnLocationTxn", "FullName", "Name on Card")]
        public string FullName { get; set; }
    }
}