using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class DashboardVM
    {
    }
    public class Dashboard_Report_Token
    {
        public int TokenId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string RequestCategory { get; set; }
        public string ApprovedByAdmin { get; set; }
        public string RequestStatus { get; set; }
        public Dashboard_Report Dashboard_ReportList { get; set; }
        public Dashboard_Report Dashboard_InvReportList { get; set; }
    }

    public class Dashboard_Report
    {
        public int RequestId { get; set; }
        public int RequestQuantity { get; set; }
        public string RequestCategory { get; set; }
        public int TokenId { get; set; }
        public int RequestItemId { get; set; }
    }

    public class Dashboard_AssetStock
    {
        public int AssetItemId { get; set; }
        public int tItemId { get; set; }
        public string AssetItemName { get; set; }
        public string ItemName { get; set; }
        public int PurchaseQuantity { get; set; }
        public int RemainingQuantity { get; set; }
    }

    public class Dashboard_InventoryStock
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int PurchaseQuantity { get; set; }
        public int RemainingQuantity { get; set; }
    }

    public class Dashboard_PurchaseItemQuantity
    {
        public int AssetItemId { get; set; }
        public string AssetItemName { get; set; }
        public int AssetCount { get; set; }
        public int PurchaseQuantity { get; set; }
        public Dashboard_AssetCount Dashboard_AssetItemRowCount { get; set; }
    }
    public class Dashboard_AssetCount
    {
        public int AssetItemId { get; set; }
        public string AssetItemName { get; set; }
        public int AssetCount { get; set; }
        public int PurchaseQuantity { get; set; }
    }

    public class Dashboard_PurchaseInvItemQuantity
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int InvCount { get; set; }
        public int PurchaseQuantity { get; set; }
        public Dashboard_InventoryCount Dashboard_InvItemRowCount { get; set; }
    }
    public class Dashboard_InventoryCount
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int InvCount { get; set; }
        public int PurchaseQuantity { get; set; }
    }
    public class Dashboard_Department
    {
        public int TokenId { get; set; }
        public int RequestItemId { get; set; }
        public int AssetItemId { get; set; }
        public string AssetItemName { get; set; }
        public int RequestQuantity { get; set; }
        public string ApprovedByAdmin { get; set; }
        public string RequestStatus { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }

    public class Dashboard_DepartmentInv
    {
        public int TokenId { get; set; }
        public int RequestItemId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int RequestQuantity { get; set; }
        public string ApprovedByAdmin { get; set; }
        public string RequestStatus { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }
    public class Dashboard_Detail
    {
        public List<Dashboard_Report_Token> DepartmentList { get; set; }
        public Dashboard_Report QuantityList { get; set; }
        public List<Dashboard_AssetStock> AssetStockList { get; set; }
        public List<Dashboard_InventoryStock> InventoryStockList { get; set; }
        public List<Dashboard_PurchaseItemQuantity> Dashboard_PurchaseItemQuantityList { get; set; }
        public List<Dashboard_PurchaseInvItemQuantity> Dashboard_PurchaseInvItemQuantityList { get; set; }
        public List<Dashboard_Department> Dashboard_DepartmentsRequestStatus { get; set; }
        public List<Dashboard_DepartmentInv> Dashboard_DepartmentsInventoryRequestStatus { get; set; }
    }
}