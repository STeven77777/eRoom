using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using MetroOil.LoyaltyOps.Models;
using System.Web;
using System.Threading.Tasks;

namespace MetroOil.LoyaltyOps
{
    public class AccessibilityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _AccessibilityList = (List<AccessibilityModel>)HttpContext.Current.Session["Accessibility"];

            //if (!filterContext.HttpContext.Request.IsAjaxRequest() || _AccessibilityList == null)
            if (_AccessibilityList == null)
            {
                //var _Controller = filterContext.RouteData.Values["Controller"];
                var rp = ApiClient.GetJsonAsync<APIResponseModel<List<AccessibilityModel>>>("UserMatrix/UserGroupPageAccess");

                if (rp != null && rp.Result != null)
                {
                    _AccessibilityList = rp.Result.Result;
                    filterContext.HttpContext.Session["Accessibility"] = _AccessibilityList;
                }
            }

            if (_AccessibilityList == null || !_AccessibilityList.Any())
            {
                filterContext.Result = new HttpStatusCodeResult(403);
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}