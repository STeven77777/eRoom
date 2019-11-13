using AutoMapper;
using MetroOil.LoyaltyOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Controllers
{
    [Authorize(Roles = "Internal")]
    public class CustomersController : Controller
    {
        public ActionResult Index(string id)
        {
            //ViewBag.AcctNo = id;
            return View();
        }

        #region "List Of Web Pages"
        //[CompressFilter]
        //[AccessibilityXtra]
        public ActionResult Tmpl(string Prefix) 
        {
            switch (Prefix)
            {
                case "acctsum":
                    return PartialView("Partials/Profile/AccountSummary");
                case "cusdt":
                    return PartialView("Partials/Profile/AcctCusInfo");
                case "cont":
                    return PartialView("Partials/Profile/Contacts");
                case "contd":
                    return PartialView("Partials/Profile/ContactsDetail", new ContactModel());
                case "contnew":
                    return PartialView("Partials/Profile/ContactsNew");
                case "addr":
                    return PartialView("Partials/Profile/Address");
                case "addrd":
                    return PartialView("Partials/Profile/AddressDetail");
                case "addrnew":
                    return PartialView("Partials/Profile/AddressNew");
                case "fini":
                    return PartialView("Partials/Financial/FinancialInfo", new FinancialInfoModel()
                    {
                        TaxRefNo = "2378724",
                        WriteOffDate = "01/01/2017"
                    });
                case "finps":
                    return PartialView("Partials/Financial/FinancialPosition");
                case "crda":
                    return PartialView("Partials/Financial/Creditability", new CreditAssessmentModel());
                case "secdeptlst":
                    return PartialView("Partials/Financial/SecurityDepositList");
                case "secdeptnew":
                    return PartialView("Partials/Financial/SecurityDepositNew", new CreditAssessmentModel());
                case "secdeptd":
                    return PartialView("Partials/Financial/SecurityDepositDetail", new CreditAssessmentModel());
                case "pdp":
                    return PartialView("Partials/PricingDetails/ProductPrice");
                case "pdpd":
                    return PartialView("Partials/PricingDetails/ProductPriceDetail");
                case "pdpnew":
                    return PartialView("Partials/PricingDetails/ProductPriceNew");
                case "dnr":
                    return PartialView("Partials/PricingDetails/DiscountRebate");
                case "dnrd":
                    return PartialView("Partials/PricingDetails/DiscountRebateDetail");
                case "dnrnew":
                    return PartialView("Partials/PricingDetails/DiscountRebateNew");
                case "pacpt":
                    return PartialView("Partials/UsageControl/ProductAcceptance");
                //case "pretrd":
                //    return PartialView("Partials/UsageControl/ProductRestrictionsDetail", new ProductRestrictionsModel()
                //    {
                //        ProductGroup = "001",
                //        TotalCountLimit = "50",
                //        TotalAmountLimit = "50",
                //        TotalVolumeLimit = "80",
                //        AccountLimitTnx = "20",
                //        VolumeLimitTnx = "25"
                //    });
                //case "pretrnew":
                //    return PartialView("Partials/UsageControl/ProductRestrictionsNew");
                case "vellmt":
                    return PartialView("Partials/UsageControl/VelocityLimit");
                case "locacpt":
                    return PartialView("Partials/UsageControl/LocationAcceptance");
                //case "state":
                //    return PartialView("Partials/UsageControl/StationDealer");
                case "cstctr":
                    return PartialView("Partials/CostCentre/CostCentre");
                case "cstctrd":
                    return PartialView("Partials/CostCentre/CostCentreDetail");
                case "cstctrnew":
                    return PartialView("Partials/CostCentre/CostCentreNew");
                case "ccpretr":
                    return PartialView("Partials/CostCentre/ProductRestrictions");
                case "ccpretrd":
                    return PartialView("Partials/CostCentre/ProductRestrictionsDetail");
                case "ccpretrnew":
                    return PartialView("Partials/CostCentre/ProductRestrictionsNew");
                case "ccvellmt":
                    return PartialView("Partials/CostCentre/VelocityLimit");
                case "cclocacpt":
                    return PartialView("Partials/CostCentre/LocationAcceptance");
                case "ccstate":
                    return PartialView("Partials/CostCentre/StationDealer");
                case "allcards":
                    return PartialView("Partials/Card/AllCardByAllCus");
                    
                case "crdlst":
                    return PartialView("Partials/Card/Cardlist");
                case "newcrd":
                    return PartialView("Partials/Card/CreateCards");
                //case "crdd":
                //    return PartialView("Partials/Card/CardDetails");
                case "evntntf":
                    return PartialView("Partials/Admin/EventNotification");
                case "evntntfd":
                    return PartialView("Partials/Admin/EventNotificationDetail");
                case "evntntfnew":
                    return PartialView("Partials/Admin/EventNotificationNew");
                case "fmgr":
                    return PartialView("Partials/Admin/FileManager");
                case "txn":
                    return PartialView("Partials/Transactions/Transaction");
                default:
                    return PartialView();
            }
        }
        #endregion

        #region Customers
        public async Task<ActionResult> GetSearchCustomers(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<CustomersModel>();

            var list = new List<CustomersModel>() {
                new CustomersModel(){AcctNo = "7438743", CmpyName = "HSBC Sentul", AcctSts="Active", PlasticType = "Postpaid", AcctClass="VIP", ApprovedOn="22/03/2016", CreatedOn="11/03/2015"},
                new CustomersModel(){AcctNo = "7483838", CmpyName = "HSBC Kepong", AcctSts="Blocked", PlasticType = "Prepaid", AcctClass="Normal", ApprovedOn="23/06/2017", CreatedOn="21/11/2016"},
                new CustomersModel(){AcctNo = "7483840", CmpyName = "HSBC KL", AcctSts="Suspended", PlasticType = "Postpaid", AcctClass="VIP", ApprovedOn="27/10/2015", CreatedOn="04/11/2014"},
                new CustomersModel(){AcctNo = "7483841", CmpyName = "HSBC Selangor", AcctSts="Closed", PlasticType = "Prepaid", AcctClass="Normal", ApprovedOn="24/12/2016", CreatedOn="30/03/2015"},
                new CustomersModel(){AcctNo = "7454757", CmpyName = "Maybank Selangor", AcctSts="Active", PlasticType = "Postpaid", AcctClass="Normal", ApprovedOn="26/12/2016", CreatedOn="25/08/2015"},
                new CustomersModel(){AcctNo = "9048548", CmpyName = "CIMB Selangor", AcctSts="Closed", PlasticType = "Prepaid", AcctClass="Normal", ApprovedOn="24/11/2016", CreatedOn="10/09/2015"},
                new CustomersModel(){AcctNo = "7845745", CmpyName = "CIMB KL", AcctSts="Active", PlasticType = "Postpaid", AcctClass="VIP", ApprovedOn="24/12/2017", CreatedOn="11/12/2016"},
                new CustomersModel(){AcctNo = "1039434", CmpyName = "UOB Selangor", AcctSts="Active", PlasticType = "Postpaid", AcctClass="Normal", ApprovedOn="14/12/2016", CreatedOn="10/03/2015"},
                new CustomersModel(){AcctNo = "8596595", CmpyName = "Maybank KL", AcctSts="Closed", PlasticType = "Prepaid", AcctClass="VIP", ApprovedOn="24/10/2016", CreatedOn="11/02/2015"},
                new CustomersModel(){AcctNo = "9549599", CmpyName = "Maybank Selangor", AcctSts="Active", PlasticType = "Postpaid", AcctClass="Normal", ApprovedOn="10/12/2016", CreatedOn="10/03/2015"},
                new CustomersModel(){AcctNo = "8399483", CmpyName = "HSBC Selangor", AcctSts="Active", PlasticType = "Postpaid", AcctClass="Normal", ApprovedOn="10/05/2017", CreatedOn="11/11/2016"},
                new CustomersModel(){AcctNo = "9489548", CmpyName = "Maybank Selangor", AcctSts="Active", PlasticType = "Prepaid", AcctClass="VIP", ApprovedOn="15/11/2017", CreatedOn="19/10/2016"}
            };

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            switch (Params.iSortCol_0)
            {
                case 1:
                    if (Params.sSortDir_0 == "asc")
                        _filtered = list.OrderBy(x => x.AcctNo).ToList();
                    if (Params.sSortDir_0 == "desc")
                        _filtered = list.OrderByDescending(x => x.AcctNo).ToList();
                    break;
                case 2:
                    if (Params.sSortDir_0 == "asc")
                        _filtered = list.OrderBy(x => x.CmpyName).ToList();
                    if (Params.sSortDir_0 == "desc")
                        _filtered = list.OrderByDescending(x => x.CmpyName).ToList();
                    break;
                case 3:
                    if (Params.sSortDir_0 == "asc")
                        _filtered = list.OrderBy(x => x.AcctSts).ToList();
                    if (Params.sSortDir_0 == "desc")
                        _filtered = list.OrderByDescending(x => x.AcctSts).ToList();
                    break;
                case 4:
                    if (Params.sSortDir_0 == "asc")
                        _filtered = list.OrderBy(x => x.PlasticType).ToList();
                    if (Params.sSortDir_0 == "desc")
                        _filtered = list.OrderByDescending(x => x.PlasticType).ToList();
                    break;
                case 5:
                    if (Params.sSortDir_0 == "asc")
                        _filtered = list.OrderBy(x => x.AcctClass).ToList();
                    if (Params.sSortDir_0 == "desc")
                        _filtered = list.OrderByDescending(x => x.AcctClass).ToList();
                    break;
                case 6:
                    if (Params.sSortDir_0 == "asc")
                        _filtered = list.OrderBy(x => x.ApprovedOn).ToList();
                    if (Params.sSortDir_0 == "desc")
                        _filtered = list.OrderByDescending(x => x.ApprovedOn).ToList();
                    break;
                case 7:
                    if (Params.sSortDir_0 == "asc")
                        _filtered = list.OrderBy(x => x.CreatedOn).ToList();
                    if (Params.sSortDir_0 == "desc")
                        _filtered = list.OrderByDescending(x => x.CreatedOn).ToList();
                    break;
                default:
                    _filtered = list;
                    break;
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = _filtered.Where(p => p.AcctNo.ToString().Contains(Params.sSearch) || p.CmpyName.ToLower().Contains(Params.sSearch) || p.AcctSts.ToString().Contains(Params.sSearch) || p.PlasticType.ToLower().Contains(Params.sSearch) || p.AcctClass.ToLower().Contains(Params.sSearch) || p.ApprovedOn.Contains(Params.sSearch)).ToList();

            }

            _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.AcctNo, x.CmpyName, x.AcctSts, x.PlasticType, x.AcctClass, x.ApprovedOn, x.CreatedOn })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Customer Details
        public async Task<ActionResult> GetAcctStatusHis(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<AccountStatusChange>();

            var list = new List<AccountStatusChange>() {
                new AccountStatusChange(){ChangedFrom = "Suspended", ChangedTo = "Active", ChangeReason = "Override Account Status", ChangedOn="31/12/2017 12:04:45 PM", ChangedBy="user02"},
                new AccountStatusChange(){ChangedFrom = "Terminated", ChangedTo = "Suspended", ChangeReason = "Suspect Fraud", ChangedOn="16/01/2017 10:23:45 AM", ChangedBy="user01"},
                new AccountStatusChange(){ChangedFrom = "Suspended", ChangedTo = "Terminated", ChangeReason = "Fraud", ChangedOn="10/01/2017 11:32:15 AM", ChangedBy="user03"},
            };

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            //if (!string.IsNullOrEmpty(Params.sSearch))
            //{
            //    _filtered = list.Where(p => p.RefKey.ToString().Contains(Params.sSearch) || p.CmpyName1.ToLower().Contains(Params.sSearch) || p.TaskNo.ToString().Contains(Params.sSearch) || p.Priority.ToLower().Contains(Params.sSearch) || p.Sts.ToLower().Contains(Params.sSearch) || p.CreateDateStr.Contains(Params.sSearch)).ToList();
            //    _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            //}
            //else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select(x => new object[] { x.ChangedFrom, x.ChangedTo, x.ChangeReason, x.ChangedOn, x.ChangedBy })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Financial Info
        public async Task<ActionResult> GetInstantTxn(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<TransactionModel>();

            var list = new List<TransactionModel>() {
                new TransactionModel(){TxnNo="3892343", CardNo="839843547584758", TxnAmt="100.00", TxnDate="01/02/2017", BookingDate="11/12/2017", DueDate="23/12/2017", NameOnCard="Alex Tan", DriverCardNo="58498549894", TxnCategory="Rebate", TxnDesc="Payment", ReceiptNo="4954854845", CreateOn="11/11/2015", CreatedBy="cts01"},
                new TransactionModel(){TxnNo="7584545", CardNo="898545748548584", TxnAmt="100.00", TxnDate="03/02/2017", BookingDate="11/12/2017", DueDate="23/12/2017", NameOnCard="Seng", DriverCardNo="434354543", TxnCategory="Rebate", TxnDesc="Payment", ReceiptNo="3243445545", CreateOn="11/11/2015", CreatedBy="cts01"},
                new TransactionModel(){TxnNo="4839854", CardNo="483945984958945", TxnAmt="100.00", TxnDate="03/03/2017", BookingDate="11/12/2017", DueDate="23/12/2017", NameOnCard="Yong Kim", DriverCardNo="676343565634", TxnCategory="Rebate", TxnDesc="Payment", ReceiptNo="766545446", CreateOn="11/11/2015", CreatedBy="cts01"},
            };

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            //if (!string.IsNullOrEmpty(Params.sSearch))
            //{
            //    _filtered = list.Where(p => p.RefKey.ToString().Contains(Params.sSearch) || p.CmpyName1.ToLower().Contains(Params.sSearch) || p.TaskNo.ToString().Contains(Params.sSearch) || p.Priority.ToLower().Contains(Params.sSearch) || p.Sts.ToLower().Contains(Params.sSearch) || p.CreateDateStr.Contains(Params.sSearch)).ToList();
            //    _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            //}
            //else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.TxnNo, x.CardNo, x.TxnAmt, x.TxnDate, x.BookingDate, x.DueDate, x.NameOnCard, x.DriverCardNo, x.TxnCategory, x.TxnDesc, x.ReceiptNo, x.CreateOn, x.CreatedBy })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetOnlineTxn(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<TransactionModel>();

            var list = new List<TransactionModel>() {
                new TransactionModel(){TxnNo="3892343", CardNo="839843547584758", TxnAmt="50.00", TxnDate="01/02/2017", BookingDate="11/12/2017", DueDate="23/11/2016", NameOnCard="Alex Tan", DriverCardNo="58498549894", TxnCategory="Top Up", TxnDesc="Top Up", ReceiptNo="4954854845", CreatedBy="cts02", CreateOn="11/11/2015", InvoiceNo="84958454", Status="Approved", MerchantNo="787548", SiteID="43490", TerminalID="785854", MTI="300"},
                new TransactionModel(){TxnNo="7584545", CardNo="898545748548584", TxnAmt="50.00", TxnDate="03/02/2017", BookingDate="11/12/2017", DueDate="23/11/2016", NameOnCard="Seng", DriverCardNo="434354543", TxnCategory="Top Up", TxnDesc="Top Up", ReceiptNo="3243445545", CreatedBy="cts01", CreateOn="11/11/2015", InvoiceNo="84958454", Status="Approved", MerchantNo="34343", SiteID="5454", TerminalID="76734", MTI="400"},
                new TransactionModel(){TxnNo="4839854", CardNo="483945984958945", TxnAmt="50.00", TxnDate="03/03/2017", BookingDate="11/12/2017", DueDate="23/11/2016", NameOnCard="Yong Kim", DriverCardNo="676343565634", TxnCategory="Top Up", TxnDesc="Top Up", ReceiptNo="766545446", CreatedBy="cts01", CreateOn="11/11/2015", InvoiceNo="84958454", Status="Pending", MerchantNo="23454", SiteID="78545", TerminalID="6563", MTI="200"},
            };

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            //if (!string.IsNullOrEmpty(Params.sSearch))
            //{
            //    _filtered = list.Where(p => p.RefKey.ToString().Contains(Params.sSearch) || p.CmpyName1.ToLower().Contains(Params.sSearch) || p.TaskNo.ToString().Contains(Params.sSearch) || p.Priority.ToLower().Contains(Params.sSearch) || p.Sts.ToLower().Contains(Params.sSearch) || p.CreateDateStr.Contains(Params.sSearch)).ToList();
            //    _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            //}
            //else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.TxnNo, x.CardNo, x.TxnAmt, x.TxnDate, x.InvoiceNo, x.Status, x.NameOnCard, x.TxnCategory, x.TxnDesc, x.MerchantNo, x.SiteID, x.TerminalID, x.MTI })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetBatchTxn(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<TransactionModel>();

            var list = new List<TransactionModel>() {
                new TransactionModel(){TxnNo="3892343", CardNo="839843547584758", TxnAmt="10.00", TxnDate="01/02/2017", BookingDate="11/12/2017", DueDate="23/12/2017", NameOnCard="Alex Tan", DriverCardNo="58498549894", TxnCategory="Charge", TxnDesc="Card Replacement Penalty", ReceiptNo="4954854845", CreatedBy="cts01", CreateOn="11/11/2015"},
                new TransactionModel(){TxnNo="7584545", CardNo="898545748548584", TxnAmt="10.00", TxnDate="03/02/2017", BookingDate="11/12/2017", DueDate="23/12/2017", NameOnCard="Seng", DriverCardNo="434354543", TxnCategory="Charge", TxnDesc="Card Replacement Penalty", ReceiptNo="3243445545", CreatedBy="cts01", CreateOn="11/11/2015"},
                new TransactionModel(){TxnNo="4839854", CardNo="483945984958945", TxnAmt="10.00", TxnDate="03/03/2018", BookingDate="11/12/2017", DueDate="23/12/2017", NameOnCard="Yong Kim", DriverCardNo="676343565634", TxnCategory="Charge", TxnDesc="Card Replacement Penalty", ReceiptNo="766545446", CreatedBy="cts01", CreateOn="11/11/2015"},
            };

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            //if (!string.IsNullOrEmpty(Params.sSearch))
            //{
            //    _filtered = list.Where(p => p.RefKey.ToString().Contains(Params.sSearch) || p.CmpyName1.ToLower().Contains(Params.sSearch) || p.TaskNo.ToString().Contains(Params.sSearch) || p.Priority.ToLower().Contains(Params.sSearch) || p.Sts.ToLower().Contains(Params.sSearch) || p.CreateDateStr.Contains(Params.sSearch)).ToList();
            //    _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            //}
            //else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.TxnNo, x.CardNo, x.TxnAmt, x.TxnDate, x.BookingDate, x.DueDate, x.NameOnCard, x.TxnCategory, x.TxnDesc, x.CreateOn, x.CreatedBy })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetOfflineTxn(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<TransactionModel>();

            var list = new List<TransactionModel>() {
                new TransactionModel(){TxnNo="3892343", CardNo="839843547584758", TxnAmt="20.00", TxnDate="01/02/2017", BookingDate="11/12/2017", DueDate="23/12/2017", NameOnCard="Alex Tan", DriverCardNo="58498549894", TxnCategory="Sales", TxnDesc="Merchant Retail Sales", ReceiptNo="4954854845", CreatedBy="cts01", CreateOn="11/11/2015"},
                new TransactionModel(){TxnNo="7584545", CardNo="898545748548584", TxnAmt="20.00", TxnDate="03/02/2017", BookingDate="11/12/2017", DueDate="23/12/2017", NameOnCard="Seng", DriverCardNo="434354543", TxnCategory="Sales", TxnDesc="Merchant Retail Sales", ReceiptNo="3243445545", CreatedBy="cts01", CreateOn="11/11/2015"},
                new TransactionModel(){TxnNo="4839854", CardNo="483945984958945", TxnAmt="20.00", TxnDate="03/03/2018", BookingDate="11/12/2017", DueDate="23/12/2017", NameOnCard="Yong Kim", DriverCardNo="676343565634", TxnCategory="Sales", TxnDesc="Merchant Retail Sales", ReceiptNo="766545446", CreatedBy="cts01", CreateOn="11/11/2015"},
            };

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            //if (!string.IsNullOrEmpty(Params.sSearch))
            //{
            //    _filtered = list.Where(p => p.RefKey.ToString().Contains(Params.sSearch) || p.CmpyName1.ToLower().Contains(Params.sSearch) || p.TaskNo.ToString().Contains(Params.sSearch) || p.Priority.ToLower().Contains(Params.sSearch) || p.Sts.ToLower().Contains(Params.sSearch) || p.CreateDateStr.Contains(Params.sSearch)).ToList();
            //    _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            //}
            //else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.TxnNo, x.CardNo, x.TxnAmt, x.TxnDate, x.DueDate, x.ReceiptNo, x.NameOnCard, x.TxnCategory, x.TxnDesc, x.CreateOn, x.CreatedBy })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCreditAssessment(string UserId)
        {

            CreditAssessmentModel model = new CreditAssessmentModel();
            model.CrdLmt = 4300.25m;
            model.CrdRskCat = "0";
            model.TmpCrdLmtFrom = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
            model.TmpCrdLmtTo = DateTime.Now.ToString("dd/MM/yyyy");
            model.ExpDate = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
            model.NxtAssessmentDate = DateTime.Now.ToString("dd/MM/yyyy");
            model.Remarks = "Provide detail here";
            model.PymtMode = "0";
            model.PymtTerms = "0";
            model.CrdAllowanceFactor = 30;
            model.SalesTerritory = "0";

            model.ReqSecurityDeposit = true;
            var CrdRskCatLst = new List<SelectListItem>()
                {
                    new SelectListItem(){Value="0", Text="Low"},
                    new SelectListItem(){Value="1", Text="Medium"},
                    new SelectListItem(){Value="2", Text="High"}
                };

            var pymtModeLst = new List<SelectListItem>()
            {
                new SelectListItem(){Value="0", Text="Bank Transfer"},
            };

            var pymtTermLst = new List<SelectListItem>()
            {
                new SelectListItem(){Value="0", Text="3 Months"},
            };

            var salesTerritoryLst = new List<SelectListItem>()
            {
                new SelectListItem(){Value="0", Text="Central"},
            };

            // get dropdown list
            var Selects = new CreditAssessmentModel
            {
                CrdRskCatLst = CrdRskCatLst,
                DepositTypeLst = new List<SelectListItem>(),
                PymtModeLst = pymtModeLst,
                PymtTermsLst = pymtTermLst,
                SalesTerritoryLst = salesTerritoryLst,
            };

            return Json(new { Model = model, Selects = Selects }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetCreditLimitHistory(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<CrdAssHisModel>();

            var list = new List<CrdAssHisModel>() {
                new CrdAssHisModel(){Type="Temporary Credit Limit", ChangedFrom="65000.00", ChangedTo="48000.00", ExpiredDate="-", ValidityPeriod="12/12/2015 to 14/12/2018", ChangedOn="10/12/2016 03:13:46", ChangedBy="user01"},
                new CrdAssHisModel(){Type="New Credit Limit", ChangedFrom="55000.00", ChangedTo="65000.00", ExpiredDate="31/12/2019", ValidityPeriod="-", ChangedOn="11/05/2016 12:11:23", ChangedBy="user02"},
                new CrdAssHisModel(){Type="New Credit Limit", ChangedFrom="50000.00", ChangedTo="55000.00", ExpiredDate="31/12/2019", ValidityPeriod="-", ChangedOn="11/12/2015 11:33:51", ChangedBy="user01"},
            };

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            //if (!string.IsNullOrEmpty(Params.sSearch))
            //{
            //    _filtered = list.Where(p => p.RefKey.ToString().Contains(Params.sSearch) || p.CmpyName1.ToLower().Contains(Params.sSearch) || p.TaskNo.ToString().Contains(Params.sSearch) || p.Priority.ToLower().Contains(Params.sSearch) || p.Sts.ToLower().Contains(Params.sSearch) || p.CreateDateStr.Contains(Params.sSearch)).ToList();
            //    _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            //}
            //else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select((x, index) => new object[] {Params.iDisplayStart + index + 1, x.Type, x.ChangedFrom, x.ChangedTo, x.ChangedOn, x.ChangedBy })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetSecurityDepositLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<SecurityDepositModel>() { 
                new SecurityDepositModel(){Amt="5,000.00", Type="Bank Guarantee", ExpDate="11/2/2017", BankName="Maybank", CreatedOn="11/2/2016", CreatedBy="user02"}
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.Amt, x.Type, x.ExpDate, x.BankName, x.CreatedOn, x.CreatedBy })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetSecurityDepositHis(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<SecurityDepositHisModel>() { 
                new SecurityDepositHisModel(){Type="Bank Guarantee", ChangedFrom="11/12/2017", ChangedTo="11/1/2017", ChangedOn="11/1/2018", ChangedBy="user02"}
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.Type, x.ChangedFrom, x.ChangedTo, x.ChangedOn, x.ChangedBy })
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Pricing
        public async Task<ActionResult> GetProductPriceLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<ProductPriceModel>() { 
                new ProductPriceModel(){ProductCode="1001", ProductName="PRIMAX 92", ProductCat="Fuel", ProductType="Diesel", PricingModel="Price 1", CreatedOn="01/03/2017", CreatedBy="user01"},
                new ProductPriceModel(){ProductCode="1002", ProductName="PRIMAX 92", ProductCat="Fuel", ProductType="Diesel", PricingModel="Price 2", CreatedOn="01/12/2016", CreatedBy="user02"}
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.ProductCode, x.ProductName, x.ProductCat, x.ProductType, x.PricingModel, x.CreatedOn, x.CreatedBy })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetPricingModelLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<PricingModel>() { 
                new PricingModel(){UnitPrice="11.17", ValidityPeriod="11/02/2016 - 13/05/2017"}
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { x.UnitPrice, x.ValidityPeriod, "Edit", "Delete" })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetPricingModelHisLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<PricingModel>() { 
                new PricingModel(){UnitPrice="11.17", ValidityPeriod="11/02/2016 - 13/05/2017", CreatedOn="11/02/2011", CreatedBy="user01"}
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.UnitPrice, x.ValidityPeriod, x.CreatedOn, x.CreatedBy })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Discount and Debate
        //public ActionResult DiscountRebate()
        //{
        //    ViewBag.PgCd = "cus-all-pri-dnr";
        //    return View();
        //}

        //public ActionResult DiscountRebateDetail()
        //{
        //    ViewBag.PgCd = "cus-all-pri-dnr";
        //    return View();
        //}

        //public ActionResult DiscountRebateNew()
        //{
        //    ViewBag.PgCd = "cus-all-pri-dnr";
        //    return View();
        //}

        public async Task<ActionResult> GetDiscountRebateLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<DiscountRebateModel>() { 
                new DiscountRebateModel(){PlanID="001", PlanTitle="Plan 01", PlanType="Volume Based", ValidityPeriod="01/02/2014 to 02/02/2017", CreatedOn="01/03/2017", CreatedBy="user01"},
                new DiscountRebateModel(){PlanID="002", PlanTitle="Plan 02", PlanType="Volume Based", ValidityPeriod="01/02/2014 to 02/02/2017", CreatedOn="01/03/2017", CreatedBy="user01"}
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.PlanID, x.PlanTitle, x.PlanType, x.ValidityPeriod, x.CreatedOn, x.CreatedBy })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetValueSettingsLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<ValueDiscountRebateModel>() { 
                new ValueDiscountRebateModel(){TierValue="99,999,999.00", BasicValue="1.0", BilledValue="1.0", UpdatedOn="02/03/2017", UpdatedBy="user01"}
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { x.TierValue, x.BasicValue, x.BilledValue, x.UpdatedOn, x.UpdatedBy })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Product Group
        //public async Task<ActionResult> GetProductGrpHisLst(jQueryDataTableParamModel Params)
        //{
        //    var _filtered = new List<ProductGroupHistoryModel>() { 
        //        new ProductGroupHistoryModel(){ ProductGroup="001 Petrol & Diesel only", Action="Add", DoneOn="01/12/2017", DoneBy="user01" },
        //        new ProductGroupHistoryModel(){ ProductGroup="010 Service only", Action="Delete", DoneOn="01/11/2016", DoneBy="user01" }
        //    };
        //    return Json(new
        //    {
        //        sEcho = Params.sEcho,
        //        iTotalRecords = 1,
        //        iTotalDisplayRecords = 1,
        //        aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.ProductGroup, x.Action, x.DoneOn, x.DoneBy })
        //    }, JsonRequestBehavior.AllowGet);
        //}

        public async Task<JsonResult> GetProductRestrictions(string UserId)
        {
            ProductRestrictionsModel model = new ProductRestrictionsModel(){ProductGroup="001", TotalCountLimit="11", TotalAmountLimit="55", TotalVolumeLimit="89", CreatedOn="01/03/2017", CreatedBy="user01"};
            
            // get dropdown list
            var Selects = new ProductRestrictionsModel
            {
            };

            return Json(new { Model = model, Selects = Selects }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Location Acceptance
        //public async Task<ActionResult> GetLocationAcceptanceLst(jQueryDataTableParamModel Params)
        //{
        //    var _filtered = new List<LocationAcceptanceModel>() { 
        //        new LocationAcceptanceModel(){State="Selangor", CreatedOn="01/03/2017", CreatedBy="user01"},
        //        new LocationAcceptanceModel(){State="Johor", CreatedOn="01/03/2017", CreatedBy="user01"},
        //    };
        //    return Json(new
        //    {
        //        sEcho = Params.sEcho,
        //        iTotalRecords = 1,
        //        iTotalDisplayRecords = 1,
        //        aaData = _filtered.Select((x, index) => new object[] {"", Params.iDisplayStart + index + 1, x.State, "View list", x.CreatedOn, x.CreatedBy })
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //public async Task<ActionResult> GetStationLst(jQueryDataTableParamModel Params)
        //{
        //    var _filtered = new List<StationDealerModel>() { 
        //        new StationDealerModel(){State="Selangor", LocationNo="7478438", RefNo="R2SL_KLNG_J1", LocationName="PSS Batu Meru", BusinessName="Nadi SB"},
        //        new StationDealerModel(){State="Selangor", LocationNo="7467455", RefNo="R2SL_KLNG_A", LocationName="PSS Dm SB", BusinessName="Kamalia SB"},
        //    };
        //    return Json(new
        //    {
        //        sEcho = Params.sEcho,
        //        iTotalRecords = 1,
        //        iTotalDisplayRecords = 1,
        //        aaData = _filtered.Select((x, index) => new object[] { "", Params.iDisplayStart + index + 1, x.State, x.LocationNo, x.LocationName, x.BusinessName, x.RefNo })
        //    }, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Cost Centre
        public async Task<ActionResult> GetCostCentreLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<CostCentreModel>() { 
                new CostCentreModel(){CostCenterCode="1001", CostCenterName="Cost Centre A", PersonInCharge="Amy Tan", CreatedOn="01/03/2017", CreatedBy="user01", UpdatedOn="02/04/2017", UpdatedBy="user02"},
                new CostCentreModel(){CostCenterCode="1002", CostCenterName="Cost Centre B", PersonInCharge="Siti Zaleha", CreatedOn="01/03/2017", CreatedBy="user02", UpdatedOn="05/06/2017", UpdatedBy="user02"},
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.CostCenterCode, x.CostCenterName, x.PersonInCharge, x.CreatedOn, x.CreatedBy, x.UpdatedOn, x.UpdatedBy })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Product Acceptance
        public async Task<ActionResult> GetProductItemsByGrp(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<ProductAcceptanceItemModel>() { 
                    new ProductAcceptanceItemModel(){ ProductCode="204", ProductName="Petrol 97", ProductCategory="Fuel", ProductType="Premium Petrol" },
                    new ProductAcceptanceItemModel(){ ProductCode="203", ProductName="Petrol 95", ProductCategory="Fuel", ProductType="Normal Petrol" },
                    new ProductAcceptanceItemModel(){ ProductCode="202", ProductName="Diesel #2", ProductCategory="Fuel", ProductType="Dynamic Diesel" },
                    new ProductAcceptanceItemModel(){ ProductCode="201", ProductName="Diesel #1", ProductCategory="Fuel", ProductType="Dynamic Diesel" }
                };

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.ProductCode, x.ProductName, x.ProductCategory, x.ProductType })
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetProductRestrictionsLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<ProductRestrictionsModel>() { 
                new ProductRestrictionsModel(){ProductGroup="001", TotalCountLimit="11", TotalAmountLimit="55", TotalVolumeLimit="89", CreatedOn="01/03/2017", CreatedBy="user01"},
                new ProductRestrictionsModel(){ProductGroup="002", TotalCountLimit="54", TotalAmountLimit="76", TotalVolumeLimit="233", CreatedOn="01/03/2017", CreatedBy="user01"}
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.ProductGroup, x.TotalCountLimit, x.TotalAmountLimit, x.TotalVolumeLimit, x.CreatedOn, x.CreatedBy })
            }, JsonRequestBehavior.AllowGet);
        }

        //public async Task<JsonResult> GetProductRestrictions(string UserId)
        //{
        //    ProductRestrictionsModel model = new ProductRestrictionsModel() { ProductGroup = "001", TotalCountLimit = "11", TotalAmountLimit = "55", TotalVolumeLimit = "89", CreatedOn = "01/03/2017", CreatedBy = "user01" };

        //    // get dropdown list
        //    var Selects = new ProductRestrictionsModel
        //    {
        //    };

        //    return Json(new { Model = model, Selects = Selects }, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Cards
        public async Task<ActionResult> GetPendingCards(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<CardListModel>() {
                new CardListModel(){CardCreationRefNo="150020", ActNo="9894934943", CardType="Standalone", CardMediaType="Magnetic Stripe", NameOnCard="Macy Brown", CardStatus="Pending"},
                new CardListModel(){CardCreationRefNo="150019", ActNo="9894934943", CardType="Standalone", CardMediaType="Magnetic Stripe", NameOnCard="Hassan Ali", CardStatus="Rejected"},
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.CardCreationRefNo, x.ActNo, x.CardType, x.CardMediaType, x.NameOnCard, x.CardStatus })
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetCreatedCards(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<CardListModel>() { 
                new CardListModel(){CardCreationRefNo="150020", CardNo="738278", ActNo="9894934943",CardType="Standalone", CardMediaType="Magnetic Stripe", NameOnCard="Alex Tan", CardStatus="Terminated"},
                new CardListModel(){CardCreationRefNo="150019", CardNo="70119", ActNo="9894934943",CardType="Standalone", CardMediaType="Magnetic Stripe", NameOnCard="Rowan King", CardStatus="Suspended"},
                new CardListModel(){CardCreationRefNo="150001", CardNo="70001", ActNo="9894934943",CardType="Standalone", CardMediaType="Magnetic Stripe", NameOnCard="Lisa S.", CardStatus="Active"},
                new CardListModel(){CardCreationRefNo="150033", CardNo="70970", ActNo="9894934943",CardType="Standalone", CardMediaType="Magnetic Stripe", NameOnCard="Amy Mastura", CardStatus="Active"}
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.CardCreationRefNo, x.CardNo, x.ActNo, x.CardType, x.CardMediaType, x.NameOnCard, x.CardStatus })
            }, JsonRequestBehavior.AllowGet);
        }

        //public async Task<ActionResult> GetAllCards(jQueryDataTableParamModel Params)
        //{
        //    var _filtered = new List<CardListModel>() { 
        //        new CardListModel(){CardNo="-", TempCardNo="1398943", ActNo="92203924", CardType="Standalone", NameOnCard="Alice Tan", CardStatus="Pending", CardExpiryDate="03/04/2017", CreatedOn="01/03/2017", CreatedBy="user01"},
        //        new CardListModel(){CardNo="2988493437", TempCardNo="7458745", ActNo="38998899", CardType="Gift Card", NameOnCard="Sarah", CardStatus="Approved", CardExpiryDate="03/04/2011", CreatedOn="01/03/2017", CreatedBy="user02"},
        //    };
        //    return Json(new
        //    {
        //        sEcho = Params.sEcho,
        //        iTotalRecords = 1,
        //        iTotalDisplayRecords = 1,
        //        aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.CardNo, x.TempCardNo, x.ActNo, x.NameOnCard, x.CardType, x.CardStatus, x.CreatedOn })
        //    }, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Event & Notification
        public async Task<ActionResult> GetEventNotificationLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<EventNotificationModel>() { 
                new EventNotificationModel(){EventID="1001", EventTitle="Account Overdue", Severity="High", Status="Active", CreatedOn="01/03/2017", CreatedBy="user01"},
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.EventID, x.EventTitle, x.Severity, x.Status, x.CreatedOn, x.CreatedBy })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region File Manager
        public async Task<ActionResult> GetFileManagerLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<FileUploadModel>() { 
                new FileUploadModel(){FileName = "BankGuarantee_22032017", UploadedBy="user01", UploadedOn="11/05/2011", FileSize="5 KB", FileType=".pdf"},
                new FileUploadModel(){FileName = "LetterOfOffer_22032017", UploadedBy="user02", UploadedOn="12/05/2011", FileSize="13 KB", FileType=".pdf"},
                new FileUploadModel(){FileName = "TAHDKK_378434", UploadedBy="user02", UploadedOn="15/05/2007", FileSize="13 KB", FileType=".png"},
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { "", Params.iDisplayStart + index + 1, x.FileName, x.FileType, x.UploadedBy, x.UploadedOn, x.FileSize })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Transactions
        public async Task<ActionResult> GetTransactionsLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<TransactionModel>() { 
                new TransactionModel(){ TxnNo = "475848", CardNo="4835748843", TxnType="Batch Txn", TxnCategory="Payment", TxnDate="11/12/2017", TxnAmt="50.0", Status="Unbilled"},
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] {Params.iDisplayStart + index + 1, x.TxnNo, x.CardNo, x.TxnType, x.TxnCategory, x.TxnDate, x.TxnAmt })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetTransactionsPstedLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<TransactionModel>() { 
                new TransactionModel(){ TxnNo = "475848", CardNo="4835748843", TxnType="Batch Txn", TxnCategory="Payment", TxnDate="11/12/2017", TxnAmt="50.0", Status="Unbilled"},
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] { Params.iDisplayStart + index + 1, x.TxnNo, x.CardNo, x.TxnType, x.TxnCategory, x.TxnDate, x.TxnAmt, x.Status })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetTransactionItemLst(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<TransactionItemModel>() { 
                new TransactionItemModel(){ ProductCode="48383", ProductName="PRIMAX 92", ProductCategory="Fuel", ProductType="Diezel", PricingModel="Price 01", ProductQuantity="14.2", ProductAmt="83.00", TaxRate="6", TaxAmt="3.22", TaxCode="T6" },
            };
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = 1,
                iTotalDisplayRecords = 1,
                aaData = _filtered.Select((x, index) => new object[] {Params.iDisplayStart + index + 1, x.ProductCode, x.ProductName, x.ProductCategory, x.ProductType, x.PricingModel, x.ProductQuantity, x.ProductAmt, x.TaxRate, x.TaxAmt, x.TaxCode })
            }, JsonRequestBehavior.AllowGet);
        }
        
        #endregion
    }
}