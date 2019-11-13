using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models.Admin
{
    public class ChildTree
    {
        public string title { get; set; }
        public List<Child2Tree> children { get; set; }
        public bool? activate { get; set; }
        public bool? select { get; set; }
        public bool? hideCheckbox { get; set; }
        public bool? unselectable { get; set; }
        public ChildTree()
        {
            children = new List<Child2Tree>();
        }
    }
}