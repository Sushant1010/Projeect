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
    public class A_DepreciationController : Controller
    {
        A_DepreciationRepo db = new A_DepreciationRepo();
        GetDropDown ddl = new GetDropDown();
        // GET: Department
        public ActionResult Index()
        {
            return View(db.GetAllDepreciationList());
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_DepreciationVM detail = db.getDepreciationDetail((int)id);

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
            A_Depreciation saveDepre = new A_Depreciation();

            saveDepre.DepreciationName = frm["DepreciationName"];
            saveDepre.DepreciationRate = Convert.ToDecimal(frm["DepreciationRate"]);
            saveDepre.Description = frm["Description"];
            saveDepre.EnteredDate = DateTime.Now;
            saveDepre.EnteredBy = Convert.ToInt32(Session["UserId"]);
            saveDepre.DepartmentId = Convert.ToInt32(frm["DepartmentId"]);

            db.AddDepreciation(saveDepre);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_DepreciationVM detail = db.getDepreciationDetail((int)id);

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
            A_Depreciation updateDepre = new A_Depreciation();

            updateDepre.DepreciationName = frm["DepreciationName"];
            updateDepre.DepreciationRate = Convert.ToDecimal(frm["DepreciationRate"]);
            updateDepre.Description = frm["Description"];

            updateDepre.LastUpdatedDate = DateTime.Now;
            updateDepre.LastUpdatedBy = Convert.ToInt32(Session["UserId"]);

            db.UpdateDepreciation(updateDepre, (int)id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteDepreciation((int)id, DateTime.Now, Convert.ToInt32(Session["UserId"]));
            return RedirectToAction("Index");
        }
    }
}