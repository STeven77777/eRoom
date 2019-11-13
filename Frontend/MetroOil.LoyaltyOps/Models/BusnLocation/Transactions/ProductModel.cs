using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models.BusnLocation
{
    public class ProductModel
    {
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public decimal Qty { get; set; }
        public decimal Amt { get; set; }
        public decimal Points { get; set; }
    }
}