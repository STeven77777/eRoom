using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace eRoom.Shared.CoreLib.Common
{
    public enum PortType
    {
        Invalid = 0,
        Mobile = 1,
        Web = 2,
    }
    public enum DeviceType : byte
    {
        //Invalid = 0, //Find if it serves any purpose?
        Web = 1,
        Android = 2,
        IOS = 3,
    }
    public enum RoleType : byte
    {
        User = 0,
        Merchant = 1,
        Lms = 2,
    }
    public enum AppType : byte
    {
        User = 1,
        Merchant = 2,
        Lms = 3

    }
    public enum PnActionType : byte
    {
        Message = 0,
        URL = 1,
        Screen = 2,
        html = 3,
        Reminder = 4,
    }

    public enum UserTypeCode : sbyte
    {
        [Description("Member")]
        MEMR = 1,
        [Description("Merchant")]
        MERCH = 2,
        [Description("LoyaltyOps")]
        OPS = 3
    }

    public enum InputSourceInd : sbyte
    {
        [Description("MemberAPI")]
        MBRAPI = 1,
        [Description("MerchantAPI")]
        MERCHAPI = 2,
        [Description("LoyaltyOpsAPI")]
        OPSAPI = 3
    }
    public enum OAuthType : byte
    {
        Facebook = 1,
        Google = 2
    }
    public enum ContentCode : byte
    {
        [Description("T&C")]
        TC = 1,
        [Description("Privacy Policy")]
        PP = 2

    }

    public enum GrantType : byte
    {
        [Description("Password")]
        Password = 1,
        [Description("Refresh Token")]
        RefreshToken = 2

    }

}
