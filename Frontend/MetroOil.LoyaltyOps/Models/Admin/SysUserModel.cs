using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.Admin
{
    public class SysUserModel
    {
        [DisplayNameLocalizedAttribute("OpsUser", "UserId", "User ID")]
        public string UserId { get; set; }
        public string UserIdHash { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("OpsUser", "LoginID", "Username")]
        public string LoginID { get; set; }

        public string EntityId { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("OpsUser", "FullName", "Full Name")]
        public string FullName { get; set; }
        [DisplayNameLocalizedAttribute("OpsUser", "TitleCd", "Title")]
        public string TitleCd { get; set; }
        public string TitleName { get; set; }
        public IEnumerable<SelectListItem> Titles { get; set; }
        [DisplayNameLocalizedAttribute("OpsUser", "HpNo", "Mobile Phone No.")]
        [RegularExpression(@"^([\+]?(?:00)?[0-9]{1,12})$", ErrorMessage = "Mobile Phone No. is invalid.")]
        public string HpNo { get; set; }
        public string FullHpNo { get; set; }
        public string HpCtryCode { get; set; }
        public IEnumerable<SelectListItem> HpCtryCodes { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("OpsUser", "EmailAddress", "Email")]
        [EmailAddress(ErrorMessage = "Incorrect email format.")]
        public string EmailAddress { get; set; }
        [DisplayNameLocalizedAttribute("OpsUser", "Sts", "User Status")]
        public string Sts { get; set; }
        public string StsName { get; set; }
        public List<SelectListItem> Stses { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("OpsUser", "UserGroupCd", "User Role")]
        public string UserGroupCd { get; set; }
        public string UserGroupName { get; set; }
        public IEnumerable<SelectListItem> UserGroups { get; set; }

        [DisplayNameLocalizedAttribute("OpsUser", "CreatedDate", "Created On")]
        public DateTime? CreatedDate { get; set; }
        [DisplayNameLocalizedAttribute("OpsUser", "CreatedBy", "Created By")]
        public string CreatedBy { get; set; }
        [DisplayNameLocalizedAttribute("OpsUser", "CreatedByName", "Created By")]
        public string CreatedByName { get; set; }

        [DisplayNameLocalizedAttribute("OpsUser", "IsLockedOut", "Locked")]
        public bool? IsLockedOut { get; set; }

        public SysUserModel()
        {
            Titles = new List<SelectListItem>();
            HpCtryCodes = new List<SelectListItem>();
            Stses = new List<SelectListItem>();
            UserGroups = new List<SelectListItem>();
        }
    }
}