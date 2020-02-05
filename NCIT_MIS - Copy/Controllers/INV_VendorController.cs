


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
    public class INV_VendorController : Controller
    {
        INV_VendorRepo db = new INV_VendorRepo();
        GetDropDown ddl = new GetDropDown();
        // GET: INV_Vendor
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

            INV_VendorVM detail = db.getVendorDetail((int)id);

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
            INV_Vendor saveVen = new INV_Vendor();

            saveVen.VendorName = frm["VendorName"];
            saveVen.VendorCode = frm["VendorCode"];
            saveVen.Address = frm["Address"];
            saveVen.Phone = frm["Phone"];
            saveVen.Mobile = frm["Mobile"];
            saveVen.PanNo = frm["PanNo"];

            saveVen.EnteredDate = DateTime.Now;
            saveVen.EnteredBy = Convert.ToInt32(Session["UserId"]);
            saveVen.DepartmentId = Convert.ToInt32(Session["DepartmentId"]);

            db.AddVendor(saveVen);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            INV_VendorVM detail = db.getVendorDetail((int)id);

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
            INV_Vendor updateVen = new INV_Vendor();

            updateVen.VendorName = frm["VendorName"];
            updateVen.VendorName = frm["Address"];
            updateVen.VendorName = frm["Phone"];
            updateVen.VendorName = frm["Mobile"];
            updateVen.VendorName = frm["PanNo"];
            updateVen.VendorCode = frm["VendorCode"].ToUpper();

            updateVen.LastUpdatedDate = DateTime.Now;
            updateVen.LastUpdatedBy = Convert.ToInt32(frm["SesUserId"]);

            db.UpdateVendor(updateVen, (int)id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = db.DeleteVendor((int)id);
            return RedirectToAction("Index");
        }
    }
}