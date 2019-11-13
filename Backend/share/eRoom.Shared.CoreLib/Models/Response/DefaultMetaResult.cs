using System;
using System.Collections.Generic;
using System.Text;
using static eRoom.Shared.CoreLib.Common.Constants;

namespace eRoom.Shared.CoreLib.Models.Response
{
    public class DefaultMetaResult
    {
        public DefaultMetaResult()
        {
            ResponseCode = MetaResult.ResponseCode;
            ResponseDesc = MetaResult.ResponseDesc;
        }
        public DefaultMetaResult(int responseCode, string responseDesc)
        {
            ResponseCode = responseCode;
            ResponseDesc = responseDesc;
        }
        public int ResponseCode { get; set; }

        public string ResponseDesc { get; set; }
    }
}
