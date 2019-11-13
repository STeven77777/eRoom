using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class PagingRequest
    {
        [Range(0, int.MaxValue)]
        public int PageNo { get; set; }
        [Range(0, int.MaxValue)]
        public int NoRecordPerPage { get; set; }
        public string SortColumnName { get; set; }
        public string SortDirection { get; set; }
        public string SearchText { get; set; }
    }
}
