using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Entities
{
    public class PeopleEntity
    {        
        [Required]
        public string PeopleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        //public string Birthday { get; set; }
        [DisplayName("Birthday")]
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string JobName { get; set; }
        public string HomeTown { get; set; }
        public string FatherFullName { get; set; }
        public string FatherID { get; set; }
        public string FatherPhone { get; set; }
        public string FatherJob { get; set; }
        public string MotherFullName { get; set; }
        public string MotherID { get; set; }
        public string MotherPhone { get; set; }
        public string MotherJob { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public bool? IsDelete { get; set; }
        public string DeleteDate { get; set; }
        public string DeleteUser { get; set; }
        public string Description { get; set; }
    }
}
