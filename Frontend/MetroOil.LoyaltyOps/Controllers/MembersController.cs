using System;

using MetroOil.LoyaltyOps.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MetroOil.LoyaltyOps.Models.Members;
using MetroOil.LoyaltyOps.Helpers;

namespace MetroOil.LoyaltyOps.Controllers
{
    //[Authorize(Roles = "Internal")]
    public class MembersController : Controller
    {
        // GET: Members
        //[Accessibility]
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
                case "MemSearch":
                    return PartialView("Partials/Search", new MemberSearchModel());
                case "MemNew":
                    return PartialView("Partials/New", new MemberModel());
                case "MemSumm":
                    return PartialView("Partials/Profile/Summary");
                case "MemInfo":
                    return PartialView("Partials/Profile/Info", new MemberModel());
                case "MemCont":
                    return PartialView("Partials/Profile/Contact", new ContactModel());
                case "MemCrds":
                    return PartialView("Partials/Cards/Cards");
                case "MemCrdD":
                    return PartialView("Partials/Cards/CardDetail", new CardModel());
                case "MemCrdInfo":
                    return PartialView("Partials/Cards/CardInfo", new CardModel());
                case "MemAddr":
                    return PartialView("Partials/Profile/Address");
                case "MemAddrD":
                    return PartialView("Partials/Profile/AddressDetail", new AddressModel());
                case "MemAddrNew":
                    return PartialView("Partials/Profile/AddressNew", new AddressModel());
                case "MemTxnPoints":
                    return PartialView("Partials/Transactions/Points", new TxnPointSearchModel());
                case "MemTxnVouchers":
                    return PartialView("Partials/Transactions/Vouchers", new TxnVoucherSearchModel());
                case "MemTxnVoucherDetail":
                    return PartialView("Partials/Transactions/VoucherInfo", new TxnVoucherModel());
                default:
                    return PartialView();
            }
        }
        #endregion

        public async Task<JsonResult> FillData(string Prefix, string AcctNoHash, string AddressID, string CardNo, string VoucherNo)
        {
            switch (Prefix)
            {
                case "MemSearch":
                    {
                        var _obj = new MemberSearchModel()
                        {
                            
                        };
                        var _selects = new MemberSearchModel()
                        {
                            Stses = await Common.GetRefLib(Enums.REF_TYPE_ACCT_STATUS),
                            States = await Common.GetState(null),
                            MMFroms = await Common.GetMonths(),
                            MMTos = await Common.GetMonths()
                        };
                        return Json(new FormResponseModel<MemberSearchModel, MemberSearchModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_VALID,
                            Model = _obj,
                            Selects = _selects
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MemNew":
                    {
                        var _newObj = new MemberModel()
                        {
                            HpCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE,
                            Gender = Enums.DEFAULT_GENDER,
                            TnC = true
                        };
                        var _newSelects = new MemberModel()
                        {
                            HpCtryCodes = await Common.GetRefLib(Enums.REF_TYPE_TEL_COUNTRY),
                            Genders = await Common.GetRefLib(Enums.REF_TYPE_GENDER),
                            Titles = await Common.GetRefLib(Enums.REF_TYPE_TITLE)
                        };
                        return Json(new FormResponseModel<MemberModel, MemberModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_VALID,
                            Model = _newObj,
                            Selects = _newSelects
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MemInfo":
                    {
                        var _infoObj = await ApiClient.GetJsonAsync<APIResponseModel<MemberModel>>
                            ("member/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));

                        if (_infoObj!=null && _infoObj.Result != null)
                        {
                            _infoObj.Result.AcctNoHash = Helper.Encrypt(_infoObj.Result.AcctNo);
                        }

                        var _infoSelects = new MemberModel()
                        {
                            Titles = await Common.GetRefLib(Enums.REF_TYPE_TITLE),
                            //Countries = await Common.GetRefLib(Enums.REF_TYPE_COUNTRY),
                            //MemberTiers = await Common.GetRefLib(Enums.REF_TYPE_MEMBERTIER),
                            Genders = await Common.GetRefLib(Enums.REF_TYPE_GENDER),
                            AccountStatuses = await Common.GetRefLib(Enums.REF_TYPE_ACCT_STATUS),
                            //HpCtryCodes = await Common.GetRefLib(Enums.REF_TYPE_TEL_COUNTRY),
                            DriverTypes = await Common.GetRefLib(Enums.REF_TYPE_DRIVER_TYPE),
                            VehicleTypes = await Common.GetRefLib(Enums.REF_TYPE_VEHICLE_TYPE),
                            CompanyTypes = await Common.GetRefLib(Enums.REF_TYPE_COMPANY_TYPE),
                            MaritalStses = await Common.GetRefLib(Enums.REF_TYPE_MARITAL_STS),
                            EmploymentStses = await Common.GetRefLib(Enums.REF_TYPE_EMPLOYMENT_STS),
                            StsNewReasons = await Common.GetRefLib(Enums.REF_TYPE_MEMBER_STS_REASON),
                        };
                        return Json(new FormResponseModel<MemberModel, MemberModel>()
                        {
                            ResponseCode = _infoObj.ResponseCode,
                            ResponseDesc = _infoObj.ResponseDesc,
                            Model = _infoObj.Result,
                            Selects = _infoSelects,
                            GeneralInfo = _infoObj.Result
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MemAddr":
                    {
                        var memberInfo = await ApiClient.GetJsonAsync<APIResponseModel<MemberModel>>
                                ("member/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));

                        if (memberInfo != null && memberInfo.Result != null)
                        {
                            memberInfo.Result.AcctNoHash = Helper.Encrypt(memberInfo.Result.AcctNo);
                        }

                        return Json(new FormResponseModel<MemberModel, MemberModel>()
                        {
                            ResponseCode = memberInfo.ResponseCode,
                            ResponseDesc = memberInfo.ResponseDesc,
                            GeneralInfo = memberInfo.Result
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MemAddrNew":
                    {
                        var memberInfo = await ApiClient.GetJsonAsync<APIResponseModel<MemberModel>>
                                ("member/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));

                        if (memberInfo != null && memberInfo.Result != null && memberInfo.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            memberInfo.Result.AcctNoHash = Helper.Encrypt(memberInfo.Result.AcctNo);

                            var _newAddress = new AddressModel() {
                                //CtryCd = Enums.DEFAULT_COUNTRY_CODE
                            };
                            var _newAddressSelects = new AddressModel()
                            {
                                Countries = await Common.GetRefLib(Enums.REF_TYPE_COUNTRY),
                                AddressTypes = await Common.GetRefLib(Enums.REF_TYPE_ADDRESS, Enums.REF_NO_ADDRESS),
                                States = await Common.GetState(Enums.DEFAULT_COUNTRY_CODE)
                            };

                            return Json(new FormResponseModel<AddressModel, MemberModel>()
                            {
                                ResponseCode = Enums.RESPONSE_CODE_VALID,
                                ResponseDesc = "",
                                Model = _newAddress,
                                Selects = _newAddressSelects,
                                GeneralInfo = memberInfo.Result
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new FormResponseModel<AddressModel, MemberModel>()
                            {
                                ResponseCode = memberInfo.ResponseCode,
                                ResponseDesc = memberInfo.ResponseDesc
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                case "MemAddrD":
                    {
                        var memberInfo = await ApiClient.GetJsonAsync<APIResponseModel<MemberModel>>
                            ("member/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));
                        
                        if (memberInfo != null && memberInfo.Result != null && memberInfo.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            memberInfo.Result.AcctNoHash = Helper.Encrypt(memberInfo.Result.AcctNo);

                            var addrObj = await ApiClient.GetJsonAsync<APIResponseModel<AddressModel>>
                                ("common/Address?" + Helper.GetQueryString(new { Ids = AddressID }));

                            if (addrObj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                            {
                                if(addrObj.Result != null && memberInfo.Result != null && addrObj.Result.EntityId == memberInfo.Result.EntityId) // valid member and address
                                {
                                    var addrSelects = new AddressModel()
                                    {
                                        Countries = await Common.GetRefLib(Enums.REF_TYPE_COUNTRY),
                                        AddressTypes = await Common.GetRefLib(Enums.REF_TYPE_ADDRESS, Enums.REF_NO_ADDRESS),
                                        States = await Common.GetState(addrObj.Result.CtryCd)
                                    };

                                    return Json(new FormResponseModel<AddressModel, MemberModel>()
                                    {
                                        ResponseCode = addrObj.ResponseCode,
                                        ResponseDesc = addrObj.ResponseDesc,
                                        Model = addrObj.Result,
                                        Selects = addrSelects,
                                        GeneralInfo = memberInfo.Result
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        return Json(new FormResponseModel<AddressModel, MemberModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MemCrds":
                    {
                        var memberInfo = await ApiClient.GetJsonAsync<APIResponseModel<MemberModel>>
                                ("member/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));

                        if (memberInfo != null && memberInfo.Result != null)
                        {
                            memberInfo.Result.AcctNoHash = Helper.Encrypt(memberInfo.Result.AcctNo);
                        }

                        return Json(new FormResponseModel<CardModel, MemberModel>()
                        {
                            ResponseCode = memberInfo.ResponseCode,
                            ResponseDesc = memberInfo.ResponseDesc,
                            GeneralInfo = memberInfo.Result
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MemCrdInfo":
                    {
                        var memberInfo = await ApiClient.GetJsonAsync<APIResponseModel<MemberModel>>
                            ("member/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));

                        if (memberInfo != null && memberInfo.Result != null && memberInfo.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            memberInfo.Result.AcctNoHash = Helper.Encrypt(memberInfo.Result.AcctNo);

                            var _obj = await ApiClient.GetJsonAsync<APIResponseModel<CardModel>>
                                ("MemberCard/GetMemberCardCurrentActiveCardSelect?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));

                            if (_obj != null && _obj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                            {
                                if (_obj.Result != null && memberInfo.Result != null && _obj.Result.AcctNo == memberInfo.Result.AcctNo) // valid member and card
                                {
                                    var _selects = new CardModel()
                                    {
                                        CardTypes = await Common.GetCardType(),
                                    };

                                    return Json(new FormResponseModel<CardModel, MemberModel>()
                                    {
                                        ResponseCode = _obj.ResponseCode,
                                        ResponseDesc = _obj.ResponseDesc,
                                        Model = _obj.Result,
                                        Selects = _selects,
                                        GeneralInfo = memberInfo.Result
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        return Json(new FormResponseModel<AddressModel, MemberModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MemCrdD":
                    {
                        var memberInfo = await ApiClient.GetJsonAsync<APIResponseModel<MemberModel>>
                            ("member/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));

                        if (memberInfo != null && memberInfo.Result != null && memberInfo.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            memberInfo.Result.AcctNoHash = Helper.Encrypt(memberInfo.Result.AcctNo);

                            var _obj = await ApiClient.GetJsonAsync<APIResponseModel<CardModel>>
                                ("MemberCard/GetMemberCardBySelect?" + Helper.GetQueryString(new { CardNo }));

                            if (_obj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                            {
                                if (_obj.Result != null && memberInfo.Result != null && _obj.Result.AcctNo == memberInfo.Result.AcctNo) // valid member and card
                                {
                                    var _selects = new CardModel()
                                    {
                                        CardTypes = await Common.GetCardType(),
                                    };

                                    return Json(new FormResponseModel<CardModel, MemberModel>()
                                    {
                                        ResponseCode = _obj.ResponseCode,
                                        ResponseDesc = _obj.ResponseDesc,
                                        Model = _obj.Result,
                                        Selects = _selects,
                                        GeneralInfo = memberInfo.Result
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        return Json(new FormResponseModel<AddressModel, MemberModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MemCont":
                    {
                        var memberInfo = await ApiClient.GetJsonAsync<APIResponseModel<MemberModel>>
                            ("member/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));

                        if(memberInfo != null && memberInfo.Result != null && memberInfo.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            memberInfo.Result.AcctNoHash = Helper.Encrypt(memberInfo.Result.AcctNo);

                            var _Obj = await ApiClient.GetJsonAsync<APIResponseModel<ContactModel>>
                            ("common/ContactByEntityId?" + Helper.GetQueryString(new { memberInfo.Result.EntityId }));

                            if(_Obj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                            {
                                if(_Obj.Result != null && _Obj.Result.EntityId == memberInfo.Result.EntityId)
                                {
                                    var _Selects = new ContactModel();
                                    if (_Obj != null && _Obj.Result != null)
                                    {
                                        var telCountries = await Common.GetRefLib(Enums.REF_TYPE_TEL_COUNTRY, null, null, null, true);
                                        _Selects = new ContactModel()
                                        {
                                            //Titles = await Common.GetRefLib(Enums.REF_TYPE_TITLE),
                                            HpCtryCodes = telCountries,
                                            WorkTelCtryCodes = telCountries,
                                            HomeTelCtryCodes = telCountries,
                                            FaxCtryCodes = telCountries,
                                            HpNewHpCtryCodes = telCountries
                                        };
                                        // Set object
                                        _Obj.Result.AcctNoHash = memberInfo.Result.AcctNoHash;
                                        // Set default country
                                        _Obj.Result.HpCtryCode = _Obj.Result.HpCtryCode ?? Enums.DEFAULT_TEL_COUNTRY_CODE;
                                        _Obj.Result.WorkTelCtryCode = _Obj.Result.WorkTelCtryCode ?? Enums.DEFAULT_TEL_COUNTRY_CODE;
                                        _Obj.Result.FaxCtryCode = _Obj.Result.FaxCtryCode ?? Enums.DEFAULT_TEL_COUNTRY_CODE;
                                        _Obj.Result.HpNewHpCtryCode = _Obj.Result.HpNewHpCtryCode ?? Enums.DEFAULT_TEL_COUNTRY_CODE;
                                    }
                                    return Json(new FormResponseModel<ContactModel, MemberModel>()
                                    {
                                        ResponseCode = _Obj.ResponseCode,
                                        ResponseDesc = _Obj.ResponseDesc,
                                        Model = _Obj.Result,
                                        Selects = _Selects,
                                        GeneralInfo = memberInfo.Result
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        return Json(new FormResponseModel<AddressModel, MemberModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MemTxnPoints":
                    {
                        var memberInfo = await ApiClient.GetJsonAsync<APIResponseModel<MemberModel>>
                            ("member/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));

                        if (memberInfo != null && memberInfo.Result != null && memberInfo.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            memberInfo.Result.AcctNoHash = Helper.Encrypt(memberInfo.Result.AcctNo);

                            var _Obj = new TxnPointSearchModel()
                            {
                                AcctNoHash = memberInfo.Result.AcctNoHash,
                                DateFrom = DateTime.Today,
                                DateTo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59),
                                TxnInd = Enums.TXN_UNPOSTED//.TXN_POSTED
                            };

                            var _Selects = new TxnPointSearchModel()
                            {
                                BusnLocationNos = await Common.GetBusinessLocations(),
                            };
                            
                            return Json(new FormResponseModel<TxnPointSearchModel, MemberModel>()
                            {
                                ResponseCode = Enums.RESPONSE_CODE_VALID,
                                Model = _Obj,
                                Selects = _Selects,
                                GeneralInfo = memberInfo.Result
                            }, JsonRequestBehavior.AllowGet);
                        }

                        return Json(new FormResponseModel<TxnPointSearchModel, MemberModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MemTxnVouchers":
                    {
                        var memberInfo = await ApiClient.GetJsonAsync<APIResponseModel<MemberModel>>
                            ("member/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));

                        if (memberInfo != null && memberInfo.Result != null && memberInfo.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            memberInfo.Result.AcctNoHash = Helper.Encrypt(memberInfo.Result.AcctNo);

                            var _Obj = new TxnVoucherSearchModel()
                            {
                                AcctNoHash = memberInfo.Result.AcctNoHash,
                            };

                            var _Selects = new TxnVoucherSearchModel()
                            {
                                Stses = await Common.GetRefLib(Enums.REF_TYPE_VOUCHER_STS),
                                //ProdTypes = await Common.GetProductTypes()
                                //VoucherSourceCds = await Common.GetRefLib(Enums.REF_TYPE_VOUCHER_SOURCE),
                            };

                            return Json(new FormResponseModel<TxnVoucherSearchModel, MemberModel>()
                            {
                                ResponseCode = Enums.RESPONSE_CODE_VALID,
                                Model = _Obj,
                                Selects = _Selects,
                                GeneralInfo = memberInfo.Result
                            }, JsonRequestBehavior.AllowGet);
                        }

                        return Json(new FormResponseModel<TxnVoucherSearchModel, MemberModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "MemTxnVoucherDetail":
                    {
                        var memberInfo = await ApiClient.GetJsonAsync<APIResponseModel<MemberModel>>
                            ("member/Account?" + Helper.GetQueryString(new { AcctNo = Helper.Decrypt(AcctNoHash) }));

                        if (memberInfo != null && memberInfo.Result != null && memberInfo.ResponseCode == Enums.RESPONSE_CODE_VALID)
                        {
                            memberInfo.Result.AcctNoHash = Helper.Encrypt(memberInfo.Result.AcctNo);

                            var _obj = await ApiClient.GetJsonAsync<APIResponseModel<TxnVoucherModel>>
                                ("member/GetVoucherDetail?" + Helper.GetQueryString(new { VoucherNo }));

                            if (_obj.ResponseCode == Enums.RESPONSE_CODE_VALID)
                            {
                                if (_obj.Result != null && memberInfo.Result != null && _obj.Result.AcctNo == memberInfo.Result.AcctNo) // valid member and card
                                {
                                    var _selects = new TxnVoucherModel()
                                    {
                                        Stses = await Common.GetRefLib(Enums.REF_TYPE_VOUCHER_STS),
                                        RedeemedBusnLocations = await Common.GetBusinessLocations(),
                                        RedeemedBys = await Common.GetUserList(Enums.USER_TYPE_MERCHANT)
                                    };

                                    return Json(new FormResponseModel<TxnVoucherModel, MemberModel>()
                                    {
                                        ResponseCode = _obj.ResponseCode,
                                        ResponseDesc = _obj.ResponseDesc,
                                        Model = _obj.Result,
                                        Selects = _selects,
                                        GeneralInfo = memberInfo.Result
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        return Json(new FormResponseModel<TxnVoucherModel, MemberModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                        }, JsonRequestBehavior.AllowGet);
                    }
                default:
                    return Json(new { });
            }
        }

        #region Account List
        public async Task<ActionResult> AccountList(jQueryDataTableParamModel Params, MemberSearchModel model)
        {
            var displyColumns = new string[] { "AcctNo", "FullName", "TitleName", "NewIc", "StsName", "JoinDate" };
            if(Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            if (model != null)
            {
                if(model.JoinDateFrom != null) {
                    model.JoinDateFrom = new DateTime(model.JoinDateFrom.Value.Year, model.JoinDateFrom.Value.Month, model.JoinDateFrom.Value.Day);
                }
                if (model.JoinDateTo != null)
                {
                    model.JoinDateTo = new DateTime(model.JoinDateTo.Value.Year, model.JoinDateTo.Value.Month, model.JoinDateTo.Value.Day, 23, 59, 59);
                }
                if (model.DOBDateFrom != null)
                {
                    model.DOBDateFrom = new DateTime(model.DOBDateFrom.Value.Year, model.DOBDateFrom.Value.Month, model.DOBDateFrom.Value.Day);
                }
                if (model.DOBDateTo != null)
                {
                    model.DOBDateTo = new DateTime(model.DOBDateTo.Value.Year, model.DOBDateTo.Value.Month, model.DOBDateTo.Value.Day, 23, 59, 59);
                }
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<MemberModel>>
                ("member/AccountList?" + Helper.GetQueryString(Params) + "&" + Helper.GetQueryString(model));

            if (response == null || response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response = new APIResponseListModel<MemberModel>();
                response.Result.RecordList = new List<MemberModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.AcctNo, x.FullName, x.TitleName, x.NewIc, x.StsName, x.JoinDate.Value.ToString(Enums.DATE_FORMAT), Helpers.Helper.Encrypt(x.AcctNo) })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region New Member
        [HttpPost]
        public async Task<JsonResult> CreateMember(MemberModel model)
        {
            model.InputSourceInd = Enums.INPUT_SOURCEIIND_MBR;
            model.VerifyHpNo = false; // The phone no. is not verified in the first creating.
            model.LoginID = model.HpCtryCode + model.HpNo;

            var rp = await ApiClient.PostJsonAsync<APIResponseModel<MemberModel>>
                ("member/ClassicLogin", JsonConvert.SerializeObject(model));

            if (rp.ResponseCode == Enums.RESPONSE_CODE_VALID && rp.Result != null)
            {
                rp.Result.AcctNoHash = Helper.Encrypt(rp.Result.AcctNo);
            }

            return Json(rp, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Member Info
        [HttpPatch]
        public async Task<JsonResult> Account(MemberModel model)
        {
            model.AcctNo = Helper.Decrypt(model.AcctNoHash);
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<MemberModel>>
                ("member/Account", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> AccountLogList(jQueryDataTableParamModel Params, string AcctNoHash)
        {
            var response = await ApiClient.GetJsonAsync<APIResponseListModel<LogChangeModel>>
                ("member/AccountLogList?AcctNo=" + Helper.Decrypt(AcctNoHash) + "&" + Helper.GetQueryString(Params));

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

        [HttpPatch]
        public async Task<JsonResult> AccountStatus(MemberStatusModel model)
        {
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<MemberModel>>
                ("member/AccountStatus", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Card
        [HttpGet]
        public async Task<ActionResult> Cards(jQueryDataTableParamModel Params, string AcctNoHash)
        {
            var displyColumns = new string[] { "CardNo", "CardLogoName", "PlasticTypeName", "EmbName", "StartDate", "ExpiryDate", "CardStsName", "CardTypeName", "Remark", "CreatedDate", "CreatedByName" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<CardModel>>
                ("MemberCard/MemberCardList?AcctNo=" + Helper.Decrypt(AcctNoHash) + "&" + Helper.GetQueryString(Params));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<CardModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { x.CardNo, x.CardLogoName, x.PlasticTypeName, x.EmbName, x.StartDate != null ? x.StartDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty, x.ExpiryDate != null ? x.ExpiryDate.Value.ToString(Enums.DATE_FORMAT) : string.Empty, x.CardStsName, x.CardTypeName, x.Remark, x.CreatedDate.ToString(Enums.DATE_FORMAT), x.CreatedByName })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateCard(CardModel model)
        {
            model.AcctNo = Helper.Decrypt(model.AcctNoHash);

            var result = await ApiClient.PostJsonAsync<APIResponseModel<CardModel>>
                ("MemberCard/MemberCardCreate", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Transaction
        [HttpGet]
        public async Task<ActionResult> GetMemberTxnByAcctNo(jQueryDataTableParamModel Params, TxnPointSearchModel txnSearch)
        {
            var displyColumns = new string[] { "TxnNo",
                    "TxnCategoryName",
                    "TxnDate",
                    "TxnAmount",
                    "TxnPoints",
                    "PrcsDate",
                    "BusnLocationNo",
                    "TermId",
                    "RefNo",
                    "CardNo" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            if (txnSearch != null)
            {
                txnSearch.AcctNo = Helper.Decrypt(txnSearch.AcctNoHash);
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<TxnPointModel>>
                ("member/GetMemberTxnByAcctNo?" + Helper.GetQueryString(txnSearch) + "&" + Helper.GetQueryString(Params));

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
                    x.PrcsDate != null ? x.PrcsDate.Value.ToString(Enums.DATE_TIME_FORMAT) : string.Empty,
                    x.BusnLocationNo,
                    x.TermId,
                    x.RefNo,
                    x.CardNo
                })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetMemberTxnByTxnId(string TxnId, string TxnInd)
        {
            var _infoObj = await ApiClient.GetJsonAsync<APIResponseModel<TxnPointModel>>
                            ("Member/GetMemberTxnDetailByTxnId?" + Helper.GetQueryString(new { TxnId, TxnInd }));

            return Json(_infoObj, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetMemberTxnProductsByTxnId(jQueryDataTableParamModel Params, string TxnId, string TxnInd)
        {
            var displyColumns = new string[] { "ProductName",
                    "ProductTypeName",
                    "Qty",
                    "Amt",
                    "Points" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<ProductModel>>
                ("member/GetMemberTxnProductDetailByAcctNo?TxnId=" + TxnId + "&TxnInd=" + TxnInd + "&" + Helper.GetQueryString(Params));

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

        #region Txn Vouchers
        [HttpGet]
        public async Task<ActionResult> GetMemberVouchersByAcctNo(jQueryDataTableParamModel Params, TxnVoucherSearchModel txnSearch)
        {
            var displyColumns = new string[] { "VoucherNo",
                    "RefNo",
                    "VoucherSourceName",
                    "ItemName",
                    "ItemDescp",
                    "StartDate",
                    "EndDate",
                    "RedeemedDate",
                    "StatusName" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            if (txnSearch != null)
            {
                txnSearch.AcctNo = Helper.Decrypt(txnSearch.AcctNoHash);

                //if(txnSearch.VoucherStartDateTo != null)
                //{
                //    txnSearch.VoucherStartDateTo = txnSearch.VoucherStartDateTo.Value.AddDays(1).AddSeconds(-1);
                //}

                //if(txnSearch.VoucherExpiredDateTo != null)
                //{
                //    txnSearch.VoucherExpiredDateTo = txnSearch.VoucherExpiredDateTo.Value.AddDays(1).AddSeconds(-1);
                //}

                //if (txnSearch.RedeemedDateTo != null)
                //{
                //    txnSearch.RedeemedDateTo = txnSearch.RedeemedDateTo.Value.AddDays(1).AddSeconds(-1);
                //}
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<TxnVoucherModel>>
                ("member/GetMemberVouchersByAcctNo?" + Helper.GetQueryString(txnSearch) + "&" + Helper.GetQueryString(Params));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<TxnVoucherModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1,
                    x.VoucherNo,
                    x.RefNo,
                    x.VoucherSourceName,
                    x.ItemName,
                    x.ItemDescp,
                    x.StartDate != null ? x.StartDate.Value.ToString(Enums.DATE_TIME_FORMAT) : string.Empty,
                    x.EndDate != null ? x.EndDate.Value.ToString(Enums.DATE_TIME_FORMAT) : string.Empty,
                    x.RedeemedDate != null ? x.RedeemedDate.Value.ToString(Enums.DATE_TIME_FORMAT) : string.Empty,
                    x.StatusName
                })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPatch]
        public async Task<JsonResult> MemberVoucher(TxnVoucherStatusModel model)
        {
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<TxnVoucherModel>>
                ("member/MemberVoucher", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetMemberVoucherStatusLogs(jQueryDataTableParamModel Params, string VoucherNo)
        {
            var response = await ApiClient.GetJsonAsync<APIResponseListModel<TxnVoucherStsLogModel>>
                ("member/GetMemberVoucherStatusLogs?VoucherNo=" + VoucherNo + "&" + Helper.GetQueryString(Params));

            if (response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response.Result.RecordList = new List<TxnVoucherStsLogModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.StatusName, x.Remark, x.CreatedDateStr, x.CreatedByName })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}