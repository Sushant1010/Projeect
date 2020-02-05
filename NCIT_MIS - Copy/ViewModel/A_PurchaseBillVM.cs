using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class A_PurchaseBillVM
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
        public List<AssetPurchaseItemByBillPurshaseId> AssetPurchaseItemByBillPurshaseIdList { get; set; }
    }

    public class AssetPurchaseItemByBillPurshaseId
    {
        public int PurchaseBillId { get; set; }
        public int AssetItemId { get; set; }
        public string AssetItemName { get; set; }
        public decimal PurchaseQuantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
    }

    public class A_PurchaseItemVM
    {
        public int PurchaseItemId { get; set; }
        public int DepartmentId { get; set; }
        public int PurchaseBillId { get; set; }
        public int AssetCategoryId { get; set; }
        public int AssetItemId { get; set; }
        public string AssetItemName { get; set; }
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

    public class A_Purchase
    {
        public A_PurchaseBillVM A_PurchaseBillDetail { get; set; }
        public List<A_PurchaseBillVM> A_PurchaseBillList { get; set; }
        public List<A_PurchaseItemVM> A_PurchaseItemList { get; set; }
    }
}