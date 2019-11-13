using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models.BusnLocation
{
    public class TxnPointModel
    {
        public string TxnNo { get; set; }
        public string RefNo { get; set; }
        public string CardNo { get; set; }
        public DateTime? TxnDate { get; set; }
        public DateTime? PrcsDate { get; set; }
        public string TxnDescp { get; set; }
        public string TxnCategoryName { get; set; }
        public decimal TxnAmount { get; set; }
        public decimal TxnPoints { get; set; }
        public string TermId { get; set; }
        public string ExtOrderNo { get; set; }
        public string EmbName { get; set; }
        public string AcctNo { get; set; }
    }
}