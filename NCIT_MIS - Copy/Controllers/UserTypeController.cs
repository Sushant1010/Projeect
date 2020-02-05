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
    public class UserTypeController : Controller
    {
        UserTypeRepo db = new UserTypeRepo();
        GetDropDown ddl = new GetDropDown();
        // GET: Department
        public ActionResult Index()
        {
            return View(db.GetAllUserTypeList());
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SystemUserTypeVM detail = db.getUserTypeDetail((int)id);

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
            Sys_UserType saveUT = new Sys_UserType();

            saveUT.UserTypeName = frm["UserTypeName"];
            saveUT.EnteredDate = DateTime.Now;
            saveUT.EnteredBy = Convert.ToInt32(Session["UserId"]);
            saveUT.DepartmentId = Convert.ToInt32(Session["DepartmentId"]);

            db.AddUT(saveUT);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SystemUserTypeVM detail = db.getUserTypeDetail((int)id);

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
            Sys_UserType updateUT = new Sys_UserType();

            updateUT.UserTypeName = frm["UserTypeName"];
            updateUT.LastUpdatedDate = DateTime.Now;
            updateUT.LastUpdatedBy = Convert.ToInt32(Session["UserId"]);

            db.UpdateUT(updateUT, (int)id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteUT((int)id, DateTime.Now, Convert.ToInt32(Session["UserId"]));
            return RedirectToAction("Index");
        }
    }
}