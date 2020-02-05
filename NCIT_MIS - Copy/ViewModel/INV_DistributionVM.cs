using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class INV_DistributionVM
    {
        public int DistributionItemId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int DistributionBillId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int UnitId { get; set; }
        public int RequestQuantity { get; set; }
        public string UnitName { get; set; }
        public decimal SalesQuantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public bool? IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }
    }
}






    