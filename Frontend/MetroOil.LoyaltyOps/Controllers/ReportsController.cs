using System;
using MetroOil.LoyaltyOps.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MetroOil.LoyaltyOps.Models.Reports;
using MetroOil.LoyaltyOps.Helpers;

namespace MetroOil.LoyaltyOps.Controllers
{
    //[Authorize(Roles = "Internal")]
    public class ReportsController : Controller
    {
        // GET: Reports
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
                case "RptViewer":
                    return PartialView("Partials/ReportViewer", new ReportViewerModel());
                default:
                    return PartialView();
            }
        }
        #endregion

        public async Task<JsonResult> FillData(string Prefix, string ReportCd)
        {
            switch (Prefix)
            {
                case "RptViewer":
                    {
                        var _obj = new ReportViewerModel()
                        {
                        };
                        var _selects = new ReportViewerModel()
                        {
                            RptNames = await GetReportTypes(),
                        };
                        return Json(new FormResponseModel<ReportViewerModel, ReportViewerModel>()
                        {
                            ResponseCode = Enums.RESPONSE_CODE_VALID,
                            Model = _obj,
                            Selects = _selects
                        }, JsonRequestBehavior.AllowGet);
                    }
                default:
                    return Json(new { });
            }
        }

        #region Get Report Type
        public static Task<List<SelectListItem>> GetReportTypes(int? refNo = null)
        {
            var _obj = new { IssNo = Enums.ISS_NO };

            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<ReportTypeModel>>>("Report/GetReportTypes?" + Helper.GetQueryString(_obj)).Result;

            if (rp != null && rp.Result != null)
            {
                return Task.Run(() => rp.Result.Select(x => new SelectListItem() { Value = x.RptId, Text = x.CodeNDescp }).ToList());
            }
            return Task.Run(() => new List<SelectListItem>());
        }
        //[HttpGet]
        //public async Task<JsonResult> GetReportTypes()
        //{
        //    var _obj = new { IssNo = Enums.ISS_NO };

        //    var rp = ApiClient.GetJsonAsync<APIResponseModel<List<ReportTypeModel>>>("Report/GetReportTypes?" + Helper.GetQueryString(_obj)).Result;

        //    if (rp != null && rp.Result != null)
        //    {
        //        var result = rp.Result.Select(x => new SelectListItem() { Value = x.RptId, Text = x.CodeNDescp }).ToList();
        //        return Json(new { result }, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(new { }, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Rpt Dynamic control/ layout
        [HttpGet]
        public async Task<ActionResult> GetReportControl(string RptId)
        {
            var _obj = new { IssNo = Enums.ISS_NO, RptId };

            var rp = ApiClient.GetJsonAsync<APIResponseModel<List<RptDynControlModel>>>("Report/GetReportControlSelect?" + Helper.GetQueryString(_obj)).Result;

            if (rp != null && rp.Result != null)
            {
                return Json(rp.Result, JsonRequestBehavior.AllowGet);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetReportColumns(string RptId)
        {
            var _obj = new { IssNo = Enums.ISS_NO, RptId };

            var rp = ApiClient.GetJsonAsync<APIResponseListModel<string>>("Report/GetColumnNamesByFiltering?" + Helper.GetQueryString(_obj)).Result;

            if (rp != null && rp.Result != null)
            {
                return Json(rp.Result.RecordList, JsonRequestBehavior.AllowGet);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Report Viewer Data
        public async Task<ActionResult> GetReportViewer(jQueryDataTableParamModel Params, string RptId, string FilterParameterList)
        {
            if (Params.iSortCol_0 > 0)
            {
                Params.iSortCol_0 -= 1; // Remove first Index column
            }

            var response = await ApiClient.GetJsonAsync<APIResponseListModel<object>>
                ("Report/GetReportByFiltering?RptId=" + RptId + "&FilterParameterList=" + FilterParameterList + "&" + Helper.GetQueryString(Params));

            if (response == null || response.ResponseCode != 0 || response.Result == null || response.Result.RecordList == null) // empty data/ error
            {
                response = new APIResponseListModel<object>();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalDisplayRecords = response.Result.RecordCount,
                iTotalRecords = response.Result.RecordCount,
                aaData = response.Result.RecordList,
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}