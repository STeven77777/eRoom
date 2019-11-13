using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetroOil.LoyaltyOps.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MetroOil.LoyaltyOps;

namespace MetroOil.LoyaltyOps.Helpers
{
    public class Common
    {
        public static Task<List<SelectListItem>> GetRefLib(string refType, int? refNo = null, int? refInd = null, string refId = null, bool IsCodeNDescp = false, string excludeRefCd = null)
        {
            var _obj = new { IssNo = Enums.ISS_NO, RefType = refType, RefNo = refNo, RefInd = refInd, RefId = refId, ExcludeRefCd = excludeRefCd };

            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<RefLibResponse>>>("RefLib/Type/" + Helper.GetQueryString(_obj)).Result;

            if (rp != null && rp.Result != null)
            {
                return Task.Run(() => rp.Result.Select(x => new SelectListItem() { Value = x.RefCd, Text = IsCodeNDescp ? x.Descp : x.Descp }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }

        public static Task<List<SelectListItem>> GetCardType(int? refNo = null)
        {
            var _obj = new { IssNo = Enums.ISS_NO };

            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<CardTypeModel>>>("common/GetCardType?" + Helper.GetQueryString(_obj)).Result;

            if (rp != null && rp.Result != null)
            {
                return Task.Run(() => rp.Result.Select(x => new SelectListItem() { Value = x.CardType, Text = x.Descp }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }

        public static Task<List<SelectListItem>> GetState(string countryCd)
        {
            var _obj = new { IssNo = Enums.ISS_NO, CountryCd = countryCd };

            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<StateModel>>>("common/State?" + Helper.GetQueryString(_obj)).Result;

            if (rp != null && rp.Result != null)
            {
                return Task.Run(() => rp.Result.Select(x => new SelectListItem() { Value = x.StateCd, Text = x.Descp }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }

        public static Task<List<SelectListItem>> GetUserGroup()
        {
            var rp = ApiClient.GetJsonAsync<APIResponseListModel<UserGroupModel>>("common/GetUserGroup").Result;

            if (rp != null && rp.Result != null && rp.Result.RecordList != null)
            {
                return Task.Run(() => rp.Result.RecordList.Select(x => new SelectListItem() { Value = x.UserGroupCd, Text = x.Descp }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }

        //public static Task<List<SelectListItem>> GetRedeemProductTypes()
        //{
        //    var rp = ApiClient.GetJsonAsync<APIResponseListModel<RedeemProdTypeModel>>("common/redeemProductTypes").Result;

        //    if (rp != null && rp.Result != null && rp.Result.RecordList != null)
        //    {
        //        return Task.Run(() => rp.Result.RecordList.Select(x => new SelectListItem() { Value = x.Code, Text = x.Descp }).ToList());
        //    }
        //    return Task.Run(() => new List<SelectListItem>());
        //}

        public static Task<List<SelectListItem>> GetUserList(string userType)
        {
            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<UserModel>>>("ops/OpsUserList?userType=" + userType).Result;

            if (rp != null && rp.Result != null)
            {
                return Task.Run(() => rp.Result.Select(x => new SelectListItem() { Value = x.UserId, Text = x.Descp }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }

        public static Task<List<SelectListItem>> GetProductClasses(string TxnInd, bool IsActiveOnly)
        {
            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<CodeDescpModel>>>("common/ProductClass?TxnInd=" + TxnInd + "&IsActiveOnly=" + IsActiveOnly).Result;

            if (rp != null && rp.Result != null)
            {
                return Task.Run(() => rp.Result.Select(x => new SelectListItem() { Value = x.Code, Text = x.Descp }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }

        public static Task<List<SelectListItem>> GetProductTypes(string prodClassCd, bool IsActiveOnly)
        {
            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<CodeDescpModel>>>("common/productTypes?ProdClassCd=" + prodClassCd + "&IsActiveOnly=" + IsActiveOnly).Result;

            if (rp != null && rp.Result != null)
            {
                return Task.Run(() => rp.Result.Select(x => new SelectListItem() { Value = x.Code, Text = x.Descp }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }

        public static Task<List<SelectListItem>> GetExternalPartner()
        {
            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<CodeDescpModel>>>("Reward/GetExternalPartner").Result;

            if (rp != null && rp.Result != null)
            {
                return Task.Run(() => rp.Result.Select(x => new SelectListItem() { Value = x.Code, Text = x.Descp }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }
        
        #region Business Location
        public static Task<List<SelectListItem>> GetBusinessLocations()
        {
            var _obj = new { AcqNo = Enums.ACQ_NO };

            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<BusinessLocationModel>>>("merchant/BusinessLocationsSelect?" + Helper.GetQueryString(_obj)).Result;

            if (rp != null && rp.Result != null)
            {
                return Task.Run(() => rp.Result.Select(x => new SelectListItem() { Value = x.BusnLocationNo, Text = x.BusnName }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }
        #endregion

        public static Task<List<SelectListItem>> GetMerchants()
        {
            var _obj = new { AcqNo = Enums.ACQ_NO };

            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<MetroOil.LoyaltyOps.Models.Merchants.Account.MerchantAcctModel>>>("merchant/GetMerchant?" + Helper.GetQueryString(_obj)).Result;

            if (rp != null && rp.Result != null)
            {
                return Task.Run(() => rp.Result.Select(x => new SelectListItem() { Value = x.AcctNo, Text = x.BusnName }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }

        public static Task<List<SelectListItem>> GetMonths()
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem() { Value = "1", Text = "Jan" });
            result.Add(new SelectListItem() { Value = "2", Text = "Feb" });
            result.Add(new SelectListItem() { Value = "3", Text = "Mar" });
            result.Add(new SelectListItem() { Value = "4", Text = "Apr" });
            result.Add(new SelectListItem() { Value = "5", Text = "May" });
            result.Add(new SelectListItem() { Value = "6", Text = "Jun" });
            result.Add(new SelectListItem() { Value = "7", Text = "Jul" });
            result.Add(new SelectListItem() { Value = "8", Text = "Aug" });
            result.Add(new SelectListItem() { Value = "9", Text = "Sep" });
            result.Add(new SelectListItem() { Value = "10", Text = "Oct" });
            result.Add(new SelectListItem() { Value = "11", Text = "Nov" });
            result.Add(new SelectListItem() { Value = "12", Text = "Dec" });
            return Task.Run(() => result);
        }

        public static Task<List<SelectListItem>> GetCardRanges()
        {
            //var _obj = new { AcqNo = Enums.ACQ_NO };

            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<CardRangeSelectModel>>>("common/cardRange").Result;

            if (rp != null && rp.Result != null)
            {
                return Task.Run(() => rp.Result.Select(x => new SelectListItem() { Value = x.CardRangeId, Text = x.CodeNDescp }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }
    }
}