using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroOil.LoyaltyOps.Helpers;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.BusnLocation
{
    public class CardRangeAcceptanceModel
    {
        public string CardRangeId { get; set; }
        public string BusnLocationNo { get; set; }
        public string BusnLocationNoHash { get; set; }
        public int Ids { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedDateStr
        {
            get
            {
                if (CreatedDate != null)
                    return CreatedDate.Value.ToString(Enums.DATE_FORMAT);
                return string.Empty;
            }
        }
        public string Descp { get; set; }
    }

    public class CardRangeAcceptanceListModel
    {
        public List<CardRangeAcceptanceModel> CardRangeAcceptanceLst { get; set; }
        public List<SelectListItem> CardRangeSelects { get; set; }

        public CardRangeAcceptanceListModel()
        {
            CardRangeAcceptanceLst = new List<CardRangeAcceptanceModel>();
            CardRangeSelects = new List<SelectListItem>();
        }
    }
}