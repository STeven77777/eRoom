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
    public class MerchantUserModel
    {
        [DisplayNameLocalizedAttribute("Merchants", "UserId", "User ID")]
        public string UserId { get; set; }
        public string UserIdHash { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Merchants", "LoginID", "Username")]
        public string LoginID { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Merchants", "BusnLocationNo", "Business Location No.")]
        public string BusnLocationNo { get; set; }
        public IEnumerable<SelectListItem> BusnLocationNos { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Merchants", "BusnLocationUserRoleCd", "User Role")]
        public string BusnLocationUserRoleCd { get; set; }
        public string UserRoleName { get; set; }
        public IEnumerable<SelectListItem> BusnLocationUserRoles { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Merchants", "FullName", "Full Name")]
        public string FullName { get; set; }
        [DisplayNameLocalizedAttribute("Merchants", "TitleCd", "Title")]
        public string TitleCd { get; set; }
        public string TitleName { get; set; }
        [Required]
        public IEnumerable<SelectListItem> Titles { get; set; }

        [DisplayNameLocalizedAttribute("Merchants", "HpNo", "Mobile Phone No.")]
        [RegularExpression(@"^([\+]?(?:00)?[0-9]{1,12})$", ErrorMessage = "Mobile Phone No. is invalid.")]
        public string HpNo { get; set; }

        public string HpCtryCode { get; set; }
        public IEnumerable<SelectListItem> HpCtryCodes { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("Member", "EmailAddress", "Email")]
        [EmailAddress(ErrorMessage = "Incorrect email format.")]
        public string EmailAddress { get; set; }
        [DisplayNameLocalizedAttribute("Merchants", "Sts", "User Status")]
        public string Sts { get; set; }
        public string StsName { get; set; }
        public IEnumerable<SelectListItem> Stses { get; set; }
        [DisplayNameLocalizedAttribute("Merchants", "CreatedDate", "Created On")]
        public DateTime? CreatedDate { get; set; }
        [DisplayNameLocalizedAttribute("Merchants", "CreatedBy", "Created By")]
        public string CreatedBy { get; set; }
        [DisplayNameLocalizedAttribute("Merchants", "CreatedByName", "Created By")]
        public string CreatedByName { get; set; }

        [DisplayNameLocalizedAttribute("Merchants", "IsLockedOut", "Locked")]
        public bool? IsLockedOut { get; set; }
        public MerchantUserModel()
        {
            Titles = new List<SelectListItem>();
            BusnLocationNos = new List<SelectListItem>();
            BusnLocationUserRoles = new List<SelectListItem>();
            Stses = new List<SelectListItem>();
        }
    }
}