
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
    public class INV_DistributionController : Controller
    {
        INV_DistributionRepo db = new INV_DistributionRepo();
        GetDropDown ddl = new GetDropDown();
        public ActionResult Index()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            var lst = db.getDistributionList(depid);
            return View(lst);
        }

        public ActionResult Detail(int? id)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            //_purchasebill sn = (db.getbillserialno(depid, (int)id));
            //int bsn = convert.toint32(sn.billserialno);
            //viewbag.bsn = bsn;
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var details = db.GetDistributionDetail((int)id);
            //if (details.inv_distributiondetail == null)
            //{
            //    return ttpnotfound();
            //}
            return View(details);
            
        }

        public ActionResult Create()
        {
            //INV_DistributionBill sn = (db.GetBillSerialNo());
            //if (sn == null)
            //{
            //    ViewBag.BSN = 1;
            //}
            //else
            //{
            //    int sn1 = Convert.ToInt32(sn.BillSerialNo) + 1;
            //    ViewBag.BSN = sn1;
            //}
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name");
            ViewBag.CategoryList = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
            ViewBag.ItemList = new SelectList(ddl.getItemList(depid), "Id", "Name");
            ViewBag.UnitList = new SelectList(ddl.getUnitList(depid), "Id", "Name");
            ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection frm, string[] hddrowindex)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            int purchaseid = 0;
            int totalcount = Convert.ToInt32(frm["hddrowindex"]);
            try
            {
             //Purchase Item Add
                if (hddrowindex != null)
                {
                    for (int i = 1; i <= totalcount; i++)
                    {
                        if (i == Convert.ToInt32(frm["" + i]))
                        {
                            INV_DistributionItem pitem = new INV_DistributionItem();
                            pitem.DepartmentId = Convert.ToInt32(frm["DepartmentId"]);
                            //pitem.DistributionBillId = purchaseid;
                            pitem.ItemId = Convert.ToInt32(frm["ItemId-" + i]);
                            pitem.SalesQuantity = Convert.ToDecimal(frm["Quantity-" + i]);
                            //pitem.Rate = Convert.ToDecimal(frm["Rate-" + i]);
                            pitem.UnitId = Convert.ToInt32(frm["UnitId-" + i]);
                            //pitem.Total = Convert.ToDecimal(frm["Total-" + i]);
                            pitem.IsVerified = true;
                            pitem.VerifiedBy = uid;
                            pitem.VerifiedDate = cdate;
                            pitem.IsDeleted = false;
                            pitem.EnteredBy = uid;
                            pitem.EnteredDate = cdate;
                            db.AddInventoryDistributionItem(pitem);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //int depid = Convert.ToInt32(Session["DepartmentId"]);
                ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name");
                ViewBag.CategoryList = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
                ViewBag.CategoryId = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
                ViewBag.ItemList = new SelectList(ddl.getItemList(depid), "Id", "Name");
                ViewBag.UnitList = new SelectList(ddl.getUnitList(depid), "Id", "Name");
                ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name");
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var details = db.GetDistributionDetail((int)id);
            ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name",details.DepartmentId);

            ViewBag.CategoryList = new SelectList(ddl.getCategoryList(depid), "Id", "Name", details.CategoryId);
            ViewBag.CategoryId = new SelectList(ddl.getCategoryList(depid), "Id", "Name", details.CategoryId);
            ViewBag.ItemList = new SelectList(ddl.getItemList(depid), "Id", "Name", details.ItemId);
            ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name",details.ItemId);
            ViewBag.UnitList = new SelectList(ddl.getUnitList(depid), "Id", "Name", details.UnitId);
            return View(details);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm, string[] hddrowindex)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            int distributionid = Convert.ToInt32(frm["DistributionBillId"]);
            int totalcount = Convert.ToInt32(frm["hddrowindex"]);
            try
            {
                //For bill Section
                INV_DistributionBill pbill = new INV_DistributionBill();
                

                pbill.DepartmentId = depid;
                pbill.LastUpdatedBy = uid;
                pbill.LastUpdatedDate = cdate;
                pbill.IsVerified = 1;
                pbill.VerifiedBy = uid;
                pbill.VerifiedDate = cdate;
                pbill.IsDeleted = false;

        

              

                db.EditInventoryDistributionItemDelete(cdate, uid, distributionid);
                //db.EditInventoryDistribution(pbill);

                //Purchase Item Add
                if (hddrowindex != null)
                {
                    INV_DistributionItem pitem = new INV_DistributionItem();
                    for (int i = 1; i <= totalcount; i++)
                    {
                        if (frm["DistributionItemId-" + i] == "0")
                        {
                            if (i == Convert.ToInt32(frm["" + i]))
                            {
                                pitem.DepartmentId = depid;
                                pitem.DistributionBillId = distributionid;
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
                                db.AddInventoryDistributionItem(pitem);
                            }
                        }
                        else
                        {
                            if (i == Convert.ToInt32(frm["" + i]))
                            {
                                pitem.DepartmentId = depid;
                                //pitem.PurchaseBillId = purchaseid;
                                pitem.DistributionItemId = Convert.ToInt32(frm["PurchaseItemId-" + i]);
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
                                db.EditInventoryDistributionItem(pitem);
                            }
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //int depid = Convert.ToInt32(Session["DepartmentId"]);
                
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
            //db.DeleteInventoryDistributionBill((int)id, uid, cdate);
            db.EditInventoryDistributionItemDelete(cdate, uid, (int)id);
            return RedirectToAction("Index");
        }

        public ActionResult Create1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int tokenId = Convert.ToInt32(id);
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            INV_DistributionVM depLoc = db.getDepartmentLocationByTokenId(tokenId);
            ViewBag.DepartmentId = depLoc.DepartmentId;
            ViewBag.DepartmentName = depLoc.DepartmentName;
            ViewBag.RequestQuantity = depLoc.RequestQuantity;
            ViewBag.CategoryId = depLoc.CategoryId;
            ViewBag.CategoryName = depLoc.CategoryName;
            ViewBag.ItemId = depLoc.ItemId;
            ViewBag.TokenId = tokenId;
            ViewBag.ItemName = depLoc.ItemName;
            ViewBag.UnitId = depLoc.UnitId;
            ViewBag.UnitName = depLoc.UnitName;

            return View();
        }

        [HttpPost]
        public ActionResult Create1(FormCollection frm, string[] hddrowindex)
        {
            A_AssetRepo aar = new A_AssetRepo();
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            int num = 0;
            int totalcount = Convert.ToInt32(frm["hddrowindex"]);
            try
            {
                //Purchase Item Add
                if (hddrowindex != null)
                {
                    for (int i = 1; i <= totalcount; i++)
                    {
                        if (i == Convert.ToInt32(frm["" + i]))
                        {
                            INV_DistributionItem pitem = new INV_DistributionItem();
                            pitem.DepartmentId = Convert.ToInt32(frm["DepartmentId"]);
                            //pitem.DistributionBillId = purchaseid;
                            int tokenId = Convert.ToInt32(frm["TokenId"]);
                            pitem.ItemId = Convert.ToInt32(frm["ItemId"]);
                            pitem.SalesQuantity = Convert.ToDecimal(frm["SalesQuantity"]);
                            //pitem.Rate = Convert.ToDecimal(frm["Rate-" + i]);
                            pitem.UnitId = Convert.ToInt32(frm["UnitId"]);
                            //pitem.Total = Convert.ToDecimal(frm["Total-" + i]);
                            pitem.IsVerified = true;
                            pitem.VerifiedBy = uid;
                            pitem.VerifiedDate = cdate;
                            pitem.IsDeleted = false;
                            pitem.EnteredBy = uid;
                            pitem.EnteredDate = cdate;
                            num = db.AddInventoryDistributionItem(pitem);
                            if(num > 0)
                            {
                                aar.ApprovedRequestTokenStore(tokenId, uid, cdate);
                            }
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //int depid = Convert.ToInt32(Session["DepartmentId"]);
                ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name");
                ViewBag.CategoryList = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
                ViewBag.CategoryId = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
                ViewBag.ItemList = new SelectList(ddl.getItemList(depid), "Id", "Name");
                ViewBag.UnitList = new SelectList(ddl.getUnitList(depid), "Id", "Name");
                ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name");
                return View();
            }
        }
    }
}

