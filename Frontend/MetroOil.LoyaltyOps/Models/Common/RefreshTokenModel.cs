using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models
{
    public class RefreshTokenModel
    {
        public string AppsVersionCd { get; set; }
        public string AppsVersionName { get; set; }
        public string RefreshToken { get; set; }
    }
}