using Dapper;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class DashboardRepo
    {
        public int DepartmentCount()
        {
            string sql = "select count(*) from Sys_Department where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int RemainingAssetItemCount()
        {
            int RemainingItem = PurchaseQuantityCount()- TotalAssetCount();
            return RemainingItem;
        }
        public int PurchaseQuantityCount()
        {
            string sql = "select SUM(api.PurchaseQuantity) from A_PurchaseItem api" +
                " where api.IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public int TotalAssetCount()
        {
            string sql = "select COUNT(AssetItemId) from A_Asset"+
                " where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int AssetCount()
        {
            string sql = "select count(*) from A_Asset where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int AssetRequestNotificationAdmin()
        {
            string sql = "select count(*) from Request_Token where IsDeleted=0 and ApprovedByAdmin != 'Approved' and RequestStatus != 'Approved' and RequestCategory ='Asset'";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int AssetRequestNotification()
        {
            string sql = "select count(*) from Request_Token where IsDeleted=0 and ApprovedByAdmin = 'Approved' and RequestStatus != 'Approved' and RequestCategory='Asset'";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }


        public int RemainingInventoryItemCount()
        {
            int RemainingItem = PurchaseInvQuantityCount() - TotalInventoryCount();
            return RemainingItem;
        }
        public int PurchaseInvQuantityCount()
        {
            string sql = "select SUM(api.PurchaseQuantity) from Inv_PurchaseItem api" +
                " where api.IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public int TotalInventoryCount()
        {
            string sql = "select sum(COALESCE(SalesQuantity,0)) from Inv_DistributionItem" +
                " where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int InventoryCount()
        {
            string sql = "select count(*) from Inv_PurchaseItem where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int InventoryRequestNotificationAdmin()
        {
            string sql = "select count(*) from Request_Token where IsDeleted=0 and ApprovedByAdmin != 'Approved' and RequestStatus != 'Approved' and RequestCategory='Inventory'";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int InventoryRequestNotification()
        {
            string sql = "select count(*) from Request_Token where IsDeleted=0 and ApprovedByAdmin = 'Approved' and RequestStatus != 'Approved' and RequestCategory='Inventory'";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int UserCount()
        {
            string sql = "select count(*) from Sys_User where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int AssetVendorCount()
        {
            string sql = "select count(*) from A_Vendor where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public int InventoryVendorCount()
        {
            string sql = "select count(*) from Inv_Vendor where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public Dashboard_Detail DashboardDisplay(int uid)
        {
            Dashboard_Detail detail = new Dashboard_Detail();
            detail.DepartmentList = GetDepartmentList(uid);
            //For Asset
            for (int i = 0; i < detail.DepartmentList.Count; i++)
            {
                detail.DepartmentList[i].Dashboard_ReportList = GetQuantity(detail.DepartmentList[i].TokenId);
            }
            //detail.Dashboard_AssetStockQuantityList = AssetTotalItemCount(uid);
            detail.AssetStockList = GetAssetStockList();

            detail.Dashboard_PurchaseItemQuantityList = GetPurchaseItemQuntityList();
            for (int i = 0; i < detail.Dashboard_PurchaseItemQuantityList.Count; i++)
            {
                detail.Dashboard_PurchaseItemQuantityList[i].Dashboard_AssetItemRowCount = GetAssetItemQuntityList(detail.Dashboard_PurchaseItemQuantityList[i].AssetItemId, detail.Dashboard_PurchaseItemQuantityList[i].PurchaseQuantity);
            }
            //For Inventory
            for (int i = 0; i < detail.DepartmentList.Count; i++)
            {
                detail.DepartmentList[i].Dashboard_InvReportList = GetInvQuantity(detail.DepartmentList[i].TokenId);
            }
            detail.InventoryStockList = GetInvStockList();
            detail.Dashboard_PurchaseInvItemQuantityList = GetPurchaseInvItemQuntityList();
            for (int i = 0; i < detail.Dashboard_PurchaseInvItemQuantityList.Count; i++)
            {
                detail.Dashboard_PurchaseInvItemQuantityList[i].Dashboard_InvItemRowCount = GetInvItemQuntityList(detail.Dashboard_PurchaseInvItemQuantityList[i].ItemId, detail.Dashboard_PurchaseInvItemQuantityList[i].PurchaseQuantity);
            }


            //Dashboard for Department Users
            detail.Dashboard_DepartmentsRequestStatus = GetRequestStatus(uid);
            detail.Dashboard_DepartmentsInventoryRequestStatus = GetInvRequestStatus(uid);
            return detail;
        }

        public List<Dashboard_PurchaseItemQuantity> GetPurchaseItemQuntityList()
        {
            string sql = "select pi.AssetItemId, ai.AssetItemName, SUM(pi.PurchaseQuantity) PurchaseQuantity from A_PurchaseItem pi" +
                " left join A_Item ai on ai.AssetItemId = pi.AssetItemId" +
                " where pi.IsDeleted=0 "+
                " group by pi.AssetItemId, ai.AssetItemName" +
                " order by PurchaseQuantity asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Dashboard_PurchaseItemQuantity>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public Dashboard_AssetCount GetAssetItemQuntityList(int astItemId, int pqty)
        {
            string sql = "select top 5 AssetItemId,COALESCE(" + pqty + "-COUNT(AssetItemId),0) AssetCount from A_Asset" +
                " where IsDeleted=0 and AssetItemId =" + astItemId +
                " group by AssetItemId" +
                " order by AssetCount asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Dashboard_AssetCount>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public List<Dashboard_PurchaseInvItemQuantity> GetPurchaseInvItemQuntityList()
        {
            string sql = "select pi.ItemId, ai.ItemName, SUM(pi.PurchaseQuantity) PurchaseQuantity from Inv_PurchaseItem pi" +
                " left join Inv_Item ai on ai.ItemId = pi.ItemId" +
                " where pi.IsDeleted=0 " +
                " group by pi.ItemId, ai.ItemName" +
                " order by PurchaseQuantity asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Dashboard_PurchaseInvItemQuantity>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public Dashboard_InventoryCount GetInvItemQuntityList(int ItemId, int pqty)
        {
            string sql = "select top 5 ItemId,COALESCE(" + pqty + "-sum(SalesQuantity),0) InvCount from Inv_DistributionItem" +
                " where IsDeleted=0 and ItemId =" + ItemId +
                " group by ItemId" +
                " order by InvCount asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Dashboard_InventoryCount>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        
        public List<Dashboard_Report_Token> GetDepartmentList(int uid)
        {
            UserPermissionRepo up = new UserPermissionRepo();
            var userPermission = up.getUesrDetail(Convert.ToInt32(uid));
            string sql = "select Top 5 r.TokenId,r.RequestToken, r.RequestCategory, d.DepartmentName,r.ApprovedByAdmin, r.RequestStatus" +
                "  from Request_Token r" +
                " left join Sys_Department d on d.DepartmentId = r.DepartmentId" +
                " where 1 = 1 and r.IsDeleted = 0";
            if ((userPermission.AssetModuleAllow) == true || (userPermission.InventoryModuleAllow) == true)
            {
                sql += " and ApprovedByAdmin = 'Approved'";
            }
            else
            {
                sql += " and ApprovedByAdmin != 'Approved'";
            }
                sql += " and RequestStatus != 'Approved' ";
            sql += " order by r.RequestToken  desc";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<Dashboard_Report_Token>(sql).ToList();
            }
        }

        public Dashboard_Report GetQuantity(int tokenId)
        {
            string sql = "select SUM(r.RequestQuantity) RequestQuantity, r.RequestItemId" +
                " from Request r" +
                " left join Request_Token rt on rt.TokenId = r.TokenId" +
                " left join A_Item ai on ai.AssetItemId = r.RequestItemId" +
                " where 1=1 and r.IsDeleted=0  and r.TokenId=" + tokenId +
                " group by r.RequestItemId";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<Dashboard_Report>(sql).SingleOrDefault();
            }
        }

        public Dashboard_Report GetInvQuantity(int tokenId)
        {
            string sql = "select SUM(r.RequestQuantity) RequestQuantity, r.RequestItemId" +
                " from Request r" +
                " left join Request_Token rt on rt.TokenId = r.TokenId" +
                " left join Inv_Item ai on ai.ItemId = r.RequestItemId" +
                " where 1=1 and r.IsDeleted=0 and rt.RequestCategory='Inventory' and r.TokenId=" + tokenId +
                " group by r.RequestItemId";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<Dashboard_Report>(sql).SingleOrDefault();
            }
        }
        //public List<Dashboard_AssetStockQuantity> AssetTotalItemCount(int uid)
        //{
        //    UserPermissionRepo up = new UserPermissionRepo();
        //    var userPermission = up.getUesrDetail(Convert.ToInt32(uid));
        //    string sql = "select top 5 api.AssetItemId, ai.AssetItemName, sum(api.PurchaseQuantity)-count(a.AssetItemId) RemainingQuantity from A_PurchaseItem api " +
        //        "left join Request r on r.RequestItemId = api.AssetItemId " +
        //        "left join Request_Token rt on rt.TokenId = r.TokenID " +
        //        "left join A_Asset a on a.AssetItemId = api.AssetItemId " +
        //        "left join A_Item ai on ai.AssetItemId = api.AssetItemId " +
        //        "where api.IsDeleted = 0 and a.IsDeleted = 0 ";
        //        if ((userPermission.AssetModuleAllow) == true || (userPermission.InventoryModuleAllow) == true)
        //        {
        //            sql += " and rt.ApprovedByAdmin = 'Approved'";
        //        }
        //        else
        //        {
        //            sql += " and rt.ApprovedByAdmin != 'Approved'";
        //        }
        //             sql += " and rt.RequestStatus != 'Approved' "+
        //        "group by api.AssetItemId, ai.AssetItemName " +
        //        "order by RemainingQuantity asc";
        //    using (var db = DbHelper.GetDBConnection())
        //    {
        //        db.Close();
        //        return db.Query<Dashboard_AssetStockQuantity>(sql).ToList();
        //    }
        //}
        public List<Dashboard_AssetStock> GetAssetStockList()
        {
            string sql = "select top 5 api.AssetItemId, ai.AssetItemName, sum(api.PurchaseQuantity)-count(a.AssetItemId) RemainingQuantity from A_PurchaseItem api " +
                "left join A_Asset a on a.AssetItemId = api.AssetItemId " +
                "left join A_Item ai on ai.AssetItemId = api.AssetItemId " +
                "where api.IsDeleted = 0 " +
                "group by api.AssetItemId, ai.AssetItemName " +
                "order by RemainingQuantity asc";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<Dashboard_AssetStock>(sql).ToList();
            }
        }

        public List<Dashboard_InventoryStock> GetInvStockList()
        {
            string sql = "select top 5 api.ItemId, ai.ItemName, sum(api.PurchaseQuantity)-sum(a.SalesQuantity) RemainingQuantity from Inv_PurchaseItem api " +
                "left join Inv_DistributionItem a on a.ItemId = api.ItemId " +
                "left join Inv_Item ai on ai.ItemId = api.ItemId " +
                "where api.IsDeleted = 0 " +
                "group by api.ItemId, ai.ItemName " +
                "order by RemainingQuantity asc";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<Dashboard_InventoryStock>(sql).ToList();
            }
        }

        //Dashboard for Department users
        public List<Dashboard_Department> GetRequestStatus(int uid)
        {
            string sql = "select top 5 rt.ApprovedByAdmin,rt.RequestStatus,l.LocationName,ai.AssetItemName,r.RequestQuantity,rt.EnteredBy" +
                " from Request_Token rt" +
                " left join Request r on r.TokenId = rt.TokenId" +
                " left join A_item ai on ai.AssetItemId = r.RequestItemId" +
                " left join A_Location l on l.LocationId = rt.LocationId" +
                " where rt.IsDeleted = 0 and rt.RequestCategory ='Asset' and rt.EnteredBy =" + uid +
                " order by rt.EnteredDate desc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Dashboard_Department>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<Dashboard_DepartmentInv> GetInvRequestStatus(int uid)
        {
            string sql = "select top 5 rt.ApprovedByAdmin,rt.RequestStatus,ai.ItemName,r.RequestQuantity,rt.EnteredBy" +
                " from Request_Token rt" +
                " left join Request r on r.TokenId = rt.TokenId" +
                " left join Inv_item ai on ai.ItemId = r.RequestItemId" +
                //" left join A_Location l on l.LocationId = rt.LocationId" +
                " where rt.IsDeleted = 0 and rt.RequestCategory ='Inventory' and rt.EnteredBy =" + uid +
                " order by rt.EnteredDate desc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Dashboard_DepartmentInv>(sql).ToList();
                db.Close();
                return lst;
            }
        }
    }
}