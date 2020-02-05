using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Web;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;

namespace NCIT_MIS.Repository
{
    public class UserRepo
    {

        public List<SystemUserVM> getAllUesrList()
        {
            string sql = "select *,u.Email UEmail from Sys_User u" +
                " left join Sys_Department d on d.DepartmentId = u.DepartmentId" +
                " left join Sys_UserType ut on ut.UserTypeId = u.UserType" +
                " where u.IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {

                //db.Execute(sql);
                var lst = db.Query<SystemUserVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public int AddUsers(Sys_User saveUser)
        {
            string sql = "insert into Sys_User(FullName,Email,Password,DepartmentId,UserType,ActiveCode,IsActive,EnteredDate,EnteredBy,LastUpdatedDate" +
                ",LastUpdatedBy,IsDeleted,DeletedBy,DeletedDate, PasswordSalt,IsAdmin,AssetModuleAllow,InventoryModuleAllow,RequestModuleAllow)" +
                " values(@FullName,@Email,@Password,@DepartmentId,@UserType,null,@IsActive,@EnteredDate,@EnteredBy,null" +
                ",0,0,0,null,@PasswordSalt,0,0,0,0)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveUser).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public bool UpdateUser(Sys_User updateUser, int Id)
        {
            string sql = " Update Sys_User set FullName=@FullName, Email=@Email, Password=@Password, DepartmentId=@DepartmentId," +
                "UserType=@UserType, IsActive=@IsActive, LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and UserId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateUser);
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

        public SystemUserVM getUesrDetail(int id)
        {
            string sql = "select * from Sys_User where IsDeleted=0 and UserId="+id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<SystemUserVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool DeleteUser(int Id, DateTime DeletedDate, int DeletedBy)
        {
            string sql = " Update Sys_User set IsDeleted=1, DeletedBy=" + DeletedBy + ", DeletedDate='" + DeletedDate + "' where UserId= " + Id;
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