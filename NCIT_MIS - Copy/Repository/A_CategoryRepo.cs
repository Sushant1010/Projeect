using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class A_CategoryRepo
    {
        public int AddCategory(A_Category saveCate)
        {
            string sql = "insert into A_Category(CategoryName,CategoryCode,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,DepartmentId," +
                "IsDeleted,DeletedBy,DeletedDate)" +
                " values(@CategoryName,@CategoryCode,@EnteredBy,@EnteredDate,0,null,@DepartmentId," +
                "0,0,null)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveCate).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public List<A_CategoryVM> GetAllCategoryList()
        {
            string sql = "select * from A_Category c " +
                "left join Sys_Department d on d.departmentId = c.departmentId" +
                " where c.IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                //db.Execute(sql);
                var lst = db.Query<A_CategoryVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public A_CategoryVM getCategoryDetail(int id)
        {
            string sql = "select * from A_Category c" +
                " left join Sys_Department d on d.departmentId = c.departmentId" +
                " where c.IsDeleted=0 and c.CategoryId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<A_CategoryVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool UpdateCategory(A_Category updateCate, int Id)
        {
            string sql = " Update A_Category set CategoryName=@CategoryName, CategoryCode=@CategoryCode," +
                "LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and CategoryId= " + Id;
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

        public bool DeleteCategory(int Id, DateTime deletedDate, int deletedby)
        {
            string sql = " Update A_Category set IsDeleted=1, DeletedBy=" + deletedby + ", DeletedDate=" + deletedDate + " where CategoryId= " + Id;
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