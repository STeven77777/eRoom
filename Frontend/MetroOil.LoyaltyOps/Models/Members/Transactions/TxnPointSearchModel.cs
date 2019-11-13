using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.Members
{
    public class TxnPointSearchModel
    {
        public int TxnInd { get; set; }// 1 -Post , 2- Online/Unpost
        public string AcctNo { get; set; }
        public string AcctNoHash { get; set; }
        [DisplayNameLocalizedAttribute("Txn", "TxnId", "Transaction No.")]
        public string TxnId { get; set; }
        [DisplayNameLocalizedAttribute("Txn", "BusnLocationNo", "Transaction Date & Time")]
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        [DisplayNameLocalizedAttribute("Txn", "BusnLocationNo", "Business Location No.")]
        public string BusnLocationNo { get; set; }
        public IEnumerable<SelectListItem> BusnLocationNos { get; set; }
        [DisplayNameLocalizedAttribute("Txn", "TermId", "Terminal No. / User ID (Merchant)")]
        public string TermId { get; set; }
    }
}