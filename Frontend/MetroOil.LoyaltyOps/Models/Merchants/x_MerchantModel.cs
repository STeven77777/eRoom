using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.Merchants
{
    public class x_MerchantModel
    {
        [DisplayNameLocalizedAttribute("Merchant", "MainAcctNo", "Merchant Account No.")]
        public string MerchAcctNo { get; set; }
        public string MerchAcctNoHash { get; set; }
        public string EntityId { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Merchant", "BusnName", "Business Name")]
        public string BusnName { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Merchant", "CoRegNo", "Company Register No.")]
        public string CoRegNo { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "CoRegDate", "Company Registration Date")]
        public DateTime CoRegDate { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Merchant", "CoRegName", "Company Register Name")]
        public string CoRegName { get; set; }

        //public string

        [DisplayNameLocalizedAttribute("Merchant", "TaxId", "Tax ID")]
        public string TaxId { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "AgreementDate", "Agreement Date")]
        public DateTime AgreementDate { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "AgreementNo", "Agreement No.")]
        public string AgreementNo { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "PersonInChargeFirstName", "Person In Charge First Name")]
        public string PersonInChargeFirstName { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "PersonInChargeLastName", "Person In Charge Last Name")]
        public string PersonInChargeLastName { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "Sts", "Merchant Account Status")]
        public string Sts { get; set; }
        public List<SelectListItem> Statuses { get; set; }
        public string StsName { get; set; }

        [DisplayNameLocalizedAttribute("Merchant", "CreatedDate", "Created On")]
        public DateTime CreatedDate { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "CreatedBy", "Created By")]
        public string CreatedBy { get; set; }

        public x_MerchantModel()
        {
            Statuses = new List<SelectListItem>();
        }
    }
}