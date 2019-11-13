using MetroOil.LoyaltyOps.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Models
{
    public class CreditAssessmentModel
    {
        [DisplayNameLocalizedAttribute("CreditAssessment", "CrdLmtLbl", "Credit Limit (RM)")]
        public decimal CrdLmt { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "TmpCrdLmtLbl", "Temporary Credit Limit")]
        public int TmpCrdLmt { get; set; }
        //[DisplayNameLocalizedAttribute("CreditAssessment", "TmpCrdLmtFromLbl", "Validity Period for Temporary Credit Limit")]
        public string TmpCrdLmtFrom { get; set; }
        public string TmpCrdLmtTo { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "CrdRskCatLbl", "Credit Risk Category")]
        public string CrdRskCat { get; set; }
        public List<SelectListItem> CrdRskCatLst { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "ExpDateLbl", "Expiry Date")]
        public string ExpDate { get; set; }
        [Required]
        [RegularExpression(@"^\\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "Not a valid Phone number")]
        [DisplayNameLocalizedAttribute("CreditAssessment", "NxtAssessmentDateLbl", "Next Assessment Date")]
        public string NxtAssessmentDate { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "RemarksLbl", "Remarks")]
        public string Remarks { get; set; }

        [DisplayNameLocalizedAttribute("CreditAssessment", "ReqSecurityDeposit", "Required Security Deposit?")]
        public bool ReqSecurityDeposit { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "CurrentSecurityDepositAmt", "Current Security Deposit Amount")]
        public int CurrentSecurityDepositAmt { get; set; }

        [DisplayNameLocalizedAttribute("CreditAssessment", "PymtMode", "Payment Mode")]
        public string PymtMode { get; set;}
        public List<SelectListItem> PymtModeLst { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CreditAssessment", "PymtTerms", "Payment Terms")]
        public string PymtTerms { get; set; }
        public List<SelectListItem> PymtTermsLst { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "GracePeriod", "Grace Period (days)")]
        public int GracePeriod { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "CrdAllowanceFactor", "Credit Allowance Factor (%)")]
        public int CrdAllowanceFactor { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "SalesTerritory", "Sales Territory")]
        public string SalesTerritory { get; set; }
        public List<SelectListItem> SalesTerritoryLst { get; set; }

        #region Security Deposit
        [DisplayNameLocalizedAttribute("CreditAssessment", "DepositAmt", "Security Deposit Amount")]
        public string DepositAmt { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "DepositType", "Security Deposit Type")]
        public string DepositType { get; set; }
        public List<SelectListItem> DepositTypeLst { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "RefNo", "Reference No.")]
        public string RefNo { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "SAPNo", "SAP No.")]
        public string SAPNo { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "SecuriryDepositExpDate", "Expiry Date")]
        public string SecuriryDepositExpDate { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "BankName", "Bank Name")]
        public string BankName { get; set; }
        public List<SelectListItem> BankNameLst { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "BranchCode", "Branch Code")]
        public string BranchCode { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "BankAcctType", "Bank Account Type")]
        public string BankAcctType { get; set; }
        public List<SelectListItem> BankAcctTypeLst { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "BankAcctNo", "Bank Account No.")]
        public string BankAcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CreditAssessment", "Remarks", "Remarks")]
        public string DepositRemarks { get; set; }
        #endregion
    }
}