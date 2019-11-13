using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MetroOil.LoyaltyOps.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class IdentityUserModel : IdentityUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string ResponseDesc { get; set; }
        public string ResponseCode { get; set; }
        public DateTime Expiry { get; set; }
        public string RoldID { get; set; }
        public string RefreshToken { get; set; }
    }
}