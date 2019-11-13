using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using MetroOil.LoyaltyOps.Models;
using System.Web;

namespace MetroOil.LoyaltyOps
{
    public class AccessibilityXtraAttribute : ActionFilterAttribute
    {
        private string OverrideController = null;
        private string OverrideAction = null;
        private string OverrideSection = null;

        public AccessibilityXtraAttribute() { }
        public AccessibilityXtraAttribute(string _OverrideController, string _OverrideAction, string _OverrideSection)
        {
            this.OverrideAction = _OverrideAction;
            this.OverrideController = _OverrideController;
            this.OverrideSection = _OverrideSection;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var _AccessibilityList = (List<AccessibilityModel>)HttpContext.Current.Session["Accessibility"];
            //var _SectionCd = filterContext.RequestContext.HttpContext.Request.QueryString["Prefix"];
            //var _Controller = filterContext.RouteData.Values["Controller"] + "";

            //if (_AccessibilityList == null) // Session end or haven't get matrix
            //{
            //    var rp = ApiClient.GetJsonAsync<APIResponseModel<List<AccessibilityModel>>>("UserMatrix/UserGroupPageAccess?basePageShortCode");
                
            //    if (rp != null && rp.Result != null)
            //    {
            //        _AccessibilityList = rp.Result.Result;
            //        filterContext.HttpContext.Session["Accessibility"] = _AccessibilityList;
            //    }
            //}

            //if(_Controller != "Home")
            //{
            //    if ((_AccessibilityList == null || !_AccessibilityList.Any()))
            //    {
            //        filterContext.Result = new HttpStatusCodeResult(403);
            //        filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            //    }
            //}

            //if (_SectionCd != null)
            //{
            //    var _SectionInfo = _AccessibilityList.FirstOrDefault(p => p.ShortDescp.ToLower() == _SectionCd.ToLower());
            //    if (_SectionInfo == null || !_SectionInfo.GroupPageStatus)
            //    {
            //        filterContext.Result = new HttpStatusCodeResult(403);
            //        filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            //    }
            //}

            base.OnActionExecuting(filterContext);
        }
    }
}