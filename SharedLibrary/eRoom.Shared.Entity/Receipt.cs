//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eRoom.Shared.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Receipt
    {
        public string ReceiptID { get; set; }
        public string BillID { get; set; }
        public string PeopleID { get; set; }
        public Nullable<float> ReceiptAmount { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string DeleteUser { get; set; }
        public string Description { get; set; }
    }
}
