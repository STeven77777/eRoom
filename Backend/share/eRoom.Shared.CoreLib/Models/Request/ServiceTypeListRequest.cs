using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class ServiceTypeListRequest : PagingRequest
    {
        public string ServiceTypeName { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
