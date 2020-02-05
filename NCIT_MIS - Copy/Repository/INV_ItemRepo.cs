

using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class INV_ItemRepo
    {
        public List<INV_ItemVM> getAllItemList()
        {
            string sql = "select * from INV_Item where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_ItemVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public int AddUsers(INV_Item saveItem)
        {
            string sql = "insert into INV_Item(ItemName,ItemCode,CategoryId,DepartmentId,IsWarranty,WarrantyDuration," +
                "WarrantyFromDate,WarrantyFromDateBS,WarrantyToDate,WarrantyToDateBS,EnteredDate,EnteredBy,LastUpdatedDate" +
                ",LastUpdatedBy,IsDeleted,DeletedBy,DeletedDate)" +
                " values(@ItemName,@ItemCode,@CategoryId,@DepartmentId,@IsWarranty,@WarrantyDuration," +
                "@WarrantyFromDate,@WarrantyFromDateBS,@WarrantyToDate,@WarrantyToDateBS,@EnteredDate,@EnteredBy,null" +
                ",0,0,0,null)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveItem).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public bool UpdateItem(INV_Item updateItem, int Id)
        {
            string sql = " Update INV_Item set ItemName=@ItemName, ItemCode=@ItemCode, CategoryId=@CategoryId," +
                " IsWarranty=@IsWarranty, WarrantyDuration=@WarrantyDuration, WarrantyFromDate=@WarrantyFromDate," +
                " WarrantyFromDateBS=@WarrantyFromDateBS, WarrantyToDate=@WarrantyToDate, WarrantyToDateBS=@WarrantyToDateBS," +
                " LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and ItemId= " + Id;
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

        public INV_ItemVM getItemDetail(int id)
        {
            string sql = "select * from INV_Item i" +
                " left join INV_Category c on c.CategoryId= i.CategoryId" +
                " where i.IsDeleted=0 and i.ItemId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_ItemVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool DeleteItem(int Id, DateTime DeletedDate, int DeletedBy)
        {
            string sql = " Update INV_Item set IsDeleted=1, DeletedBy=" + DeletedBy + ", DeletedDate='" + DeletedDate + "' where ItemId= " + Id;
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