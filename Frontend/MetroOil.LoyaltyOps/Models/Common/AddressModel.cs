using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace MetroOil.LoyaltyOps.Models
{
    public class AddressModel
    {
        public int EntityId { get; set; }
        public string Ids { get; set; }
        [DisplayNameLocalizedAttribute("Address", "MailingInd", "Mailling Address")]
        public bool? MailingInd { get; set; }
        public string MailingIndName { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Address", "AddressTypeCd", "Address Type")]
        public string AddressTypeCd { get; set; }
        public List<SelectListItem> AddressTypes { get; set; }
        public string AddressTypeName { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("Address", "Street1", "Address Line 1")]
        public string Street1 { get; set; }
        [DisplayNameLocalizedAttribute("Address", "Street2", "Address Line 2")]
        public string Street2 { get; set; }
        [DisplayNameLocalizedAttribute("Address", "Street3", "Address Line 3")]
        public string Street3 { get; set; }
        [DisplayNameLocalizedAttribute("Address", "City", "City")]
        public string City { get; set; }
        public string CtryName { get; set; }
        [DisplayNameLocalizedAttribute("Address", "StateCd", "State")]
        public string StateCd { get; set; }
        public string StateName { get; set; }
        public List<SelectListItem> States { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("Address", "ZipCd", "Postal Code")]
        public string ZipCd { get; set; }
        [DisplayNameLocalizedAttribute("Address", "CtryCd", "Country")]
        public string CtryCd { get; set; }
        public List<SelectListItem> Countries { get; set; }
        private DateTime _CreatedDate { get; set; }
        //private DateTime _CreateDate { get; set; }
        [DisplayNameLocalizedAttribute("Address", "CreatedDate", "Created On")]
        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
        [DisplayNameLocalizedAttribute("Address", "CreatedByName", "Created By")]
        public string CreatedByName { get; set; }

        public AddressModel()
        {
            AddressTypes = new List<SelectListItem>();
            States = new List<SelectListItem>();
            Countries = new List<SelectListItem>();
        }
    }
}
