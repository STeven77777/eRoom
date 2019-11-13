using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class MotorTypeListRequest : PagingRequest
    {
        public string MotorTypeID { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
