using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.Admin
{
    public class UserRoleModel
    {
        [Required]
        [DisplayNameLocalizedAttribute("OpsUser", "UserRoleNo", "User Role No.")]
        public string UserRoleNo { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("OpsUser", "UserRoleName", "User Role Name")]
        public string UserRoleName { get; set; }

        public string StsName { get; set; }
        [DisplayNameLocalizedAttribute("OpsUser", "Sts", "Status")]
        public string Sts { get; set; }
        public IEnumerable<SelectListItem> StatusLitst { get; set; }

        [DisplayNameLocalizedAttribute("OpsUser", "CreatedOn", "Created On")]
        public DateTime? CreatedOn { get; set; }


        [DisplayNameLocalizedAttribute("OpsUser", "CreatedBy", "Created By")]
        public string CreatedBy { get; set; }

        public UserRoleModel()
        {
            StatusLitst = new  List<SelectListItem>();
        }

    }
}