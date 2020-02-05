



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class INV_SalesBillVM

    {
        public int SalesBillId { get; set; }
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
        public string VendorName { get; set; }
        public List<InventorySalesItemByBillSalesId> InventorySalesItemByBillSalesIdList { get; set; }
    }

    public class InventorySalesItemByBillSalesId
    {
        public int SalesBillId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal SalesQuantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
    }

    public class INV_SalesItemVM
    {
        public int SalesItemId { get; set; }
        public int DepartmentId { get; set; }
        public int SalesBillId { get; set; }
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
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

    public class INV_Sales
    {
        public INV_SalesBillVM INV_SalesBillDetail { get; set; }
        public List<INV_SalesBillVM> INV_SalesBillList { get; set; }
        public List<INV_SalesItemVM> INV_SalesItemList { get; set; }
    }
}