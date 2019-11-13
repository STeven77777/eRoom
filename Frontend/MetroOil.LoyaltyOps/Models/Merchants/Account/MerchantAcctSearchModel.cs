using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.Merchants.Account
{
    public class MerchantAcctSearchModel
    {
        [DisplayNameLocalizedAttribute("MerchantAcct", "AcctNo", "Merchant Account No.")]
        public string AcctNo { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "BusnName", "Business Name")]
        public string BusnName { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "CoRegNo", "Company Registration No.")]
        public string CoRegNo { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "CoRegName", "Company Name (Registered)")]
        public string CoRegName { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "HpNo", "Mobile Phone No.")]
        public string HpNo { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "EmailAddress", "Email")]
        public string EmailAddress { get; set; }

        [DisplayNameLocalizedAttribute("MerchantAcct", "City", "City")]
        public string City { get; set; }

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

        public MerchantAcctSearchModel()
        {
            SelectedStates = new List<string>();
            States = new List<SelectListItem>();
        }
    }
}
