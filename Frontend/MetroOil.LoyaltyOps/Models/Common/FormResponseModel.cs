using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models
{
    public class FormResponseModel<T, G>
    {
        public int ResponseCode { get; set; }
        public string ResponseDesc { get; set; }
        public T Model { get; set; }
        public T Selects { get; set; }
        public G GeneralInfo { get; set; }
    }
}