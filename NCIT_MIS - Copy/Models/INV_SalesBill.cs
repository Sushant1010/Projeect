



using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Models
{

    public class INV_SalesBill
    {
        [Key]
        [Column(Order = 0)]

        public int SalesBillId { get; set; }


        [Column(Order = 1)]

        public int DepartmentId { get; set; }

        public int VendorId { get; set; }

        public string BillNo { get; set; }

        public string BillDate { get; set; }

        public string BillDateBS { get; set; }

        public int BillSerialNo { get; set; }

        public string Remarks { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal VatPercent { get; set; }

        public decimal TotalWithVat { get; set; }

        public int VatApplicable { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal TaxableAmount { get; set; }

        public int IsVerified { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime VerifiedDate { get; set; }


        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}