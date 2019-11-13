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
    public class TxnVoucherModel
    {
        public int VoucherId { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "VoucherNo", "Voucher Code")]
        public string VoucherNo { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "RefNo", "Txn. No.")]
        public string RefNo { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "StartDateStr", "Voucher Start Date & Time")]
        public string StartDateStr
        {
            get
            {
                if (StartDate != null)
                    return StartDate.Value.ToString(Enums.DATE_TIME_FORMAT);
                else
                    return string.Empty;
            }
        }
        public DateTime? StartDate { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "EndDateStr", "Voucher Expiry Date & Time")]
        public string EndDateStr
        {
            get
            {
                if (EndDate != null)
                    return EndDate.Value.ToString(Enums.DATE_TIME_FORMAT);
                else
                    return string.Empty;
            }
        }
        public DateTime? EndDate { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "IssuedDateStr", "Issue Date")]
        public string IssuedDateStr
        {
            get
            {
                if (IssuedDate != null)
                    return IssuedDate.Value.ToString(Enums.DATE_FORMAT);
                else
                    return string.Empty;
            }
        }
        public DateTime? IssuedDate { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "RedeemedDateStr", "Voucher Redeemed Date & Time")]
        public string RedeemedDateStr
        {
            get
            {
                if (RedeemedDate != null)
                    return RedeemedDate.Value.ToString(Enums.DATE_TIME_FORMAT);
                else
                    return string.Empty;
            }
        }
        public DateTime? RedeemedDate { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "ItemName", "Voucher Name")]
        public string ItemName { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "ItemDescp", "Description")]
        public string ItemDescp { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "StatusName", "Status")]
        public string StatusName { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "VoucherSourceName", "Voucher Source")]
        public string VoucherSourceName { get; set; }
        //[DisplayNameLocalizedAttribute("TxnVoucher", "ProdTypeName", "Category")]
        //public string ProdTypeName { get; set; }

        public string AcctNo { get; set; }
        public string ProdCd { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "Remark1", "Terms and Conditions")]
        public string Remark1 { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "Remark2", "Terms and Conditions")]
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
        public string Remark4 { get; set; }
        public string Remark5 { get; set; }
        public string ImageURLPath { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "Pts", "Points")]
        public decimal Pts { get; set; }
        public decimal Amt { get; set; }
        public string Sts { get; set; }
        public IEnumerable<SelectListItem> Stses { get; set; }
        public string IssuedUserId { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "IssuedByName", "Issued By")]
        public string IssuedByName { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "PurchaseDateStr", "Voucher Purchase Date & Time")]
        public string PurchaseDateStr
        {
            get
            {
                if (PurchaseDate != null)
                    return PurchaseDate.Value.ToString(Enums.DATE_TIME_FORMAT);
                else
                    return string.Empty;
            }
        }
        public DateTime? PurchaseDate { get; set; }
        public string PurchaseUserId { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "PurchaseByName", "Purchased By")]
        public string PurchaseByName { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("TxnVoucher", "RedeemedUserId", "Authorized By")]
        public string RedeemedUserId { get; set; }
        [DisplayNameLocalizedAttribute("TxnVoucher", "RedeemedByName", "Authorized By")]
        public string RedeemedByName { get; set; }
        public string VoucherSourceCd { get; set; }
        public bool ThirdPartyVoucherInd { get; set; }
        public string ProdType { get; set; }

        [DisplayNameLocalizedAttribute("TxnVoucher", "RedeemedBusnLocationName", "Redeemed At")]
        public string RedeemedBusnLocationName { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("TxnVoucher", "StsNewRemarks", "Remarks")]
        public string StsNewRemarks { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("TxnVoucher", "RedeemedBusnLocation", "Redeemed At")]
        public string RedeemedBusnLocation { get; set; }
        public IEnumerable<SelectListItem> RedeemedBusnLocations { get; set; }
        public IEnumerable<SelectListItem> RedeemedBys { get; set; }
    }
}