using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MetroOil.LoyaltyOps.Models;

namespace MetroOil.LoyaltyOps.Helpers
{
    public static class AuthorizationExtensions
    {
        public static bool SectionIsEnabled<TModel>(this HtmlHelper<TModel> helper, string ShortDescp)
        {
            //AccessibilityModel Section = new AccessibilityModel();
            ////if (HttpContext.Current.Session["Accessibility"] == null)
            ////    return false;
            //var _AccessibilityList = (List<AccessibilityModel>)HttpContext.Current.Session["Accessibility"];

            //if (_AccessibilityList == null) // Session end or haven't get matrix
            //{
            //    var rp = ApiClient.GetJsonAsync<APIResponseModel<List<AccessibilityModel>>>("UserMatrix/UserGroupPageAccess?basePageShortCode");

            //    if (rp != null && rp.Result != null)
            //    {
            //        _AccessibilityList = rp.Result.Result;
            //        HttpContext.Current.Session["Accessibility"] = _AccessibilityList;
            //    }
            //}

            ////var accessibility = (List<AccessibilityModel>)HttpContext.Current.Session["Accessibility"];
            //if (!string.IsNullOrEmpty(ShortDescp) && _AccessibilityList != null)
            //    Section = _AccessibilityList.FirstOrDefault(p => p.ShortDescp.ToLower() == ShortDescp.ToLower());

            //if (Section == null)
            //    return false;
            //return Section.GroupPageStatus;
            return true;
        }
    }
}