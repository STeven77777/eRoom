
using MetroOil.LoyaltyOps.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MetroOil.LoyaltyOps.Models.Members;
using MetroOil.LoyaltyOps.Helpers;
using MetroOil.LoyaltyOps.Models.eRoom;

namespace MetroOil.LoyaltyOps.Controllers
{
    public class CommonController : Controller
    {
        #region Address
        public async Task<ActionResult> AddressList(jQueryDataTableParamModel Params, string EntityId)
        {
            var displyColumns = new string[] { "RoomID", "RoomName", "RoomTypeID", "StatusID", "IsActive", "Description", "CreateDate", "CreateUser"};
            //var displyColumns = new string[] { "MailingIndName", "AddressTypeName", "Street1", "Street2", "Street3", "City", "StateName", "ZipCd", "CtryName", "CreatedDate", "CreatedByName" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<RoomModel>>("rooms/roomlist?" + Helper.GetQueryString(Params));
            //var response = await ApiClient.GetJsonAsync<APIResponseListModel<AddressModel>>("common/AddressList?EntityId=" + EntityId + "&" + Helper.GetQueryString(Params));
            if (response.ResponseCode != 0) // empty data/ error
            {
                response.Result.RecordList = new List<RoomModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = response.Result.RecordCount,
                iTotalDisplayRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.RoomID, x.RoomName, x.RoomTypeID, x.StatusID, x.IsActive, x.Description, x.CreateDate == null ? "" : System.Convert.ToDateTime(x.CreateDate).ToString(Enums.DATE_FORMAT), x.CreateUser})
                //aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.MailingIndName, x.AddressTypeName, x.Street1, x.Street2, x.Street3, x.City, x.StateName, x.ZipCd, x.CtryName, x.CreatedDate.ToString(Enums.DATE_FORMAT), x.CreatedByName, x.Ids })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddAddress(AddressModel model)
        {
            model.MailingInd = model.MailingInd ?? false;
            var result = await ApiClient.PostJsonAsync<APIResponseModel<AddressModel>>
                ("common/Address", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPatch]
        public async Task<JsonResult> UpdateAddress(AddressModel model)
        {
            model.MailingInd = model.MailingInd ?? false;
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<AddressModel>>
                ("common/Address", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAddress(AddressModel model)
        {
            var result = await ApiClient.DeleteJsonAsync<APIResponseModel<AddressModel>>
                ("common/Address", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Contact
        public async Task<ActionResult> ContactList(jQueryDataTableParamModel Params, string EntityId)
        {
            var displyColumns = new string[] { "MainIndName", "FullName", "OccupationName", "WorkTelNo", "HpNo", "EmailAddress" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<ContactModel>>("common/ContactList?EntityId=" + EntityId + "&" + Helper.GetQueryString(Params));
            if (response.ResponseCode != 0) // empty data/ error
            {
                response.Result.RecordList = new List<ContactModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = response.Result.RecordCount,
                iTotalDisplayRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.MainIndName, x.FullName, x.OccupationName, x.WorkTelNo, x.HpNo, x.EmailAddress, x.Ids })
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Update contact for member
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<JsonResult> ContactForMember(ContactModel model)
        {
            if (string.IsNullOrEmpty(model.FaxNo))
            {
                model.FaxCtryCode = string.Empty;
            }

            if (string.IsNullOrEmpty(model.WorkTelNo))
            {
                model.WorkTelCtryCode = string.Empty;
            }

            var result = await ApiClient.PatchJsonAsync<APIResponseModel<ContactModel>>
                ("common/ContactForMember", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPatch]
        public async Task<JsonResult> ContactMobileUpdate(MobileChangeNoModel model)
        {
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<MobileChangeNoModel>>
                ("common/ContactMobileUpdate", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public async Task<ActionResult> ContactLogList(jQueryDataTableParamModel Params, string EntityId)
        //{
        //    var response = await ApiClient.GetJsonAsync<APIResponseListModel<ContactModel>>("common/ContactLogList?EntityId=" + EntityId + "&" + Helper.GetQueryString(Params));
        //    if (response.ResponseCode != 0) // empty data/ error
        //    {
        //        response.Result.RecordList = new List<ContactModel>();
        //    }

        //    return Json(new
        //    {
        //        sEcho = Params.sEcho,
        //        iTotalRecords = response.Result.RecordCount,
        //        iTotalDisplayRecords = response.Result.RecordCount,
        //        aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.MainIndName, x.FamilyName, x.GivenName, x.TitleCd, x.HpNo, x.WorkTelNo, x.HomeTelNo, x.FaxNo, x.EmailAddress, x.Ids })//ToString(Enums.DATE_TIME_FORMAT)
        //    }, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public async Task<ActionResult> ContactLogList(jQueryDataTableParamModel Params, string Ids)
        {
            var response = await ApiClient.GetJsonAsync<APIResponseListModel<LogChangeModel>>
                ("Common/ContactLogList?Ids=" + Ids + "&" + Helper.GetQueryString(Params));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                //response.Result = new LogChangeModelList();
                response.Result.RecordList = new List<LogChangeModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { x.FieldDesc, x.OldVal, x.NewVal, x.Description, x.CreatedDate.ToString(Enums.DATE_FORMAT), x.CreatedByName })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddContact(ContactModel model)
        {
            if (string.IsNullOrEmpty(model.FaxNo))
            {
                model.FaxCtryCode = string.Empty;
            }

            if (string.IsNullOrEmpty(model.WorkTelNo))
            {
                model.WorkTelCtryCode = string.Empty;
            }

            if (string.IsNullOrEmpty(model.HomeTelNo))
            {
                model.HomeTelCtryCode = string.Empty;
            }

            if (string.IsNullOrEmpty(model.HpNo))
            {
                model.HpCtryCode = string.Empty;
            }

            var result = await ApiClient.PostJsonAsync<APIResponseModel<ContactModel>>
                ("common/Contact", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPatch]
        public async Task<JsonResult> UpdateContact(ContactModel model)
        {
            if (string.IsNullOrEmpty(model.FaxNo))
            {
                model.FaxCtryCode = string.Empty;
            }

            if (string.IsNullOrEmpty(model.WorkTelNo))
            {
                model.WorkTelCtryCode = string.Empty;
            }

            if (string.IsNullOrEmpty(model.HomeTelNo))
            {
                model.HomeTelCtryCode = string.Empty;
            }

            if (string.IsNullOrEmpty(model.HpNo))
            {
                model.HpCtryCode = string.Empty;
            }

            var result = await ApiClient.PatchJsonAsync<APIResponseModel<ContactModel>>
                ("common/Contact", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteContact(ContactModel model)
        {
            var result = await ApiClient.DeleteJsonAsync<APIResponseModel<ContactModel>>
                ("common/Contact", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region General 
        [HttpGet]
        public async Task<ActionResult> WebGetState(string CountryCd)
        {
            var rp = Common.GetState(CountryCd);
            return Json(rp.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetProductTypes(string prodClassCd)
        {
            var rp = Common.GetProductTypes(prodClassCd, false);
            return Json(rp.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetRefLib(string refType)
        {
            var result = Common.GetRefLib(refType);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}