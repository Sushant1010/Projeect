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
    public class DepartmentController : Controller
    {
        DepartmentRepo db = new DepartmentRepo();
        // GET: Department
        public ActionResult Index()
        {
            return View(db.GetAllDepartmentList());
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DepartmentVM detail = db.getDepartmentDetail((int)id);

            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection frm)
        {
            Sys_Department saveDepart = new Sys_Department();

            saveDepart.DepartmentName = frm["DepartmentName"];
            saveDepart.DepartmentCode = frm["DepartmentCode"].ToUpper();
            saveDepart.Email = frm["Email"];
            saveDepart.PhoneNo = Convert.ToInt32(frm["PhoneNo"]);

            saveDepart.EnteredDate = DateTime.Now;
            saveDepart.EnteredBy = Convert.ToInt32(Session["UserId"]);

            db.AddDepartment(saveDepart);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DepartmentVM detail = db.getDepartmentDetail((int)id);

            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm, int? id)
        {
            Sys_Department updateDepart = new Sys_Department();

            updateDepart.DepartmentName = frm["DepartmentName"];
            updateDepart.DepartmentCode = frm["DepartmentCode"].ToUpper();
            updateDepart.Email = frm["Email"];
            updateDepart.PhoneNo = Convert.ToInt32(frm["PhoneNo"]);

            updateDepart.LastUpdatedDate = DateTime.Now;
            updateDepart.LastUpdatedBy = Convert.ToInt32(Session["UserId"]);

            db.UpdateDepartment(updateDepart, (int)id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteDepartment((int)id, DateTime.Now, Convert.ToInt32(Session["UserId"]));
            return RedirectToAction("Index");
        }
    }
}