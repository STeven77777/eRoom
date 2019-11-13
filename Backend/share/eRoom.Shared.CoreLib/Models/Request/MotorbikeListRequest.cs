using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class MotorbikeListRequest : PagingRequest
    {
     //   public string MotorbikeID { get; set; }
        public string MotorbikeName { get; set; }
        public DateTime? CreateDate { get; set; }
        //    public string MotorTypeID { get; set; }
        //     public string PeopleID { get; set; }
        //    public string Color { get; set; }        
    }
}
