
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroOil.LoyaltyOps.Models
{
    public class TerminalChangeSts
    {
        public string Ids { get; set; }

        public string Sts { get { return StsNew; } }
        public string ReasonCd { get { return StsNewReason; } }
        public string Descp { get { return StsNewRemarks; } }

        public string StsNewRemarks { get; set; }
        public string StsNew { get; set; }
        public string StsNewReason { get; set; }
    }
}
