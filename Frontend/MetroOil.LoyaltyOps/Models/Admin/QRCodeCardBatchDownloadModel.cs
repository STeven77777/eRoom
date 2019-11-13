using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.Admin
{
    public class QRCodeCardBatchDownloadModel
    {
        public string Token { get; set; }

        public QRCodeCardBatchDownloadModel()
        {
        }
    }
}