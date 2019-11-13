using AutoMapper;
using MetroOil.LoyaltyOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MetroOil.LoyaltyOps.Controllers
{
    [Authorize(Roles = "Internal")]
    public class TasksController : Controller
    {
        // GET: Tasks
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
                case "mytsk":
                    return PartialView("Partials/MyTasks");
                default:
                    return PartialView();
            }
        }
        #endregion

        #region Transaction
        public async Task<ActionResult> GetTxns(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<TransactionModel>();

            var list = new List<TransactionModel>() {
                new TransactionModel(){TxnNo="3892343", TxnAmt="54.33", TxnDate="01/02/2017", CreatedBy="cts02", CreateOn="11/11/2015, 13:00:01", Priority="High", CurTskLvl="1"},
                new TransactionModel(){TxnNo="7584545", TxnAmt="100.33", TxnDate="03/02/2017", CreatedBy="cts01", CreateOn="01/03/2015, 01:02:00", Priority="Medium", CurTskLvl="0"},
                new TransactionModel(){TxnNo="4839854", TxnAmt="37.32", TxnDate="03/03/2018", CreatedBy="cts01", CreateOn="09/06/2017, 05:29:35", Priority="Low", CurTskLvl="2"},
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
                aaData = _filtered.Select((x, index) => new object[] {Params.iDisplayStart + index + 1, x.TxnNo, x.TxnDate, x.TxnAmt, x.Priority, x.CreateOn, x.CurTskLvl })
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}