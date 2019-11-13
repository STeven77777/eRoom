using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.Merchants.Account
{
    public class MerchantAcctModel
    {
        [DisplayNameLocalizedAttribute("MerchantAcct", "AcctNo", "Merchant Account No.")]
        public string AcctNo { get; set; }
        public string MerchAcctNoHash { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("MerchantAcct", "BusnName", "Business Name")]
        public string BusnName { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "Sts", "Account Status")]
        public string Sts { get; set; }
        public string StatusName { get; set; }
        public List<SelectListItem> AccountStatuses { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "Ownership", "Company Type")]
        public string Ownership { get; set; }
        public string OwnershipName { get; set; }
        public List<SelectListItem> OwnershipTypes { get; set; }


        [DisplayNameLocalizedAttribute("MerchantAcct", "CreatedDate", "Created On")]
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

        [DisplayNameLocalizedAttribute("MerchantAcct", "CreatedByName", "Created By")]
        public string CreatedByName { get; set; }
        public string CreatedBy { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "CoRegNo", "Company Registration No.")]
        public string CoRegNo { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "CoRegName", "Company Name (Registered)")]
        public string CoRegName { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "CoRegDate", "Company Registration Date")]
        public DateTime? CoRegDate { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "PersonInChargeFullName", "Person In Charge")]
        public string PersonInChargeFullName { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "TaxId", "Tax ID")]
        public string TaxId { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "AgreementDate", "Agreement Date")]
        public DateTime? AgreementDate { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "AgreementNo", "Agreement No.")]
        public string AgreementNo { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "BusnSize", "Business Size")]
        public char? BusnSize { get; set; }

        public List<SelectListItem> BusnSizes { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "StsNewRemarks", "Remarks")]
        public string StsNewRemarks { get; set; }
        [DisplayNameLocalizedAttribute("MerchantAcct", "StsNew", "New Account Status")]
        public string StsNew { get; set; }
        [DisplayNameLocalizedAttribute("MerchantAcct", "StsNewReason", "Status Change Reason")]
        public string StsNewReason { get; set; }
        public IEnumerable<SelectListItem> StsNewReasons { get; set; }

        public int EntityId { get; set; }

        public MerchantAcctModel()
        {
            StsNewReasons = new List<SelectListItem>();
            AccountStatuses = new List<SelectListItem>();
            OwnershipTypes = new List<SelectListItem>();
            BusnSizes = new List<SelectListItem>();
        }
    }
}
