using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//[RegularExpression(@"^([\+]?(?:00)?[0-9]{1,12})$", ErrorMessage = "Not a valid Home Phone number")]
//[DisplayNameLocalizedAttribute("Contact", "HomePhoneNoLbl", "Home Phone No.")]

namespace MetroOil.LoyaltyOps.Models.Members
{
    public class MemberModel
    {
        [DisplayNameLocalizedAttribute("Member", "MainAcctNo", "Member Account No.")]
        public string AcctNo { get; set; }
        public string AcctNoHash { get; set; }
        [DisplayNameLocalizedAttribute("Member", "LoginID", "Login ID (Mobile Phone No.)")]
        public string LoginID { get; set; }

        public int EntityId { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Member", "FullName", "Full Name")]
        public string FullName { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Member", "TitleCd", "Title")]
        public string TitleCd { get; set; }
        public string TitleName { get; set; }
        public IEnumerable<SelectListItem> Titles { get; set; }

        [DisplayNameLocalizedAttribute("Member", "HpNo", "Mobile Phone No.")]
        [RegularExpression(@"^([\+]?(?:00)?[0-9]{1,12})$", ErrorMessage = "Mobile Phone No. is invalid.")]
        public string HpNo { get; set; }
        public string FullHpNo { get; set; }
        public string HpCtryCode { get; set; }
        public IEnumerable<SelectListItem> HpCtryCodes { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Member", "EmailAddr", "Email")]
        [EmailAddress(ErrorMessage = "Incorrect email format.")]
        public string EmailAddr { get; set; }

        public string InputSourceInd { get; set; }
        public bool? VerifyHpNo { get; set; }

        [DisplayNameLocalizedAttribute("Member", "JoinDate", "Joining Date")]
        public DateTime? JoinDate { get; set; }
        [DisplayNameLocalizedAttribute("Member", "Sts", "Account Status")]
        public string Sts { get; set; }
        public string StsName { get; set; }
        public List<SelectListItem> AccountStatuses { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Member", "Gender", "Gender")]
        public string Gender { get; set; }
        public List<SelectListItem> Genders { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Member", "Dob", "Date of Birth")]
        public DateTime? Dob { get; set; }
        [DisplayNameLocalizedAttribute("Member", "NewIc", "Identity No.")]
        public string NewIc { get; set; }
        [DisplayNameLocalizedAttribute("Member", "PassportNo", "Passport No.")]
        public string PassportNo { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("Member", "TnC", "This member has agreed to the Terms and Conditions.")]
        public bool? TnC { get; set; }

        public bool HasTickedTnC { get { return TnC ?? false; } } // For UI only, if TnC is ticked, it can not be changed. HasTickedTnC just make this rule.

        [DisplayNameLocalizedAttribute("Member", "MaritalStsCd", "Marital Status")]
        public string MaritalStsCd { get; set; }
        public IEnumerable<SelectListItem> MaritalStses { get; set; }
        [DisplayNameLocalizedAttribute("Member", "EmploymentStsCd", "Employment Status")]
        public string EmploymentStsCd { get; set; }
        public IEnumerable<SelectListItem> EmploymentStses { get; set; }

        [DisplayNameLocalizedAttribute("Member", "VehicleTypeCd", "Vehicle Type")]
        public string VehicleTypeCd { get; set; }
        public IEnumerable<SelectListItem> VehicleTypes { get; set; }

        [DisplayNameLocalizedAttribute("Member", "DriverTypeCd", "Driver Type")]
        public string DriverTypeCd { get; set; }
        public IEnumerable<SelectListItem> DriverTypes { get; set; }

        [DisplayNameLocalizedAttribute("Member", "CompanyTypeCd", "Company Type")]
        public string CompanyTypeCd { get; set; }
        public IEnumerable<SelectListItem> CompanyTypes { get; set; }

        [DisplayNameLocalizedAttribute("Member", "UseExternalLogin", "Login through social media account?")]
        public bool? UseExternalLogin { get; set; }
        [DisplayNameLocalizedAttribute("Member", "UseClassicLogin", "Login through username and password?")]
        public bool? UseClassicLogin { get; set; }

        [DisplayNameLocalizedAttribute("Member", "ExternalAccount", "Social Media Account")]
        public string ExternalAccount { get; set; }

        [DisplayNameLocalizedAttribute("Member", "InputSourceName", "Sign Up Via")]
        public string InputSourceName { get; set; }

        [DisplayNameLocalizedAttribute("Member", "IsLockedOut", "Locked")]
        public bool? IsLockedOut { get; set; }

        [DisplayNameLocalizedAttribute("Member", "StsNewRemarks", "Remarks")]
        public string StsNewRemarks { get; set; }
        [DisplayNameLocalizedAttribute("Member", "StsNew", "New Account Status")]
        public string StsNew { get; set; }
        [DisplayNameLocalizedAttribute("Member", "StsNewReason", "Status Change Reason")]
        public string StsNewReason { get; set; }
        public IEnumerable<SelectListItem> StsNewReasons { get; set; }

        public string JoinDateStr
        {
            get
            {
                if (JoinDate != null)
                {
                    return JoinDate.Value.ToString(Enums.DATE_FORMAT);
                }
                return string.Empty;
            }
        }

        public MemberModel()
        {
            Titles = new List<SelectListItem>();
            AccountStatuses = new List<SelectListItem>();
            MaritalStses = new List<SelectListItem>();
            EmploymentStses = new List<SelectListItem>();
            VehicleTypes = new List<SelectListItem>();
            DriverTypes = new List<SelectListItem>();
            CompanyTypes = new List<SelectListItem>();
            StsNewReasons = new List<SelectListItem>();
        }
    }
}