using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class A_ItemRepo
    {
        public List<A_ItemVM> getAllItemList()
        {
            string sql = "select * from A_Item where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<A_ItemVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public int AddUsers(A_Item saveItem)
        {
            string sql = "insert into A_Item(AssetItemName,AssetItemCode,AssetCategoryId,DepartmentId,IsWarranty,WarrantyDuration," +
                "WarrantyFromDate,WarrantyFromDateBS,WarrantyToDate,WarrantyToDateBS,EnteredDate,EnteredBy,LastUpdatedDate" +
                ",LastUpdatedBy,IsDeleted,DeletedBy,DeletedDate)" +
                " values(@AssetItemName,@AssetItemCode,@AssetCategoryId,@DepartmentId,@IsWarranty,@WarrantyDuration," +
                "@WarrantyFromDate,@WarrantyFromDateBS,@WarrantyToDate,@WarrantyToDateBS,@EnteredDate,@EnteredBy,null" +
                ",0,0,0,null)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveItem).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public bool UpdateItem(A_Item updateItem, int Id)
        {
            string sql = " Update A_Item set AssetItemName=@AssetItemName, AssetItemCode=@AssetItemCode, AssetCategoryId=@AssetCategoryId," +
                " IsWarranty=@IsWarranty, WarrantyDuration=@WarrantyDuration, WarrantyFromDate=@WarrantyFromDate," +
                " WarrantyFromDateBS=@WarrantyFromDateBS, WarrantyToDate=@WarrantyToDate, WarrantyToDateBS=@WarrantyToDateBS," +
                " LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and AssetItemId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateItem);
                db.Close();
                if (lst > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public A_ItemVM getItemDetail(int id)
        {
            string sql = "select * from A_Item i" +
                " left join A_Category c on c.CategoryId= i.AssetCategoryId" +
                " where i.IsDeleted=0 and i.AssetItemId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<A_ItemVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool DeleteItem(int Id, DateTime DeletedDate, int DeletedBy)
        {
            string sql = " Update A_Item set IsDeleted=1, DeletedBy=" + DeletedBy + ", DeletedDate='" + DeletedDate + "' where AssetItemId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql);
                db.Close();
                if (lst > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}