using MetroOil.LoyaltyOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MetroOil.LoyaltyOps.Models.Merchants;
using Newtonsoft.Json;
using MetroOil.LoyaltyOps.Helpers;
using MetroOil.LoyaltyOps.Models.Merchants.Account;

namespace MetroOil.LoyaltyOps.Controllers
{
    //[Authorize(Roles = "Internal")]
    public class MerchantsController : Controller
    {
        // GET: Cards
        //[Accessibility]
        public ActionResult Index()
        {
            return View();
        }

        #region "List Of Web Pages"
        //[CompressFilter]
        [AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {

                case "MerchUserNew":
                    return PartialView("Partials/Users/New", new MerchantUserModel());
                case "MerchUsers":
                    return PartialView("Partials/Users/List");
                case "MerchUserInfo":
                    return PartialView("Partials/Users/Info", new MerchantUserModel());
                case "MerchAccts":
                    return PartialView("Partials/Account/List");
                case "MerchAcctNew":
                    return PartialView("Partials/Account/New", new MerchantAcctModel());
                case "MerchAcctInfo":
                    return PartialView("Partials/Account/Info", new MerchantAcctModel());

                case "MerchAcctAddrs":
                    return PartialView("Partials/Account/Address/Address");
                case "MerchAcctAddrNew":
                    return PartialView("Partials/Account/Address/AddressNew", new AddressModel());
                case "MerchAcctAddrD":
                    return PartialView("Partials/Account/Address/AddressDetail", new AddressModel());

                case "MerchAcctConts":
                    return PartialView("Partials/Account/Contact/Contact");
                case "MerchAcctContNew":
                    return PartialView("Partials/Account/Contact/ContactNew", new ContactModel());
                case "MerchAcctContD":
                    return PartialView("Partials/Account/Contact/ContactDetail", new ContactModel());

                case "MerchAcctBusnLocations":
                    return PartialView("Partials/Account/BusnLocation/BusnLocations");
                default:
                    return PartialView();
            }
        }
        #endregion

