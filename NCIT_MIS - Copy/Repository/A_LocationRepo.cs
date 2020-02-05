using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class A_LocationRepo
    {
        public int AddLocation(A_Location saveLoc)
        {
            string sql = "insert into A_Location(LocationName,LocationCode,DepartmentId,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate," +
                "IsDeleted,DeletedBy,DeletedDate)" +
                " values(@LocationName,@LocationCode,@DepartmentId,@EnteredBy,@EnteredDate,0,null," +
                "0,0,null)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveLoc).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public List<A_LocationVM> GetAllLocationList()
        {
            string sql = "select * from A_Location l " +
                "left join Sys_Department d on d.departmentId = l.departmentId" +
                " where l.IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {

                //db.Execute(sql);
                var lst = db.Query<A_LocationVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public A_LocationVM getLocationDetail(int id)
        {
            string sql = "select * from A_Location l" +
                " left join Sys_Department d on d.departmentId = l.departmentId" +
                " where l.IsDeleted=0 and l.LocationId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<A_LocationVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool UpdateLocation(A_Location updateLoc, int Id)
        {
            string sql = " Update A_Location set LocationName=@LocationName, LocationCode=@LocationCode, DepartmentId=@DepartmentId," +
                "LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and LocationId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateLoc);
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

        public bool DeleteLocation(int Id, DateTime DeletedDate, int DeletedBy)
        {
            string sql = " Update A_Location set IsDeleted=1, DeletedBy=" + DeletedBy + ", DeletedDate=" + DeletedDate + " where LocationId= " + Id;
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