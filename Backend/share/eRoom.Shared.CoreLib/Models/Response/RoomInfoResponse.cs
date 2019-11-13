using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Response
{
    public class RoomInfoResponse
    {
        public string RoomID { get; set; }
        public Nullable<int> RoomTypeID { get; set; }
        public string RoomName { get; set; }
        public string StatusID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string DeleteUser { get; set; }
        public string Description { get; set; }
    }
}
