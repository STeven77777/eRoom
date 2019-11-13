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
    public class TxnVoucherSearchModel
    {
        public string AcctNo { get; set; }
        public string AcctNoHash { get; set; }
        [DisplayNameLocalizedAttribute("Txn", "TxnId", "Transaction No.")]
        public string TxnId { get; set; }
        [DisplayNameLocalizedAttribute("Txn", "VoucherNo", "Voucher Code")]
        public string VoucherNo { get; set; }
        //[DisplayNameLocalizedAttribute("Txn", "ProdType", "Voucher Type")]
        //public string ProdType { get; set; }
        //public IEnumerable<SelectListItem> ProdTypes { get; set; }
        //[DisplayNameLocalizedAttribute("Txn", "VoucherSourceCd", "Voucher Source")]
        //public string VoucherSourceCd { get; set; }
        //public IEnumerable<SelectListItem> VoucherSourceCds { get; set; }

        [DisplayNameLocalizedAttribute("Txn", "Sts", "Status")]
        public string Sts { get; set; }
        public IEnumerable<SelectListItem> Stses { get; set; }
        public DateTime? VoucherStartDateFrom { get; set; }
        public DateTime? VoucherStartDateTo { get; set; }
        public DateTime? VoucherExpiredDateFrom { get; set; }
        public DateTime? VoucherExpiredDateTo { get; set; }
        public DateTime? RedeemedDateFrom { get; set; }
        public DateTime? RedeemedDateTo { get; set; }
    }
}