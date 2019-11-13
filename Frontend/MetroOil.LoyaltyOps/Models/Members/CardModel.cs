using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models.Members
{
    public class CardModel
    {
        public string AcctNo { get; set; }
        public string AcctNoHash { get; set; }
        [DisplayNameLocalizedAttribute("Card", "CardNo", "Card No.")]
        public string CardNo { get; set; }
        public string CurrentOldCardNo
        {
            get
            {
                return CardNo;
            }
        }
        [DisplayNameLocalizedAttribute("Card", "EmbName", "Name on Card")]
        public string EmbName { get; set; }
        [DisplayNameLocalizedAttribute("Card", "CardLogoName", "Card Logo")]
        public string CardLogoName { get; set; }
        [DisplayNameLocalizedAttribute("Card", "CardType", "Card Type")]
        public string CardType { get; set; }
        public IEnumerable<SelectListItem> CardTypes { get; set; }
        public string CardTypeName { get; set; }
        [DisplayNameLocalizedAttribute("Card", "PlasticTypeName", "Plastic Type")]
        //public string PlasticType { get; set; }
        public string PlasticTypeName { get; set; }
        //public string CardSts { get; set; }
        [DisplayNameLocalizedAttribute("Card", "CardStsName", "Card Status")]
        public string CardStsName { get; set; }
        [DisplayNameLocalizedAttribute("Card", "StartDate", "Card Validity Period")]
        public DateTime? StartDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        [DisplayNameLocalizedAttribute("Card", "Remark", "Remarks")]
        public string Remark { get; set; }
        public DateTime CreatedDate { get; set; }

        public string CurrentCardType { get { return CardType; } }
        public CardModel()
        {
            CardTypes = new List<SelectListItem>();
        }
    }
}