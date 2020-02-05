using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class A_AssetRepo
    {
        public int AddAsset(A_Asset asset)
        {
            string sql = " insert into A_Asset(" +
                         " DepartmentId, LocationId, AssetItemId, AssetUniqueCode, AssetDate, AssetDateBS,Description,UsefullLife," +
                         " IsDepreciationApplicable,DepreciationStartDate,DepreciationStartDateBS,DepreciationRemarks," +
                         " IsScrap,ScrapDate,ScrapDateBS,ScrapRealizedValue," +
                         " EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate, IsDeleted," +
                         "  DeletedDate, DeletedBy" +
                         ")" +
                         " values(" +
                         "@DepartmentId, @LocationId, @AssetItemId, @AssetUniqueCode, @AssetDate, @AssetDateBS, @Description, @UsefullLife," +
                         " @IsDepreciationApplicable, @DepreciationStartDate,@DepreciationStartDateBS,@DepreciationRemarks," +
                         " @IsScrap, @ScrapDate, @ScrapDateBS, @ScrapRealizedValue," +
                         " @EnteredBy, @EnteredDate, 0, null, 0," +
                         "  null, 0 " +
                         ")  SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                int a = db.Query<int>(sql, asset).SingleOrDefault();
                db.Close();
                return a;

            }
        }

        public List<A_AssetVM> getAssetList(int depid)
        {
            string sql = "select * from A_Asset a " +
                "left join Sys_Department d on d.departmentId = a.departmentId" +
                " left join A_Location l on l.LocationId = a.LocationId" +
                " left join A_Item ai on ai.AssetItemId = a.AssetItemId" +
                " where a.IsDeleted=0 ";

            using (var db = DbHelper.GetDBConnection())
            {

                //db.Execute(sql);
                var lst = db.Query<A_AssetVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public A_AssetVM getAssetDetail(int id)
        {
            string sql = "select * from A_Asset a" +
                " left join A_Item ai on ai.AssetItemId = a.AssetItemId" +
                " left join Sys_Department d on d.DepartmentId = a.DepartmentId" +
                " left join A_Location l on l.LocationId = a.LocationId" +
                " left join A_Category c on c.CategoryId = ai.AssetCategoryId" +
                " where a.IsDeleted=0 and a.AssetId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<A_AssetVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool UpdateAsset(A_Asset updateAsset, int Id)
        {
            string sql = " Update A_Asset set AssetDate=@AssetDate, AssetDateBS=@AssetDateBS, Description=@Description,UsefullLife=@UsefullLife," +
                "IsDepreciationApplicable=@IsDepreciationApplicable, DepreciationStartDate=@DepreciationStartDate, DepreciationStartDateBS=@DepreciationStartDateBS, DepreciationRemarks=@DepreciationRemarks," +
                "IsScrap=@IsScrap, ScrapDate=@ScrapDate, ScrapDateBS=@ScrapDateBS, ScrapRealizedValue=@ScrapRealizedValue, " +
                "LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and AssetId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateAsset);
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

        public bool DeleteAsset(int Id, DateTime deletedDate, int deletedby)
        {
            string sql = " Update A_Asset set IsDeleted=1, DeletedBy=" + deletedby + ", DeletedDate='" + deletedDate + "' where AssetId= " + Id;
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
        public A_AssetVM getDepartmentLocationByTokenId(int TokenId)
        {
            string sql = "select d.DepartmentId, d.DepartmentName,l.LocationId,l.LocationName, r.RequestQuantity," +
                " ai.AssetItemId, ai.AssetItemName, ac.CategoryId, ac.CategoryName from Request_Token rt" +
                " left join Sys_Department d on d.DepartmentId = rt.DepartmentId" +
                " left join A_Location l on l.LocationId = rt.LocationId" +
                " left join Request r on r.TokenId = rt.TokenId" +
                " left join A_Item ai on ai.AssetItemId = r.RequestItemId" +
                " left join A_Category ac on ac.CategoryId = ai.AssetCategoryId" +
                " where rt.IsDeleted=0 and rt.TokenId ="+TokenId+" Order By DepartmentName Asc";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<A_AssetVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public void ApprovedRequestTokenStore(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update Request_Token set RequestStatus='Approved',RequestStatusDate= '" + deleteddate + "'" +
            " where TokenId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }
    }
}