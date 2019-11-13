using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class PeopleListRequest : PagingRequest
    {
        //public string PeopleID { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        //public string Phone { get; set; }
        //public string JobName { get; set; }
        //public string HomeTown { get; set; }
        //public string FatherFullName { get; set; }
        //public string MotherFullName { get; set; }
        //public string FatherJob { get; set; }
        //public string MotherJob { get; set; }
    }
}
