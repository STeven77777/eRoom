using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models.Admin
{
    public class UserAccessManagementModel
    {
        public string UserGroupCd { get; set; }
        [DisplayNameLocalizedAttribute("OpsUser", "UserRoleName", "User Role Name")]
        public string UserGroupName { get; set; }
        public string StsName { get; set; }
        [DisplayNameLocalizedAttribute("OpsUser", "Sts", "Status")]
        public string Sts { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        [DisplayNameLocalizedAttribute("OpsUser", "CreatedOn", "Created On")]
        public string CreatedByName { get; set; }
    }
}