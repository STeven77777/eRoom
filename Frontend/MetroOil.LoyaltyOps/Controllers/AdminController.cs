
using MetroOil.LoyaltyOps.Models;
using System.Collections.Generic;
using MetroOil.LoyaltyOps.Models.Admin;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MetroOil.LoyaltyOps.Helpers;
using System.Linq;
using System.Text.RegularExpressions;

namespace MetroOil.LoyaltyOps.Controllers
{
    //[Authorize(Roles = "Internal")]
    public class AdminController : Controller
    {
        // GET: Admin
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
                case "AdSysUsers":
                    return PartialView("Partials/SysUsers/List");
                case "AdSysUserNew":
                    return PartialView("Partials/SysUsers/New", new SysUserModel());
                case "AdSysUserInfo":
                    return PartialView("Partials/SysUsers/Info", new SysUserModel());

                case "AdUserRoleAccNew":
                    return PartialView("Partials/UserMatrix/NewUserRoleAccess", new UserRoleModel());
                case "AdUserRoleAccLst":
                    return PartialView("Partials/UserMatrix/UserAccessInfo");
                case "AdUserRoleAccInfo":
                    return PartialView("Partials/UserMatrix/UserRoleAccessInfo",new UserRoleModel());

                case "AdRwdItms":
                    return PartialView("Partials/RewardItems/List");
                case "AdRwdItmNew":
                    return PartialView("Partials/RewardItems/New", new RewardItemModel());
                case "AdRwdItmInfo":
                    return PartialView("Partials/RewardItems/Info", new RewardItemModel());

