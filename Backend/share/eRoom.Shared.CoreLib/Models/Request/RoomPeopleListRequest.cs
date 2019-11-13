using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class RoomPeopleListRequest : PagingRequest
    {
        public string RoomPeopleID { get; set; }
        public string PeopleID { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
