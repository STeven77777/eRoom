using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.Admin
{
    public class RewardItemModel
    {
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "ProdCd", "Item Code")]
        public string ProdCd { get; set; } // Item Code
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "ItemName", "Item Name")]
        public string ItemName { get; set; } // Item Name
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "ItemDescp", "Description")]
        public string ItemDescp { get; set; } // Description
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "ProdType", "Item Category")]
        public string ProdType { get; set; }// Category
        public IEnumerable<SelectListItem> ProdTypes { get; set; }
        [DisplayNameLocalizedAttribute("RewardItem", "ProdTypeName", "Item Category")]
        public string ProdTypeName { get; set; } // Category
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "ProdClassCd", "Item Class")]
        public string ProdClassCd { get; set; } // Item Class
        public IEnumerable<SelectListItem> ProdClassCds { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "ProdClassName", "Item Class")]
        public string ProdClassName { get; set; } // Item Class
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "Sts", "Status")]
        public string Sts { get; set; }
        public IEnumerable<SelectListItem> Stses { get; set; }
        public string StatusName { get; set; }
        [DisplayNameLocalizedAttribute("RewardItem", "CreatedDate", "Created On")]
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        [DisplayNameLocalizedAttribute("RewardItem", "CreatedDate", "Created By")]
        public string CreatedByName { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "StartDate", "Available Period")]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        //[DisplayNameLocalizedAttribute("RewardItem", "Amt", "Cost (PHP)")]
        //public decimal? Amt { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "Pts", "Points to Redeem")]
        public decimal? Pts { get; set; }
        [DisplayNameLocalizedAttribute("RewardItem", "VoucherInd", "Tick this if it is a voucher")]
        public bool? VoucherInd { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "VoucherSourceCd", "Voucher Source")]
        public string VoucherSourceCd { get; set; }
        public IEnumerable<SelectListItem> VoucherSourceCds { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "VoucherAllocateTypeCd", "Voucher Allocation")]
        public string VoucherAllocateTypeCd { get; set; }
        public IEnumerable<SelectListItem> VoucherAllocateTypeCds { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "VoucherValidDays", "Validity of Days")]
        public int? VoucherValidDays { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "VoucherValidStartDate", "Validity Period")]
        public DateTime? VoucherValidStartDate { get; set; }
        public DateTime? VoucherValidEndDate { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("RewardItem", "ExtPartnerCd", "External Partner")]
        public string ExtPartnerCd { get; set; }
        public IEnumerable<SelectListItem> ExtPartnerCds { get; set; }
        [DisplayNameLocalizedAttribute("RewardItem", "Remark1", "Terms and Conditions")]
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }

        public string VoucherValidPeriodTypeCd { get; set; }
        public IEnumerable<SelectListItem> VoucherValidPeriodTypeCds { get; set; }

        public RewardItemModel()
        {
            ProdTypes = new List<SelectListItem>();
            ProdClassCds = new List<SelectListItem>();
            Stses = new List<SelectListItem>();
            VoucherSourceCds = new List<SelectListItem>();
            VoucherAllocateTypeCds = new List<SelectListItem>();
            VoucherValidPeriodTypeCds = new List<SelectListItem>();
            ExtPartnerCds = new List<SelectListItem>();
        }
    }
}