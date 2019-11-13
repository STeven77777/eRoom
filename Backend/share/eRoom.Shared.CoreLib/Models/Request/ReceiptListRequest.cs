using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class ReceiptListRequest : PagingRequest
    {
        public string BillID { get; set; }
        public string PeopleID { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
