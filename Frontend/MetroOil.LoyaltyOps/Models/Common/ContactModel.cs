using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models
{
    public class ContactModel
    {
        public int AcctNo { get; set; }
        public string AcctNoHash { get; set; }
        public int EntityId { get; set; }
        public string Ids { get; set; }

        [DisplayNameLocalizedAttribute("Contact", "MainInd", "Main Contact")]
        public bool? MainInd { get; set; }
        public string MainIndName { get; set; }

        //[Required]
        //[DisplayNameLocalizedAttribute("Contact", "FamilyName", "Last Name/Family Name")]
        //public string FamilyName { get; set; }
        //[Required]
        //[DisplayNameLocalizedAttribute("Contact", "GivenName", "First Name/Given Name")]
        //public string GivenName { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Contact", "FullName", "Contact Person")]
        public string FullName { get; set; }

        //[DisplayNameLocalizedAttribute("Contact", "TitleCd", "Title")]
        //public string TitleCd { get; set; }
        //public IEnumerable<SelectListItem> Titles { get; set; }

        //public string TitleName { get; set; }

        [DisplayNameLocalizedAttribute("Contact", "Occupation", "Position")]
        public string Occupation { get; set; }
        public IEnumerable<SelectListItem> Occupations { get; set; }

        public string OccupationName { get; set; }

        [RegularExpression(@"^([\+]?(?:00)?[0-9]{1,12})$", ErrorMessage = "Not a valid Home Phone number")]
        [DisplayNameLocalizedAttribute("Contact", "HomeTelNo", "Home Phone No.")]
        public string HomeTelNo { get; set; }
        public string HomeTelCtryCode { get; set; }
        public IEnumerable<SelectListItem> HomeTelCtryCodes { get; set; }

        [RegularExpression(@"^([\+]?(?:00)?[0-9]{1,12})$", ErrorMessage = "Not a valid Mobile Phone number")]
        [DisplayNameLocalizedAttribute("Contact", "HpNo", "Mobile Phone No.")]
        public string HpNo { get; set; }
        public string HpCtryCode { get; set; }
        public IEnumerable<SelectListItem> HpCtryCodes { get; set; }

        [RegularExpression(@"^([\+]?(?:00)?[0-9]{1,12})$", ErrorMessage = "Not a valid Work Phone number")]
        [DisplayNameLocalizedAttribute("Contact", "WorkTelNo", "Work Phone No.")]
        public string WorkTelNo { get; set; }
        public string WorkTelCtryCode { get; set; }
        public IEnumerable<SelectListItem> WorkTelCtryCodes { get; set; }

        [RegularExpression(@"^([\+]?(?:00)?[0-9]{1,12})$", ErrorMessage = "Not a valid Fax number")]
        [DisplayNameLocalizedAttribute("Contact", "FaxNo", "Fax No.")]
        public string FaxNo { get; set; }
        public string FaxCtryCode { get; set; }
        public IEnumerable<SelectListItem> FaxCtryCodes { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayNameLocalizedAttribute("Contact", "EmailAddress", "Email")]
        public string EmailAddress { get; set; }

        [DisplayNameLocalizedAttribute("Contact", "CreatedDate", "Created On")]
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
        [DisplayNameLocalizedAttribute("Contact", "CreatedByName", "Created By")]
        public string CreatedByName { get; set; }

        public bool VerifyHpNo { get; set; }

        #region Change Mobile Phone No.
        [RegularExpression(@"^([\+]?(?:00)?[0-9]{1,12})$", ErrorMessage = "Not a valid Mobile Phone number")]
        [DisplayNameLocalizedAttribute("Contact", "HpNewHpNo", "New Mobile Phone No.")]
        public string HpNewHpNo { get; set; }
        public string HpNewHpCtryCode { get; set; }
        public IEnumerable<SelectListItem> HpNewHpCtryCodes { get; set; }

        [DisplayNameLocalizedAttribute("Contact", "HpNewRemarks", "Remarks")]
        public string HpNewRemarks { get; set; }
        #endregion

        public ContactModel()
        {
            Occupations = new List<SelectListItem>();
            HomeTelCtryCodes = new List<SelectListItem>();
            HpCtryCodes = new List<SelectListItem>();
            WorkTelCtryCodes = new List<SelectListItem>();
            FaxCtryCodes = new List<SelectListItem>();
        }
    }

    public class MobileChangeNoModel
    {
        public string AcctNo { get { return Helper.Decrypt(AcctNoHash); } }
        public string AcctNoHash { get; set; }
        //public string Ids { get; set; }
        public string HpNo { get { return HpNewHpNo; } }
        public string HpCtryCode { get { return HpNewHpCtryCode; } }
        public string Descp { get { return HpNewRemarks; } }

        public string HpNewHpNo { get; set; }
        public string HpNewHpCtryCode { get; set; }
        public string HpNewRemarks { get; set; }
    }
}