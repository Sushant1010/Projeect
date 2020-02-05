using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class DepartmentRepo
    {
        public int AddDepartment(Sys_Department saveDepart)
        {
            string sql = "insert into Sys_Department(DepartmentName,Email,DepartmentCode,PhoneNo,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate," +
                "IsDeleted,DeletedBy,DeletedDate)" +
                " values(@DepartmentName,@Email,@DepartmentCode,@PhoneNo,@EnteredBy,@EnteredDate,0,null," +
                "0,0,null)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveDepart).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public List<Sys_Department> GetAllDepartmentList()
        {
            string sql = "select * from Sys_Department where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {

                //db.Execute(sql);
                var lst = db.Query<Sys_Department>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public DepartmentVM getDepartmentDetail(int id)
        {
            string sql = "select * from Sys_Department where IsDeleted=0 and DepartmentId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<DepartmentVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool UpdateDepartment(Sys_Department updateDepart, int Id)
        {
            string sql = " Update Sys_Department set DepartmentName=@DepartmentName, Email=@Email, DepartmentCode=@DepartmentCode, PhoneNo=@PhoneNo," +
                "LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and DepartmentId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateDepart);
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

        public bool DeleteDepartment(int Id, DateTime DeletedDate, int DeletedBy)
        {
            string sql = " Update Sys_Department set IsDeleted=1, DeletedBy=" + DeletedBy + ", DeletedDate='" + DeletedDate + "' where DepartmentId= " + Id;
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