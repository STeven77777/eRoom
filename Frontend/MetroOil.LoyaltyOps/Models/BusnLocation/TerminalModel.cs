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
    public class TerminalModel
    {
        [Required]
        [DisplayNameLocalizedAttribute("BusnLot", "TermId", "Terminal No.")]
        public string TermId { get; set; }
        [DisplayNameLocalizedAttribute("BusnLot", "BusnLocation", "Business Location No.")]
        public string BusnLocation { get; set; }
        public string BusnLocationNoHash { get; set; }

        //public string BusnLocationNo { get; set; } // Will be removed soon

        [DisplayNameLocalizedAttribute("BusnLot", "ProdTypeCd", "Device Model")]
        public string ProdTypeCd { get; set; }
        //public List<SelectListItem> ProdTypeCds { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("BusnLot", "DeviceTypeCd", "Device Type")]
        public string DeviceTypeCd { get; set; }
        public List<SelectListItem> DeviceTypeCds { get; set; }
        public string DeviceTypeName { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "SerialNo", "Serial No.")]
        public string SerialNo { get; set; }
        [DisplayNameLocalizedAttribute("BusnLot", "TermSrc", "Termial Source")]
        public string TermSrc { get; set; }
        public List<SelectListItem> TermSrcs { get; set; }

        [DisplayNameLocalizedAttribute("BusnLot", "DeployDate", "Deployment Date")]
        public DateTime? DeployDate { get; set; }
        [DisplayNameLocalizedAttribute("BusnLot", "Sts", "Terminal Status")]
        public string Sts { get; set; }
        public List<SelectListItem> Stses { get; set; }
        [DisplayNameLocalizedAttribute("BusnLot", "StatusName", "Terminal Status")]
        public string StatusName { get; set; }
        [DisplayNameLocalizedAttribute("BusnLot", "CreatedByName", "Created By")]
        public string CreatedByName { get; set; }
        [DisplayNameLocalizedAttribute("BusnLot", "CreatedDate", "Created On")]
        public DateTime? CreatedDate { get; set; }
        public string Ids { get; set; }

        //Change Status
        //public string StsNew { get; set; }
        //public string StsNewReason { get; set; }
        //public string StsNewRemarks { get; set; }
        [DisplayNameLocalizedAttribute("MerchantAcct", "StsNewRemarks", "Remarks")]
        public string StsNewRemarks { get; set; }
        [DisplayNameLocalizedAttribute("MerchantAcct", "StsNew", "New Account Status")]
        public string StsNew { get; set; }
        [DisplayNameLocalizedAttribute("MerchantAcct", "StsNewReason", "Status Change Reason")]
        public string StsNewReason { get; set; }
        public List<SelectListItem> StsNewReasons { get; set; }

        public TerminalModel()
        {
            StsNewReasons = new List<SelectListItem>();
            DeviceTypeCds = new List<SelectListItem>();
            TermSrcs = new List<SelectListItem>();
            Stses = new List<SelectListItem>();
        }
    }
}