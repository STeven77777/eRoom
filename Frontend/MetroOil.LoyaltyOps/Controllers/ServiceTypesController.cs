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
    //ServiceTypesController
    public class ServiceTypesController : Controller
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
                case "ServiceTypeSearch":
                    return PartialView("List", new ServiceTypeModel());
                case "ServiceTypeInfoNew":
                    return PartialView("New", new ServiceTypeModel());
                case "ServiceTypeInfo":
                    return PartialView("Info", new ServiceTypeModel());
                default:
                    return PartialView();
            }
        }
        #endregion

        public async Task<JsonResult> FillData(string Prefix, string ServiceTypeIDHash, string ServiceTypeAddressId, string ServiceTypeContactId, string TerminalIds)
        {
            switch (Prefix)
            {
                case "ServiceTypeSearch":
                    {
                        var _infoObj = new ServiceTypeModel();

                        var _States = await Common.GetState(null);
                        var _infoSelects = new ServiceTypeModel()
                        {
                            //States = _States,
                            //DBAStates = _States
                        };

                        return Json(new FormResponseModel<ServiceTypeModel, ServiceTypeModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_VALID,
                            ResponseDesc = string.Empty,
                            Model = _infoObj,
                            Selects = _infoSelects,
                            GeneralInfo = _infoObj
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "ServiceTypeInfoNew":
                    {
                        var _newObj = new ServiceTypeModel()
                        {

                        };
                        var _newSelects = new ServiceTypeModel()
                        {
                            //AcctNos = await Common.GetMerchants()
                        };
                        return Json(new { Model = _newObj, Selects = _newSelects }, JsonRequestBehavior.AllowGet);
                    }
                case "ServiceTypeInfo":
                    {
                        var _infoObj = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                            ("serviceTypes/" + Helper.Decrypt(ServiceTypeIDHash));

                        if (_infoObj != null && _infoObj.Result != null)
                        {
                            _infoObj.Result.ServiceTypeIDHash = ServiceTypeIDHash;
                        }

                        var _infoSelects = new ServiceTypeModel()
                        {
                            //AcctNos = await Common.GetMerchants(),
                            //Mccs = await Common.GetRefLib(Enums.REF_TYPE_MCC),
                            //Sics = await Common.GetRefLib(Enums.REF_TYPE_SIC),
                            //OwnershipTypes = await Common.GetRefLib(Enums.REF_TYPE_MERCH_OWNERSHIP),
                            //DBAStates = await Common.GetState(null),
                            //StsNewReasons = await Common.GetRefLib(Enums.REF_TYPE_MERCH_STS_REASON),
                            //Stses = await Common.GetRefLib(Enums.REF_TYPE_MERCH_ACCT_STATUS),
                        };

                        return Json(new FormResponseModel<ServiceTypeModel, ServiceTypeModel>()
                        {
                            ResponseCode = _infoObj.ResponseCode,
                            ResponseDesc = _infoObj.ResponseDesc,
                            Model = _infoObj.Result,
                            Selects = _infoSelects,
                            GeneralInfo = _infoObj.Result
                        }, JsonRequestBehavior.AllowGet);
                    }

                case "ServiceTypeAddrs":
                    {
                        //var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                        //    ("merchant/BusinessLocation?" + Helper.GetQueryString(new { ServiceType = Helper.Decrypt(ServiceTypeIDHash) }));
                        //var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>("ServiceTypes/" + Helper.GetQueryString(new { ServiceTypeNumber = Helper.Decrypt(ServiceTypeIDHash) }));
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>("ServiceTypes/" + Helper.Decrypt(ServiceTypeIDHash));
                        //var _info = await ApiClient.GetJsonAsync<APIResponseListModel<ServiceTypeModel>>("ServiceTypes/GetList");
                        //var _info = await ApiClient.GetArrayJsonAsync<APIResponseModel<List<ServiceTypeModel>>>("ServiceTypes/GetList");

                        if (_info != null && _info.Result != null)
                        {
                            // _info.Result.ServiceTypeID = Helper.Encrypt(_info.Result.ServiceTypeID);
                            return Json(new FormResponseModel<ServiceTypeModel, ServiceTypeModel>()
                            {
                                ResponseCode = _info.ResponseCode,
                                ResponseDesc = _info.ResponseDesc,
                                GeneralInfo = _info.Result,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new FormResponseModel<ServiceTypeModel, ServiceTypeModel>()
                            {
                                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                case "ServiceTypeAddrNew":
                    {
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { ServiceType = Helper.Decrypt(ServiceTypeIDHash) }));

                        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            //_info.Result.ServiceTypeIDHash = Helper.Encrypt(_info.Result.ServiceTypeID);

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

                            return Json(new FormResponseModel<AddressModel, ServiceTypeModel>()
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
                            return Json(new FormResponseModel<AddressModel, ServiceTypeModel>()
                            {
                                ResponseCode = _info.ResponseCode,
                                ResponseDesc = _info.ResponseDesc
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                //case "ServiceTypeAddrD":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { ServiceType = Helper.Decrypt(ServiceTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            //_info.Result.ServiceTypeIDHash = Helper.Encrypt(_info.Result.AcctNo);

                //            var addrObj = await ApiClient.GetJsonAsync<APIResponseModel<AddressModel>>
                //                ("common/Address?" + Helper.GetQueryString(new { Ids = ServiceTypeAddressId }));

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

                //                    return Json(new FormResponseModel<AddressModel, ServiceTypeModel>()
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

                //        return Json(new FormResponseModel<AddressModel, ServiceTypeModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "ServiceTypeConts":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { ServiceType = Helper.Decrypt(ServiceTypeIDHash) }));

                //        if (_info != null && _info.Result != null)
                //        {
                //            _info.Result.ServiceTypeIDHash = Helper.Encrypt(_info.Result.ServiceType);
                //            return Json(new FormResponseModel<ServiceTypeModel, ServiceTypeModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //        else
                //        {
                //            return Json(new FormResponseModel<ServiceTypeModel, ServiceTypeModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "ServiceTypeContNew":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { ServiceType = Helper.Decrypt(ServiceTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.ServiceTypeIDHash = Helper.Encrypt(_info.Result.ServiceType);
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

                //            return Json(new FormResponseModel<ContactModel, ServiceTypeModel>()
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
                //            return Json(new FormResponseModel<AddressModel, ServiceTypeModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "ServiceTypeContD":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { ServiceType = Helper.Decrypt(ServiceTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.ServiceTypeIDHash = Helper.Encrypt(_info.Result.ServiceType);

                //            var _Obj = await ApiClient.GetJsonAsync<APIResponseModel<ContactModel>>
                //                ("common/ContactById?" + Helper.GetQueryString(new { Ids = ServiceTypeContactId }));

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

                //                    return Json(new FormResponseModel<ContactModel, ServiceTypeModel>()
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

                //        return Json(new FormResponseModel<AddressModel, ServiceTypeModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "ServiceTypeTerminals":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { ServiceType = Helper.Decrypt(ServiceTypeIDHash) }));

                //        if (_info != null && _info.Result != null)
                //        {
                //            _info.Result.ServiceTypeIDHash = Helper.Encrypt(_info.Result.ServiceType);
                //            return Json(new FormResponseModel<ServiceTypeModel, ServiceTypeModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //        else
                //        {
                //            return Json(new FormResponseModel<ServiceTypeModel, ServiceTypeModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "ServiceTypeTerminalNew":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { ServiceType = Helper.Decrypt(ServiceTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.ServiceTypeIDHash = Helper.Encrypt(_info.Result.ServiceType);
                //            var _Obj = new TerminalModel()
                //            {
                //                ServiceTypeIDHash = _info.Result.ServiceTypeIDHash
                //            };

                //            var _Selects = new TerminalModel()
                //            {
                //                DeviceTypeCds = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_DEVICE_TYPE),
                //                TermSrcs = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_SRC),
                //                Stses = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_STS)
                //            };

                //            return Json(new FormResponseModel<TerminalModel, ServiceTypeModel>()
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
                //            return Json(new FormResponseModel<TerminalModel, ServiceTypeModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "ServiceTypeTerminalInfo":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { ServiceType = Helper.Decrypt(ServiceTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.ServiceTypeIDHash = Helper.Encrypt(_info.Result.ServiceType);

                //            var _Obj = await ApiClient.GetJsonAsync<APIResponseModel<TerminalModel>>
                //                ("merchant/TerminalInventory?" + Helper.GetQueryString(new { Ids = TerminalIds }));

                //            if (_Obj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //            {
                //                if (_Obj.Result != null && _info.Result != null && _Obj.Result.ServiceType == _info.Result.ServiceType) // valid merchant and address
                //                {
                //                    _Obj.Result.Ids = TerminalIds;

                //                    var _Selects = new TerminalModel()
                //                    {
                //                        DeviceTypeCds = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_DEVICE_TYPE),
                //                        TermSrcs = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_SRC),
                //                        Stses = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_STS),
                //                        StsNewReasons = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_STS_REASON)
                //                    };

                //                    return Json(new FormResponseModel<TerminalModel, ServiceTypeModel>()
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

                //        return Json(new FormResponseModel<TerminalModel, ServiceTypeModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "ServiceTypeTxnPoints":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { ServiceType = Helper.Decrypt(ServiceTypeIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.ServiceTypeIDHash = Helper.Encrypt(_info.Result.ServiceType);

                //            var _Obj = new TxnPointSearchModel()
                //            {
                //                ServiceTypeIDHash = _info.Result.ServiceTypeIDHash,
                //                DateFrom = DateTime.Today,
                //                DateTo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59),
                //                TxnInd = Enums.TXN_UNPOSTED//.TXN_POSTED
                //            };

                //            var _Selects = new TxnPointSearchModel()
                //            {
                //                //ServiceTypeNos = await Common.GetBusinessLocations(),
                //            };

                //            return Json(new FormResponseModel<TxnPointSearchModel, ServiceTypeModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_VALID,
                //                Model = _Obj,
                //                Selects = _Selects,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }

                //        return Json(new FormResponseModel<TxnPointSearchModel, ServiceTypeModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "ServiceTypeCrdRangeAccpt":
                //    {
                //        var ServiceTypeNo = Helper.Decrypt(ServiceTypeIDHash);
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<ServiceTypeModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { ServiceType = ServiceTypeNo }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.ServiceTypeIDHash = Helper.Encrypt(_info.Result.ServiceType);

                //            var _params = new jQueryDataTableParamModel()
                //            {
                //                iDisplayStart = 0,
                //                iDisplayLength = int.MaxValue, // get all, no paging
                //                AllowPaging = false
                //            };

                //            var _cardRangeLst = await ApiClient.GetJsonAsync<APIResponseListModel<CardRangeAcceptanceModel>>
                //                ("merchant/CardRangeList?ServiceTypeNo=" + ServiceTypeNo + "&" + Helper.GetQueryString(_params));

                //            var _Obj = new CardRangeAcceptanceListModel();
                //            if (_cardRangeLst != null && _cardRangeLst.Result != null && _cardRangeLst.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //            {
                //                _Obj.CardRangeAcceptanceLst = _cardRangeLst.Result.RecordList;
                //            }

                //            var _Selects = new CardRangeAcceptanceListModel()
                //            {
                //                CardRangeSelects = await Common.GetCardRanges()
                //            };

                //            return Json(new FormResponseModel<CardRangeAcceptanceListModel, ServiceTypeModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_VALID,
                //                Model = _Obj,
                //                Selects = _Selects,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }

                //        return Json(new FormResponseModel<TxnPointSearchModel, ServiceTypeModel>()
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
        public async Task<ActionResult> ServiceTypeSearch(jQueryDataTableParamModel Params, ServiceTypeModel model)
        {
            var displyColumns = new string[] { "ServiceTypeID", "ServiceTypeName", "CreateDate", "CreateUser", "UpdateDate", "UpdateUser", "Description" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<ServiceTypeModel>>
                ("serviceTypes/serviceTypelist?" + Helper.GetQueryString(Params) + "&" + Helper.GetQueryString(model));

            if (response == null || response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<ServiceTypeModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.ServiceTypeID, x.ServiceTypeName,
                    x.CreateDate != null ? x.CreateDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty, x.CreateUser,
                    x.UpdateDate != null ? x.UpdateDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty, x.UpdateUser,
                    x.Description, Helpers.Helper.Encrypt(x.ServiceTypeID.ToString()) })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region New Business Location
        [HttpPost]
        public async Task<JsonResult> AddServiceType(ServiceTypeModel model)
        {
            //    model.AllCardRange = model.AllCardRange ?? false;
            //    model.AllTxnCodeMapping = model.AllTxnCodeMapping ?? false;

            // Call API to update 
            var rp = await ApiClient.PostJsonAsync<APIResponseModel<ServiceTypeModel>>
                ("ServiceTypes", JsonConvert.SerializeObject(model));

            if (rp.ResponseCode == Enums.RESPONSE_CODE_VALID && rp.Result != null)
            {
                rp.Result.ServiceTypeIDHash = Helper.Encrypt(rp.Result.ServiceTypeID.ToString());
            }

            return Json(rp, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Business Location Info
        [HttpPatch]
        public async Task<JsonResult> UpdateServiceType(ServiceTypeModel model)
        {
            //model.ServiceTypeID = Helper.Decrypt(model.ServiceTypeID);
            //var result = await ApiClient.PatchJsonAsync<APIResponseModel<ServiceTypeModel>>
            //  ("merchant/BusinessLocation", JsonConvert.SerializeObject(model));
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<ServiceTypeModel>>
                ("ServiceTypes", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get merchant account log list
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AccountLogList(jQueryDataTableParamModel Params, string ServiceTypeIDHash)
        {
            var response = await ApiClient.GetJsonAsync<APIResponseListModel<LogChangeModel>>
                ("merchant/BusinessLocationLogList?ServiceType=" + Helper.Decrypt(ServiceTypeIDHash) + "&" + Helper.GetQueryString(Params));

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
        public async Task<JsonResult> ServiceTypeStatus(Models.Merchants.Account.MerchantAcctStatusModel model)
        {
            var result = await ApiClient.PostJsonAsync<APIResponseModel<Models.Merchants.Account.MerchantAcctStatusModel>>
                ("merchant/BusinessLocationStatus", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Terminal Management
        public async Task<ActionResult> TerminalList(jQueryDataTableParamModel Params, string ServiceTypeIDHash)
        {
            var displyColumns = new string[] { "TermId", "DeviceTypeName", "DeployDate", "StatusName", "CreatedDate", "CreatedByName" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<TerminalModel>>
                ("merchant/TerminalInventoryList?" + Helper.GetQueryString(Params) + "&ServiceTypeNo=" + Helper.Decrypt(ServiceTypeIDHash));

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
        //    model.ServiceType = Helper.Decrypt(model.ServiceTypeIDHash);
        //    //model.ServiceTypeNo = model.ServiceType;// will be removed soon
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
                //txnSearch.ServiceType = Helper.Decrypt(txnSearch.ServiceTypeIDHash);
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
        //    model.ServiceTypeNo = Helper.Decrypt(model.ServiceTypeIDHash);
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