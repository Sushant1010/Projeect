using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class UserPermissionRepo
    {
        public SystemUserVM getUesrDetail(int id)
        {
            string sql = "select FullName,Email,IsAdmin,AssetModuleAllow,InventoryModuleAllow,RequestModuleAllow from Sys_User where IsDeleted=0 and UserId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<SystemUserVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool UpdateUserPermission(Sys_User updateUserPermission, int Id)
        {
            string sql = " Update Sys_User set IsAdmin=@IsAdmin, AssetModuleAllow=@AssetModuleAllow, InventoryModuleAllow=@InventoryModuleAllow," +
                " RequestModuleAllow=@RequestModuleAllow, LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and UserId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateUserPermission);
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