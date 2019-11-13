using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroOil.LoyaltyOps.Models.Admin
{
    public class MatrixPage
    {
        public long PageId { get; set; }
        public bool GroupStatus { get; set; }
    }

    public class UserGroupMatrix
    {
        public string UserGroupCd { get; set; }
        public string UserGroupName { get; set; }
        public string Sts { get; set; }
        public List<UserMatrixPage> MatrixTree { get; set; }
        public List<MatrixPage> Matrix
        {
            get
            {
                if (MatrixTree != null && MatrixTree.Any())
                {
                    return GetMatrix(MatrixTree);
                }
                return new List<MatrixPage>();
            }
        }

        public UserGroupMatrix()
        {
        }

        public List<MatrixPage> GetMatrix(List<UserMatrixPage> tree)
        {
            var result = new List<MatrixPage>();

            foreach(var item in tree)
            {
                result.Add(new MatrixPage() {
                    PageId = item.PageId,
                    GroupStatus = item.GroupStatus
                });

                if(item.SubPages != null && item.SubPages.Any())
                {
                    result.AddRange(GetMatrix(item.SubPages));
                }
            }

            return result;
        }
    }
}