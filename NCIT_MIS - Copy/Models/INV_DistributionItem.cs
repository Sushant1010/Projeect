

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace NCIT_MIS.Models
{
    [Table("NCIT_MIS.INV_DistributionItem")]
    public class INV_DistributionItem
    {
        [Key]

        public int DistributionItemId { get; set; }


        public int DepartmentId { get; set; }


        public int DistributionBillId { get; set; }


        public int ItemId { get; set; }


        public decimal SalesQuantity { get; set; }

        public int UnitId { get; set; }
        public decimal? Rate { get; set; }


        public decimal? Total { get; set; }

        public bool? IsVerified { get; set; }

        public int? VerifiedBy { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}