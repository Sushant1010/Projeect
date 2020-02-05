using System;



using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class INV_ItemVM
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public int CategoryId { get; set; }

        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal? SellingPrice { get; set; }

        

        public int? QuantityPer { get; set; }

        public string ItemCode { get; set; }

        public int WarrantyDuration { get; set; }

        public bool? IsDepreciation { get; set; }

        public DateTime? WarrantyFromDate { get; set; }

        public string WarrantyFromDateBS { get; set; }

        public DateTime? WarrantyToDate { get; set; }

        public string WarrantyToDateBS { get; set; }

        public bool? IsWarranty { get; set; }

        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? EnteredBy { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int? DepartmentId { get; set; }

        public string CategoryName { get; set; }
    }
}