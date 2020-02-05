



using NCIT_MIS.Models;
using NCIT_MIS.Repository;
using NCIT_MIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NCIT_MIS.Controllers
{
    public class INV_SalesController : Controller
    {
        INV_SalesRepo db = new INV_SalesRepo();
        GetDropDown ddl = new GetDropDown();
        public ActionResult Index()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
          
            return View();
        }

        public ActionResult Details(int? id)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            //A_PurchaseBill sn = (db.GetBillSerialNo(depid, (int)id));
            //int BSN = Convert.ToInt32(sn.BillSerialNo);
            //ViewBag.BSN = BSN;
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var details = db.GetSalesDetail(depid, (int)id);
            if (details.INV_SalesBillDetail == null)
            {
                return HttpNotFound();
            }
            return View(details);
        }

        public ActionResult Create()
        {
            INV_SalesBill sn = (db.GetBillSerialNo());
            if (sn == null)
            {
                ViewBag.BSN = 1;
            }
            else
            {
                int sn1 = Convert.ToInt32(sn.BillSerialNo) + 1;
                ViewBag.BSN = sn1;
            }
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            ViewBag.VendorId = new SelectList(ddl.getVendorList(depid), "Id", "Name");
            ViewBag.CategoryList = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
            ViewBag.ItemList = new SelectList(ddl.getItemList(depid), "Id", "Name");
            ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm, string[] hddrowindex)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            int salesid = 0;
            int totalcount = Convert.ToInt32(frm["hddrowindex"]);
            try
            {
                //For bill Section
                INV_SalesBill pbill = new INV_SalesBill();
                pbill.BillDate = frm["BillDate"];
                pbill.BillDateBS = frm["BillDateBS"];
                pbill.BillNo = frm["BillNo"];
                pbill.BillSerialNo = Convert.ToInt32(frm["BillSerialNo"]);
                pbill.VendorId = Convert.ToInt32(frm["VendorId"]);
                pbill.Remarks = frm["Remarks"];

                pbill.DepartmentId = depid;
                pbill.EnteredBy = uid;
                pbill.EnteredDate = cdate;
                pbill.IsVerified = 1;
                pbill.VerifiedBy = uid;
                pbill.VerifiedDate = cdate;
                pbill.IsDeleted = false;

                pbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                pbill.TotalWithVat = Convert.ToDecimal(frm["TotalWithVat"]);
                pbill.DiscountPercent = Convert.ToInt32(frm["DiscountPercent"]);
                pbill.DiscountAmount = Convert.ToDecimal(frm["DiscountAmount"]);
                pbill.TaxableAmount = Convert.ToDecimal(frm["TaxableAmount"]);

                if (frm["VatApplicable"] == "Yes")
                {
                    pbill.VatApplicable = 1;
                    pbill.VatAmount = Convert.ToDecimal(frm["VatAmount"]);
                    pbill.VatPercent = Convert.ToInt32(frm["VatPercent"]);
                }
                else
                {
                    pbill.VatApplicable = 0;
                    pbill.VatAmount = 0;
                    pbill.VatPercent = 0;
                }

                salesid = db.AddInventorySales(pbill);

                //Purchase Item Add
                if (hddrowindex != null)
                {
                    for (int i = 1; i <= totalcount; i++)
                    {
                        if (i == Convert.ToInt32(frm["" + i]))
                        {
                            INV_SalesItem pitem = new INV_SalesItem();
                            pitem.DepartmentId = depid;
                            pitem.SalesBillId = salesid;
                            pitem.ItemId = Convert.ToInt32(frm["ItemId-" + i]);
                            pitem.SalesQuantity = Convert.ToDecimal(frm["Quantity-" + i]);
                            pitem.Rate = Convert.ToDecimal(frm["Rate-" + i]);
                            pitem.Total = Convert.ToDecimal(frm["Total-" + i]);
                            pitem.IsVerified = true;
                            pitem.VerifiedBy = uid;
                            pitem.VerifiedDate = cdate;
                            pitem.IsDeleted = false;
                            pitem.EnteredBy = uid;
                            pitem.EnteredDate = cdate;
                            db.AddInventorySalesItem(pitem);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //int depid = Convert.ToInt32(Session["DepartmentId"]);
                ViewBag.VendorId = new SelectList(ddl.getVendorList(depid), "Id", "Name");
                ViewBag.CategoryList = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
                ViewBag.CategoryId = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
                ViewBag.ItemList = new SelectList(ddl.getItemList(depid), "Id", "Name");
                ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name");
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            //A_PurchaseBill sn = (db.GetBillSerialNo(depid, (int)id));
            //int BSN = Convert.ToInt32(sn.BillSerialNo);
            //ViewBag.BSN = BSN;
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var details = db.GetSalesDetail(depid, (int)id);
            if (details.INV_SalesBillDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.VendorId = new SelectList(ddl.getVendorList(depid), "Id", "Name", details.INV_SalesBillDetail.VendorId);
            ViewBag.CategoryList = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
            ViewBag.ItemList = new SelectList(ddl.getItemList(depid), "Id", "Name");
            ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name");
            return View(details);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm, string[] hddrowindex)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            int salesid = Convert.ToInt32(frm["SalesBillId"]);
            int totalcount = Convert.ToInt32(frm["hddrowindex"]);
            try
            {
                //For bill Section
                INV_SalesBill pbill = new INV_SalesBill();
                pbill.SalesBillId = salesid;
                pbill.BillDate = frm["BillDate"];
                pbill.BillDateBS = frm["BillDateBS"];
                pbill.BillNo = frm["BillNo"];
                pbill.BillSerialNo = Convert.ToInt32(frm["BillSerialNo"]);
                pbill.VendorId = Convert.ToInt32(frm["VendorId"]);
                pbill.Remarks = frm["Remarks"];

                pbill.DepartmentId = depid;
                pbill.LastUpdatedBy = uid;
                pbill.LastUpdatedDate = cdate;
                pbill.IsVerified = 1;
                pbill.VerifiedBy = uid;
                pbill.VerifiedDate = cdate;
                pbill.IsDeleted = false;

                pbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                pbill.TotalWithVat = Convert.ToDecimal(frm["TotalWithVat"]);
                pbill.DiscountPercent = Convert.ToInt32(frm["DiscountPercent"]);
                pbill.DiscountAmount = Convert.ToDecimal(frm["DiscountAmount"]);
                pbill.TaxableAmount = Convert.ToDecimal(frm["TaxableAmount"]);

                if (frm["VatApplicable"] == "Yes")
                {
                    pbill.VatApplicable = 1;
                    pbill.VatAmount = Convert.ToDecimal(frm["VatAmount"]);
                    pbill.VatPercent = Convert.ToInt32(frm["VatPercent"]);
                }
                else
                {
                    pbill.VatApplicable = 0;
                    pbill.VatAmount = 0;
                    pbill.VatPercent = 0;
                }

                db.EditInventorySalesItemDelete(cdate, uid, salesid);
                db.EditInventorySales(pbill);

                //Purchase Item Add
                if (hddrowindex != null)
                {
                    INV_SalesItem pitem = new INV_SalesItem();
                    for (int i = 1; i <= totalcount; i++)
                    {
                        if (frm["SalesItemId-" + i] == "0")
                        {
                            if (i == Convert.ToInt32(frm["" + i]))
                            {
                                pitem.DepartmentId = depid;
                                pitem.SalesBillId = salesid;
                                pitem.ItemId = Convert.ToInt32(frm["ItemId-" + i]);
                                pitem.SalesQuantity = Convert.ToDecimal(frm["Quantity-" + i]);
                                pitem.Rate = Convert.ToDecimal(frm["Rate-" + i]);
                                pitem.Total = Convert.ToDecimal(frm["Total-" + i]);
                                pitem.IsVerified = true;
                                pitem.VerifiedBy = uid;
                                pitem.VerifiedDate = cdate;
                                pitem.IsDeleted = false;
                                pitem.EnteredBy = uid;
                                pitem.EnteredDate = cdate;
                                db.AddInventorySalesItem(pitem);
                            }
                        }
                        else
                        {
                            if (i == Convert.ToInt32(frm["" + i]))
                            {
                                pitem.DepartmentId = depid;
                                //pitem.PurchaseBillId = purchaseid;
                                pitem.SalesItemId = Convert.ToInt32(frm["SalesItemId-" + i]);
                                pitem.ItemId = Convert.ToInt32(frm["ItemId-" + i]);
                                pitem.SalesQuantity = Convert.ToDecimal(frm["Quantity-" + i]);
                                pitem.Rate = Convert.ToDecimal(frm["Rate-" + i]);
                                pitem.Total = Convert.ToDecimal(frm["Total-" + i]);
                                //pitem.IsVerified = true;
                                //pitem.VerifiedBy = uid;
                                //pitem.VerifiedDate = cdate;
                                pitem.IsDeleted = false;
                                pitem.DeletedBy = 0;
                                pitem.DeletedDate = null;
                                pitem.LastUpdatedBy = uid;
                                pitem.LastUpdatedDate = cdate;
                                db.EditInventorySalesItem(pitem);
                            }
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //int depid = Convert.ToInt32(Session["DepartmentId"]);
                ViewBag.VendorId = new SelectList(ddl.getVendorList(depid), "Id", "Name");
                ViewBag.CategoryList = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
                ViewBag.CategoryId = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
                ViewBag.ItemList = new SelectList(ddl.getItemList(depid), "Id", "Name");
                ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name");
                return View();
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            db.DeleteInventorySalesBill((int)id, uid, cdate);
            db.EditInventorySalesItemDelete(cdate, uid, (int)id);
            return RedirectToAction("Index");
        }
    }
}