using System;
using System.ComponentModel;
using System.Threading;
using System.Web;

namespace MetroOil.LoyaltyOps.Helpers
{
    public class DisplayNameLocalizedAttribute : DisplayNameAttribute
    {
        private readonly string m_ResourceName;
        private readonly string m_ClassName;
        private readonly string m_DefaultValue;

        public DisplayNameLocalizedAttribute(string className, string resourceName, string defaultValue="")
        {
            m_ResourceName = resourceName;
            m_ClassName = className;
            m_DefaultValue = defaultValue;
        }

        public override string DisplayName
        {
            get
            {
                // get and return the resource object
                //var rcs = string.Empty;
                //try { 
                //rcs = HttpContext.GetGlobalResourceObject(
                //       m_ClassName,
                //       m_ResourceName,
                //       Thread.CurrentThread.CurrentCulture).ToString();
                //}
                //catch (Exception e)
                //{
                //    // Log error resource here
                //}

                //if (string.IsNullOrEmpty(rcs))
                //{
                //    rcs = m_DefaultValue;
                //}

                //return rcs;

                // By pass resource object, get from default value
                return m_DefaultValue;
            }
        }
    }
}