using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models
{
    public class BusnLocationModel
    {
        [DisplayNameLocalizedAttribute("BusnLot", "BusnLocation", "Business Location No.")]
        public string BusnLocation { get; set; }
        public string BusnLocationNoHash { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("BusnLot", "BusnName", "Business Name")]
        public string BusnName { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "CoRegName", "Company Name (Registered)")]
        public string CoRegName { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "CoRegNo", "Company Registration No.")]
        public string CoRegNo { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "CoRegDate", "Company Registration Date")]
        public DateTime? CoRegDate { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "PersonInChargeFullName", "Person In Charge")]
        public string PersonInChargeFullName { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "AgreementNo", "Agreement No.")]
        public string AgreementNo { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "AgreementDate", "Agreement Date")]
        public DateTime? AgreementDate { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "AllTxnCodeMapping", "Accept all transaction codes")]
        public bool? AllTxnCodeMapping { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "AllCardRange", "Accept all card range")]
        public bool? AllCardRange { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "TaxId", "Tax ID")]
        public string TaxId { get; set; }

        //[DisplayNameLocalizedAttribute("BusnLot", "BusnSize", "Business Size")]
        //public char? BusnSize { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "Ownership", "Company Type")]
        public string Ownership { get; set; }
        public string OwnershipName { get; set; }
        public List<SelectListItem> OwnershipTypes { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "GeoLatitude", "Latitude")]
        public string GeoLatitude { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "GeoLongitude", "Longitude")]
        public string GeoLongitude { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "Mcc", "Merchant Category Code (MCC)")]
        public string Mcc { get; set; }
        public List<SelectListItem> Mccs { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "Sic", "Standard Industry Code (SIC)")]
        public string Sic { get; set; }
        public List<SelectListItem> Sics { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "DBAName", "DBA Name")]
        public string DBAName { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "DBACity", "DBA City")]
        public string DBACity { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "DBAState", "DBA State/Region")]
        public string DBAState { get; set; }
        public List<SelectListItem> DBAStates { get; set; }

        public string Sts { get; set; }
        public List<SelectListItem> Stses { get; set; }
        public string StateName { get; set; }
        public string StatusName { get; set; }
        [DisplayNameLocalizedAttribute("BusnLot", "CreatedDate", "Created On")]
        public DateTime? CreatedDate { get; set; }
        public string CreatedDateStr
        {
            get
            {
                if (CreatedDate != null)
                {
                    return CreatedDate.Value.ToString(Enums.DATE_FORMAT);
                }
                return string.Empty;
            }
        }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedDateStr
        {
            get
            {
                if (ModifiedDate != null)
                {
                    return ModifiedDate.Value.ToString(Enums.DATE_FORMAT);
                }
                else if (CreatedDate != null)
                {
                    return CreatedDate.Value.ToString(Enums.DATE_FORMAT);
                }
                return string.Empty;
            }
        }
        [Required]
        [DisplayNameLocalizedAttribute("BusnLot", "AcctNo", "Merchant Account No.")]
        public string AcctNo { get; set; }
        public List<SelectListItem> AcctNos { get; set; }
        [DisplayNameLocalizedAttribute("BusnLot", "CreatedBy", "Created By")]
        public string CreatedBy { get; set; }
        [DisplayNameLocalizedAttribute("BusnLot", "CreatedByName", "Created By")]
        public string CreatedByName { get; set; }

        public int? EntityId { get; set; }

        #region Change Sts
        [DisplayNameLocalizedAttribute("BusnLot", "StsNewRemarks", "Remarks")]
        public string StsNewRemarks { get; set; }
        [DisplayNameLocalizedAttribute("BusnLot", "StsNew", "New Account Status")]
        public string StsNew { get; set; }
        [DisplayNameLocalizedAttribute("BusnLot", "StsNewReason", "Status Change Reason")]
        public string StsNewReason { get; set; }
        public IEnumerable<SelectListItem> StsNewReasons { get; set; }
        #endregion

        public BusnLocationModel()
        {
            Stses = new List<SelectListItem>();
            AcctNos = new List<SelectListItem>();
            OwnershipTypes = new List<SelectListItem>();
            Mccs = new List<SelectListItem>();
            Sics = new List<SelectListItem>();
            DBAStates = new List<SelectListItem>();
        }
    }
}