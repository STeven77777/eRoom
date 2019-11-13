
using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models
{
    public class BusnLocationSearchModel
    {
        [DisplayNameLocalizedAttribute("BusnLot", "BusnLocation", "Business Location No.")]
        public string BusnLocation { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "BusnName", "Business Name")]
        public string BusnName { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "DBAName", "DBA Name")]
        public string DBAName { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "DBAState", "DBA State/Region")]
        public string DBAState { get; set; }
        public List<SelectListItem> DBAStates { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "HpNo", "Mobile Phone No.")]
        public string HpNo { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "EmailAddress", "Email")]
        public string EmailAddress { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "City", "City")]
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

        public BusnLocationSearchModel()
        {
            SelectedStates = new List<string>();
            States = new List<SelectListItem>();
            DBAStates = new List<SelectListItem>();
        }
    }
}
