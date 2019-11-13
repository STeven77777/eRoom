using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models.Admin
{
    public class RootTreeObject
    {
        public string title { get; set; }
        public string tooltip { get; set; }
        public bool? select { get; set; }
        public bool? isFolder { get; set; }
        public string key { get; set; }
        public List<ChildTree> children { get; set; }
        public bool? expand { get; set; }
        public RootTreeObject()
        {
            children = new List<ChildTree>();
        }
    }
}