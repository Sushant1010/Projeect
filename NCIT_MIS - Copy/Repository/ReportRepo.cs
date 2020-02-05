using Dapper;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class ReportRepo
    {
        public List<AssetReport> AssetReport(string depid, string locid, string catid, string assid)
        {
            string sql = "select ai.AssetItemName, d.DepartmentName, l.LocationName, COUNT(a.AssetItemId) Quantity/*, SUM(api.Rate) Amount*/" +
                " from A_Asset a" +
                " left join Sys_Department d on d.DepartmentId = a.DepartmentId" +
                " left join A_Location l on l.LocationId = a.LocationId" +
                " left join A_Item ai on ai.AssetItemId = a.AssetItemId" +
                //" left join A_PurchaseItem api on api.AssetItemId = a.AssetItemId" +
                " where a.IsDeleted=0";
            if(depid != "")
            {
                sql += " and a.DepartmentId =" + depid;
            }
            if (locid != "")
            {
                sql += " and a.LocationId =" + locid;
            }

            if (assid != "")
            {
                sql += " and a.AssetItemId =" + assid;
            }
            sql +=" Group by ai.AssetItemName, d.DepartmentName, l.LocationName";
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<AssetReport>(sql).ToList();
            }
        }

        public Stock_Report GetAssetStockList()
        {
            Stock_Report detail = new Stock_Report();
            detail.AssetPurchaseReportList = GetAssetPurchaseReportList();
            //detail.AssetReportList = GetAssetReportList();
            for (int i = 0; i < detail.AssetPurchaseReportList.Count; i++)
            {
                detail.AssetPurchaseReportList[i].AssetReportDetail = GetAssetReportList(detail.AssetPurchaseReportList[i].AssetItemId);
            }
            return detail;
        }

        public List<AssetPurchaseReport> GetAssetPurchaseReportList( )
        {
            string sql = "select api.AssetItemId,ai.AssetItemName,Sum(api.PurchaseQuantity) RemainingQuantity" +
                " from A_PurchaseItem api" +
                " left join A_Item ai on ai.AssetItemId = api.AssetItemId" +
                " where api.IsDeleted = 0" +
                " group by ai.AssetItemName,api.AssetItemId";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<AssetPurchaseReport>(sql).ToList();
            }
        }

        public AssetReport GetAssetReportList(int assetitemid)
        {
            string sql = "select ai.AssetItemId, ai.AssetItemName, COUNT(a.AssetItemId) Quantity" +
                " from A_Asset a" +
                " left join A_Item ai on ai.AssetItemId = a.AssetItemId" +
                " where a.IsDeleted = 0 and a.AssetItemId=" + assetitemid+
                " group by ai.AssetItemName,ai.AssetItemId";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<AssetReport>(sql).SingleOrDefault();
            }
        }

        public List<InventoryReport> InventoryReport(string depid, string locid, string catid, string Invid)
        {
            string sql = "select i.ItemName, d.DepartmentName,sum(di.SalesQuantity) Quantity" +
                " from INV_DistributionItem di" +
                " left join INV_Item i on i.ItemId = di.ItemId" +
                " left join Sys_Department d on d.DepartmentId = di.DepartmentId" +
                " where di.IsDeleted = 0";
            if (depid != "")
            {
                sql += " and di.DepartmentId =" + depid;
            }

            if (Invid != "")
            {
                sql += " and di.ItemId =" + Invid;
            }
            sql += " group by i.ItemName, d.DepartmentName order by i.ItemName asc";
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<InventoryReport>(sql).ToList();
            }
        }

        public Stock_Report GetInvStockList()
        {
            Stock_Report detail = new Stock_Report();
            detail.InventoryPurchaseReportList = GetInvPurchaseReportList();
            //detail.AssetReportList = GetAssetReportList();
            for (int i = 0; i < detail.InventoryPurchaseReportList.Count; i++)
            {
                detail.InventoryPurchaseReportList[i].InventoryReportDetail = GetInvReportList(detail.InventoryPurchaseReportList[i].ItemId);
            }
            return detail;
        }

        public List<InventoryPurchaseReport> GetInvPurchaseReportList()
        {
            string sql = "select api.ItemId,ai.ItemName,Sum(api.PurchaseQuantity) RemainingQuantity" +
                " from Inv_PurchaseItem api" +
                " left join Inv_Item ai on ai.ItemId = api.ItemId" +
                " where api.IsDeleted = 0" +
                " group by ai.ItemName,api.ItemId";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<InventoryPurchaseReport>(sql).ToList();
            }
        }

        public InventoryReport GetInvReportList(int itemid)
        {
            string sql = "select ai.ItemId, ai.ItemName, SUM(a.SalesQuantity) Quantity" +
                " from Inv_DistributionItem a" +
                " left join INV_Item ai on ai.ItemId = a.ItemId" +
                " where a.IsDeleted = 0 and a.ItemId=" + itemid +
                " group by ai.ItemName,ai.ItemId";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<InventoryReport>(sql).SingleOrDefault();
            }
        }

    }
}