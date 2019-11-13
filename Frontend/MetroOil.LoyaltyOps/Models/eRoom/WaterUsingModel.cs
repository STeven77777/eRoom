using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using eRoom.Shared.Entity;

namespace MetroOil.LoyaltyOps.Models.eRoom
{
    public class WaterUsingModel : WaterUsing
    {
        public string WaterUsingIDHash { get; set; }
       
    }
}