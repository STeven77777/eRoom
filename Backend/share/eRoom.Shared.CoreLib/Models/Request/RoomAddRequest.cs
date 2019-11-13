using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class RoomAddRequest
    {
        public string RoomID { get; set; }
        public string RoomTypeID { get; set; }
        public string RoomName { get; set; }
        public string StatusID { get; set; }
        public string CreateUser { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
