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
    public class A_ItemController : Controller
    {
        A_ItemRepo db = new A_ItemRepo();
        GetDropDown ddl = new GetDropDown();


        // GET: User
        public ActionResult Index()
        {

            var detail = db.getAllItemList();
            return View(detail);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_ItemVM detail = db.getItemDetail((int)id);

            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        public ActionResult Create()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            ViewBag.CategoryId = new SelectList(ddl.getCategoryListByDepartment(depid), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection frm)
        {
            A_Item saveItem = new A_Item();

            saveItem.AssetItemName = frm["AssetName"];
            saveItem.AssetItemCode = frm["AssetCode"];
            saveItem.AssetCategoryId = Convert.ToInt32(frm["CategoryId"]);
            saveItem.DepartmentId = Convert.ToInt32(Session["DepartmentId"]);

            if (frm["IsWarranty"] == "Y")
            {
                saveItem.IsWarranty = true;
                saveItem.WarrantyDuration = Convert.ToInt32(frm["WarrantyDuration"]);
                saveItem.WarrantyFromDate = DateTime.ParseExact(frm["FromDate"], "yyyy-MM-dd", null);
                saveItem.WarrantyFromDateBS = frm["FromDateBS"];
                saveItem.WarrantyToDate = DateTime.ParseExact(frm["ToDate"], "yyyy-MM-dd", null);
                saveItem.WarrantyToDateBS = frm["ToDateBS"];
            }
            else
            {
                saveItem.IsWarranty = false;
                saveItem.WarrantyDuration = 0;
                saveItem.WarrantyFromDate = null;
                saveItem.WarrantyFromDateBS = null;
                saveItem.WarrantyToDate = null;
                saveItem.WarrantyToDateBS = null;
            }

            saveItem.EnteredDate = DateTime.Now;
            saveItem.EnteredBy = Convert.ToInt32(Session["UserId"]);

            db.AddUsers(saveItem);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_ItemVM detail = db.getItemDetail((int)id);

            if (detail == null)
            {
                return HttpNotFound();
            }
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            ViewBag.AssetCategoryId = new SelectList(ddl.getCategoryListByDepartment(depid), "Id", "Name", detail.AssetCategoryId);
            return View(detail);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm, int? id)
        {
            A_Item updateItem = new A_Item();

            updateItem.AssetItemName = frm["AssetItemName"];
            updateItem.AssetItemCode = frm["AssetItemCode"];
            updateItem.AssetCategoryId = Convert.ToInt32(frm["AssetCategoryId"]);
            updateItem.DepartmentId = Convert.ToInt32(Session["DepartmentId"]);

            if (frm["IsWarranty"] == "Y")
            {
                updateItem.IsWarranty = true;
                updateItem.WarrantyDuration = Convert.ToInt32(frm["WarrantyDuration"]);
                updateItem.WarrantyFromDate = DateTime.ParseExact(frm["WarrantyFromDate"], "yyyy-MM-dd", null);
                updateItem.WarrantyFromDateBS = frm["WarrantyFromDateBS"];
                updateItem.WarrantyToDate = DateTime.ParseExact(frm["WarrantyToDate"], "yyyy-MM-dd", null);
                updateItem.WarrantyToDateBS = frm["WarrantyToDateBS"];
            }
            else
            {
                updateItem.IsWarranty = false;
                updateItem.WarrantyDuration = 0;
                updateItem.WarrantyFromDate = null;
                updateItem.WarrantyFromDateBS = null;
                updateItem.WarrantyToDate = null;
                updateItem.WarrantyToDateBS = null;
            }

            updateItem.LastUpdatedDate = DateTime.Now;
            updateItem.LastUpdatedBy = Convert.ToInt32(Session["UserId"]);

            db.UpdateItem(updateItem, (int)id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteItem((int)id, DateTime.Now, Convert.ToInt32(Session["UserId"]));
            return RedirectToAction("Index");
        }
    }
}