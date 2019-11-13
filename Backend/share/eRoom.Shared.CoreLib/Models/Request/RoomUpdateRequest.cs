using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class RoomUpdateRequest
    {
        [Required]
        public string RoomID { get; set; }
        public Nullable<int> RoomTypeID { get; set; }
        public string RoomName { get; set; }
        public string StatusID { get; set; }      
       
        public string UpdateUser { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
     
        public string Description { get; set; }
    }
}
