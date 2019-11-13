using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class RoomStatusListRequest : PagingRequest
    {
        public string StatusName { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
