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
    public class MemberSearchModel
    {
        [DisplayNameLocalizedAttribute("MemberSearch", "MainAcctNo", "Member Account No.")]
        public string AcctNo { get; set; }
        [DisplayNameLocalizedAttribute("MemberSearch", "FullName", "Full Name")]
        public string FullName { get; set; }
        [DisplayNameLocalizedAttribute("MemberSearch", "CardNo", "Card No.")]
        public string CardNo { get; set; }
        [DisplayNameLocalizedAttribute("MemberSearch", "Sts", "Account Status")]
        public string Sts { get; set; }
        public List<SelectListItem> Stses { get; set; }
        public List<string> SelectedAcctStses { get; set; }
        public string AcctStsText
        {
            get
            {
                if (SelectedAcctStses.Any())
                {
                    return string.Join("|", SelectedAcctStses.ToArray());
                }
                return null;
            }
        }
        public DateTime? JoinDateFrom { get; set; }
        public DateTime? JoinDateTo { get; set; }
        [DisplayNameLocalizedAttribute("MemberSearch", "Dob", "Date of Birth")]
        public DateTime? DOBDateFrom { get; set; }
        public DateTime? DOBDateTo { get; set; }
        [DisplayNameLocalizedAttribute("MemberSearch", "HpNo", "Mobile Phone No.")]
        [RegularExpression(@"^([\+]?(?:00)?[0-9]{1,12})$", ErrorMessage = "Mobile Phone No. is invalid.")]
        public string HpNo { get; set; }
        [DisplayNameLocalizedAttribute("MemberSearch", "EmailAddress", "Email")]
        public string EmailAddress { get; set; }
        [DisplayNameLocalizedAttribute("MemberSearch", "City", "City")]
        public string City { get; set; }
        //[DisplayNameLocalizedAttribute("MemberSearch", "StateCd", "State")]
        //public string StateCd { get; set; }
        //public string StateName { get; set; }
        public List<string> SelectedStates { get; set; }
        public List<SelectListItem> States { get; set; }
        public string StateCdText
        {
            get
            {
                if (SelectedStates.Any())
                {
                    return string.Join("|", SelectedStates.ToArray());
                }
                return null;
            }
        }
        public string MMFrom { get; set; }
        public List<SelectListItem> MMFroms { get; set; }
        public string DDFrom { get; set; }
        public List<SelectListItem> DDFroms { get; set; }
        public string MMTo { get; set; }
        public List<SelectListItem> MMTos { get; set; }
        public string DDTo { get; set; }
        public List<SelectListItem> DDTos { get; set; }

        public DateTime? DOBDDMMFrom
        {
            get
            {
                if (!string.IsNullOrEmpty(MMFrom) && !string.IsNullOrEmpty(DDFrom))
                {
                    return new DateTime(1904, int.Parse(MMFrom), int.Parse(DDFrom));
                }
                return null;
            }
        }

        public DateTime? DOBDDMMTo
        {
            get
            {
                if (!string.IsNullOrEmpty(MMTo) && !string.IsNullOrEmpty(DDTo))
                {
                    return new DateTime(1904, int.Parse(MMTo), int.Parse(DDTo));
                }
                return null;
            }
        }

        public MemberSearchModel()
        {
            Stses = new List<SelectListItem>();
            SelectedAcctStses = new List<string>();
            SelectedStates = new List<string>();
            States = new List<SelectListItem>();
            MMFroms = new List<SelectListItem>();
            DDFroms = new List<SelectListItem>();
            MMTos = new List<SelectListItem>();
            DDTos = new List<SelectListItem>();
        }
    }
}