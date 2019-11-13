using MetroOil.LoyaltyOps.Helpers;
using System;
using System.ComponentModel;
using eRoom.Shared.Entity;

namespace MetroOil.LoyaltyOps.Models.eRoom
{
    public class InvoiceModel : Invoice
    {
        public string InvoiceIDHash { get; set; }
      
    }
}