        public async Task<JsonResult> FillData(string Prefix, string MerchantUserIdHash, string merchAcctNoHash, string merchAddressId, string merchContactId)
        {
            switch (Prefix)
            {
                case "MerchUserNew":
                    {
                        var _newObj = new MerchantUserModel()
                        {
                            HpCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE
                        };
                        var _newSelects = new MerchantUserModel()
                        {
                            BusnLocationNos = await Common.GetBusinessLocations(),
                            BusnLocationUserRoles = await Common.GetRefLib(Enums.REF_TYPE_MERCH_USER_ROLE),
                            Titles = await Common.GetRefLib(Enums.REF_TYPE_TITLE),
                            HpCtryCodes = await Common.GetRefLib(Enums.REF_TYPE_TEL_COUNTRY, null, null, null, true)
                        };
                        return Json(new { Model = _newObj, Selects = _newSelects }, JsonRequestBehavior.AllowGet);
                    }
                case "MerchUserInfo":
                    {
                        var _infoObj = await ApiClient.GetJsonAsync<APIResponseModel<MerchantUserModel>>
                            ("merchant/User?" + Helper.GetQueryString(new { UserId = Helper.Decrypt(MerchantUserIdHash) }));

                        if (_infoObj != null && _infoObj.Result != null)
                        {
                            _infoObj.Result.UserIdHash = Helper.Encrypt(_infoObj.Result.UserId);
                        }

                        var _infoSelects = new MerchantUserModel()
                        {
                            BusnLocationNos = await Common.GetBusinessLocations(),
                            BusnLocationUserRoles = await Common.GetRefLib(Enums.REF_TYPE_MERCH_USER_ROLE),
                            Titles = await Common.GetRefLib(Enums.REF_TYPE_TITLE),
                            Stses = await Common.GetRefLib(Enums.REF_TYPE_USER_STS),
                            HpCtryCodes = await Common.GetRefLib(Enums.REF_TYPE_TEL_COUNTRY, null, null, null, true)
                        };

                        return Json(new FormResponseModel<MerchantUserModel, MerchantUserModel>()
                        {
                            ResponseCode = _infoObj.ResponseCode,
                            ResponseDesc = _infoObj.ResponseDesc,
                            Model = _infoObj.Result,
                            Selects = _infoSelects,
                            GeneralInfo = _infoObj.Result
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MerchUsers":
                    {
                        var _infoObj = new MerchantUserSearchModel();

                        var _infoSelects = new MerchantUserSearchModel()
                        {
                            BusnLocationNos = await Common.GetBusinessLocations(),
                            BusnLocationUserRoles = await Common.GetRefLib(Enums.REF_TYPE_MERCH_USER_ROLE),
                            Titles = await Common.GetRefLib(Enums.REF_TYPE_TITLE),
                            Stses = await Common.GetRefLib(Enums.REF_TYPE_USER_STS)
                        };

                        return Json(new FormResponseModel<MerchantUserSearchModel, MerchantUserSearchModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_VALID,
                            ResponseDesc = string.Empty,
                            Model = _infoObj,
                            Selects = _infoSelects,
                            GeneralInfo = _infoObj
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MerchAccts":
                    {
                        var _selects = new MerchantAcctSearchModel()
                        {
                            States = await Common.GetState(null),
                        };
                        return Json(new FormResponseModel<MerchantAcctSearchModel, MerchantAcctSearchModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_VALID,
                            Selects = _selects
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MerchAcctNew":
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                case "MerchAcctInfo":
                    {
                        var _infoObj = await ApiClient.GetJsonAsync<APIResponseModel<MerchantAcctModel>>
                            ("merchant/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(merchAcctNoHash) }));

                        if (_infoObj != null && _infoObj.Result != null)
                        {
                            _infoObj.Result.MerchAcctNoHash = Helper.Encrypt(_infoObj.Result.AcctNo);
                        }

                        var _infoSelects = new MerchantAcctModel()
                        {
                            OwnershipTypes = await Common.GetRefLib(Enums.REF_TYPE_MERCH_OWNERSHIP),
                            BusnSizes = await Common.GetRefLib(Enums.REF_TYPE_BUSN_SIZE),
                            AccountStatuses = await Common.GetRefLib(Enums.REF_TYPE_MERCH_ACCT_STATUS),
                            StsNewReasons = await Common.GetRefLib(Enums.REF_TYPE_MERCH_STS_REASON),
                        };
                        return Json(new FormResponseModel<MerchantAcctModel, MerchantAcctModel>()
                        {
                            ResponseCode = _infoObj.ResponseCode,
                            ResponseDesc = _infoObj.ResponseDesc,
                            Model = _infoObj.Result,
                            Selects = _infoSelects,
                            GeneralInfo = _infoObj.Result
                        }, JsonRequestBehavior.AllowGet);
                    }

                case "MerchAcctAddrs":
                    {
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<MerchantAcctModel>>
                                ("merchant/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(merchAcctNoHash) }));

                        if (_info != null && _info.Result != null)
                        {
                            _info.Result.MerchAcctNoHash = Helper.Encrypt(_info.Result.AcctNo);

                            return Json(new FormResponseModel<MerchantAcctModel, MerchantAcctModel>()
                            {
                                ResponseCode = _info.ResponseCode,
                                ResponseDesc = _info.ResponseDesc,
                                GeneralInfo = _info.Result
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new FormResponseModel<MerchantAcctModel, MerchantAcctModel>()
                            {
                                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                case "MerchAcctAddrNew":
                    {
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<MerchantAcctModel>>
                                ("merchant/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(merchAcctNoHash) }));

                        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            _info.Result.MerchAcctNoHash = Helper.Encrypt(_info.Result.AcctNo);

                            var _newAddress = new AddressModel()
                            {
                                //CtryCd = Enums.DEFAULT_COUNTRY_CODE
                            };
                            var _newAddressSelects = new AddressModel()
                            {
                                Countries = await Common.GetRefLib(Enums.REF_TYPE_COUNTRY),
                                AddressTypes = await Common.GetRefLib(Enums.REF_TYPE_ADDRESS, Enums.REF_NO_ADDRESS),
                                States = await Common.GetState(Enums.DEFAULT_COUNTRY_CODE)
                            };

                            return Json(new FormResponseModel<AddressModel, MerchantAcctModel>()
                            {
                                ResponseCode = Enums.RESPONSE_CODE_VALID,
                                ResponseDesc = "",
                                Model = _newAddress,
                                Selects = _newAddressSelects,
                                GeneralInfo = _info.Result
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new FormResponseModel<AddressModel, MerchantAcctModel>()
                            {
                                ResponseCode = _info.ResponseCode,
                                ResponseDesc = _info.ResponseDesc
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                case "MerchAcctAddrD":
                    {
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<MerchantAcctModel>>
                            ("merchant/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(merchAcctNoHash) }));

                        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            _info.Result.MerchAcctNoHash = Helper.Encrypt(_info.Result.AcctNo);

                            var addrObj = await ApiClient.GetJsonAsync<APIResponseModel<AddressModel>>
                                ("common/Address?" + Helper.GetQueryString(new { Ids = merchAddressId }));

                            if (addrObj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                            {
                                if (addrObj.Result != null && _info.Result != null && addrObj.Result.EntityId == _info.Result.EntityId) // valid merchant and address
                                {
                                    var addrSelects = new AddressModel()
                                    {
                                        Countries = await Common.GetRefLib(Enums.REF_TYPE_COUNTRY),
                                        AddressTypes = await Common.GetRefLib(Enums.REF_TYPE_ADDRESS, Enums.REF_NO_ADDRESS),
                                        States = await Common.GetState(addrObj.Result.CtryCd)
                                    };

                                    return Json(new FormResponseModel<AddressModel, MerchantAcctModel>()
                                    {
                                        ResponseCode = addrObj.ResponseCode,
                                        ResponseDesc = addrObj.ResponseDesc,
                                        Model = addrObj.Result,
                                        Selects = addrSelects,
                                        GeneralInfo = _info.Result
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        return Json(new FormResponseModel<AddressModel, MerchantAcctModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                        }, JsonRequestBehavior.AllowGet);
                    }

                case "MerchAcctConts":
                    {
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<MerchantAcctModel>>
                                ("merchant/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(merchAcctNoHash) }));

                        if (_info != null && _info.Result != null)
                        {
                            _info.Result.MerchAcctNoHash = Helper.Encrypt(_info.Result.AcctNo);

                            return Json(new FormResponseModel<MerchantAcctModel, MerchantAcctModel>()
                                {
                                    ResponseCode = _info.ResponseCode,
                                    ResponseDesc = _info.ResponseDesc,
                                    GeneralInfo = _info.Result
                                }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new FormResponseModel<MerchantAcctModel, MerchantAcctModel>()
                            {
                                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                case "MerchAcctContNew":
                    {
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<MerchantAcctModel>>
                                ("merchant/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(merchAcctNoHash) }));

                        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            _info.Result.MerchAcctNoHash = Helper.Encrypt(_info.Result.AcctNo);
                            var _Selects = new ContactModel();

                            var _Obj = new ContactModel()
                            {
                                HomeTelCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE,
                                HpCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE,
                                WorkTelCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE,
                                FaxCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE,
                                HpNewHpCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE,
                                MainInd = false,
                            };

                            var telCountries = await Common.GetRefLib(Enums.REF_TYPE_TEL_COUNTRY, null, null, null, true);
                            _Selects = new ContactModel()
                            {
                                Occupations = await Common.GetRefLib(Enums.REF_TYPE_OCCUPATION),
                                HpCtryCodes = telCountries,
                                WorkTelCtryCodes = telCountries,
                                HomeTelCtryCodes = telCountries,
                                FaxCtryCodes = telCountries,
                                HpNewHpCtryCodes = telCountries
                            };

                            return Json(new FormResponseModel<ContactModel, MerchantAcctModel>()
                            {
                                ResponseCode = Enums.RESPONSE_CODE_VALID,
                                ResponseDesc = "",
                                Model = _Obj,
                                Selects = _Selects,
                                GeneralInfo = _info.Result
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new FormResponseModel<AddressModel, MerchantAcctModel>()
                            {
                                ResponseCode = _info.ResponseCode,
                                ResponseDesc = _info.ResponseDesc
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                case "MerchAcctContD":
                    {
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<MerchantAcctModel>>
                            ("merchant/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(merchAcctNoHash) }));

                        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            _info.Result.MerchAcctNoHash = Helper.Encrypt(_info.Result.AcctNo);

                            var _Obj = await ApiClient.GetJsonAsync<APIResponseModel<ContactModel>>
                                ("common/ContactById?" + Helper.GetQueryString(new { Ids = merchContactId }));

                            if (_Obj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                            {
                                if (_Obj.Result != null && _info.Result != null && _Obj.Result.EntityId == _info.Result.EntityId) // valid merchant and address
                                {
                                    var telCountries = await Common.GetRefLib(Enums.REF_TYPE_TEL_COUNTRY, null, null, null, true);
                                    var _Selects = new ContactModel()
                                    {
                                        Occupations = await Common.GetRefLib(Enums.REF_TYPE_OCCUPATION),
                                        HpCtryCodes = telCountries,
                                        WorkTelCtryCodes = telCountries,
                                        HomeTelCtryCodes = telCountries,
                                        FaxCtryCodes = telCountries,
                                        HpNewHpCtryCodes = telCountries
                                    };

                                    //_Obj.Result.HpCtryCode = _Obj.Result.HpCtryCode ?? Enums.DEFAULT_COUNTRY_CODE;
                                    //_Obj.Result.WorkTelCtryCode = _Obj.Result.WorkTelCtryCode ?? Enums.DEFAULT_COUNTRY_CODE;
                                    //_Obj.Result.FaxCtryCode = _Obj.Result.FaxCtryCode ?? Enums.DEFAULT_COUNTRY_CODE;
                                    //_Obj.Result.HomeTelCtryCode = _Obj.Result.HomeTelCtryCode ?? Enums.DEFAULT_COUNTRY_CODE;

                                    return Json(new FormResponseModel<ContactModel, MerchantAcctModel>()
                                    {
                                        ResponseCode = _Obj.ResponseCode,
                                        ResponseDesc = _Obj.ResponseDesc,
                                        Model = _Obj.Result,
                                        Selects = _Selects,
                                        GeneralInfo = _info.Result
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        return Json(new FormResponseModel<AddressModel, MerchantAcctModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                        }, JsonRequestBehavior.AllowGet);
                    }

                case "MerchAcctBusnLocations":
                    {
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<MerchantAcctModel>>
                                ("merchant/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(merchAcctNoHash) }));

                        if (_info != null && _info.Result != null)
                        {
                            _info.Result.MerchAcctNoHash = Helper.Encrypt(_info.Result.AcctNo);
                            return Json(new FormResponseModel<MerchantAcctModel, MerchantAcctModel>()
                            {
                                ResponseCode = _info.ResponseCode,
                                ResponseDesc = _info.ResponseDesc,
                                GeneralInfo = _info.Result
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new FormResponseModel<MerchantAcctModel, MerchantAcctModel>()
                            {
                                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                default:
                    return Json(new { });
            }
        }

        #region Merchant User
        [HttpPost]
        public async Task<JsonResult> NewMerchantUser(MerchantUserModel model)
        {
            var rp = await ApiClient.PostJsonAsync<APIResponseModel<MerchantUserModel>>
                ("merchant/User", JsonConvert.SerializeObject(model));

            if (rp.ResponseCode == Enums.RESPONSE_CODE_VALID && rp.Result != null)
            {
                rp.Result.UserIdHash = Helper.Encrypt(rp.Result.UserId);
            }

            return Json(rp, JsonRequestBehavior.AllowGet);
        }

        [HttpPatch]
        public async Task<JsonResult> UpdateMerchantUser(MerchantUserModel model)
        {
            model.UserId = Helper.Decrypt(model.UserIdHash);
            model.IsLockedOut = model.IsLockedOut ?? false;

            var result = await ApiClient.PatchJsonAsync<APIResponseModel<MerchantUserModel>>
                ("merchant/User", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> UserPassword(string UserIdHash)
        {
            var result = await ApiClient.PostJsonAsync<APIResponseModel<MerchantUserModel>>
                ("merchant/UserPassword", JsonConvert.SerializeObject(new { UserId = Helper.Decrypt(UserIdHash) }));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> MerchantUserList(jQueryDataTableParamModel Params, MerchantUserSearchModel model)
        {
            var displyColumns = new string[] { "UserId", "FullName", "TitleName", "StsName", "UserRoleName", "BusnLocationNo", "HpNo", "EmailAddress", "CreatedDate", "CreatedByName" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<MerchantUserModel>>
                ("merchant/UserList?" + Helper.GetQueryString(Params) + "&" + Helper.GetQueryString(model));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<MerchantUserModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.UserId, x.FullName, x.TitleName, x.StsName, x.UserRoleName, x.BusnLocationNo, x.HpNo, x.EmailAddress, x.CreatedDate != null ? x.CreatedDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty, x.CreatedByName, Helper.Encrypt(x.UserId) })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Merchant Account

        /// <summary>
        /// Merchant Account listing 
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult> MerchantAcctList(jQueryDataTableParamModel Params, MerchantAcctSearchModel model)
        {
            var displyColumns = new string[] { "AcctNo", "BusnName", "StatusName", "Ownership", "CreatedDate", "CreatedByName" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<MerchantAcctModel>>
                ("merchant/AccountList?" + Helper.GetQueryString(Params) + "&" + Helper.GetQueryString(model));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<MerchantAcctModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.AcctNo, x.BusnName, x.StatusName, x.Ownership,  x.CreatedDate != null ? x.CreatedDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty, x.CreatedByName, Helper.Encrypt(x.AcctNo) })
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create new merchant account.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> NewMerchantAcct(MerchantAcctModel model)
        {
            var rp = await ApiClient.PostJsonAsync<APIResponseModel<MerchantAcctModel>>
                ("merchant/Account", JsonConvert.SerializeObject(model));

            if (rp.ResponseCode == Enums.RESPONSE_CODE_VALID && rp.Result != null)
            {
                rp.Result.MerchAcctNoHash = Helper.Encrypt(rp.Result.AcctNo);
            }

            return Json(rp, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public async Task<JsonResult> Account(x_MerchantModel model)
        //{
        //    // Call API to update 
        //    var rp = await ApiClient.PostJsonAsync<APIResponseModel<x_MerchantModel>>
        //        ("merchant/Account", JsonConvert.SerializeObject(model));

        //    if (rp.ResponseCode == Enums.RESPONSE_CODE_VALID && rp.Result != null)
        //    {
        //        rp.Result.MerchAcctNoHash = Helper.Encrypt(rp.Result.MerchAcctNo);
        //    }

        //    return Json(rp, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// Update merchant account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<JsonResult> UpdateMerchantAcct(MerchantAcctModel model)
        {
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<MerchantUserModel>>
                ("merchant/Account", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get merchant account log list
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="AcctNoHash"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AccountLogList(jQueryDataTableParamModel Params, string AcctNoHash)
        {
            var response = await ApiClient.GetJsonAsync<APIResponseListModel<LogChangeModel>>
                ("merchant/AccountLogList?AcctNo=" + Helper.Decrypt(AcctNoHash) + "&" + Helper.GetQueryString(Params));

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
                aaData = response.Result.RecordList.Select((x, index) => new object[] { x.OldVal, x.NewVal, x.Reason, x.Description, x.CreatedDate.ToString(Enums.DATE_TIME_FORMAT), x.CreatedByName })
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Update merchant account status
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<JsonResult> AccountStatus(MerchantAcctStatusModel model)
        {
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<MerchantAcctModel>>
                ("merchant/AccountStatus", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Busn Location by Acct No
        public async Task<ActionResult> BusnLocationListByMerchAcctNo(jQueryDataTableParamModel Params, string AcctNoHash)
        {
            var displyColumns = new string[] { "BusnLocation", "BusnName", "StateName", "StatusName", "CreatedDate", "CreatedByName" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<BusnLocationModel>>
                ("merchant/BusinessLocationListByAcctNo?" + Helper.GetQueryString(Params) + "&AcctNo=" + Helper.Decrypt(AcctNoHash));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<BusnLocationModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.BusnLocation, x.BusnName, x.StateName, x.StatusName, x.CreatedDate != null ? x.CreatedDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty, x.CreatedByName, Helper.Encrypt(x.BusnLocation) })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}