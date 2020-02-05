


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
    public class INV_UnitController : Controller
    {
        // GET: INV_Categoty
        INV_UnitRepo db = new INV_UnitRepo();
        public ActionResult Index()
        {
            return View(db.GetAllUnitist());
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            INV_UnitVM detail = db.getUnitDetail((int)id);

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
            INV_Unit saveUnit = new INV_Unit();

            saveUnit.UnitName = frm["UnitName"];
            saveUnit.UnitCode = frm["UnitCode"].ToUpper();
            saveUnit.EnteredDate = DateTime.Now;
            saveUnit.EnteredBy = Convert.ToInt32(frm["SesUserId"]);
            saveUnit.DepartmentId = Convert.ToInt32(frm["SesDepartmentId"]);

            db.AddUnit(saveUnit);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            INV_UnitVM detail = db.getUnitDetail((int)id);

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
            INV_Unit updateUnit = new INV_Unit();

            updateUnit.UnitName = frm["UnitName"];
            updateUnit.UnitCode = frm["UnitCode"].ToUpper();

            updateUnit.LastUpdatedDate = DateTime.Now;
            updateUnit.LastUpdatedBy = Convert.ToInt32(frm["SesUserId"]);

            db.UpdateUnit(updateUnit, (int)id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = db.DeleteUnit((int)id);
            return RedirectToAction("Index");
        }
    }
}