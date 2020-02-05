using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class A_DepreciationRepo
    {
        public int AddDepreciation(A_Depreciation saveDepre)
        {
            string sql = "insert into A_Depreciation(DepreciationName,DepreciationRate,Description,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,DepartmentId," +
                "IsDeleted,DeletedBy,DeletedDate)" +
                " values(@DepreciationName,@DepreciationRate,@Description,@EnteredBy,@EnteredDate,0,null,@DepartmentId," +
                "0,0,null)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveDepre).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public List<A_DepreciationVM> GetAllDepreciationList()
        {
            string sql = "select * from A_Depreciation l " +
                "left join Sys_Department d on d.departmentId = l.departmentId" +
                " where l.IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                //db.Execute(sql);
                var lst = db.Query<A_DepreciationVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public A_DepreciationVM getDepreciationDetail(int id)
        {
            string sql = "select * from A_Depreciation l" +
                " left join Sys_Department d on d.departmentId = l.departmentId" +
                " where l.IsDeleted=0 and l.DepreciationId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<A_DepreciationVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool UpdateDepreciation(A_Depreciation updateDepre, int Id)
        {
            string sql = " Update A_Depreciation set DepreciationName=@DepreciationName, DepreciationRate=@DepreciationRate, Description=@Description," +
                "LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and DepreciationId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateDepre);
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

        public bool DeleteDepreciation(int Id, DateTime DeletedDate, int DeletedBy)
        {
            string sql = " Update A_Depreciation set IsDeleted=1, DeletedBy=" + DeletedBy + ", DeletedDate='" + DeletedDate + "' where DepreciationId= " + Id;
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