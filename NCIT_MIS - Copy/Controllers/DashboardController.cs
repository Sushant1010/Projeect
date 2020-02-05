using NCIT_MIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NCIT_MIS.Controllers
{
    public class DashboardController : Controller
    {
        DashboardRepo db = new DashboardRepo();
        public ActionResult Index()
        {
            if(Session["UserId"] == null)
            {
                RedirectToAction("Index", "Login");
            }
            ViewBag.DepartmentCount = db.DepartmentCount();
            ViewBag.RemainingAssetItemCount = db.RemainingAssetItemCount();
            ViewBag.AssetCount = db.AssetCount();
            ViewBag.UserCount = db.UserCount();
            ViewBag.AssetVendorCount = db.AssetVendorCount();
            ViewBag.AssetRequestNotificationAdmin = db.AssetRequestNotificationAdmin();
            ViewBag.AssetRequestNotification = db.AssetRequestNotification();
            //ViewBag.InventoryCount = db.InventoryCount();


            ViewBag.RemainingInventoryItemCount = db.RemainingInventoryItemCount();
            //ViewBag.InventoryCount = db.InventoryCount();
            //ViewBag.InventoryVendorCount = db.InventoryVendorCount();
            ViewBag.InventoryRequestNotificationAdmin = db.InventoryRequestNotificationAdmin();
            ViewBag.InventoryRequestNotification = db.InventoryRequestNotification();

            var display = db.DashboardDisplay(Convert.ToInt32(Session["UserId"]));
            return View(display);
        }
    }
}