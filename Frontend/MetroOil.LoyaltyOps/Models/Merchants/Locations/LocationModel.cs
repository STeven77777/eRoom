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
    public class LocationModel
    {
        [DisplayNameLocalizedAttribute("Merchant", "AcctNo", "Account No.")]
        public long AcctNo { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "BusnName", "Business Name")]
        public string BusnName { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "CoRegNo", "Company Register No.")]
        public string CoRegNo { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "CoRegName", "Company Register Name")]
        public string CoRegName { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "PersonInChargeName", "Person In Charge Name")]
        public string PersonInChargeName { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "TaxID", "Tax ID")]
        public string TaxID { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "AgreementNo", "Agreement No.")]
        public string AgreementNo { get; set; }
        [DisplayNameLocalizedAttribute("Merchant", "AgreementDate", "Agreement Date")]
        public DateTime? AgreementDate { get; set; }
    }
}