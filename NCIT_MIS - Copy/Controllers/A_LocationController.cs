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
    public class A_LocationController : Controller
    {
        // GET: A_Location
        A_LocationRepo db = new A_LocationRepo();
        GetDropDown ddl = new GetDropDown();
        // GET: Department
        public ActionResult Index()
        {
            return View(db.GetAllLocationList());
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_LocationVM detail = db.getLocationDetail((int)id);

            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection frm)
        {
            A_Location saveLoc = new A_Location();

            saveLoc.LocationName = frm["LocationName"];
            saveLoc.LocationCode = frm["LocationCode"].ToUpper();
            saveLoc.DepartmentId = Convert.ToInt32(frm["DepartmentId"]);
            saveLoc.EnteredDate = DateTime.Now;
            saveLoc.EnteredBy = Convert.ToInt32(Session["UserId"]);

            db.AddLocation(saveLoc);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_LocationVM detail = db.getLocationDetail((int)id);

            if (detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name",detail.DepartmentId);
            return View(detail);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm, int? id)
        {
            A_Location updateLoc = new A_Location();

            updateLoc.LocationName = frm["LocationName"];
            updateLoc.LocationCode = frm["LocationCode"].ToUpper();
            updateLoc.DepartmentId = Convert.ToInt32(frm["DepartmentId"]);

            updateLoc.LastUpdatedDate = DateTime.Now;
            updateLoc.LastUpdatedBy = Convert.ToInt32(Session["UserId"]);

            db.UpdateLocation(updateLoc, (int)id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteLocation((int)id, DateTime.Now, Convert.ToInt32(Session["UserId"]));
            return RedirectToAction("Index");
        }
    }
}