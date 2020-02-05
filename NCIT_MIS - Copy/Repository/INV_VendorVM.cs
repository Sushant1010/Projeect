

using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class INV_VendorRepo
    {
        public int AddVendor(INV_Vendor saveVen)
        {
            string sql = "insert into INV_Vendor(VendorName,VendorCode,Address,Phone,Mobile,PanNo,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,DepartmentId,IsDeleted)" +
                " values(@VendorName,@VendorCode,@Address,@Phone,@Mobile,@PanNo,@EnteredBy,@EnteredDate,0,null,@DepartmentId,0)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveVen).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public List<INV_VendorVM> GetAllVendorList()
        {
            string sql = "select * from INV_Vendor c " +
                "left join Sys_Department d on d.departmentId = c.departmentId";

            using (var db = DbHelper.GetDBConnection())
            {
                //db.Execute(sql);
                var lst = db.Query<INV_VendorVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }




        public INV_VendorVM getVendorDetail(int id)
        {
            string sql = "select * from INV_Vendor c" +
                " left join Sys_Department d on d.departmentId = c.departmentId" +
                " where c.VendorId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_VendorVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool UpdateVendor(INV_Vendor updateVen, int Id)
        {
            string sql = " Update INV_Vendor set VendorName=@VendorName, VendorCode=@VendorCode, Address=@Address, Phone=@Phone, Mobile=@Mobile, PanNo=@PanNo" +
                "LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where VendorId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateVen);
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

        public bool DeleteVendor(int Id)
        {
            string sql = " Delete INV_Vendor where VendorId= " + Id;
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