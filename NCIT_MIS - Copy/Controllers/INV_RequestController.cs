

using NCIT_MIS.Models;
using NCIT_MIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NCIT_MIS.Controllers
{
    public class INV_RequestController : Controller
    {
        // GET: INV_Request
        Inv_RequestRepo db = new Inv_RequestRepo();
        GetDropDown ddl = new GetDropDown();
        public ActionResult Index()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            var lst = db.GetRequestListDetail(depid);
            return View(lst);
        }

        public ActionResult Request_Inventory()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            ViewBag.VendorId = new SelectList(ddl.getInventoryVendorList(depid), "Id", "Name");
            ViewBag.CategoryList = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
            ViewBag.ItemList = new SelectList(ddl.getItemList(depid), "Id", "Name");
            ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name");
            //ViewBag.LocationList = new SelectList(ddl.getLocationList(depid), "Id", "Name");
            ViewBag.DepartmentList = new SelectList(ddl.getDepartmentList(), "Id", "Name");
            ViewBag.UnitList = new SelectList(ddl.getUnitList(depid=0), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Request_Inventory(FormCollection frm, string[] hddrowindex)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            int totalcount = Convert.ToInt32(frm["hddrowindex"]);
            int token = 0;
            Request_Token checkToken = db.TokenCount();
            if (checkToken == null)
            {
                token = 1;
            }
            else
            {
                token = Convert.ToInt32(checkToken.RequestToken) + 1;
            }

            try
            {
                Request_Token rt = new Request_Token();
                rt.DepartmentId = depid;
                //rt.LocationId = Convert.ToInt32(frm["LocationId-1"]);
                rt.UnitId = Convert.ToInt32(frm["UnitId-1"]);
                rt.RequestToken = token;
                rt.RequestCategory = "Inventory";
                rt.ApprovedByAdmin = "Pending";
                rt.RequestStatus = "Pending";
                rt.IsDeleted = false;
                rt.EnteredBy = uid;
                rt.EnteredDate = cdate;

                int tokenid = db.AddToken(rt);

                if (hddrowindex != null)
                {
                    for (int i = 1; i <= totalcount; i++)
                    {
                        if (i == Convert.ToInt32(frm["" + i]))
                        {
                            Request requestItem = new Request();
                            requestItem.TokenId = tokenid;
                            requestItem.RequestItemId = Convert.ToInt32(frm["ItemId-" + i]);
                            requestItem.RequestQuantity = Convert.ToInt32(frm["Quantity-" + i]);
                            requestItem.UnitId = Convert.ToInt32(frm["UnitId-1"]);
                            requestItem.RequestedBy = uid;
                            requestItem.RequestedDate = cdate;

                            requestItem.IsDeleted = false;
                            requestItem.EnteredBy = uid;
                            requestItem.EnteredDate = cdate;
                            db.AddRequestItem(requestItem);
                        }
                    }
                }
                return RedirectToAction("Index","Request");
            }
            catch (Exception ex)
            {
                ViewBag.CategoryList = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
                ViewBag.CategoryId = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
                ViewBag.ItemList = new SelectList(ddl.getItemList(depid), "Id", "Name");
                ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name");
                ViewBag.LocationList = new SelectList(ddl.getLocationList(depid), "Id", "Name");
                return View();
            }
        }

        public ActionResult Request_InventoryEdit(int? id)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var details = db.GetRequestDetail(depid, (int)id);
            if (details.Request == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryList = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
            ViewBag.ItemList = new SelectList(ddl.getItemList(depid), "Id", "Name");
            ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name");
            ViewBag.LocationList = new SelectList(ddl.getLocationList(depid), "Id", "Name");
            return View(details);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Request_InventoryEdit(FormCollection frm, string[] hddrowindex)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            int tokenid = Convert.ToInt32(frm["TokenId"]);
            int totalcount = Convert.ToInt32(frm["hddrowindex"]);
            int LocationId = Convert.ToInt32(frm["LocationId-1"]);
            try
            {
                db.EditRequestToken(tokenid, LocationId);
                db.EditRequestItemDelete(cdate, uid, tokenid);

                //Purchase Item Add
                if (hddrowindex != null)
                {
                    Request requestItem = new Request();
                    for (int i = 1; i <= totalcount; i++)
                    {
                        if (frm["RequestId-" + i] == "0")
                        {
                            if (i == Convert.ToInt32(frm["" + i]))
                            {
                                requestItem.TokenId = tokenid;
                                requestItem.RequestItemId = Convert.ToInt32(frm["ItemId-" + i]);
                                requestItem.RequestQuantity = Convert.ToInt32(frm["Quantity-" + i]);
                                requestItem.RequestedBy = uid;
                                requestItem.RequestedDate = cdate;

                                requestItem.IsDeleted = false;
                                requestItem.EnteredBy = uid;
                                requestItem.EnteredDate = cdate;
                                db.AddRequestItem(requestItem);
                            }
                        }
                        else
                        {
                            if (i == Convert.ToInt32(frm["" + i]))
                            {
                                requestItem.RequestId = Convert.ToInt32(frm["RequestId-" + i]);
                                requestItem.RequestItemId = Convert.ToInt32(frm["ItemId-" + i]);
                                requestItem.RequestQuantity = Convert.ToInt32(frm["Quantity-" + i]);

                                requestItem.IsDeleted = false;
                                requestItem.DeletedBy = 0;
                                requestItem.DeletedDate = null;
                                requestItem.LastUpdatedBy = uid;
                                requestItem.LastUpdatedDate = cdate;
                                db.EditRequestItem(requestItem);
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
                ViewBag.LocationList = new SelectList(ddl.getLocationList(depid), "Id", "Name");
                return View();
            }
        }

        public ActionResult Request_InventoryDelete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            db.DeleteRequestToken((int)id, uid, cdate);
            db.EditRequestItemDelete(cdate, uid, (int)id);
            return RedirectToAction("Index");
        }

        //admin purpose
        public ActionResult Request_InventoryAccept()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            var lst = db.GetRequestListDetailAdmin(depid);
            return View(lst);
        }

        public ActionResult Request_InventoryApproved(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            db.ApprovedRequestToken((int)id, uid, cdate);
            return RedirectToAction("Request_InventoryAccept");
        }

        public ActionResult Request_InventoryReject(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            db.RejectRequestToken((int)id, uid, cdate);
            return RedirectToAction("Request_InventoryAccept");
        }

        //store purpose
        public ActionResult Request_InventoryAcceptStore()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            var lst = db.GetRequestListDetailStore(depid);
            return View(lst);
        }

        //public ActionResult Request_AssetApprovedStore(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    int uid = Convert.ToInt32(Session["UserId"]);
        //    var cdate = DateTime.Now;
        //    db.ApprovedRequestTokenStore((int)id, uid, cdate);
        //    return RedirectToAction("Request_AssetAccept");
        //}

        public ActionResult Request_InventoryRejectStore(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            db.RejectRequestTokenStore((int)id, uid, cdate);
            return RedirectToAction("Request_InventoryAccept");
        }
    }
}