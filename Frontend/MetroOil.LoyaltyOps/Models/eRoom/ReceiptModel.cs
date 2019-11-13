using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eRoom.Shared.Entity;

namespace MetroOil.LoyaltyOps.Models.eRoom
{
    public class ReceiptModel : Receipt
    {
        public string ReceiptIDHash { get; set; }
    
    }
}