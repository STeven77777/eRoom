using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models
{
    public class jQueryDataTableParamModel
    {//sColumns
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

        /// <summary>
        /// Sort column index
        /// </summary>
        public int iSortCol_0 { get; set; }

        /// <summary>
        /// Sort column type
        /// </summary>
        public string sSortDir_0 { get; set; }

        public bool AllowPaging { get; set; }

        public string  SortColumnName { get; set; }

        // MAP TO API
        public int PageNo
        {
            get
            {
                if (iDisplayLength > 0)
                {
                    return (iDisplayStart / iDisplayLength) + 1;
                }
                return 1;
            }
        }
        public int NoRecordPerPage { get { return iDisplayLength; } }
        public int SortColumnIndex { get { return iSortCol_0; } }
        public string SortDirection { get { return sSortDir_0; } }
        public string SearchText { get { return sSearch; } }

        public jQueryDataTableParamModel()
        {
            AllowPaging = true;
        }
    }
}