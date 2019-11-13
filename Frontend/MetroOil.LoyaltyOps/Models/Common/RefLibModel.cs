using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models
{
    public class RefLibModel
    {
        public string RefCd { get; set; }
        public string Descp { get; set; }
        public string CodeNDescp { get; set; }
    }


    public class RefLibResponse
    {


        #region Member Variables

        private string strIssNo = string.Empty;
        private string strRefType = string.Empty;
        private string strRefCd = string.Empty;
        private string intRefNo = string.Empty;
        private string strRefInd = string.Empty;
        private string strRefId = string.Empty;
        private string intMapInd = string.Empty;
        private string strDescp = string.Empty;

        #endregion


        #region Properties 

        /// <summary>
        /// IssNo
        /// 
        /// </summary>
        //[DisplayName("IssNo")]
        public string IssNo
        {
            get { return strIssNo; }
            set { strIssNo = value; }
        }

        /// <summary>
        /// RefType
        /// 
        /// </summary>
        //[DisplayName("RefType")]
        public string RefType
        {
            get { return strRefType; }
            set { strRefType = value; }
        }

        /// <summary>
        /// RefCd
        /// 
        /// </summary>
        //[DisplayName("RefCd")]
        //[Key]
        public string RefCd
        {
            get { return strRefCd; }
            set { strRefCd = value; }
        }

        /// <summary>
        /// RefNo
        /// 
        /// </summary>
        //[DisplayName("RefNo")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string RefNo
        {
            get { return intRefNo; }
            set { intRefNo = value; }
        }

        /// <summary>
        /// RefInd
        /// 
        /// </summary>
        //[DisplayName("RefInd")]
        public string RefInd
        {
            get { return strRefInd; }
            set { strRefInd = value; }
        }

        /// <summary>
        /// RefId
        /// 
        /// </summary>
        //[DisplayName("RefId")]
        public string RefId
        {
            get { return strRefId; }
            set { strRefId = value; }
        }

        /// <summary>
        /// MapInd
        /// 
        /// </summary>
        //[DisplayName("MapInd")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string MapInd
        {
            get { return intMapInd; }
            set { intMapInd = value; }
        }

        /// <summary>
        /// Descp
        /// 
        /// </summary>
        //[DisplayName("Descp")]
        public string Descp
        {
            get { return strDescp; }
            set { strDescp = value; }
        }


        #endregion


        #region Constructor

       
        #endregion



    }
}