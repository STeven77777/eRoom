using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class RoomDetailListRequest : PagingRequest
    {
        public string RoomDetailID { get; set; }
        public string RoomID { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DateIn { get; set; }
    }
}
