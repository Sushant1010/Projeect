using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class ReportVM
    {
    }

    public class AssetReport
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int AssetItemId { get; set; }
        public string AssetItemName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }

    public class AssetPurchaseReport
    {
        public int AssetItemId { get; set; }
        public string AssetItemName { get; set; }
        
        public string RemainingQuantity { get; set; }

        public AssetReport AssetReportDetail { get; set; }
    }

    public class InventoryReport
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }

    public class InventoryPurchaseReport
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public string RemainingQuantity { get; set; }

        public InventoryReport InventoryReportDetail { get; set; }
    }
    public class Stock_Report
    {
        public List<AssetPurchaseReport> AssetPurchaseReportList { get; set; }
        public List<AssetReport> AssetReportList { get; set; }

        public List<InventoryPurchaseReport> InventoryPurchaseReportList { get; set; }
        public List<InventoryReport> InventoryReportList { get; set; }
    }
}