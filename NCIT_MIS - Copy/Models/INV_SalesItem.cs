



using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace NCIT_MIS.Models
{

    public class INV_SalesItem
    {
        [Key]

        public int SalesItemId { get; set; }


        public int DepartmentId { get; set; }


        public int SalesBillId { get; set; }


        public int ItemId { get; set; }


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

        [Column(TypeName = "date")]
        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}