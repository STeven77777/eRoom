using System.ComponentModel;

namespace eRoom.Shared.CoreLib.Common
{
    public class Constants
    {
        public const byte IssNo = 1;
        public static string SecretKey = "p@ssw0rdp@ssw0rd";
        public static string DefaultMerchantPin = "1234";
        public static int DefaultMerhcantPasswordLength = 8;
        public static string InitialPassword = "Initial Password";

        public const string AppTokenHeader = "app_token";

        public const int VerificationTokenExpiry = 10;//In Minutes

        public static class MetaResult
        {
            public const int ResponseCode = 0;
            public const string ResponseDesc = "Success";
        }

        public static class CustomClaimTypes
        {
            public const string UserRole = "UserRole";
            public const string DeviceId = "DeviceId";
            public const string DeviceCode = "DeviceCode";
            public const string PhoneNumber = "PhoneNumber";
        }

        public static class Policy
        {
            public const string MemberUserType = "MbrUser";
            public const string MerchantUserType = "MerchUser";
            public const string LoyaltyOpsUserType = "OpsUser";

            public const string AppToken = "AppToken";
        }

        public static class SMSContentCode
        {
            //New Password
            public const string TemplateCdPWD = "NWPWD";

            //Verification Code
            public const string TemplateCdVerificationCd = "VC";
        }

        public static class PushNotificationContentCode
        {
            //Points Issuance (Sales)
            public const string PointSales = "PS";
            //Points Redeemption (Redeemption)
            public const string PointRedeemption = "PR";
            //Voucher Purchase (by redeeming points)
            public const string VoucherPurchase = "VP";
            //Voucher Redemption 
            public const string VoucherRedemption = "VR";
            //Voucher Redemption 
            public const string EGiftPurchase = "EP";
        }

        /// <summary>
        /// Transaction process Indicator 
        /// </summary>
        public static class TransactionInd
        {
            public const byte Posted = 1;
            public const byte Online = 2;
        }

        public static class Status
        {
            public const string Active = "A";
            public const string InActive = "I";
        }

        public static class Controls
        {
            public const string AcctNo = "AcctNo";
            public const string StanID = "StanID";
            public const string UserID = "UserID";
            public const string EDCBatchId = "EDCBatchId";
            public const string PrcsId = "PrcsId";
        }

        public static class IAuthApiConstants
        {
            public const string SourceId = "ZAP";
            public const double PaymentCardType = 0;

            public const string GstTaxInvoiceNumber = "123456";
            public const string GstTaxCode = "T1";
            public const double TotalGstAmount = 0;
            public const double GstTaxPercentage = 0;
            public const string GstProductCode = "";

            public const string RedemptionTranType = "PointsRedemption";
            public const string RedemptionVoidTranType = "PointsRedemptionVoid";

            public const string DefaultDeviceId = "0000001";
            public const string DefaultBusinessLocationId = "DFT001";

            public const string GiftawayProductCode = "0400";
        }

        public static class ProductGroup
        {
            public const string Sales = "01";
            public const string RedemptionMerchandise = "02";
            public const string RedemptionFuelVoucher = "03";
            public const string RedemptionGiftAway = "04";
        }

        /// <summary>
        /// Api messages used as generic response on error.
        /// </summary>
        public static class ApiMessages
        {
            public const string GiftawayApiError = "Error occurred when calling giftaway api. Please try later.";
            public const string IAuthApiError = "Error occurred when calling transaction api. Please try later.";
        }

        /// <summary>
        /// External Partner Codes
        /// </summary>
        public class PartnerCodes
        {
            public const string Giftaway = "GA";
        }

        /// <summary>
        /// Audit Log Types
        /// </summary>
        public class AuditType
        {
            public const char Login = 'I';
            public const char RefreshLogin = 'R';
            public const char Logout = 'O';
        }
    }

    public class EmailContent
    {
        //Email Content Codes
        public const int NewMerchantUserCreate = 1;
        public const int UserPasswordReset = 2;
        public const int NewOpsUserUserCreate = 3;
        public const int OpsUserPasswordReset = 4;
        public const int MemberResetPassword = 5;
        public const int OpsUserForgotPassword = 6;

        //Email Content Seperator
        public const string Seperator = "|";

        //SMS Content Codes : todo: Update to new
        public const string TemplateCdPWD = "NWPWD";
        public const string TemplateCdVerificationCd = "VC";
    }

    public enum SMSTemplates
    {
        //Default value for sms template
        [Description("Verification Code")]
        VC = 0,
        [Description("Sign Up")]
        SGNUP = 1,
        [Description("Update Mobile Number")]
        UPMOB = 2,
        [Description("Reset Password")]
        RSPWD = 3

    }

