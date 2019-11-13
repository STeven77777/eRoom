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
    public class CustomersModel
    {
        public string AcctNo { get; set; }
        public string CmpyName { get; set; }
        public string AcctSts { get; set; }
        public string PlasticType { get; set; }
        public string AcctClass { get; set; }
        public string ApprovedOn { get; set; }
        public string CreatedOn { get; set; }
    }

    public class AccountStatusChange
    {
        public string ChangedFrom { get; set; }
        public string ChangedTo { get; set; }
        public string ChangeReason { get; set; }
        public string ChangedOn { get; set; }
        public string ChangedBy { get; set; }
    }

    public class FinancialInfoModel
    {
        [DisplayNameLocalizedAttribute("Financial", "TaxRefLbl", "Tax Reference No.")]
        public string TaxRefNo { get; set; }
        [DisplayNameLocalizedAttribute("Financial", "DunningCodeLbl", "Dunning Code")]
        public string DunningCode { get; set; }
        [DisplayNameLocalizedAttribute("Financial", "WriteOffDateLbl", "Write-off Date")]
        public string WriteOffDate { get; set; }
        [DisplayNameLocalizedAttribute("Financial", "CycNoLbl", "Cycle No.")]
        public string CycNo { get; set; }
        public List<SelectListItem> CycNoList { get; set; }
        [DisplayNameLocalizedAttribute("Financial", "StmtPrefLbl", "Statement/ Invoice Preference")]
        public int StmtPref { get; set; }
        public List<SelectListItem> StmtPrefList { get; set; }
        [DisplayNameLocalizedAttribute("Financial", "StmtTypeLbl", "Statement Type")]
        public int StmtType { get; set; }
        public List<SelectListItem> StmtTypeList { get; set; }
        [DisplayNameLocalizedAttribute("Financial", "LatePymtChargesLbl", "Late Payment Charges")]
        public bool LatePymtCharges { get; set; }

        public FinancialInfoModel()
        {
            CycNoList = new List<SelectListItem>();
            CycNoList.Add(new SelectListItem() { Value = "0", Text = "7th day of the month" });
        }
    }

    public class CrdAssHisModel{
        public string Type { get; set; }
        public string ChangedFrom { get; set; }
        public string ChangedTo { get; set; }
        public string ExpiredDate { get; set; }
        public string ValidityPeriod { get; set; }
        public string ChangedOn { get; set; }
        public string ChangedBy { get; set; }
    }

    public class SecurityDepositModel
    {
        public string Amt { get; set; }
        public string Type { get; set; }
        public string ExpDate { get; set; }
        public string BankName { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class SecurityDepositHisModel
    {
        public string Type { get; set; }
        public string ChangedFrom { get; set; }
        public string ChangedTo { get; set; }
        public string ChangedOn { get; set; }
        public string ChangedBy { get; set; }
    }

    public class ProductPriceModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductCat { get; set; }
        public string ProductType { get; set; }
        public string PricingModel { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class PricingModel
    {
        public string UnitPrice { get; set; }
        public string ValidityPeriod { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class DiscountRebateModel
    {
        public string PlanID { get; set; }
        public string PlanTitle { get; set; }
        public string PlanType { get; set; }
        public string ValidityPeriod { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class ValueDiscountRebateModel
    {
        public string TierValue { get; set; }
        public string BasicValue { get; set; }
        public string BilledValue { get; set; }
        public string UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ProductRestrictionsModel
    {
        [DisplayNameLocalizedAttribute("CardtrendAccount", "ProductGroup", "Product Group")]
        public string ProductGroup { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TotalCountLimit", "Total Count Limit")]
        public string TotalCountLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TotalAmountLimit", "Total Amount Limit")]
        public string TotalAmountLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TotalVolumeLimit", "Total Volume Limit")]
        public string TotalVolumeLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "AccountLimitTnx", "Account Limit Per Transaction")]
        public string AccountLimitTnx { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "VolumeLimitTnx", "Volume Limit Per Transaction")]
        public string VolumeLimitTnx { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CreatedOn", "Created On")]
        public string CreatedOn { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CreatedBy", "Created By")]
        public string CreatedBy { get; set; }
    }

    public class ProductAcceptanceItemModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductType { get; set; }
    }

    //public class ProductGroupHistoryModel {
    //    public string ProductGroup { get; set; }
    //    public string Action { get; set; }
    //    public string DoneOn { get; set; }
    //    public string DoneBy { get; set; }
    //}

    //public class LocationAcceptanceModel
    //{
    //    public string State { get; set; }
    //    public string CreatedOn { get; set; }
    //    public string CreatedBy { get; set; }
    //}

    //public class StationDealerModel
    //{
    //    public string State { get; set; }
    //    public string LocationNo { get; set; }
    //    public string RefNo { get; set; }
    //    public string LocationName { get; set; }
    //    public string BusinessName { get; set; }
    //}

    public class CostCentreModel
    {
        public string CostCenterCode { get; set; }
        public string CostCenterName { get; set; }
        public string PersonInCharge { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class CardListModel
    {
        public string CardCreationRefNo { get; set; }
        public string ActNo { get; set; }
        public string ApplicationNo { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
        public string CardMediaType { get; set; }
        public string NameOnCard { get; set; }
        public string CardStatus { get; set; }
        public string CardExpiryDate { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class EventNotificationModel
    {
        public string EventID { get; set; }
        public string EventTitle { get; set; }
        public string Severity { get; set; }
        public string Status { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class FileUploadModel
    {
        public string FileName { get; set; }
        public string UploadedBy { get; set; }
        public string UploadedOn { get; set; }
        public string FileSize { get; set; }
        public string FileType { get; set; }
    }

    public class TransactionModel
    {
        public string TxnNo { get; set; }
        public string CardNo { get; set; }
        public string TxnType { get; set; }
        public string TxnCategory { get; set; }
        public string TxnDate { get; set; }
        public string TxnAmt { get; set; }
        public string Status { get; set; }

        public string BookingDate { get; set; }
        public string DueDate { get; set; }
        public string InvoiceNo { get; set; }
        public string NameOnCard { get; set; }
        public string DriverCardNo { get; set; }
        public string ReceiptNo { get; set; }
        public string TxnDesc { get; set; }
        public string MerchantNo { get; set; }
        public string SiteID { get; set; }
        public string TerminalID { get; set; }
        public string MTI { get; set; }
        public string CreatedBy { get; set; }
        public string CreateOn { get; set; }

        public string Priority { get; set; }
        public string CurTskLvl { get; set; }

        public string TerminalNo { get; set; }
        public string BatchNo { get; set; }
        public string SettlementDate { get; set; }
        public string SettlementStartTime { get; set; }
        public string SettlementEndTime { get; set; }
        public string LocationNo { get; set; }
    }

    public class TransactionItemModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductType { get; set; }
        public string PricingModel { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductAmt { get; set; }
        public string TaxRate { get; set; }
        public string TaxAmt { get; set; }
        public string TaxCode { get; set; }
    }
}