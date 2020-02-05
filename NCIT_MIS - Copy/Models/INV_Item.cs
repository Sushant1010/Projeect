

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Models
{

    public class INV_Item
    {
        [Key]
        [Column(Order = 0)]

        public int ItemId { get; set; }


        [Column(Order = 1)]
        [StringLength(100)]
        public string ItemName { get; set; }


        public int CategoryId { get; set; }

        public string ItemCode { get; set; }

        public string Unit { get; set; }

        public int SellingPrice { get; set; }

        public string QualityPer { get; set; }

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

    }
}