

using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class INV_CategoryRepo
    {
        public int AddCategory(INV_Category saveCate)
        {
            string sql = "insert into INV_Category(CategoryName,CategoryCode,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,DepartmentId,IsDeleted)" +
                " values(@CategoryName,@CategoryCode,@EnteredBy,@EnteredDate,0,null,@DepartmentId,0)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveCate).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public List<INV_CategoryVM> GetAllCategoryList()
        {
            string sql = "select * from INV_Category c " +
                "left join Sys_Department d on d.departmentId = c.departmentId";

            using (var db = DbHelper.GetDBConnection())
            {
                //db.Execute(sql);
                var lst = db.Query<INV_CategoryVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public INV_CategoryVM getCategoryDetail(int id)
        {
            string sql = "select * from INV_Category c" +
                " left join Sys_Department d on d.departmentId = c.departmentId" +
                " where c.CategoryId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_CategoryVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool UpdateCategory(INV_Category updateCate, int Id)
        {
            string sql = " Update INV_Category set CategoryName=@CategoryName, CategoryCode=@CategoryCode," +
                "LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where CategoryId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateCate);
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

        public bool DeleteCategory(int Id)
        {
            string sql = " Delete INV_Category where CategoryId= " + Id;
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