                case "AdQRCodeCards":
                    return PartialView("Partials/QRCodeCards/List");
                default:
                    return PartialView();
            }
        }
        #endregion
       
        public async Task<JsonResult> FillData(string Prefix, string UserIdHash, string ProdCd)
        {
            switch (Prefix)
            {
                case "AdSysUserNew":
                    {
                        var _newObj = new SysUserModel()
                        {
                            HpCtryCode = Enums.DEFAULT_TEL_COUNTRY_CODE
                        };
                        var _newSelects = new SysUserModel()
                        {
                            Titles = await Common.GetRefLib(Enums.REF_TYPE_TITLE),
                            HpCtryCodes = await Common.GetRefLib(Enums.REF_TYPE_TEL_COUNTRY, null, null, null, true),
                            UserGroups = await Common.GetUserGroup()
                        };
                        return Json(new { Model = _newObj, Selects = _newSelects }, JsonRequestBehavior.AllowGet);
                    }
                case "AdSysUserInfo":
                    {
                        var _infoObj = await ApiClient.GetJsonAsync<APIResponseModel<SysUserModel>>
                            ("ops/User?" + Helper.GetQueryString(new { UserId = Helper.Decrypt(UserIdHash) }));

                        if (_infoObj != null && _infoObj.Result != null)
                        {
                            _infoObj.Result.UserIdHash = Helper.Encrypt(_infoObj.Result.UserId);
                        }

                        var _infoSelects = new SysUserModel()
                        {
                            UserGroups = await Common.GetUserGroup(),
                            Titles = await Common.GetRefLib(Enums.REF_TYPE_TITLE),
                            Stses = await Common.GetRefLib(Enums.REF_TYPE_USER_STS),
                            HpCtryCodes = await Common.GetRefLib(Enums.REF_TYPE_TEL_COUNTRY, null, null, null, true)
                        };

                        return Json(new FormResponseModel<SysUserModel, SysUserModel>()
                        {
                            ResponseCode = _infoObj.ResponseCode,
                            ResponseDesc = _infoObj.ResponseDesc,
                            Model = _infoObj.Result,
                            Selects = _infoSelects,
                            GeneralInfo = _infoObj.Result
                        }, JsonRequestBehavior.AllowGet);
                    }
                case "AdUserRoleAccInfo":
                    {
                        var userGroup = await ApiClient.GetJsonAsync<APIResponseModel<UserAccessManagementModel>>
                          ("UserMatrix/GetUserGroupInfo?" + Helper.GetQueryString(new { userGroup = UserIdHash }));
                        // call to get user matrix group
                        var userMatrix = await ApiClient.GetArrayJsonAsync<APIResponseModel<List<UserMatrixPage>>>
                         ("UserMatrix/GetUserMatrix?" + Helper.GetQueryString(new { userGroup = UserIdHash }));


                        UserRoleModel Select = new UserRoleModel();
                        List<SelectListItem> items = new List<SelectListItem>();
                        items.Add(new SelectListItem { Text = "Active", Value = "A" });
                        items.Add(new SelectListItem { Text = "Inactive", Value = "I" });
                        Select.StatusLitst = items;
                        UserRoleModel userRole = new UserRoleModel();
                        

                        if(userGroup.Result != null)
                        {
                            userRole.UserRoleNo = userGroup.Result.UserGroupCd;
                            userRole.UserRoleName = userGroup.Result.UserGroupName;
                            userRole.Sts = userGroup.Result.Sts;
                            userRole.CreatedOn = userGroup.Result.CreatedDate;
                            userRole.CreatedBy = userGroup.Result.CreatedBy;
                        }
                        return Json(new
                        {
                            Model = userRole,
                            Selects = Select,
                            UserMatrix = userMatrix.Result,
                            ResponseCode = userMatrix.ResponseCode,
                            ResponseDesc = userMatrix.ResponseDesc
                        }, JsonRequestBehavior.AllowGet);

                    }
                case "AdUserRoleAccNew":
                    {
                        UserRoleModel userRole = new UserRoleModel();
                        var response = await ApiClient.GetArrayJsonAsync<APIResponseModel<List<UserMatrixPage>>>("UserMatrix/GetUserMatrix?userGroup=");
                        foreach (var item in response.Result)
                        {
                            // Member Root
                            RootTreeObject rootTree = new RootTreeObject();
                            rootTree.title = item.Descp;
                            rootTree.select = item.Status;
                            rootTree.expand = true;
                            rootTree.key = item.Descp;

                            if (item.SubPages.Count() > 0)
                            {
                                foreach (var subChild in item.SubPages)
                                {
                                    //Member Search
                                    ChildTree childTree = new ChildTree();
                                    childTree.select = subChild.Status;
                                    childTree.title = subChild.Descp;
                                    rootTree.children.Add(childTree);
                                    if (childTree.children.Count() > 0)
                                    {
                                        foreach (var item2 in childTree.children)
                                        {
                                            Child2Tree child2 = new Child2Tree();
                                            child2.title = item2.title;
                                        }
                                    }

                                }
                            }
                        }
                        return Json(new { Model = userRole, UserMatrix = response.Result }, JsonRequestBehavior.AllowGet);
                    }
                case "AdRwdItmNew":
                    {
                        var _newObj = new RewardItemModel()
                        {
                            VoucherValidPeriodTypeCd = Enums.VOUCHER_PERIOD_TYPE_NO_OF_DAYS,
                            VoucherInd = false
                        };
                        var _newSelects = new RewardItemModel()
                        {
                            ProdClassCds = await Common.GetProductClasses(Enums.TXN_IND_REDEMPTION, false),// Item Class
                            //ProdTypeNames = await Common.GetProductTypes(),// Item Category, will get base on item class at runtime
                            Stses = await Common.GetRefLib(Enums.REF_TYPE_REWARD_STS),
                            VoucherSourceCds = await Common.GetRefLib(Enums.REF_TYPE_VOUCHER_SOURCE),
                            VoucherAllocateTypeCds = await Common.GetRefLib(Enums.REF_TYPE_VOUCHER_ALLOCATE_TYPE),
                            VoucherValidPeriodTypeCds = await Common.GetRefLib(Enums.REF_TYPE_VOUCHER_PERIOD_TYPE),
                            ExtPartnerCds = await Common.GetExternalPartner()
                        };
                        return Json(new { Model = _newObj, Selects = _newSelects }, JsonRequestBehavior.AllowGet);
                    }
                case "AdRwdItmInfo":
                    {
                        var _infoObj = await ApiClient.GetJsonAsync<APIResponseModel<RewardItemModel>>
                            ("Reward/Select?" + Helper.GetQueryString(new { ProdCd = ProdCd }));

                        if (_infoObj != null && _infoObj.ResponseCode == Enums.RESPONSE_CODE_VALID && _infoObj.Result != null)
                        {
                            _infoObj.Result.VoucherValidPeriodTypeCd = _infoObj.Result.VoucherValidPeriodTypeCd ?? Enums.VOUCHER_PERIOD_TYPE_NO_OF_DAYS;
                            var _infoSelects = new RewardItemModel()
                            {
                                ProdClassCds = await Common.GetProductClasses(Enums.TXN_IND_REDEMPTION, false),// Item Class
                                ProdTypes = _infoObj.Result != null ? await Common.GetProductTypes(_infoObj.Result.ProdClassCd, false) : new List<SelectListItem>(),// Item Category
                                Stses = await Common.GetRefLib(Enums.REF_TYPE_REWARD_STS),
                                VoucherSourceCds = await Common.GetRefLib(Enums.REF_TYPE_VOUCHER_SOURCE),
                                VoucherAllocateTypeCds = await Common.GetRefLib(Enums.REF_TYPE_VOUCHER_ALLOCATE_TYPE),
                                VoucherValidPeriodTypeCds = await Common.GetRefLib(Enums.REF_TYPE_VOUCHER_PERIOD_TYPE),
                                ExtPartnerCds = await Common.GetExternalPartner()
                            };

                            return Json(new FormResponseModel<RewardItemModel, RewardItemModel>()
                            {
                                ResponseCode = _infoObj.ResponseCode,
                                ResponseDesc = _infoObj.ResponseDesc,
                                Model = _infoObj.Result,
                                Selects = _infoSelects,
                                GeneralInfo = _infoObj.Result
                            }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new FormResponseModel<RewardItemModel, RewardItemModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_INVALID,
                            ResponseDesc = Enums.RESPONSE_MESSAGE_INVALID
                        }, JsonRequestBehavior.AllowGet);
                    }
                default:
                    return Json(new { });
            }
        }

        #region System User (Ops User)
        [HttpPost]
        public async Task<JsonResult> SysUserNew(SysUserModel model)
        {
            var rp = await ApiClient.PostJsonAsync<APIResponseModel<SysUserModel>>
                ("ops/User", JsonConvert.SerializeObject(model));

            if (rp.ResponseCode == Enums.RESPONSE_CODE_VALID && rp.Result != null)
            {
                rp.Result.UserIdHash = Helper.Encrypt(rp.Result.UserId);
            }

            return Json(rp, JsonRequestBehavior.AllowGet);
        }

        [HttpPatch]
        public async Task<JsonResult> UpdateSysUser(SysUserModel model)
        {
            model.UserId = Helper.Decrypt(model.UserIdHash);
            model.IsLockedOut = model.IsLockedOut ?? false;

            var result = await ApiClient.PatchJsonAsync<APIResponseModel<SysUserModel>>
                ("ops/User", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SysUserPassword(string UserIdHash)
        {
            var result = await ApiClient.PostJsonAsync<APIResponseModel<SysUserModel>>
                ("ops/UserPassword", JsonConvert.SerializeObject(new { UserId = Helper.Decrypt(UserIdHash) }));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SysUserList(jQueryDataTableParamModel Params)
        {
            var displyColumns = new string[] { "FullName", "TitleName", "FullHpNo", "EmailAddress", "UserGroupName", "StsName", "CreatedDate", "CreatedByName" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<SysUserModel>>
                ("ops/UserList?" + Helper.GetQueryString(Params));

            if (response == null || response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response = new APIResponseListModel<SysUserModel>();
                response.Result.RecordList = new List<SysUserModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.FullName, x.TitleName, x.FullHpNo, x.EmailAddress, x.UserGroupName, x.StsName, x.CreatedDate != null ? x.CreatedDate.Value.ToString(Enums.DATE_TIME_FORMAT) : "", x.CreatedByName, x.LoginID, Helper.Encrypt(x.UserId) })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region User Role Management
        public async Task<ActionResult> GetUserAccessMgmt(jQueryDataTableParamModel Params)
        {
            var displyColumns = new string[] { "UserGroupCd", "UserGroupName", "StsName", "CreatedDate", "CreatedByName", "CreatedBy" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<UserAccessManagementModel>>
                ("UserMatrix/GetSecurityUserGroup?" + Helper.GetQueryString(Params));

            if (response == null || response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response = new APIResponseListModel<UserAccessManagementModel>();
                response.Result.RecordList = new List<UserAccessManagementModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.UserGroupCd, x.UserGroupName, x.StsName, x.CreatedDate != null ? x.CreatedDate.Value.ToString(Enums.DATE_TIME_FORMAT) : "", x.CreatedByName, x.CreatedBy })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPatch]
        public async Task<JsonResult> SaveUserMatrix(UserGroupMatrix model)
        {
            var result = await ApiClient.PatchJsonAsync<APIResponseModel<SysUserModel>>
                ("UserMatrix/SaveUserMatrix", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetUserMatrixByUserGroup(string userGroup)
        {
            var userMatrix = await ApiClient.GetArrayJsonAsync<APIResponseModel<List<UserMatrixPage>>>
             ("UserMatrix/GetUserMatrix?" + Helper.GetQueryString(new { userGroup = userGroup }));

            return Json(userMatrix, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Reward Items
        public async Task<ActionResult> RewardItemList(jQueryDataTableParamModel Params)
        {
            var displyColumns = new string[] { "ProdClassName", "ProdTypeName", "ProdCd", "ItemName", "ItemDescp", "StatusName", "CreatedDate", "CreatedByName" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<RewardItemModel>>
                ("Reward/List?" + Helper.GetQueryString(Params));

            if (response == null || response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response = new APIResponseListModel<RewardItemModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.ProdClassName, x.ProdTypeName, x.ProdCd, x.ItemName, x.ItemDescp, x.StatusName, x.CreatedDate != null? x.CreatedDate.Value.ToString(Enums.DATE_TIME_FORMAT) : "", x.CreatedByName })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> RewardItemNew(RewardItemModel model)
        {
            // Apply bussiness rule layout
            model.VoucherInd = model.VoucherInd ?? false;
            if (model.VoucherInd == true)
            {
                if (model.VoucherSourceCd == Enums.VOUCHER_INTERNAL)
                {
                    model.ExtPartnerCd = null;
                    if (model.VoucherValidPeriodTypeCd == Enums.VOUCHER_PERIOD_TYPE_NO_OF_DAYS)
                    {
                        model.VoucherValidStartDate = null;
                        model.VoucherValidEndDate = null;
                    }
                    else
                    {
                        model.VoucherValidDays = null;
                    }
                }
                else // be external voucher
                {
                    model.VoucherAllocateTypeCd = null;
                    model.VoucherValidPeriodTypeCd = null;
                    model.VoucherValidStartDate = null;
                    model.VoucherValidEndDate = null;
                    model.VoucherValidDays = null;
                    model.Remark1 = null;
                }
            }
            else
            {
                model.ExtPartnerCd = null;
                model.VoucherSourceCd = null;
                model.VoucherAllocateTypeCd = null;
                model.VoucherValidPeriodTypeCd = null;
                model.VoucherValidStartDate = null;
                model.VoucherValidEndDate = null;
                model.VoucherValidDays = null;
                model.Remark1 = null;
            }

            if (model.Remark1 != null)
            {
                model.Remark2 = Regex.Replace(model.Remark1, "<.*?>", "").Trim();
            }

            var rp = await ApiClient.PostJsonAsync<APIResponseModel<RewardItemModel>>
                ("Reward/Create", JsonConvert.SerializeObject(model));

            return Json(rp, JsonRequestBehavior.AllowGet);
        }

        [HttpPatch]
        public async Task<JsonResult> UpdateRewardItem(RewardItemModel model)
        {
            // Apply bussiness rule layout
            model.VoucherInd = model.VoucherInd ?? false;

            if (model.VoucherInd == true)
            {
                if(model.VoucherSourceCd == Enums.VOUCHER_INTERNAL)
                {
                    model.ExtPartnerCd = null;
                    if (model.VoucherValidPeriodTypeCd == Enums.VOUCHER_PERIOD_TYPE_NO_OF_DAYS)
                    {
                        model.VoucherValidStartDate = null;
                        model.VoucherValidEndDate = null;
                    }
                    else
                    {
                        model.VoucherValidDays = null;
                    }
                }
                else // be external voucher
                {
                    model.VoucherAllocateTypeCd = null;
                    model.VoucherValidPeriodTypeCd = null;
                    model.VoucherValidStartDate = null;
                    model.VoucherValidEndDate = null;
                    model.VoucherValidDays = null;
                    model.Remark1 = null;
                }
            }
            else
            {
                model.ExtPartnerCd = null;
                model.VoucherSourceCd = null;
                model.VoucherAllocateTypeCd = null;
                model.VoucherValidPeriodTypeCd = null;
                model.VoucherValidStartDate = null;
                model.VoucherValidEndDate = null;
                model.VoucherValidDays = null;
                model.Remark1 = null;
            }

            if (model.Remark1 != null)
            {
                model.Remark2 = Regex.Replace(model.Remark1, "<.*?>", "").Trim();
            }

            var result = await ApiClient.PatchJsonAsync<APIResponseModel<RewardItemModel>>
                ("Reward/Update", JsonConvert.SerializeObject(model));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region QR Code Cards
        public async Task<ActionResult> QRBatchList(jQueryDataTableParamModel Params)
        {
            var displyColumns = new string[] { "BatchId", "Qty", "StatusName", "CreatedDate", "CreatedByName", "", "Sts" };
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
                Params.SortColumnName = displyColumns[Params.iSortCol_0];
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<QRCodeCardBatchModel>>
                ("QRBatch/list?" + Helper.GetQueryString(Params));

            if (response == null || response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response = new APIResponseListModel<QRCodeCardBatchModel>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.BatchId, x.Qty, x.StatusName, x.CreatedDate != null ? x.CreatedDate.Value.ToString(Enums.DATE_TIME_FORMAT) : "", x.CreatedByName, "", x.Sts != null ? x.Sts.Trim() : "" })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> QRBatchDownload(string batchId)
        {
            var _obj = await ApiClient.GetJsonAsync<APIResponseModel<List<QRCodeCardBatchDownloadModel>>>
                            ("QRBatch/select?BatchId=" + batchId);
            return Json(_obj.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> QRBatchGenerate(QRCodeCardBatchModel model)
        {
            var rp = await ApiClient.PostJsonAsync<APIResponseModel<QRCodeCardBatchModel>>
                ("QRBatch/create", JsonConvert.SerializeObject(model));

            return Json(rp, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}