using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroOil.LoyaltyOps.Helpers;

namespace MetroOil.LoyaltyOps.Models
{
    public class APIResponseModel<T>
    {
        public int ResponseCode { get; set; }
        public string ResponseDesc { get; set; }
        public T Result { get; set; }

        public APIResponseModel()
        {
            ResponseCode = Enums.RESPONSE_CODE_INVALID;
        }
    }
}