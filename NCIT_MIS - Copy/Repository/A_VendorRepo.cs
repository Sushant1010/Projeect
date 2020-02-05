using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class A_VendorRepo
    {

        public List<A_VendorVM> GetAllVendorList()
        {
            string sql = "select * from A_Vendor " +
                " where IsDeleted=0";

            using (var db = DbHelper.GetDBConnection())
            {
                //db.Execute(sql);
                var lst = db.Query<A_VendorVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public A_VendorVM getVendorDetail(int id)
        {
            string sql = "select * from A_Vendor" +
                " where IsDeleted=0 and VendorId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<A_VendorVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public int AddVendor(A_Vendor saveVendor)
        {
            string sql = "insert into A_Vendor(VendorName,VendorAddress,VendorPhone,VendorMobile,PanNo,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate," +
                "IsDeleted,DeletedBy,DeletedDate)" +
                " values(@VendorName,@VendorAddress,@VendorPhone,@VendorMobile,@PanNo,@EnteredBy,@EnteredDate,0,null," +
                "0,0,null)";

            using (var db = DbHelper.GetDBConnection())
            {
                int id = db.Query<int>(sql, saveVendor).SingleOrDefault();
                db.Close();
                return id;
            }
        }

        public bool UpdateVendor(A_Vendor updateVendor, int Id)
        {
            string sql = " Update A_Vendor set VendorName=@VendorName, VendorAddress=@VendorAddress,VendorPhone=@VendorPhone,VendorMobile=@VendorMobile,PanNo=@PanNo," +
                "LastUpdatedDate=@LastUpdatedDate, LastUpdatedBy=@LastUpdatedBy where IsDeleted=0 and VendorId= " + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Execute(sql, updateVendor);
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

        public bool DeleteVendor(int Id, DateTime deletedDate, int deletedby)
        {
            string sql = " Update A_Vendor set IsDeleted=1, DeletedBy='" + deletedby + "', DeletedDate='" + deletedDate + "' where VendorId= " + Id;
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