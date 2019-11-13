using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class ServiceListRequest : PagingRequest
    {
        public string ServiceName { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
