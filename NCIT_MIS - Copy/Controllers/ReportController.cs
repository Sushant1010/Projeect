using NCIT_MIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NCIT_MIS.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        ReportRepo db = new ReportRepo();
        GetDropDown ddl = new GetDropDown();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Asset_Report(string DepartmentId="", string LocationId="", string CategoryId="", string AssetItemId="")
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name");
            ViewBag.LocationId = new SelectList(ddl.getLocationList(), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
            ViewBag.AssetItemId = new SelectList(ddl.getAssetItemList(depid), "Id", "Name");
            var lst = db.AssetReport(DepartmentId, LocationId, CategoryId, AssetItemId);
            return View(lst);
        }

        public ActionResult AssetPurchase_Report()
        {
            A_PurchaseRepo dbs = new A_PurchaseRepo();
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            var lst = dbs.GetPurchaseListDetail(depid, "", "", "", 0);
            return View(lst);
        }

        public ActionResult AssetStock_Report()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            var lst = db.GetAssetStockList();
            return View(lst);
        }

        public ActionResult Inventory_Report(string DepartmentId = "", string LocationId = "", string CategoryId = "", string ItemId = "")
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name");
            //ViewBag.LocationId = new SelectList(ddl.getLocationList(), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.getCategoryList(depid), "Id", "Name");
            ViewBag.ItemId = new SelectList(ddl.getItemList(depid), "Id", "Name");
            var lst = db.InventoryReport(DepartmentId, LocationId, CategoryId, ItemId);
            return View(lst);
        }

        public ActionResult InventoryPurchase_Report()
        {
            INV_PurchaseRepo dbs = new INV_PurchaseRepo();
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            var lst = dbs.GetPurchaseListDetail(depid, "", "", "", 0);
            ViewBag.VendorId = new SelectList(ddl.getVendorList(depid), "Id", "Name");
            return View(lst);
        }

        public ActionResult InventoryStock_Report()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            var lst = db.GetInvStockList();
            return View(lst);
        }
    }
}