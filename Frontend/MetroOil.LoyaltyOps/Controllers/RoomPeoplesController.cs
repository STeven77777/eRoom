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
    //RoomPeoplesController
    public class RoomPeoplesController : Controller
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
                case "RoomPeopleSearch":
                    return PartialView("List", new RoomPeopleModel());
                case "RoomPeopleInfoNew":
                    return PartialView("New", new RoomPeopleModel());
                case "RoomPeopleInfo":
                    return PartialView("Info", new RoomPeopleModel());
                default:
                    return PartialView();
            }
        }
        #endregion

        public async Task<JsonResult> FillData(string Prefix, string RoomPeopleIDHash, string RoomPeopleAddressId, string RoomPeopleContactId, string TerminalIds)
        {
            switch (Prefix)
            {
                case "RoomPeopleSearch":
                    {
                        var _infoObj = new RoomPeopleModel();

                        var _States = await Common.GetState(null);
                        var _infoSelects = new RoomPeopleModel()
                        {
                            //States = _States,
                            //DBAStates = _States
                        };

                        return Json(new FormResponseModel<RoomPeopleModel, RoomPeopleModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_VALID,
                            ResponseDesc = string.Empty,
                            Model = _infoObj,
                            Selects = _infoSelects,
                            GeneralInfo = _infoObj
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "RoomPeopleInfoNew":
                    {
                        var _newObj = new RoomPeopleModel()
                        {

                        };
                        var _newSelects = new RoomPeopleModel()
                        {
                            //AcctNos = await Common.GetMerchants()
                        };
                        return Json(new { Model = _newObj, Selects = _newSelects }, JsonRequestBehavior.AllowGet);
                    }
                case "RoomPeopleInfo":
                    {
                        var _infoObj = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                            ("roomPeoples/" + Helper.Decrypt(RoomPeopleIDHash));

                        if (_infoObj != null && _infoObj.Result != null)
                        {
                            _infoObj.Result.RoomPeopleIDHash = RoomPeopleIDHash;
                        }

                        var _infoSelects = new RoomPeopleModel()
                        {
                            //AcctNos = await Common.GetMerchants(),
                            //Mccs = await Common.GetRefLib(Enums.REF_TYPE_MCC),
                            //Sics = await Common.GetRefLib(Enums.REF_TYPE_SIC),
                            //OwnershipTypes = await Common.GetRefLib(Enums.REF_TYPE_MERCH_OWNERSHIP),
                            //DBAStates = await Common.GetState(null),
                            //StsNewReasons = await Common.GetRefLib(Enums.REF_TYPE_MERCH_STS_REASON),
                            //Stses = await Common.GetRefLib(Enums.REF_TYPE_MERCH_ACCT_STATUS),
                        };

                        return Json(new FormResponseModel<RoomPeopleModel, RoomPeopleModel>()
                        {
                            ResponseCode = _infoObj.ResponseCode,
                            ResponseDesc = _infoObj.ResponseDesc,
                            Model = _infoObj.Result,
                            Selects = _infoSelects,
                            GeneralInfo = _infoObj.Result
                        }, JsonRequestBehavior.AllowGet);
                    }

                case "RoomPeopleAddrs":
                    {
                        //var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                        //    ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomPeople = Helper.Decrypt(RoomPeopleIDHash) }));
                        //var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>("RoomPeoples/" + Helper.GetQueryString(new { RoomPeopleNumber = Helper.Decrypt(RoomPeopleIDHash) }));
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>("RoomPeoples/" + Helper.Decrypt(RoomPeopleIDHash));
                        //var _info = await ApiClient.GetJsonAsync<APIResponseListModel<RoomPeopleModel>>("RoomPeoples/GetList");
                        //var _info = await ApiClient.GetArrayJsonAsync<APIResponseModel<List<RoomPeopleModel>>>("RoomPeoples/GetList");

                        if (_info != null && _info.Result != null)
                        {
                            // _info.Result.RoomPeopleID = Helper.Encrypt(_info.Result.RoomPeopleID);
                            return Json(new FormResponseModel<RoomPeopleModel, RoomPeopleModel>()
                            {
                                ResponseCode = _info.ResponseCode,
                                ResponseDesc = _info.ResponseDesc,
                                GeneralInfo = _info.Result,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new FormResponseModel<RoomPeopleModel, RoomPeopleModel>()
                            {
                                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                case "RoomPeopleAddrNew":
                    {
                        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomPeople = Helper.Decrypt(RoomPeopleIDHash) }));

                        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            //_info.Result.RoomPeopleIDHash = Helper.Encrypt(_info.Result.RoomPeopleID);

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

                            return Json(new FormResponseModel<AddressModel, RoomPeopleModel>()
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
                            return Json(new FormResponseModel<AddressModel, RoomPeopleModel>()
                            {
                                ResponseCode = _info.ResponseCode,
                                ResponseDesc = _info.ResponseDesc
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                //case "RoomPeopleAddrD":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomPeople = Helper.Decrypt(RoomPeopleIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            //_info.Result.RoomPeopleIDHash = Helper.Encrypt(_info.Result.AcctNo);

                //            var addrObj = await ApiClient.GetJsonAsync<APIResponseModel<AddressModel>>
                //                ("common/Address?" + Helper.GetQueryString(new { Ids = RoomPeopleAddressId }));

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

                //                    return Json(new FormResponseModel<AddressModel, RoomPeopleModel>()
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

                //        return Json(new FormResponseModel<AddressModel, RoomPeopleModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "RoomPeopleConts":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomPeople = Helper.Decrypt(RoomPeopleIDHash) }));

                //        if (_info != null && _info.Result != null)
                //        {
                //            _info.Result.RoomPeopleIDHash = Helper.Encrypt(_info.Result.RoomPeople);
                //            return Json(new FormResponseModel<RoomPeopleModel, RoomPeopleModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //        else
                //        {
                //            return Json(new FormResponseModel<RoomPeopleModel, RoomPeopleModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "RoomPeopleContNew":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomPeople = Helper.Decrypt(RoomPeopleIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomPeopleIDHash = Helper.Encrypt(_info.Result.RoomPeople);
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

                //            return Json(new FormResponseModel<ContactModel, RoomPeopleModel>()
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
                //            return Json(new FormResponseModel<AddressModel, RoomPeopleModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "RoomPeopleContD":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomPeople = Helper.Decrypt(RoomPeopleIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomPeopleIDHash = Helper.Encrypt(_info.Result.RoomPeople);

                //            var _Obj = await ApiClient.GetJsonAsync<APIResponseModel<ContactModel>>
                //                ("common/ContactById?" + Helper.GetQueryString(new { Ids = RoomPeopleContactId }));

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

                //                    return Json(new FormResponseModel<ContactModel, RoomPeopleModel>()
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

                //        return Json(new FormResponseModel<AddressModel, RoomPeopleModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "RoomPeopleTerminals":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomPeople = Helper.Decrypt(RoomPeopleIDHash) }));

                //        if (_info != null && _info.Result != null)
                //        {
                //            _info.Result.RoomPeopleIDHash = Helper.Encrypt(_info.Result.RoomPeople);
                //            return Json(new FormResponseModel<RoomPeopleModel, RoomPeopleModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //        else
                //        {
                //            return Json(new FormResponseModel<RoomPeopleModel, RoomPeopleModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //                ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "RoomPeopleTerminalNew":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomPeople = Helper.Decrypt(RoomPeopleIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomPeopleIDHash = Helper.Encrypt(_info.Result.RoomPeople);
                //            var _Obj = new TerminalModel()
                //            {
                //                RoomPeopleIDHash = _info.Result.RoomPeopleIDHash
                //            };

                //            var _Selects = new TerminalModel()
                //            {
                //                DeviceTypeCds = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_DEVICE_TYPE),
                //                TermSrcs = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_SRC),
                //                Stses = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_STS)
                //            };

                //            return Json(new FormResponseModel<TerminalModel, RoomPeopleModel>()
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
                //            return Json(new FormResponseModel<TerminalModel, RoomPeopleModel>()
                //            {
                //                ResponseCode = _info.ResponseCode,
                //                ResponseDesc = _info.ResponseDesc
                //            }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //case "RoomPeopleTerminalInfo":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomPeople = Helper.Decrypt(RoomPeopleIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomPeopleIDHash = Helper.Encrypt(_info.Result.RoomPeople);

                //            var _Obj = await ApiClient.GetJsonAsync<APIResponseModel<TerminalModel>>
                //                ("merchant/TerminalInventory?" + Helper.GetQueryString(new { Ids = TerminalIds }));

                //            if (_Obj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //            {
                //                if (_Obj.Result != null && _info.Result != null && _Obj.Result.RoomPeople == _info.Result.RoomPeople) // valid merchant and address
                //                {
                //                    _Obj.Result.Ids = TerminalIds;

                //                    var _Selects = new TerminalModel()
                //                    {
                //                        DeviceTypeCds = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_DEVICE_TYPE),
                //                        TermSrcs = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_SRC),
                //                        Stses = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_STS),
                //                        StsNewReasons = await Common.GetRefLib(Enums.REF_TYPE_TERMINAL_STS_REASON)
                //                    };

                //                    return Json(new FormResponseModel<TerminalModel, RoomPeopleModel>()
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

                //        return Json(new FormResponseModel<TerminalModel, RoomPeopleModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "RoomPeopleTxnPoints":
                //    {
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomPeople = Helper.Decrypt(RoomPeopleIDHash) }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomPeopleIDHash = Helper.Encrypt(_info.Result.RoomPeople);

                //            var _Obj = new TxnPointSearchModel()
                //            {
                //                RoomPeopleIDHash = _info.Result.RoomPeopleIDHash,
                //                DateFrom = DateTime.Today,
                //                DateTo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59),
                //                TxnInd = Enums.TXN_UNPOSTED//.TXN_POSTED
                //            };

                //            var _Selects = new TxnPointSearchModel()
                //            {
                //                //RoomPeopleNos = await Common.GetBusinessLocations(),
                //            };

                //            return Json(new FormResponseModel<TxnPointSearchModel, RoomPeopleModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_VALID,
                //                Model = _Obj,
                //                Selects = _Selects,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }

                //        return Json(new FormResponseModel<TxnPointSearchModel, RoomPeopleModel>()
                //        {
                //            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                //            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //case "RoomPeopleCrdRangeAccpt":
                //    {
                //        var RoomPeopleNo = Helper.Decrypt(RoomPeopleIDHash);
                //        var _info = await ApiClient.GetJsonAsync<APIResponseModel<RoomPeopleModel>>
                //            ("merchant/BusinessLocation?" + Helper.GetQueryString(new { RoomPeople = RoomPeopleNo }));

                //        if (_info != null && _info.Result != null && _info.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //        {
                //            _info.Result.RoomPeopleIDHash = Helper.Encrypt(_info.Result.RoomPeople);

                //            var _params = new jQueryDataTableParamModel()
                //            {
                //                iDisplayStart = 0,
                //                iDisplayLength = int.MaxValue, // get all, no paging
                //                AllowPaging = false
                //            };

                //            var _cardRangeLst = await ApiClient.GetJsonAsync<APIResponseListModel<CardRangeAcceptanceModel>>
                //                ("merchant/CardRangeList?RoomPeopleNo=" + RoomPeopleNo + "&" + Helper.GetQueryString(_params));

                //            var _Obj = new CardRangeAcceptanceListModel();
                //            if (_cardRangeLst != null && _cardRangeLst.Result != null && _cardRangeLst.ResponseCode == Enums.RESPONSE_CODE_VALID)
                //            {
                //                _Obj.CardRangeAcceptanceLst = _cardRangeLst.Result.RecordList;
                //            }

                //            var _Selects = new CardRangeAcceptanceListModel()
                //            {
                //                CardRangeSelects = await Common.GetCardRanges()
                //            };

                //            return Json(new FormResponseModel<CardRangeAcceptanceListModel, RoomPeopleModel>()
                //            {
                //                ResponseCode = Enums.RESPONSE_CODE_VALID,
                //                Model = _Obj,
                //                Selects = _Selects,
                //                GeneralInfo = _info.Result
                //            }, JsonRequestBehavior.AllowGet);
                //        }

                //        return Json(new FormResponseModel<TxnPointSearchModel, RoomPeopleModel>()
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
        public async Task<ActionResult> RoomPeopleSearch(jQueryDataTableParamModel Params, RoomPeopleModel model)
        {
            var displyColumns = new string[] { "RoomPeopleID", "PeopleID", "RoomDetailID", "DateIn", "DateOut", "CreateDate", "Description" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<RoomPeopleModel>>
                ("roomPeoples/roomPeoplelist?" + Helper.GetQueryString(Params) + "&" + Helper.GetQueryString(model));

            if (response == null || response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<RoomPeopleModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.RoomPeopleID, x.PeopleID, x.RoomDetailID,
                    x.DateIn != null ? x.DateIn.Value.ToString(Enums.DATE_FORMAT) : string.Empty,
                    x.DateOut != null ? x.DateOut.Value.ToString(Enums.DATE_FORMAT) : string.Empty,
                    x.CreateDate != null ? x.CreateDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty,
                    x.Description, Helpers.Helper.Encrypt(x.RoomPeopleID) })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region New Business Location
        [HttpPost]
        public async Task<JsonResult> AddRoomPeople(RoomPeopleModel model)
        {
            //    model.AllCardRange = model.AllCardRange ?? false;
            //    model.AllTxnCodeMapping = model.AllTxnCodeMapping ?? false;

            // Call API to update 
            var rp = await ApiClient.PostJsonAsync<APIResponseModel<RoomPeopleModel>>
                ("RoomPeoples", JsonConvert.SerializeObject(model));

            if (rp.ResponseCode == Enums.RESPONSE_CODE_VALID && rp.Result != null)
            {
                rp.Result.RoomPeopleIDHash = Helper.Encrypt(rp.Result.RoomPeopleID);
            }

            return Json(rp, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Business Location Info
        [HttpPatch]
        public async Task<JsonResult> UpdateRoomPeople(RoomPeopleModel model)
        {
            //model.RoomPeopleID = Helper.Decrypt(model.RoomPeopleID);
            //var result = await ApiClient.PatchJsonAsync<APIResponseModel<RoomPeopleModel>>
            //  ("merchant/BusinessLocation", JsonConvert.SerializeObject(model));
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<RoomPeopleModel>>
                ("RoomPeoples", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get merchant account log list
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AccountLogList(jQueryDataTableParamModel Params, string RoomPeopleIDHash)
        {
            var response = await ApiClient.GetJsonAsync<APIResponseListModel<LogChangeModel>>
                ("merchant/BusinessLocationLogList?RoomPeople=" + Helper.Decrypt(RoomPeopleIDHash) + "&" + Helper.GetQueryString(Params));

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
        public async Task<JsonResult> RoomPeopleStatus(Models.Merchants.Account.MerchantAcctStatusModel model)
        {
            var result = await ApiClient.PostJsonAsync<APIResponseModel<Models.Merchants.Account.MerchantAcctStatusModel>>
                ("merchant/BusinessLocationStatus", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Terminal Management
        public async Task<ActionResult> TerminalList(jQueryDataTableParamModel Params, string RoomPeopleIDHash)
        {
            var displyColumns = new string[] { "TermId", "DeviceTypeName", "DeployDate", "StatusName", "CreatedDate", "CreatedByName" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<TerminalModel>>
                ("merchant/TerminalInventoryList?" + Helper.GetQueryString(Params) + "&RoomPeopleNo=" + Helper.Decrypt(RoomPeopleIDHash));

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
        //    model.RoomPeople = Helper.Decrypt(model.RoomPeopleIDHash);
        //    //model.RoomPeopleNo = model.RoomPeople;// will be removed soon
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
                //txnSearch.RoomPeople = Helper.Decrypt(txnSearch.RoomPeopleIDHash);
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
        //    model.RoomPeopleNo = Helper.Decrypt(model.RoomPeopleIDHash);
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