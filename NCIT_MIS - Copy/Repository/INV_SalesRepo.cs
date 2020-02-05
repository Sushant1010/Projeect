



using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class INV_SalesRepo
    {
        public INV_SalesBill GetBillSerialNo()
        {
            string sql = " select top 1 BillSerialNo from INV_SalesBill " +
                "where IsDeleted=0" +
                " order by BillSerialNo desc";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_SalesBill>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }

        public INV_SalesBill GetBillSerialNo(int orgid, int id)
        {
            string sql = " select BillSerialNo from INV_SalesBill" +
                " where OrganizationId=" + orgid + " and SalesId=" + id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_SalesBill>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }

        public int AddInventorySales(INV_SalesBill asales)
        {
            string sql = " insert into INV_SalesBill (" +
                        " DepartmentId,VendorId, BillNo, BillDate, BillDateBS, TotalAmount,VatApplicable,VatPercent,VatAmount, IsVerified,Remarks," +
                        " DiscountAmount,DiscountPercent,TaxableAmount,TotalWithVat,VerifiedBy, VerifiedDate, EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedBy," +
                        " DeletedDate, BillSerialNo" +
                        ")" +
                         " values " +
                         " (" +
                        "@DepartmentId,@VendorId, @BillNo, @BillDate, @BillDateBS, @TotalAmount, @VatApplicable,@VatPercent,@VatAmount, @IsVerified,@Remarks," +
                        " @DiscountAmount,@DiscountPercent,@TaxableAmount,@TotalWithVat,@VerifiedBy, @VerifiedDate, @EnteredBy, @EnteredDate, 0, null, 0, 0," +
                        " null, @BillSerialNo" +
                        ") SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                int a = db.Query<int>(sql, asales).SingleOrDefault();
                db.Close();
                return a;
            }
        }

        public int AddInventorySalesItem(INV_SalesItem salesitem)
        {
            string sql = " insert into INV_SalesItem(" +
                         "DepartmentId, SalesBillId, ItemId, SalesQuantity, Rate, Total," +
                         " IsVerified, VerifiedBy," +
                         " VerifiedDate, EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate, IsDeleted," +
                         "  DeletedDate, DeletedBy" +
                         ")" +
                         " values(" +
                         "@DepartmentId, @SalesBillId, @ItemId, @SalesQuantity, @Rate, @Total," +
                         " @IsVerified, @VerifiedBy," +
                         " @VerifiedDate, @EnteredBy, @EnteredDate, 0, null, 0," +
                         "  null, 0 " +
                         ")  SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                int a = db.Query<int>(sql, salesitem).SingleOrDefault();
                db.Close();
                return a;

            }
        }

        //For PurchaseItem Listing
        public INV_Sales GetSalesListDetail(int depid, string fromdate, string todate, string billno, int vendorId)
        {
            INV_Sales detail = new INV_Sales();
            detail.INV_SalesBillList = GetSalesList(depid, fromdate, todate, billno, vendorId);
            for (int i = 0; i < detail.INV_SalesBillList.Count; i++)
            {
                detail.INV_SalesBillList[i].InventorySalesItemByBillSalesIdList = GetSalesItemListByBillSalesId(depid, detail.INV_SalesBillList[i].SalesBillId);
            }

            // detail.AssetPurchaseItems = GetPurchaseItemList(orgid);
            return detail;
        }

        public List<INV_SalesBillVM> GetSalesList(int departmentId, string fromdate, string todate, string billno, int vendorId)
        {
            string sql = "select p.SalesBillId, p.DepartmentId, d.DepartmentName, p.VendorId, v.VendorName, p.BillNo, p.BillDate,p.BillDateBS,p.BillSerialNo," +
                " p.TotalAmount, p.VatApplicable, p.VatAmount, p.VatPercent,p.DiscountAmount,p.DiscountPercent,p.TaxableAmount,p.TotalWithVat, p.IsVerified, p.VerifiedBy, u.FullName VerifiedByName, p.VerifiedDate" +
                "  from INV_SalesBill p" +
                " left join Sys_Department d on d.DepartmentId = p.DepartmentId" +
                " left join A_Vendor v on v.VendorId = p.VendorId" +
                " left join Sys_User u on u.UserId = p.VerifiedBy" +
                " where 1 = 1 and p.IsDeleted = 0 and p.DepartmentId=" + departmentId;
            if (!string.IsNullOrEmpty(fromdate))
            {
                sql += " and p.EnteredDate >='" + fromdate + "'";
            }
            if (!string.IsNullOrEmpty(todate))
            {
                sql += " and p.EnteredDate <='" + todate + "'";
            }
            if (!string.IsNullOrEmpty(billno))
            {
                sql += " and p.BillNo like '%" + billno + "%'";
            }
            if (vendorId > 0)
            {
                sql += " and p.VendorId =" + vendorId;
            }
            sql += " order by p.BillSerialNo  desc";
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<INV_SalesBillVM>(sql).ToList();
            }
        }

        public List<InventorySalesItemByBillSalesId> GetSalesItemListByBillSalesId(int depid, int salesId)
        {
            string sql = "select p.SalesBillId, p.ItemId, ai.ItemName, p.SalesQuantity, p.Rate, p.Total " +
                " from A_PurchaseItem p" +
                " left join INV_Item ai on ai.ItemId = p.ItemId" +
                " where 1=1 and p.IsDeleted=0 and  p.SalesBillId=" + salesId + " and p.DepartmentId=" + depid;
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<InventorySalesItemByBillSalesId>(sql).ToList();
            }
        }

        //For PurchaseItem Details
        public INV_Sales GetSalesDetail(int depId, int id)
        {
            INV_Sales detail = new INV_Sales();
            detail.INV_SalesBillDetail = GetSales(depId, id);
            detail.INV_SalesItemList = GetSalesItemList(depId, id);

            return detail;
        }

        public INV_SalesBillVM GetSales(int depId, int id)
        {
            string sql = "select p.SalesBillId,p.BillSerialNo, p.DepartmentId, d.DepartmentName, p.VendorId, v.VendorName, " +
                "p.BillNo, p.BillDate,p.BillDateBS, p.TotalAmount,p.DiscountAmount,p.DiscountPercent,p.TaxableAmount,p.VatApplicable, p.VatAmount, p.VatPercent," +
                "p.TotalWithVat, p.IsVerified, p.VerifiedBy, u.FullName VerifiedByName, p.VerifiedDate,p.Remarks" +
                " from INV_SalesBill p" +
                " left join Sys_Department d on d.DepartmentId = p.DepartmentId" +
                " left join INV_Vendor v on v.VendorId = p.VendorId" +
                " left join Sys_User u on u.UserId = p.VerifiedBy" +
                " where 1 = 1 and p.IsDeleted = 0 and p.SalesBillId = " + id + " and p.DepartmentId = " + depId;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<INV_SalesBillVM>(sql).SingleOrDefault();
            }
        }

        public List<INV_SalesItemVM> GetSalesItemList(int depid, int salesid)
        {
            string sql = "select " +
                " p.SalesItemId, p.DepartmentId, d.DepartmentName, ai.tCategoryId, ac.CategoryName," +
                " p.SalesBillId, p.ItemId, ai.ItemName, p.SalesQuantity, p.Rate, p.Total," +
                " p.IsVerified, p.VerifiedBy," +
                " u.FullName VerifiedByName, p.VerifiedDate" +
                " from INV_SalesItem p" +
                " left join Sys_Department d on d.DepartmentId = p.DepartmentId" +
                " left join INV_Item ai on ai.ItemId = p.ItemId" +
                " left join Sys_User u on u.UserId = p.VerifiedBy" +
                " left join INV_Category ac on ac.CategoryId = ai.CategoryId" +
                " where 1=1 and p.IsDeleted=0 and p.SalesBillId=" + salesid + " and  p.DepartmentId=" + depid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<INV_SalesItemVM>(sql).ToList();
            }
        }

        public void EditInventorySales(INV_SalesBill asales)
        {
            string sql = " update INV_SalesBill set " +
                        "  DepartmentId=@DepartmentId,VendorId=@VendorId, BillNo=@BillNo, BillDate=@BillDate, BillDateBS=@BillDateBS," +
                        " TotalAmount=@TotalAmount, " +
                        " VatApplicable=@VatApplicable,VatAmount=@VatAmount,VATPercent=@VATPercent," +
                        " DiscountAmount=@DiscountAmount,DiscountPercent=@DiscountPercent,TaxableAmount=@TaxableAmount,TotalWithVat=@TotalWithVat," +
                        " LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate,Remarks=@Remarks" +
                        " where PurchaseBillId=@PurchaseBillId";

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql, asales);
                db.Close();
            }

        }

        public void EditInventorySalesItem(INV_SalesItem salesitem)
        {
            string sql = " Update INV_SalesItem set" +
                         " DepartmentId=@DepartmentId, ItemId=@ItemId, SalesQuantity=@SalesQuantity, Rate=@Rate, Total=@Total," +
                         " LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate, IsDeleted=@IsDeleted, DeletedBy=@DeletedBy, DeletedDate=@DeletedDate" +
                         " where SalesItemId=@SalesItemId";

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql, salesitem);
                db.Close();
            }
        }

        public void EditInventorySalesItemDelete(DateTime dateTime, int uid, int SalesBillId)
        {
            string sql = " Update INV_SalesItem set IsDeleted=1,DeletedBy=" + uid + ",DeletedDate= '" + dateTime + "'" +
            " where SalesBillId=" + SalesBillId;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }

        public void DeleteInventorySalesBill(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update INV_SalesBill set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
            " where SalesBillId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }
    }
}