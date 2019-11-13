using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Controllers
{
    [Authorize(Roles = "Internal")]
    public class CorporateController : Controller
    {
        // GET: Corporate
        public ActionResult Index()
        {
            return View();
        }

        #region "List Of Web Pages"
        //[CompressFilter]
        //[AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "corpf":
                    return PartialView("Partials/Details/CorporateProfile");
                default:
                    return PartialView();
            }
        }
        #endregion
    }
}