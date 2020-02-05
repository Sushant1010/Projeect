


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class INV_PurchaseBillVM

    {
        public int PurchaseBillId { get; set; }
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
        public List<InventoryPurchaseItemByBillPurshaseId> InventoryPurchaseItemByBillPurshaseIdList { get; set; }
      
    }

    public class InventoryPurchaseItemByBillPurshaseId
    {
        public int PurchaseBillId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal PurchaseQuantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
    }

    public class INV_PurchaseItemVM
    {
        public int PurchaseItemId { get; set; }
        public int DepartmentId { get; set; }
        public int PurchaseBillId { get; set; }
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal PurchaseQuantity { get; set; }
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

    public class INV_Purchase
    {
        public INV_PurchaseBillVM INV_PurchaseBillDetail { get; set; }
        public List<INV_PurchaseBillVM> INV_PurchaseBillList { get; set; }
        public List<INV_PurchaseItemVM> INV_PurchaseItemList { get; set; }
    }
}