    public class JwtOptions
    {
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string JwtExipry { get; set; }
        public string JwtKey { get; set; }
    }

    public class PushProviderOptions
    {
        public string ServerKey { get; set; }
        public string SenderId { get; set; }
    }

    /// <summary>
    /// SMS provider settings.(set from app.settings file)
    /// </summary>
    public class SMSProviderSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ShortCodeMask { get; set; }
        public string Url { get; set; }
    }

    /// <summary>
    /// Giftaway settings POCO.(Set from app.settings)
    /// </summary>
    public class GiftawaySettings
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public string PrivateKey { get; set; }
        public string SenderName { get; set; }
        public string SenderMobile { get; set; }
        public string SenderEmail { get; set; }
    }

    public class SP
    {
        #region SMS Related SPs

        public const string SmsProviderSelect = "SmsSenderSelect";
        public const string SmsMsgOutCreate = "SmsMsgOutCreate";
        public const string SmsMsgOutEdit = "SmsMsgOutEdit";
        public const string SmsGenerateContent = "ApiSmsGenerateContent";
        public const string EmailJobCreate = "ApiEmailJobCreate";
        public const string MobileVerificationCdCreate = "ApiMobileVerificationCdCreate";
        public const string MobileVerificationCodeValidate = "ApiVerificationCdValidate";
        public const string MemberAcctCreateFrmClassicLogin = "ApiMemberAcctCreateFrmClassicLogin";
        public const string SecurityLoginPasswordChange = "SecurityLoginPasswordChange";
        public const string UserIdValidate = "ApiSecurityUserIdValidate";
        //Checks if mobile number already exists in the system
        public const string MobileValidate = "ApiMobileValidate";
        public const string SMSResponseCreate = "ApiSmsMsgOutResponseCreate";
        #endregion SMS Related SPs

        #region Common SP's
        public const string Getstate = "ApiGetState";
        public const string GetRefLib = "ApiGetRefLib";

        public const string GetPageContent = "ApiGetPageContent";

        public const string SecurityLoginAuditCreate = "ApiSecurityLoginAuditCreate";

        public const string AccessTokenCreate = "ApiSecurityAccessTokenCreate";
        public const string RefreshTokenValidate = "ApiSecurityRefreshTokenValidate";

        public const string AppTokenValidate = "ApiSecurityAppTokenValidate";

        #endregion Common SP's

        #region Merchant Shared Related SPs
        public static string MerchantUserSelect = "ApiSecurityMerchantUserSelect";

        public static string MemberFCMTokenSelectByCardNo = "ApiMemberFCMTokenSelectByCardNo";
        public static string MemberFCMTokenSelectByAcctNo = "ApiMemberFCMTokenSelectByAcctNo";

        public static string SecurityUserDeviceUpdate = "ApiSecurityUserDeviceUpdate";
        public static string SecurityUserPasswordChange = "ApiSecurityUserPasswordChange";


        public static string GenerateNextCtrlNo = "ApiGenerateNextCtrlNo";
        public static string ForgotPassword = "ApiMobileVerificationCdCreateForgetPwd";


        public static string GetMemberVoucherByAcctNo = "ApiMemberVoucherByAcctNo";
        public static string MemberVoucherSelect = "ApiMemberVoucherSelect";

        /// <summary>
        /// Get member external vouchers (egift vouchers)
        /// </summary>
        public static string MemberExtRewardOrderByAcctNoFrmMember = "ApiMemberExtRewardOrderByAcctNoFrmMember";


        public static string MemberVoucherUpdateStatusRedeemed = "ApiMemberVoucherUpdateStatusRedeemed";

        public static string RedeemPointForRewardItemValidate = "ApiRedeemPointForRewardItemValidate";
        public static string PurchaseVoucherUsingPoints = "ApiRedeemPointForRewardItemCreateVoucher";

        public static string TxnUserCreate = "ApiTxnUserCreate";

        public static string ExtRewardOrderCreate = "ApiExtRewardOrderCreate";


        #endregion Merchant Shared Related SPs

        #region Push Notification Related
        public const string PnGenerateContent = "ApiPnGenerateContent";
        public const string PnMsgOutCreate = "ApiPnMsgOutCreate";



        #endregion

        #region Shared SPs

        public const string RewardsItemSelect = "ApiRewardsItemSelect";

        public const string GetProductClass = "ApiGetProductClass";
        public const string GetProductTypeMaster = "ApiGetProductTypeMaster";
        public const string GetProductMaster = "ApiGetProductMaster";

        #endregion

        public static string MemberAcctSummaryByUserId = "ApiMemberAcctSummaryByUserId";

        public const string MemberFind = "ApiMemberFind";

        public const string GetCardRange = "ApiGetCardRange";
    }
}

