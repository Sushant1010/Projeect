using NCIT_MIS.Repository;
using NCIT_MIS.ViewModel;
using NCIT_MIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NCIT_MIS.Controllers
{
    public class UserPermissionController : Controller
    {
        // GET: UserPermission
        UserPermissionRepo db = new UserPermissionRepo();
        GetDropDown ddl = new GetDropDown();
        public ActionResult Index()
        {

            var detail = ddl.getAllUesrList();

            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SystemUserVM detail = db.getUesrDetail((int)id);

            if (detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name", detail.DepartmentId);
            //ViewBag.UserType = new SelectList(ddl.getUserTypeList(), "Id", "Name", detail.UserTypeId);
            return View(detail);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm, int? id)
        {
            Sys_User updateUserPermission = new Sys_User();

            updateUserPermission.IsAdmin = Convert.ToBoolean(frm["IsAdmin"] != "false");
            updateUserPermission.AssetModuleAllow = Convert.ToBoolean(frm["AssetModuleAllow"] != "false");
            updateUserPermission.InventoryModuleAllow = Convert.ToBoolean(frm["InventoryModuleAllow"] != "false");
            updateUserPermission.RequestModuleAllow = Convert.ToBoolean(frm["RequestModuleAllow"] != "false");
            updateUserPermission.DepartmentId = Convert.ToInt32(frm["Department"]);
            updateUserPermission.UserType = Convert.ToInt32(frm["UserType"]);



            updateUserPermission.LastUpdatedDate = DateTime.Now;
            updateUserPermission.LastUpdatedBy = Convert.ToInt32(Session["UserId"]);

            db.UpdateUserPermission(updateUserPermission, (int)id);
            return RedirectToAction("Index");
        }
    }
}