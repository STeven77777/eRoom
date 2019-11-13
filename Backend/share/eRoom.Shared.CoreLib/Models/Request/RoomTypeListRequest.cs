using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class RoomTypeListRequest : PagingRequest
    {
        public string RoomTypeName { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
