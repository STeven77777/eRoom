using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class InvoiceDetailListRequest : PagingRequest
    {
        public string InvoiceID { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
