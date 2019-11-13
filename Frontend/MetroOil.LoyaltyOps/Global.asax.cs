using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Helpers;
using System.Security.Claims;
using MetroOil.LoyaltyOps.Helpers;
using Newtonsoft.Json;

namespace MetroOil.LoyaltyOps
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Web.Optimization.BundleTable.EnableOptimizations = false;

            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Encrypt connection string
            //AppConfigurationHelper.EncryptionConnectionString(ConfigurationManager.ConnectionStrings["pdb_ccmsContext"].ConnectionString);
            //BaseService.EncryptionConnectionString();
            // Register mapping
            //UnityConfig.RegisterMapping();
            // End register mapping
            // Register custom NLog Layout renderers
            //ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("utc_date", typeof(MetroOil.Common.Log.UtcDateRenderer));
            //ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("web_variables", typeof(MetroOil.Common.Log.WebVariablesRenderer));

            //HttpConfiguration config = GlobalConfiguration.Configuration;
            //config.Formatters.JsonFormatter.SerializerSettings.Converters.Add
            //                (new Newtonsoft.Json.Converters.StringEnumConverter());
            //config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AdjustToUniversal });
            //var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                DateFormatString = "yyyy/MM/dd hh:mm:ss"
            };

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //It's important to check whether session object is ready
            //if (HttpContext.Current.Session != null)
            //{
            //    CultureInfo ci = (CultureInfo)this.Session["Culture"];
            //    //Checking first if there is no value in session 
            //    //and set default language 
            //    //this can happen for first user's request
            //    if (ci == null)
            //    {
            //        //Sets default culture to english invariant
            //        string langName = "my";

            //        //Try to get values from Accept lang HTTP header
            //        if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
            //        {
            //            //Gets accepted list 
            //            langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
            //        }
            //        ci = new CultureInfo(langName);
            //        this.Session["Culture"] = ci;
            //    }
            //    //Finally setting culture for each request
            //    Thread.CurrentThread.CurrentUICulture = ci;
            //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            //}
        }
    }
}
