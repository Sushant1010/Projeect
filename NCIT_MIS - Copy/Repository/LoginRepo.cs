using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Web;
using NCIT_MIS.Models;

namespace NCIT_MIS.Repository
{
    public class LoginRepo
    {

        public Sys_User matchIdPassword(string email, string password)
        {
            string sql = "select * from Sys_User where IsDeleted=0 and Email='" + email + "' and Password='" + password + "'";

            using (var db = DbHelper.GetDBConnection())
            {

                //db.Execute(sql);
                var lst = db.Query<Sys_User>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public Sys_User GetSaltValue(string Email)
        {
            string sql = "select PasswordSalt from Sys_User where IsDeleted=0 and Email='" + Email + "'";

            using (var db = DbHelper.GetDBConnection())
            {

                //db.Execute(sql);
                var lst = db.Query<Sys_User>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public Sys_UserType getUserTypeName(int userTypeId)
        {
            string sql = "select UserTypeName from Sys_UserType where IsDeleted=0 and UserTypeId='" + userTypeId + "'";

            using (var db = DbHelper.GetDBConnection())
            {

                //db.Execute(sql);
                var lst = db.Query<Sys_UserType>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
    }
}