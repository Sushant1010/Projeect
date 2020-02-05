


using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class INV_DistributionRepo
    {
        

        public int AddInventoryDistributionItem(INV_DistributionItem distributionitem)
        {
            string sql = " insert into INV_DistributionItem(DepartmentId, ItemId, SalesQuantity, UnitId, IsVerified," +
                "VerifiedBy,VerifiedDate,IsDeleted,EnteredBy,EnteredDate) " +
                "Values(@DepartmentId, @ItemId, @SalesQuantity, @UnitId, @IsVerified, " +
                "@VerifiedBy, @VerifiedDate,@IsDeleted,@EnteredBy,@EnteredDate)SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                int a = db.Query<int>(sql, distributionitem).SingleOrDefault();
                db.Close();
                return a;

            }
        }

       

        public INV_DistributionVM GetDistributionDetail(int id)
        {
            string sql = "select p.DistributionItemId, p.DepartmentId, d.DepartmentName,c.categoryid, p.itemid,p.unitid" +

                " from INV_DistributionItem p" +
                " left join Sys_Department d on d.DepartmentId = p.DepartmentId" +
                " left join INV_Item i on i.ItemId = p.ItemId" +
                " left join INV_Category c on c.CategoryId = i.CategoryId" +

                " where 1 = 1 and p.IsDeleted = 0  and p.DistributionItemId=" + id; ;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<INV_DistributionVM>(sql).SingleOrDefault();
            }
        }


        public List<INV_DistributionVM> getDistributionList(int depid)
        {
            string sql = "select d.DepartmentName, i.ItemName, u.UnitName, di.SalesQuantity, di.DistributionItemId" +
                " from INV_DistributionItem di" +
                " left join Sys_Department d on d.DepartmentId = di.DepartmentId" +
                " left join INV_Unit u on u.UnitId = di.Unitid" +
                " left join INV_Item i on i.ItemId = di.ItemId" +
                " where di.IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {

                //db.Execute(sql);
                var lst = db.Query<INV_DistributionVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        
        public void EditInventoryDistributionItem(INV_DistributionItem distributionitem)
        {
            string sql = " Update INV_DistributionItem set" +
                         " DepartmentId=@DepartmentId, ItemId=@ItemId, SalesQuantity=@SalesQuantity, Rate=@Rate, Total=@Total," +
                         " LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate, IsDeleted=@IsDeleted, DeletedBy=@DeletedBy, DeletedDate=@DeletedDate" +
                         " where DistributionItemId=@DistributionItemId";

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql, distributionitem);
                db.Close();
            }
        }

        public void EditInventoryDistributionItemDelete(DateTime dateTime, int uid, int id)
        {
            string sql = " Update INV_DistributionItem set IsDeleted=1,DeletedBy=" + uid + ",DeletedDate= '" + dateTime + "'" +
            " where DistributionItemId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }

        public void DeleteInventoryDistributionBill(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update INV_DistributionBill set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
            " where DistributionBillId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }

        public INV_DistributionVM getDepartmentLocationByTokenId(int TokenId)
        {
            string sql = "select d.DepartmentId, d.DepartmentName, r.RequestQuantity," +
                " ai.ItemId, ai.ItemName, ac.CategoryId, ac.CategoryName, r.UnitId, u.UnitName from Request_Token rt" +
                " left join Sys_Department d on d.DepartmentId = rt.DepartmentId" +
                //" left join A_Location l on l.LocationId = rt.LocationId" +
                " left join Request r on r.TokenId = rt.TokenId" +
                " left join Inv_Unit u on u.UnitId = r.UnitId" +
                " left join Inv_Item ai on ai.ItemId = r.RequestItemId" +
                " left join Inv_Category ac on ac.CategoryId = ai.CategoryId" +
                " where rt.IsDeleted=0 and rt.TokenId =" + TokenId + " Order By DepartmentName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_DistributionVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }


    }
}