using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class InvoiceListRequest : PagingRequest
    {
        public string RoomID { get; set; }
        public DateTime? CreateDate { get; set; }
        public int InvoiceMonth { get; set; }
    }
}
