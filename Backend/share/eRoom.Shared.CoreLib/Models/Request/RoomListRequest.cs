using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class RoomListRequest : PagingRequest
    {
        public string RoomID { get; set; }
        public string StatusID { get; set; }
        public string RoomTypeID { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
