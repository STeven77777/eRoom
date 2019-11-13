using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models.Admin
{
    public class UserMatrixResultModel
    {

    }
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class UserMatrixPage
    {
        public UserMatrixPage()
        {

        }
        public Int64 PageId { get; set; }
        public string Descp { get; set; }
        public string ShortDescp { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? Status { get; set; }
        public bool GroupStatus { get; set; }
        public List<UserMatrixPage> SubPages { get; set; }

    }
    //[Serializable]
    //public class UserMatrixSubPageModel
    //{

    //    public UserMatrixSubPageModel()
    //    {

    //    }
    //    public Int64 PageId { get; set; }
    //    public string Descp { get; set; }
    //    public string ShortDescp { get; set; }
    //    public DateTime? CreationDate { get; set; }
    //    public bool? Status { get; set; }
    //    public bool GroupStatus { get; set; }
    //    public List<UserMatrixSubPageModel> SubPages2 { get; set; }
    //}

//public class Result
//{
//    public int PageId { get; set; }
//    public string Descp { get; set; }
//    public string ShortDescp { get; set; }
//    public DateTime CreationDate { get; set; }
//    public bool Status { get; set; }
//    public bool GroupStatus { get; set; }
//    public List<UserMatrixPage> SubPages { get; set; }
//    //public List<object> SubPages2 { get; set; }
//    //public object SubPages3 { get; set; }
//    //public object SubPages4 { get; set; }
//}

//public class RootObject
//{
//    public List<Result> Result { get; set; }
//    public int ResponseCode { get; set; }
//    public string ResponseDesc { get; set; }
//}

}