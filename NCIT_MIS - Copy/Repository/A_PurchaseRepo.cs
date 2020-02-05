using Dapper;
using NCIT_MIS.Models;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class A_PurchaseRepo
    {
        public A_PurchaseBill GetBillSerialNo()
        {
            string sql = " select top 1 BillSerialNo from A_PurchaseBill " +
                "where IsDeleted=0" +
                " order by BillSerialNo desc";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<A_PurchaseBill>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }

        public A_PurchaseBill GetBillSerialNo(int orgid, int id)
        {
            string sql = " select BillSerialNo from A_PurchaseBill" +
                " where OrganizationId=" + orgid + " and PurchaseId=" + id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<A_PurchaseBill>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }

        public int AddAssetPurchase(A_PurchaseBill apurchase)
        {
            string sql = " insert into A_PurchaseBill (" +
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

        public int AddAssetPurchaseItem(A_PurchaseItem purchaseitem)
        {
            string sql = " insert into A_PurchaseItem(" +
                         "DepartmentId, PurchaseBillId, AssetItemId, PurchaseQuantity, Rate, Total," +
                         " IsVerified, VerifiedBy," +
                         " VerifiedDate, EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate, IsDeleted," +
                         "  DeletedDate, DeletedBy" +
                         ")" +
                         " values(" +
                         "@DepartmentId, @PurchaseBillId, @AssetItemId, @PurchaseQuantity, @Rate, @Total," +
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
        public A_Purchase GetPurchaseListDetail(int depid, string fromdate, string todate, string billno, int vendorId)
        {
            A_Purchase detail = new A_Purchase();
            detail.A_PurchaseBillList = GetPurchaseList(depid, fromdate, todate, billno, vendorId);
            for (int i = 0; i < detail.A_PurchaseBillList.Count; i++)
            {
                detail.A_PurchaseBillList[i].AssetPurchaseItemByBillPurshaseIdList = GetPurchaseItemListByBillPurshaseId(depid, detail.A_PurchaseBillList[i].PurchaseBillId);
            }

            // detail.AssetPurchaseItems = GetPurchaseItemList(orgid);
            return detail;
        }

        public List<A_PurchaseBillVM> GetPurchaseList(int departmentId, string fromdate, string todate, string billno, int vendorId)
        {
            string sql = "select p.PurchaseBillId, p.DepartmentId, d.DepartmentName, p.VendorId, v.VendorName, p.BillNo, p.BillDate,p.BillDateBS,p.BillSerialNo," +
                " p.TotalAmount, p.VatApplicable, p.VatAmount, p.VatPercent,p.DiscountAmount,p.DiscountPercent,p.TaxableAmount,p.TotalWithVat, p.IsVerified, p.VerifiedBy, u.FullName VerifiedByName, p.VerifiedDate" +
                "  from A_PurchaseBill p" +
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
                return db.Query<A_PurchaseBillVM>(sql).ToList();
            }
        }

        public List<AssetPurchaseItemByBillPurshaseId> GetPurchaseItemListByBillPurshaseId(int depid, int purchaseId)
        {
            string sql = "select p.PurchaseBillId, p.AssetItemId, ai.AssetItemName, p.PurchaseQuantity, p.Rate, p.Total " +
                " from A_PurchaseItem p" +
                " left join A_Item ai on ai.AssetItemId = p.AssetItemId" +
                " where 1=1 and p.IsDeleted=0 and  p.PurchaseBillId=" + purchaseId + " and p.DepartmentId=" + depid;
            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<AssetPurchaseItemByBillPurshaseId>(sql).ToList();
            }
        }

        //For PurchaseItem Details
        public A_Purchase GetPurchaseDetail(int depId, int id)
        {
            A_Purchase detail = new A_Purchase();
            detail.A_PurchaseBillDetail = GetPurchase(depId, id);
            detail.A_PurchaseItemList = GetPurchaseItemList(depId, id);

            return detail;
        }

        public A_PurchaseBillVM GetPurchase(int depId, int id)
        {
            string sql = "select p.PurchaseBillId,p.BillSerialNo, p.DepartmentId, d.DepartmentName, p.VendorId, v.VendorName, " +
                "p.BillNo, p.BillDate,p.BillDateBS, p.TotalAmount,p.DiscountAmount,p.DiscountPercent,p.TaxableAmount,p.VatApplicable, p.VatAmount, p.VatPercent," +
                "p.TotalWithVat, p.IsVerified, p.VerifiedBy, u.FullName VerifiedByName, p.VerifiedDate,p.Remarks" +
                " from A_PurchaseBill p" +
                " left join Sys_Department d on d.DepartmentId = p.DepartmentId" +
                " left join A_Vendor v on v.VendorId = p.VendorId" +
                " left join Sys_User u on u.UserId = p.VerifiedBy" +
                " where 1 = 1 and p.IsDeleted = 0 and p.PurchaseBillId = " + id + " and p.DepartmentId = " + depId;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Close();
                return db.Query<A_PurchaseBillVM>(sql).SingleOrDefault();
            }
        }

        public List<A_PurchaseItemVM> GetPurchaseItemList(int depid, int purchaseid)
        {
            string sql = "select " +
                " p.PurchaseItemId, p.DepartmentId, d.DepartmentName, ai.AssetCategoryId, ac.CategoryName," +
                " p.PurchaseBillId, p.AssetItemId, ai.AssetItemName, p.PurchaseQuantity, p.Rate, p.Total," +
                " p.IsVerified, p.VerifiedBy," +
                " u.FullName VerifiedByName, p.VerifiedDate" +
                " from A_PurchaseItem p" +
                " left join Sys_Department d on d.DepartmentId = p.DepartmentId" +
                " left join A_Item ai on ai.AssetItemId = p.AssetItemId" +
                " left join Sys_User u on u.UserId = p.VerifiedBy" +
                " left join A_Category ac on ac.CategoryId = ai.AssetCategoryId" +
                " where 1=1 and p.IsDeleted=0 and p.PurchaseBillId=" + purchaseid + " and  p.DepartmentId=" + depid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<A_PurchaseItemVM>(sql).ToList();
            }
        }

        public void EditAssetPurchase(A_PurchaseBill apurchase)
        {
            string sql = " update A_PurchaseBill set " +
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

        public void EditAssetPurchaseItem(A_PurchaseItem purchaseitem)
        {
            string sql = " Update A_PurchaseItem set" +
                         " DepartmentId=@DepartmentId, AssetItemId=@AssetItemId, PurchaseQuantity=@PurchaseQuantity, Rate=@Rate, Total=@Total," +
                         " LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate, IsDeleted=@IsDeleted, DeletedBy=@DeletedBy, DeletedDate=@DeletedDate" +
                         " where PurchaseItemId=@PurchaseItemId";

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql, purchaseitem);
                db.Close();
            }
        }

        public void EditAssetPurchaseItemDelete(DateTime dateTime, int uid, int PurchaseBillId)
        {
            string sql = " Update A_PurchaseItem set IsDeleted=1,DeletedBy=" + uid + ",DeletedDate= '" + dateTime + "'" +
            " where PurchaseBillId=" + PurchaseBillId;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }

        public void DeleteAssetPurchaseBill(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update A_PurchaseBill set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
            " where PurchaseBillId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                db.Query(sql);
                db.Close();
            }
        }
    }
}