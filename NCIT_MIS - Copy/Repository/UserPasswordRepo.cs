using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class UserPasswordRepo
    {
        public SystemUserVM getUesrDetail(int id)
        {
            string sql = "select *,u.Email UEmail from Sys_User u" +
                " left join Sys_Department d on d.DepartmentId = u.DepartmentId" +
                " left join Sys_UserType ut on ut.UserTypeId = u.UserType" +
                " where u.IsDeleted=0 and u.UserId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<SystemUserVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public SystemUserVM CheckOldPassword(string oldPassword)
        {
            string sql = "Select * from Sys_User where password='"+oldPassword+"'";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<SystemUserVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public Sys_User getUesrDetailByUserId(int userId)
        {
            string sql = "Select * from Sys_User where UserId=" + userId;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<Sys_User>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool ChangePassword(Sys_User updateUser, int Id)
        {
            string sql = " Update Sys_User set Password=@Password, PasswordSalt=@PasswordSalt," +
                " LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and UserId= " + Id;
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
    }
}