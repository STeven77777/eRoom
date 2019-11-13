using MetroOil.LoyaltyOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using MetroOil.LoyaltyOps.Helpers;
using MetroOil.LoyaltyOps.Models.BusnLocation;
using MetroOil.LoyaltyOps.Models.eRoom;

namespace MetroOil.LoyaltyOps.Controllers
{
    //RoomTypesController
    public class RoomTypesController : Controller
    {
        // GET: Cards
        public ActionResult Index()
        {
            return View();
        }

        #region "List Of Web Pages"
        [AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "RoomTypeSearch":
                    return PartialView("List", new RoomTypeModel());
                case "RoomTypeInfoNew":
                    return PartialView("New", new RoomTypeModel());
                case "RoomTypeInfo":
                    return PartialView("Info", new RoomTypeModel());
                default:
                    return PartialView();
            }
        }
        #endregion

        public async Task<JsonResult> FillData(string Prefix, string RoomTypeIDHash, string RoomTypeAddressId, string RoomTypeContactId, string TerminalIds)
        {
            switch (Prefix)
            {
                case "RoomTypeSearch":
                    {
                        var _infoObj = new RoomTypeModel();

                        var _States = await Common.GetState(null);
                        var _infoSelects = new RoomTypeModel()
                        {
                            //States = _States,
                            //DBAStates = _States
                        };

                        return Json(new FormResponseModel<RoomTypeModel, RoomTypeModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_VALID,
                            ResponseDesc = string.Empty,
                            Model = _infoObj,
                            Selects = _infoSelects,
                            GeneralInfo = _infoObj
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "RoomTypeInfoNew":
                    {
                        var _newObj = new RoomTypeModel()
                        {

                        };
                        var _newSelects = new RoomTypeModel()
                        {
                            //AcctNos = await Common.GetMerchants()
                        };
                        return Json(new { Model = _newObj, Selects = _newSelects }, JsonRequestBehavior.AllowGet);
                    }
                case "RoomTypeInfo":
                    {
                        var _infoObj = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                            ("roomTypes/" + Helper.Decrypt(RoomTypeIDHash));

                        if (_infoObj != null && _infoObj.Result != null)
                        {
                            _infoObj.Result.RoomTypeIDHash = RoomTypeIDHash;
                        }

                        var _infoSelects = new RoomTypeModel()
                        {
                            //AcctNos = await Common.GetMerchants(),
                            //Mccs = await Common.GetRefLib(Enums.REF_TYPE_MCC),
                            //Sics = await Common.GetRefLib(Enums.REF_TYPE_SIC),
                            //OwnershipTypes = await Common.GetRefLib(Enums.REF_TYPE_MERCH_OWNERSHIP),
                            //DBAStates = await Common.GetState(null),
                            //StsNewReasons = await Common.GetRefLib(Enums.REF_TYPE_MERCH_STS_REASON),
                            //Stses = await Common.GetRefLib(Enums.REF_TYPE_MERCH_ACCT_STATUS),
                        };

                        return Json(new FormResponseModel<RoomTypeModel, RoomTypeModel>()
                        {
                            ResponseCode = _infoObj.ResponseCode,
                            ResponseDesc = _infoObj.ResponseDesc,
                            Model = _infoObj.Result,
                            Selects = _infoSelects,
                            GeneralInfo = _infoObj.Result
                        }, JsonRequestBehavior.AllowGet);
                    }

                case "RoomTypeAddrs":
                    {
                        //var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                        //    ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomType = Helper.Decrypt(RoomTypeIDHash) }));
                        //var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>("RoomTypes/" + Helper.GetQueryString(new { RoomTypeNumber = Helper.Decrypt(RoomTypeIDHash) }));
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>("RoomTypes/" + Helper.Decrypt(RoomTypeIDHash));
                        //var _info = await ApiClient.GetJsonAsync<APIResponseListModel<RoomTypeModel>>("RoomTypes/GetList");
                        //var _info = await ApiClient.GetArrayJsonAsync<APIResponseModel<List<RoomTypeModel>>>("RoomTypes/GetList");

                        if (_info != null && _info.Result != null)
                        {
                            // _info.Result.RoomTypeID = Helper.Encrypt(_info.Result.RoomTypeID);
                            return Json(new FormResponseModel<RoomTypeModel, RoomTypeModel>()
                            {
                                ResponseCode = _info.ResponseCode,
                                ResponseDesc = _info.ResponseDesc,
                                GeneralInfo = _info.Result,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new FormResponseModel<RoomTypeModel, RoomTypeModel>()
                            {
                                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                case "RoomTypeAddrNew":
                    {
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomType = Helper.Decrypt(RoomTypeIDHash) }));

                        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            //_info.Result.RoomTypeIDHash = Helper.Encrypt(_info.Result.RoomTypeID);

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

                            return Json(new FormResponseModel<AddressModel, RoomTypeModel>()
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
                            return Json(new FormResponseModel<AddressModel, RoomTypeModel>()
                            {
                                ResponseCode = _info.ResponseCode,
                                ResponseDesc = _info.ResponseDesc
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                //case "RoomTypeAddrD":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomType = Helper.Decrypt(RoomTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            //_info.Result.RoomTypeIDHash = Helper.Encrypt(_info.Result.AcctNo);

                //            var addrObj = await ApiClient.GetJsonAsync<APIResponseModel<AddressModel>>
                //                ("common/Address?" + Helper.GetQueryString(new { Ids = RoomTypeAddressId }));

                //            if (addrObj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //            {
                //                if (addrObj.Result != null && _info.Result != null && addrObj.Result.EntityId == _info.Result.EntityId) // valid merchant and address
                //                {
                //                    var addrSelects = new AddressModel()
                //                    {
                //                        Countries = await Common.GetRefLib(Enums.REF_TYPE_COUNTRY),
                //                        AddressTypes = await Common.GetRefLib(Enums.REF_TYPE_ADDRESS, Enums.REF_NO_ADDRESS),
                //                        States = await Common.GetState(addrObj.Result.CtryCd)
                //                    };

                //                    return Json(new FormResponseModel<AddressModel, RoomTypeModel>()
                //                    {
                //                        ResponseCode = addrObj.ResponseCode,
                //                        ResponseDesc = addrObj.ResponseDesc,
                //                        Model = addrObj.Result,
                //                        Selects = addrSelects,
                //                        GeneralInfo = _info.Result
                //                    }, JsonRequestBehavior.AllowGet);
                //                }
                //            }
                //        }

                //        return Json(new FormResponseModel<AddressModel, RoomTypeModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "RoomTypeConts":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomType = Helper.Decrypt(RoomTypeIDHash) }));

                //        if (_info != null && _info.Result != null)
                //        {
                //            _info.Result.RoomTypeIDHash = Helper.Encrypt(_info.Result.RoomType);
                //            return Json(new FormResponseModel<RoomTypeModel, RoomTypeModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //        else
                //        {
                //            return Json(new FormResponseModel<RoomTypeModel, RoomTypeModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "RoomTypeContNew":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomType = Helper.Decrypt(RoomTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomTypeIDHash = Helper.Encrypt(_info.Result.RoomType);
                //            var _Selects = new ContactModel();

                //            var _Obj = new ContactModel()
                //            {
                //                HomeTelCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE,
                //                HpCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE,
                //                WorkTelCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE,
                //                FaxCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE,
                //                HpNewHpCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE,
                //                MainInd = false,
                //            };

                //            var telCountries = await Common.GetRefLib(Enums.REF_TYPE_TEL_COUNTRY, null, null, null, true);
                //            _Selects = new ContactModel()
                //            {
                //                Occupations = await Common.GetRefLib(Enums.REF_TYPE_OCCUPATION),
                //                HpCtryCodes = telCountries,
                //                WorkTelCtryCodes = telCountries,
                //                HomeTelCtryCodes = telCountries,
                //                FaxCtryCodes = telCountries,
                //                HpNewHpCtryCodes = telCountries
                //            };

                //            return Json(new FormResponseModel<ContactModel, RoomTypeModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_VALID,
                //                ResponseDesc = "",
                //                Model = _Obj,
                //                Selects = _Selects,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //        else
                //        {
                //            return Json(new FormResponseModel<AddressModel, RoomTypeModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "RoomTypeContD":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomType = Helper.Decrypt(RoomTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomTypeIDHash = Helper.Encrypt(_info.Result.RoomType);

                //            var _Obj = await ApiClient.GetJsonAsync<APIResponseModel<ContactModel>>
                //                ("common/ContactById?" + Helper.GetQueryString(new { Ids = RoomTypeContactId }));

                //            if (_Obj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //            {
                //                if (_Obj.Result != null && _info.Result != null && _Obj.Result.EntityId == _info.Result.EntityId) // valid merchant and address
                //                {
                //                    var telCountries = await Common.GetRefLib(Enums.REF_TYPE_TEL_COUNTRY, null, null, null, true);
                //                    var _Selects = new ContactModel()
                //                    {
                //                        Occupations = await Common.GetRefLib(Enums.REF_TYPE_OCCUPATION),
                //                        HpCtryCodes = telCountries,
                //                        WorkTelCtryCodes = telCountries,
                //                        HomeTelCtryCodes = telCountries,
                //                        FaxCtryCodes = telCountries,
                //                        HpNewHpCtryCodes = telCountries
                //                    };

                //                    //_Obj.Result.HpCtryCode = _Obj.Result.HpCtryCode ?? Enums.DEFAULT_COUNTRY_CODE;
                //                    //_Obj.Result.WorkTelCtryCode = _Obj.Result.WorkTelCtryCode ?? Enums.DEFAULT_COUNTRY_CODE;
                //                    //_Obj.Result.FaxCtryCode = _Obj.Result.FaxCtryCode ?? Enums.DEFAULT_COUNTRY_CODE;
                //                    //_Obj.Result.HomeTelCtryCode = _Obj.Result.HomeTelCtryCode ?? Enums.DEFAULT_COUNTRY_CODE;

                //                    return Json(new FormResponseModel<ContactModel, RoomTypeModel>()
                //                    {
                //                        ResponseCode = _Obj.ResponseCode,
                //                        ResponseDesc = _Obj.ResponseDesc,
                //                        Model = _Obj.Result,
                //                        Selects = _Selects,
                //                        GeneralInfo = _info.Result
                //                    }, JsonRequestBehavior.AllowGet);
                //                }
                //            }
                //        }

                //        return Json(new FormResponseModel<AddressModel, RoomTypeModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "RoomTypeTerminals":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomType = Helper.Decrypt(RoomTypeIDHash) }));

                //        if (_info != null && _info.Result != null)
                //        {
                //            _info.Result.RoomTypeIDHash = Helper.Encrypt(_info.Result.RoomType);
                //            return Json(new FormResponseModel<RoomTypeModel, RoomTypeModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //        else
                //        {
                //            return Json(new FormResponseModel<RoomTypeModel, RoomTypeModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "RoomTypeTerminalNew":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomType = Helper.Decrypt(RoomTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomTypeIDHash = Helper.Encrypt(_info.Result.RoomType);
                //            var _Obj = new TerminalModel()
                //            {
                //                RoomTypeIDHash = _info.Result.RoomTypeIDHash
                //            };

                //            var _Selects = new TerminalModel()
                //            {
                //                DeviceTypeCds = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_DEVICE_TYPE),
                //                TermSrcs = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_SRC),
                //                Stses = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_STS)
                //            };

                //            return Json(new FormResponseModel<TerminalModel, RoomTypeModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_VALID,
                //                ResponseDesc = "",
                //                Model = _Obj,
                //                Selects = _Selects,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //        else
                //        {
                //            return Json(new FormResponseModel<TerminalModel, RoomTypeModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "RoomTypeTerminalInfo":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomType = Helper.Decrypt(RoomTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomTypeIDHash = Helper.Encrypt(_info.Result.RoomType);

                //            var _Obj = await ApiClient.GetJsonAsync<APIResponseModel<TerminalModel>>
                //                ("merchant/TerminalInventory?" + Helper.GetQueryString(new { Ids = TerminalIds }));

                //            if (_Obj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //            {
                //                if (_Obj.Result != null && _info.Result != null && _Obj.Result.RoomType == _info.Result.RoomType) // valid merchant and address
                //                {
                //                    _Obj.Result.Ids = TerminalIds;

                //                    var _Selects = new TerminalModel()
                //                    {
                //                        DeviceTypeCds = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_DEVICE_TYPE),
                //                        TermSrcs = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_SRC),
                //                        Stses = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_STS),
                //                        StsNewReasons = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_STS_REASON)
                //                    };

                //                    return Json(new FormResponseModel<TerminalModel, RoomTypeModel>()
                //                    {
                //                        ResponseCode = _Obj.ResponseCode,
                //                        ResponseDesc = _Obj.ResponseDesc,
                //                        Model = _Obj.Result,
                //                        Selects = _Selects,
                //                        GeneralInfo = _info.Result
                //                    }, JsonRequestBehavior.AllowGet);
                //                }
                //            }
                //        }

                //        return Json(new FormResponseModel<TerminalModel, RoomTypeModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "RoomTypeTxnPoints":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomType = Helper.Decrypt(RoomTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomTypeIDHash = Helper.Encrypt(_info.Result.RoomType);

                //            var _Obj = new TxnPointSearchModel()
                //            {
                //                RoomTypeIDHash = _info.Result.RoomTypeIDHash,
                //                DateFrom = DateTime.Today,
                //                DateTo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59),
                //                TxnInd = Enums.TXN_UNPOSTED//.TXN_POSTED
                //            };

                //            var _Selects = new TxnPointSearchModel()
                //            {
                //                //RoomTypeNos = await Common.GetBusinessLocations(),
                //            };

                //            return Json(new FormResponseModel<TxnPointSearchModel, RoomTypeModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_VALID,
                //                Model = _Obj,
                //                Selects = _Selects,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }

                //        return Json(new FormResponseModel<TxnPointSearchModel, RoomTypeModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "RoomTypeCrdRangeAccpt":
                //    {
                //        var RoomTypeNo = Helper.Decrypt(RoomTypeIDHash);
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomType = RoomTypeNo }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomTypeIDHash = Helper.Encrypt(_info.Result.RoomType);

                //            var _params = new jQueryDataTableParamModel()
                //            {
                //                iDisplayStart = 0,
                //                iDisplayLength = int.MaxValue, // get all, no paging
                //                AllowPaging = false
                //            };

                //            var _cardRangeLst = await ApiClient.GetJsonAsync<APIResponseListModel<CardRangeAcceptanceModel>>
                //                ("merchant/CardRangeList?RoomTypeNo=" + RoomTypeNo + "&" + Helper.GetQueryString(_params));

                //            var _Obj = new CardRangeAcceptanceListModel();
                //            if (_cardRangeLst != null && _cardRangeLst.Result != null && _cardRangeLst.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //            {
                //                _Obj.CardRangeAcceptanceLst = _cardRangeLst.Result.RecordList;
                //            }

                //            var _Selects = new CardRangeAcceptanceListModel()
                //            {
                //                CardRangeSelects = await Common.GetCardRanges()
                //            };

                //            return Json(new FormResponseModel<CardRangeAcceptanceListModel, RoomTypeModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_VALID,
                //                Model = _Obj,
                //                Selects = _Selects,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }

                //        return Json(new FormResponseModel<TxnPointSearchModel, RoomTypeModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }
                default:
                    return Json(new { });
            }
        }

        #region Business Location Search
        public async Task<ActionResult> RoomTypeSearch(jQueryDataTableParamModel Params, RoomTypeModel model)
        {
            var displyColumns = new string[] { "RoomTypeID", "RoomTypeName", "CreateDate", "CreateUser", "UpdateDate", "UpdateUser", "Description" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<RoomTypeModel>>
                ("roomTypes/roomTypelist?" + Helper.GetQueryString(Params) + "&" + Helper.GetQueryString(model));

            if (response == null || response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<RoomTypeModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.RoomTypeID, x.RoomTypeName,
                    x.CreateDate != null ? x.CreateDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty, x.CreateUser,
                    x.UpdateDate != null ? x.UpdateDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty, x.UpdateUser,
                    x.Description, Helpers.Helper.Encrypt(x.RoomTypeID.ToString()) })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region New Business Location
        [HttpPost]
        public async Task<JsonResult> AddRoomType(RoomTypeModel model)
        {
            //    model.AllCardRange = model.AllCardRange ?? false;
            //    model.AllTxnCodeMapping = model.AllTxnCodeMapping ?? false;

            // Call API to update 
            var rp = await ApiClient.PostJsonAsync<APIResponseModel<RoomTypeModel>>
                ("RoomTypes", JsonConvert.SerializeObject(model));

            if (rp.ResponseCode == Enums.RESPONSE_CODE_VALID && rp.Result != null)
            {
                rp.Result.RoomTypeIDHash = Helper.Encrypt(rp.Result.RoomTypeID.ToString());
            }

            return Json(rp, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Business Location Info
        [HttpPatch]
        public async Task<JsonResult> UpdateRoomType(RoomTypeModel model)
        {
            //model.RoomTypeID = Helper.Decrypt(model.RoomTypeID);
            //var result = await ApiClient.PatchJsonAsync<APIResponseModel<RoomTypeModel>>
            //  ("merchant/BusinessLocation", JsonConvert.SerializeObject(model));
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<RoomTypeModel>>
                ("RoomTypes", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get merchant account log list
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AccountLogList(jQueryDataTableParamModel Params, string RoomTypeIDHash)
        {
            var response = await ApiClient.GetJsonAsync<APIResponseListModel<LogChangeModel>>
                ("merchant/BusinessLocationLogList?RoomType=" + Helper.Decrypt(RoomTypeIDHash) + "&" + Helper.GetQueryString(Params));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
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
        public async Task<JsonResult> RoomTypeStatus(Models.Merchants.Account.MerchantAcctStatusModel model)
        {
            var result = await ApiClient.PostJsonAsync<APIResponseModel<Models.Merchants.Account.MerchantAcctStatusModel>>
                ("merchant/BusinessLocationStatus", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Terminal Management
        public async Task<ActionResult> TerminalList(jQueryDataTableParamModel Params, string RoomTypeIDHash)
        {
            var displyColumns = new string[] { "TermId", "DeviceTypeName", "DeployDate", "StatusName", "CreatedDate", "CreatedByName" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<TerminalModel>>
                ("merchant/TerminalInventoryList?" + Helper.GetQueryString(Params) + "&RoomTypeNo=" + Helper.Decrypt(RoomTypeIDHash));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<TerminalModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.TermId, x.DeviceTypeName, x.DeployDate != null ? x.DeployDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty, x.StatusName, x.CreatedDate != null ? x.CreatedDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty, x.CreatedByName, x.Ids })
            }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public async Task<JsonResult> AddTerminals(TerminalModel model)
        //{
        //    // Call API to update 
        //    model.RoomType = Helper.Decrypt(model.RoomTypeIDHash);
        //    //model.RoomTypeNo = model.RoomType;// will be removed soon
        //    var rp = await ApiClient.PostJsonAsync<APIResponseModel<TerminalModel>>
        //        ("merchant/TerminalInventory", JsonConvert.SerializeObject(model));

        //    return Json(rp, JsonRequestBehavior.AllowGet);
        //}

        [HttpPatch]
        public async Task<JsonResult> UpdateTerminal(TerminalModel model)
        {
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<TerminalModel>>
                ("merchant/TerminalInventory", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> TerminalLogList(jQueryDataTableParamModel Params, string Ids)
        {
            var response = await ApiClient.GetJsonAsync<APIResponseListModel<LogChangeModel>>
                ("merchant/TerminalInventoryLogList?Ids=" + Ids + "&" + Helper.GetQueryString(Params));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
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

        [HttpPatch]
        public async Task<JsonResult> TerminalStatus(TerminalChangeSts model)
        {
            var result = await ApiClient.PostJsonAsync<APIResponseModel<TerminalChangeSts>>
                ("merchant/TerminalInventoryStatus", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Transaction
        [HttpGet]
        public async Task<ActionResult> GetTxnByBusinessLocation(jQueryDataTableParamModel Params, TxnPointSearchModel txnSearch)
        {
            if (txnSearch != null)
            {
                //txnSearch.RoomType = Helper.Decrypt(txnSearch.RoomTypeIDHash);
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<TxnPointModel>>
                ("merchant/TxnByBusinessLocation?" + Helper.GetQueryString(txnSearch) + "&" + Helper.GetQueryString(Params));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<TxnPointModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1,
                    x.TxnNo,
                    x.TxnCategoryName,
                    x.TxnDate != null ? x.TxnDate.Value.ToString(Enums.DATE_TIME_FORMAT) : string.Empty,
                    x.TxnAmount.ToString ("0.00"),
                    x.TxnPoints.ToString ("0.00"),
                    x.PrcsDate != null ? x.PrcsDate.Value.ToString(Enums.DATE_TIME_FORMAT) : string.Empty
                })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetTxnDetailByBusinessLocation(string TxnId, string TxnInd)
        {
            var _infoObj = await ApiClient.GetJsonAsync<APIResponseModel<TxnPointModel>>
                            ("merchant/TxnDetailByBusinessLocation?" + Helper.GetQueryString(new { TxnId, TxnInd }));

            return Json(_infoObj, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetTxnProdDetailByBusinessLocation(jQueryDataTableParamModel Params, string TxnId, string TxnInd)
        {
            var response = await ApiClient.GetJsonAsync<APIResponseListModel<ProductModel>>
                ("merchant/TxnProdDetailByBusinessLocation?TxnId=" + TxnId + "&TxnInd=" + TxnInd + "&" + Helper.GetQueryString(Params));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<ProductModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1,
                    x.ProductName,
                    x.ProductTypeName,
                    x.Qty.ToString ("0.00"),
                    x.Amt.ToString ("0.00"),
                    x.Points.ToString ("0.00")
                })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Card Range Acceptance
        //[HttpPost]
        //public async Task<JsonResult> AddCardRange(CardRangeAcceptanceModel model)
        //{
        //    model.RoomTypeNo = Helper.Decrypt(model.RoomTypeIDHash);
        //    // Call API to update 
        //    var rp = await ApiClient.PostJsonAsync<APIResponseModel<CardRangeAcceptanceModel>>
        //        ("merchant/CardRange", JsonConvert.SerializeObject(model));

        //    return Json(rp, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public async Task<JsonResult> DeleteCardRange(CardRangeAcceptanceModel model)
        {
            var result = await ApiClient.DeleteJsonAsync<APIResponseModel<CardRangeAcceptanceModel>>
                ("merchant/CardRange?Ids=" + model.Ids, JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}