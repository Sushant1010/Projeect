using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Web;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;

namespace NCIT_MIS.Repository
{
    public class GetDropDown
    {

        public List<SystemUserVM> getAllUesrList()
        {
            string sql = "select * from Sys_User where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {

                //db.Execute(sql);
                var lst = db.Query<SystemUserVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<CommonVM> getDepartmentList()
        {
            string sql = "select DepartmentId Id, DepartmentName Name from Sys_Department" +
                " where IsDeleted=0 Order By DepartmentName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        
        public List<CommonVM> getLocationList()
        {
            string sql = "select LocationId Id, LocationName Name from A_Location" +
                " where IsDeleted=0 Order By LocationName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<CommonVM> getLocationList(int depid)
        {
            string sql = "select LocationId Id, LocationName Name from A_Location" +
                " where IsDeleted=0 and DepartmentId="+depid+" Order By LocationName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public List<CommonVM> getUserTypeList()
        {
            string sql = "select UserTypeId Id, UserTypeName Name from Sys_UserType" +
                " where IsDeleted=0 Order By UserTypeName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<CommonVM> getCategoryListByDepartment(int DepId)
        {
            string sql = "select CategoryId Id, CategoryName Name from A_Category" +
                " where IsDeleted=0 and DepartmentId=" + DepId+
                " Order By CategoryName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public CommonVM CheckPassword(string Password, int userId)
        {
            string sql = "select password Name from Sys_User where IsDeleted=0 and Password='"+Password+"' and UserId="+userId;

            using (var db=DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public List<CommonVM> getVendorList(int depid)
        {
            string sql = "Select vendorid Id, vendorName Name from A_Vendor where IsDeleted=0";
            using(var db=DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<CommonVM> getInventoryVendorList(int depid)
        {
            string sql = "Select vendorid Id, vendorName Name from INV_Vendor where IsDeleted=0";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<CommonVM> getAssetCategoryList(int depid)
        {
            string sql = "select CategoryId Id, CategoryName Name from A_Category where IsDeleted=0 "+
                " Order By CategoryName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<CommonVM> getAssetItemList(int depid)
        {
            string sql = "select AssetItemId Id, AssetItemName Name from A_Item where IsDeleted=0" +
                " Order By AssetItemName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<CommonVM> getCategoryList(int depid)
        {
            string sql = "select CategoryId Id, CategoryName Name from INV_Category where IsDeleted=0 " +
                " Order By CategoryName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<CommonVM> getItemList(int depid)
        {
            string sql = "select ItemId Id, ItemName Name from INV_Item where IsDeleted=0" +
                " Order By ItemName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }


        public List<CommonVM> GetAssetItemListByCategoryId(int depid, int cateid)
        {
            string sql = " Select AssetItemId Id,AssetItemName Name from A_Item ";
            sql += " where 1=1 and IsDeleted=" + 0;
            //if (depid > 0)
            //{
            //    sql += " and DepartmentId=" + depid;
            //}
            if (cateid > 0)
            {
                sql += " and AssetCategoryId=" + cateid;
            }
            sql += "Order By AssetItemName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<CommonVM> GetInventoryItemListByCategoryId(int depid, int cateid)
        {
            string sql = " Select ItemId Id,ItemName Name from INV_Item ";
            sql += " where 1=1 and IsDeleted=" + 0;
            //if (depid > 0)
            //{
            //    sql += " and DepartmentId=" + depid;
            //}
            if (cateid > 0)
            {
                sql += " and CategoryId=" + cateid;
            }
            sql += "Order By ItemName";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }


        public List<CommonVM> getLocationListByDepartmentId(int dep)
        {
            string sql = "select LocationId Id, LocationName Name from A_Location" +
                " where IsDeleted=0 and DepartmentId=" + dep +
                " Order By LocationName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public string getDepartmentCode(int dep)
        {
            string sql = "select DepartmentCode from Sys_Department" +
                " where IsDeleted=0 and DepartmentId=" + dep;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public string GetLocationCode(int locId)
        {
            string sql = "select LocationCode from A_Location" +
                " where IsDeleted=0 and LocationId=" + locId;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public string GetAssetCode(int assid)
        {
            string sql = "Select AssetItemCode from A_Item where IsDeleted=0 and AssetItemId=" + assid;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<string>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int GetAssetUniqueID(int assid, int locid)
        {
            string sql = "select count(*)+1 from A_Asset where IsDeleted=0 and " +
                " LocationId=" + locid +
                " and AssetItemId=" + assid;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public List<CommonVM> getUnitList(int DepId)
        {
            string sql = "select UnitId Id, UnitName Name from INV_Unit" +
                " where IsDeleted=0 ";
            if(DepId > 0)
            {
                sql += "and DepartmentId=" + DepId;
            }

            sql += " Order By UnitName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public List<CommonVM> getInvCategoryListByDepartment(int DepId)
        {
            string sql = "select CategoryId Id, CategoryName Name from INV_Category" +
                " where IsDeleted=0 and DepartmentId=" + DepId +
                " Order By CategoryName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<CommonVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public int GetQuantity(int assid, int qty)
        {
            string sql = "select SUM(api.PurchaseQuantity)-COUNT(a.AssetItemId) Quantity" +
                " from A_PurchaseItem api" +
                " left join A_Asset a on a.AssetItemId = api.AssetItemId" +
                " where api.IsDeleted = 0  and api.AssetItemId =" + assid;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<int>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public decimal GetInvQuantity(int invid, int qty)
        {
            string sql = "select SUM(api.PurchaseQuantity)-Sum(coalesce(a.SalesQuantity,0)) Quantity" +
                " from INV_PurchaseItem api" +
                " left join INV_DistributionItem a on a.ItemId = api.ItemId" +
                " where api.IsDeleted = 0  and api.ItemId =" + invid;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<decimal>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
    }
}