using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class UserTypeRepo
    {
        public int AddUT(Sys_UserType saveUT)
        {
            string sql = "insert into Sys_UserType(UserTypeName,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,DepartmentId," +
                "IsDeleted,DeletedBy,DeletedDate)" +
                " values(@UserTypeName,@EnteredBy,@EnteredDate,0,null,@DepartmentId," +
                "0,0,null)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveUT).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public List<SystemUserTypeVM> GetAllUserTypeList()
        {
            string sql = "select * from Sys_UserType ut " +
                "left join Sys_Department d on d.departmentId = ut.departmentId" +
                " where ut.IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                //db.Execute(sql);
                var lst = db.Query<SystemUserTypeVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public SystemUserTypeVM getUserTypeDetail(int id)
        {
            string sql = "select * from Sys_UserType ut" +
                " left join Sys_Department d on d.departmentId = ut.departmentId" +
                " where ut.IsDeleted=0 and ut.UserTypeId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<SystemUserTypeVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool UpdateUT(Sys_UserType updateUT, int Id)
        {
            string sql = " Update Sys_UserType set UserTypeName=@UserTypeName," +
                "LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and UserTypeId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateUT);
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

        public bool DeleteUT(int Id, DateTime deletedDate, int deletedby)
        {
            string sql = " Update Sys_UserType set IsDeleted=1, DeletedBy="+deletedby+", DeletedDate="+deletedDate+"  where UserTypeId= " + Id;
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