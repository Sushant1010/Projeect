using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Models
{
    [Table("NCIT_MIS.A_Item")]
    public class A_Item
    {
        [Key]
        [Column(Order = 0)]

        public int AssetItemId { get; set; }


        [Column(Order = 1)]
        [StringLength(100)]
        public string AssetItemName { get; set; }


        public int AssetCategoryId { get; set; }

        public string AssetItemCode { get; set; }

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