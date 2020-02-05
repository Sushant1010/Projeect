

using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class INV_PurchaseRepo
    {
        public INV_PurchaseBill GetBillSerialNo()
        {
            string sql = " select top 1 BillSerialNo from INV_PurchaseBill " +
                "where IsDeleted=0" +
                " order by BillSerialNo desc";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_PurchaseBill>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }

        public INV_PurchaseBill GetBillSerialNo(int orgid, int id)
        {
            string sql = " select BillSerialNo from INV_PurchaseBill" +
                " where OrganizationId=" + orgid + " and PurchaseId=" + id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_PurchaseBill>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }

        public int AddInventoryPurchase(INV_PurchaseBill apurchase)
        {
            string sql = " insert into INV_PurchaseBill (" +
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
                int a = db.Query<int>(sql, apurchase).SingleOrDefault();
                db.Close();
                return a;
            }
        }

        public int AddInventoryPurchaseItem(INV_PurchaseItem purchaseitem)
        {
            string sql = " insert into INV_PurchaseItem(" +
                         "DepartmentId, PurchaseBillId, ItemId, PurchaseQuantity, Rate, UnitId, Total," +
                         " IsVerified, VerifiedBy," +
                         " VerifiedDate, EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate, IsDeleted," +
                         "  DeletedDate, DeletedBy" +
                         ")" +
                         " values(" +
                         "@DepartmentId, @PurchaseBillId, @ItemId, @PurchaseQuantity, @Rate, @UnitId, @Total," +
                         " @IsVerified, @VerifiedBy," +
                         " @VerifiedDate, @EnteredBy, @EnteredDate, 0, null, 0," +
                         "  null, 0 " +
                         ")  SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                int a = db.Query<int>(sql, purchaseitem).SingleOrDefault();
                db.Close();
                return a;

            }
        }

        //For PurchaseItem Listing
        public INV_Purchase GetPurchaseListDetail(int depid, string fromdate, string todate, string billno, int vendorId)
        {
            INV_Purchase detail = new INV_Purchase();
            detail.INV_PurchaseBillList = GetPurchaseList(depid, fromdate, todate, billno, vendorId);
            for (int i = 0; i < detail.INV_PurchaseBillList.Count; i++)
            {
                detail.INV_PurchaseBillList[i].InventoryPurchaseItemByBillPurshaseIdList = GetPurchaseItemListByBillPurshaseId(depid, detail.INV_PurchaseBillList[i].PurchaseBillId);
            }

            // detail.AssetPurchaseItems = GetPurchaseItemList(orgid);
            return detail;
        }

        public List<INV_PurchaseBillVM> GetPurchaseList(int departmentId, string fromdate, string todate, string billno, int vendorId)
        {
            string sql = "select p.PurchaseBillId, p.DepartmentId, d.DepartmentName, p.VendorId, v.VendorName, p.BillNo, p.BillDate,p.BillDateBS,p.BillSerialNo," +
                " p.TotalAmount, p.VatApplicable, p.VatAmount, p.VatPercent,p.DiscountAmount,p.DiscountPercent,p.TaxableAmount,p.TotalWithVat, p.IsVerified, p.VerifiedBy, u.FullName VerifiedByName, p.VerifiedDate" +
                "  from INV_PurchaseBill p" +
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
                return db.Query<INV_PurchaseBillVM>(sql).ToList();
            }
        }

        public List<InventoryPurchaseItemByBillPurshaseId> GetPurchaseItemListByBillPurshaseId(int depid, int purchaseId)
        {
            string sql = "select p.PurchaseBillId, p.ItemId, ai.ItemName, p.PurchaseQuantity, p.Rate, p.Total " +
                " from INV_PurchaseItem p" +
                " left join INV_Item ai on ai.ItemId = p.ItemId" +
                " where 1=1 and p.IsDeleted=0 and  p.PurchaseBillId=" + purchaseId + " and p.DepartmentId=" + depid;
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<InventoryPurchaseItemByBillPurshaseId>(sql).ToList();
            }
        }

        //For PurchaseItem Details
        public INV_Purchase GetPurchaseDetail(int depId, int id)
        {
            INV_Purchase detail = new INV_Purchase();
            detail.INV_PurchaseBillDetail = GetPurchase(depId, id);
            detail.INV_PurchaseItemList = GetPurchaseItemList(depId, id);

            return detail;
        }

        public INV_PurchaseBillVM GetPurchase(int depId, int id)
        {
            string sql = "select p.PurchaseBillId,p.BillSerialNo, p.DepartmentId, d.DepartmentName, p.VendorId, v.VendorName, " +
                "p.BillNo, p.BillDate,p.BillDateBS, p.TotalAmount,p.DiscountAmount,p.DiscountPercent,p.TaxableAmount,p.VatApplicable, p.VatAmount, p.VatPercent," +
                "p.TotalWithVat, p.IsVerified, p.VerifiedBy, u.FullName VerifiedByName, p.VerifiedDate,p.Remarks" +
                " from INV_PurchaseBill p" +
                " left join Sys_Department d on d.DepartmentId = p.DepartmentId" +
                " left join INV_Vendor v on v.VendorId = p.VendorId" +
                " left join Sys_User u on u.UserId = p.VerifiedBy" +
                " where 1 = 1 and p.IsDeleted = 0 and p.PurchaseBillId = " + id + " and p.DepartmentId = " + depId;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<INV_PurchaseBillVM>(sql).SingleOrDefault();
            }
        }

        public List<INV_PurchaseItemVM> GetPurchaseItemList(int depid, int purchaseid)
        {
            string sql = "select " +
                " p.PurchaseItemId, p.DepartmentId, d.DepartmentName, ai.CategoryId, ac.CategoryName," +
                " p.PurchaseBillId, p.ItemId, ai.ItemName, p.PurchaseQuantity, p.Rate, p.Total," +
                " p.IsVerified, p.VerifiedBy," +
                " u.FullName VerifiedByName, p.VerifiedDate" +
                " from INV_PurchaseItem p" +
                " left join Sys_Department d on d.DepartmentId = p.DepartmentId" +
                " left join INV_Item ai on ai.ItemId = p.ItemId" +
                " left join Sys_User u on u.UserId = p.VerifiedBy" +
                " left join INV_Category ac on ac.CategoryId = ai.CategoryId" +
                " where 1=1 and p.IsDeleted=0 and p.PurchaseBillId=" + purchaseid + " and  p.DepartmentId=" + depid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<INV_PurchaseItemVM>(sql).ToList();
            }
        }

        public void EditInventoryPurchase(INV_PurchaseBill apurchase)
        {
            string sql = " update INV_PurchaseBill set " +
                        "  DepartmentId=@DepartmentId,VendorId=@VendorId, BillNo=@BillNo, BillDate=@BillDate, BillDateBS=@BillDateBS," +
                        " TotalAmount=@TotalAmount, " +
                        " VatApplicable=@VatApplicable,VatAmount=@VatAmount,VATPercent=@VATPercent," +
                        " DiscountAmount=@DiscountAmount,DiscountPercent=@DiscountPercent,TaxableAmount=@TaxableAmount,TotalWithVat=@TotalWithVat," +
                        " LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate,Remarks=@Remarks" +
                        " where PurchaseBillId=@PurchaseBillId";

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql, apurchase);
                db.Close();
            }

        }

        public void EditInventoryPurchaseItem(INV_PurchaseItem purchaseitem)
        {
            string sql = " Update INV_PurchaseItem set" +
                         " DepartmentId=@DepartmentId, ItemId=@ItemId, PurchaseQuantity=@PurchaseQuantity, Rate=@Rate, Total=@Total," +
                         " LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate, IsDeleted=@IsDeleted, DeletedBy=@DeletedBy, DeletedDate=@DeletedDate" +
                         " where PurchaseItemId=@PurchaseItemId";

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql, purchaseitem);
                db.Close();
            }
        }

        public void EditInventoryPurchaseItemDelete(DateTime dateTime, int uid, int PurchaseBillId)
        {
            string sql = " Update INV_PurchaseItem set IsDeleted=1,DeletedBy=" + uid + ",DeletedDate= '" + dateTime + "'" +
            " where PurchaseBillId=" + PurchaseBillId;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }

        public void DeleteInventoryPurchaseBill(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update INV_PurchaseBill set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
            " where PurchaseBillId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }
    }
}