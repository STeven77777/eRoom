using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models
{
    public class AccessibilityModel
    {
        public string PageId { get; set; }
        public string Descp { get; set; }
        public string ShortDescp { get; set; }
        public string UserId { get; set; }
        public string UserGroupCd { get; set; }
        public bool GroupPageStatus { get; set; }
    }
}