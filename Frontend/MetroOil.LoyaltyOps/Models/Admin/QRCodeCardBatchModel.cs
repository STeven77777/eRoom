using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.Admin
{
    public class QRCodeCardBatchModel
    {
        public int BatchId { get; set; }
        public int Qty { get; set; }
        public string Remark { get; set; }
        public string Sts { get; set; }
        public string StatusName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }

        public QRCodeCardBatchModel()
        {
        }
    }
}