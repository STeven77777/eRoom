using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MetroOil.LoyaltyOps.Models;

namespace MetroOil.LoyaltyOps.Controllers
{
    //[Authorize(Roles = "Internal")]
    public class HomeController : Controller
    {
        //[AccessibilityXtra]
        public ActionResult Index()
        {
            ViewBag.Target = "Home";

            return View();
        }

        #region "List Of Web Pages"
        //[CompressFilter]
        //[AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                //case "cussrh":
                //    return PartialView("Partials/CustomerSearch");
                //case "crdsrh":
                //    return PartialView("Partials/CardSearch");
                //case "mertsrh":
                //    return PartialView("Partials/MerchantSearch");
                default:
                    return PartialView();
            }
        }
        #endregion

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
           Session["Culture"] = new CultureInfo(lang);
           return Redirect(returnUrl);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}