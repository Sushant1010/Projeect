using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class INV_UnitRepo
    {
        public int AddUnit(INV_Unit saveUnit)
        {
            string sql = "insert into INV_Unit(UnitName,UnitCode,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,DepartmentId,IsDeleted)" +
                " values(@UnitName,@UnitCode,@EnteredBy,@EnteredDate,0,null,@DepartmentId,0)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveUnit).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public List<INV_UnitVM> GetAllUnitist()
        {
            string sql = "select * from INV_Unit c " +
                "left join Sys_Department d on d.departmentId = c.departmentId";

            using (var db = DbHelper.GetDBConnection())
            {
                //db.Execute(sql);
                var lst = db.Query<INV_UnitVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public INV_UnitVM getUnitDetail(int id)
        {
            string sql = "select * from INV_Unit c" +
                " left join Sys_Department d on d.departmentId = c.departmentId" +
                " where c.UnitId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_UnitVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool UpdateUnit(INV_Unit updateUnit, int Id)
        {
            string sql = " Update INV_Unit set UnitName=@UnitName, UnitCode=@UnitCode," +
                "LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where UnitId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateUnit);
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

        public bool DeleteUnit(int Id)
        {
            string sql = " Delete INV_Unit where UnitId= " + Id;
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