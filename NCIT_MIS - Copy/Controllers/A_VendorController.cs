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
    public class A_VendorController : Controller
    {
        A_VendorRepo db = new A_VendorRepo();
        GetDropDown ddl = new GetDropDown();
        public ActionResult Index()
        {
            return View(db.GetAllVendorList());
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_VendorVM detail = db.getVendorDetail((int)id);

            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        public ActionResult Create()
        {
            //ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection frm)
        {
            A_Vendor saveVendor = new A_Vendor();

            saveVendor.VendorName = frm["VendorName"];
            saveVendor.VendorAddress = frm["VendorAddress"];
            saveVendor.VendorPhone = frm["VendorPhone"];
            saveVendor.VendorMobile = frm["VendorMobile"];
            saveVendor.PanNo = frm["PanNo"];
            saveVendor.EnteredDate = DateTime.Now;
            saveVendor.EnteredBy = Convert.ToInt32(Session["UserId"]);

            db.AddVendor(saveVendor);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_VendorVM detail = db.getVendorDetail((int)id);

            if (detail == null)
            {
                return HttpNotFound();
            }
            //ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name", detail.DepartmentId);
            return View(detail);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm, int? id)
        {
            A_Vendor updateVendor = new A_Vendor();

            updateVendor.VendorName = frm["VendorName"];
            updateVendor.VendorAddress = frm["VendorAddress"];
            updateVendor.VendorPhone = frm["VendorPhone"];
            updateVendor.VendorMobile = frm["VendorMobile"];
            updateVendor.PanNo = frm["PanNo"];

            updateVendor.LastUpdatedDate = DateTime.Now;
            updateVendor.LastUpdatedBy = Convert.ToInt32(Session["UserId"]);

            db.UpdateVendor(updateVendor, (int)id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteVendor((int)id, DateTime.Now, Convert.ToInt32(Session["UserId"]));
            return RedirectToAction("Index");
        }
    }
}