using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MetroOil.LoyaltyOps.Helpers
{
    public class Enums
    {
        // Authorization
        public static int USER_TYPE_OPS = int.Parse(WebConfigurationManager.AppSettings["USER_TYPE_OPS"]);//3;// OPS USER
        public static int DEVICE_TYPE = int.Parse(WebConfigurationManager.AppSettings["DEVICE_TYPE"]);
        public static string DEVICE_ID = WebConfigurationManager.AppSettings["DEVICE_ID"];
        public static string APPS_VERSION_CD = WebConfigurationManager.AppSettings["APPS_VERSION_CD"];
        public static string APPS_VERSION_NAME = WebConfigurationManager.AppSettings["APPS_VERSION_NAME"];
        public static string APP_TOKEN = WebConfigurationManager.AppSettings["APP_TOKEN"];

        public static int ISS_NO = int.Parse(WebConfigurationManager.AppSettings["ISS_NO"]);//1;
        public static int ACQ_NO = int.Parse(WebConfigurationManager.AppSettings["ACQ_NO"]);//1;

        public static string INPUT_SOURCEIIND_MBR = WebConfigurationManager.AppSettings["INPUT_SOURCEIIND_MBR"];// "OPSAPI";

        public static string REF_TYPE_COUNTRY = "Country";
        public static string REF_TYPE_TITLE = "Title";
        public static string REF_TYPE_MEMBERTIER = "MemberTier";
        public static string REF_TYPE_GENDER = "Gender";
        public static string REF_TYPE_ADDRESS = "Address";
        public static string REF_TYPE_ACCT_STATUS = "AcctSts";
        public static string REF_TYPE_MERCH_ACCT_STATUS = "MerchAcctSts";
        public static string REF_TYPE_MEMBER_STS_REASON = "MemberStsReason";
        public static string REF_TYPE_MERCH_STS_REASON = "MerchReasonCd";
        public static string REF_TYPE_MERCH_USER_ROLE = "MerchUserRoles";
        public static string REF_TYPE_USER_STS = "UserSts";
        public static string REF_TYPE_TEL_COUNTRY = "TelCountry";
        public static string REF_TYPE_MARITAL_STS = "MaritalSts";
        public static string REF_TYPE_DRIVER_TYPE = "DriverType";
        public static string REF_TYPE_VEHICLE_TYPE = "VehicleType";
        public static string REF_TYPE_COMPANY_TYPE = "CompanyType";
        public static string REF_TYPE_EMPLOYMENT_STS = "EmploymentSts";
        public static string REF_TYPE_VOUCHER_STS = "VoucherSts";
        public static string REF_TYPE_VOUCHER_ALLOCATE_TYPE = "VoucherAllocateType";
        public static string REF_TYPE_VOUCHER_SOURCE = "VoucherSource";
        public static string REF_TYPE_VOUCHER_PERIOD_TYPE = "VoucherPeriodType";
        public static string REF_TYPE_REWARD_STS = "Status";
        public static string REF_TYPE_MERCH_OWNERSHIP = "MerchOwnership";
        public static string REF_TYPE_BUSN_SIZE = "BusnSize";
        public static string REF_TYPE_OCCUPATION = "Occupation";
        public static string REF_TYPE_SIC = "IndustryCd";
        public static string REF_TYPE_MCC = "MerchCategory";
        public static string REF_TYPE_TERMINAL_STS = "TermSts";
        public static string REF_TYPE_TERMINAL_DEVICE_TYPE = "TermType";
        public static string REF_TYPE_TERMINAL_SRC = "DeviceVendor";
        public static string REF_TYPE_TERMINAL_STS_REASON = "TermReasonCd";

        public static int REF_NO_ADDRESS = int.Parse(WebConfigurationManager.AppSettings["REF_NO_ADDRESS"]);//2;

        public static string DEFAULT_COUNTRY_CODE = WebConfigurationManager.AppSettings["DEFAULT_COUNTRY_CODE"] ?? "60";
        public static string DEFAULT_TEL_COUNTRY_CODE = WebConfigurationManager.AppSettings["DEFAULT_TEL_COUNTRY_CODE"] ?? "60";
        public static string DEFAULT_GENDER = WebConfigurationManager.AppSettings["DEFAULT_GENDER"];
        public static string VOUCHER_PERIOD_TYPE_NO_OF_DAYS = "DY";

        public static string DATE_FORMAT = WebConfigurationManager.AppSettings["DATE_FORMAT"];
        public static string DATE_TIME_FORMAT = WebConfigurationManager.AppSettings["DATE_TIME_FORMAT"];

        public static string RESPONSE_MESSAGE_INVALID = WebConfigurationManager.AppSettings["RESPONSE_MESSAGE_INVALID"];//"Validation Error Occurred!";
        public static int RESPONSE_CODE_INVALID = -1;
        public static int RESPONSE_CODE_VALID = 0;

        public static int TXN_POSTED = 1;
        public static int TXN_UNPOSTED = 2;

        public static string TXN_IND_REDEMPTION = "M";

        public static string VOUCHER_INTERNAL = "IV";
        public static string VOUCHER_EXTERNAL = "EV";

        public static string USER_TYPE_MERCHANT = "merch";

        public static string ACCESS_TOKEN = "access_token";
        public static string REFRESH_TOKEN = "refresh_token";
        public static string EXPIRY_TOKEN = "expiry_token";
    }
}