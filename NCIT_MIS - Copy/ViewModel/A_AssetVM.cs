using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class A_AssetVM
    {
        public int AssetId { get; set; }


        public int AssetItemId { get; set; }
        public int AssetCategoryId { get; set; }
        public string AssetItemName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string AssetDate { get; set; }
        public string AssetDateBS { get; set; }
        public int UsefullLife { get; set; }
        public int RequestQuantity { get; set; }
        public bool IsAccessory { get; set; }
        public int AccessoryId { get; set; }
        public bool? IsDepreciationApplicable { get; set; }
        public string DepreciationStartDate { get; set; }
        public string DepreciationStartDateBS { get; set; }
        public string DepreciationRemarks { get; set; }
        public bool? IsScrap { get; set; }
        public string ScrapDate { get; set; }
        public string ScrapDateBS { get; set; }
        public decimal ScrapRealizedValue { get; set; }
        public string AssetUniqueCode { get; set; }
        public bool IsTransfered { get; set; }
        public int AssetTransferedId { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}