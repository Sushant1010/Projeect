﻿using NCIT_MIS.Models;
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
    public class A_PurchaseController : Controller
    {
        A_PurchaseRepo db = new A_PurchaseRepo();
        GetDropDown ddl = new GetDropDown();
        public ActionResult Index(string FromDate = "", string ToDate = "", string BillNo = "", int VendorId = 0)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            var lst = db.GetPurchaseListDetail(depid, FromDate, ToDate, BillNo, VendorId);
            ViewBag.VendorId = new SelectList(ddl.getVendorList(depid), "Id", "Name");
            return View(lst);
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
            var details = db.GetPurchaseDetail(depid, (int)id);
            if (details.A_PurchaseBillDetail == null)
            {
                return HttpNotFound();
            }
            return View(details);
        }

        public ActionResult Create()
        {
            A_PurchaseBill sn = (db.GetBillSerialNo());
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
            ViewBag.CategoryList = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
            ViewBag.AssetItemList = new SelectList(ddl.getAssetItemList(depid), "Id", "Name");
            ViewBag.AssetItemId = new SelectList(ddl.getAssetItemList(depid), "Id", "Name");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm, string[] hddrowindex)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            int purchaseid = 0;
            int totalcount = Convert.ToInt32(frm["hddrowindex"]);
            try
            {
                //For bill Section
                A_PurchaseBill pbill = new A_PurchaseBill();
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

                purchaseid = db.AddAssetPurchase(pbill);

                //Purchase Item Add
                if (hddrowindex != null)
                {
                    for(int i=1; i<=totalcount; i++)
                    {
                        if (i == Convert.ToInt32(frm[""+i]))
                        {
                            A_PurchaseItem pitem = new A_PurchaseItem();
                            pitem.DepartmentId = depid;
                            pitem.PurchaseBillId = purchaseid;
                            pitem.AssetItemId = Convert.ToInt32(frm["AssetItemId-" + i]);
                            pitem.PurchaseQuantity = Convert.ToDecimal(frm["Quantity-" + i]);
                            pitem.Rate = Convert.ToDecimal(frm["Rate-" + i]);
                            pitem.Total = Convert.ToDecimal(frm["Total-" + i]);
                            pitem.IsVerified = true;
                            pitem.VerifiedBy = uid;
                            pitem.VerifiedDate = cdate;
                            pitem.IsDeleted = false;
                            pitem.EnteredBy = uid;
                            pitem.EnteredDate = cdate;
                            db.AddAssetPurchaseItem(pitem);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                //int depid = Convert.ToInt32(Session["DepartmentId"]);
                ViewBag.VendorId = new SelectList(ddl.getVendorList(depid), "Id", "Name");
                ViewBag.CategoryList = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
                ViewBag.CategoryId = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
                ViewBag.AssetItemList = new SelectList(ddl.getAssetItemList(depid), "Id", "Name");
                ViewBag.AssetItemId = new SelectList(ddl.getAssetItemList(depid), "Id", "Name");
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
            var details = db.GetPurchaseDetail(depid, (int)id);
            if (details.A_PurchaseBillDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.VendorId = new SelectList(ddl.getVendorList(depid), "Id", "Name", details.A_PurchaseBillDetail.VendorId);
            ViewBag.CategoryList = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
            ViewBag.AssetItemList = new SelectList(ddl.getAssetItemList(depid), "Id", "Name");
            ViewBag.AssetItemId = new SelectList(ddl.getAssetItemList(depid), "Id", "Name");
            return View(details);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm, string[] hddrowindex)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            int purchaseid = Convert.ToInt32(frm["PurchaseBillId"]);
            int totalcount = Convert.ToInt32(frm["hddrowindex"]);
            try
            {
                //For bill Section
                A_PurchaseBill pbill = new A_PurchaseBill();
                pbill.PurchaseBillId = purchaseid;
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
                pbill.DiscountPercent = Convert.ToDecimal(frm["DiscountPercent"]);
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

                db.EditAssetPurchaseItemDelete(cdate,uid,purchaseid);
                db.EditAssetPurchase(pbill);

                //Purchase Item Add
                if (hddrowindex != null)
                {
                    A_PurchaseItem pitem = new A_PurchaseItem();
                    for (int i = 1; i <= totalcount; i++)
                    {
                        if (frm["PurchaseItemId-" + i] == "0")
                        {
                            if (i == Convert.ToInt32(frm["" + i]))
                            {
                                pitem.DepartmentId = depid;
                                pitem.PurchaseBillId = purchaseid;
                                pitem.AssetItemId = Convert.ToInt32(frm["AssetItemId-" + i]);
                                pitem.PurchaseQuantity = Convert.ToDecimal(frm["Quantity-" + i]);
                                pitem.Rate = Convert.ToDecimal(frm["Rate-" + i]);
                                pitem.Total = Convert.ToDecimal(frm["Total-" + i]);
                                pitem.IsVerified = true;
                                pitem.VerifiedBy = uid;
                                pitem.VerifiedDate = cdate;
                                pitem.IsDeleted = false;
                                pitem.EnteredBy = uid;
                                pitem.EnteredDate = cdate;
                                db.AddAssetPurchaseItem(pitem);
                            }
                        }
                        else
                        {
                            if (i == Convert.ToInt32(frm["" + i]))
                            {
                                pitem.DepartmentId = depid;
                                //pitem.PurchaseBillId = purchaseid;
                                pitem.PurchaseItemId = Convert.ToInt32(frm["PurchaseItemId-" + i]);
                                pitem.AssetItemId = Convert.ToInt32(frm["AssetItemId-" + i]);
                                pitem.PurchaseQuantity = Convert.ToDecimal(frm["Quantity-" + i]);
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
                                db.EditAssetPurchaseItem(pitem);
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
                ViewBag.CategoryList = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
                ViewBag.CategoryId = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
                ViewBag.AssetItemList = new SelectList(ddl.getAssetItemList(depid), "Id", "Name");
                ViewBag.AssetItemId = new SelectList(ddl.getAssetItemList(depid), "Id", "Name");
                //return View();
                return RedirectToAction("Edit");
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
            db.DeleteAssetPurchaseBill((int)id, uid, cdate);
            db.EditAssetPurchaseItemDelete(cdate, uid, (int)id);
            return RedirectToAction("Index");
        }
    }
}