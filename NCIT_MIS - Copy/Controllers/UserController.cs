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
    public class UserController : Controller
    {
        UserRepo db = new UserRepo();
        GetDropDown ddl = new GetDropDown();
        PasswordHash CryptoService = new PasswordHash();


        // GET: User
        public ActionResult Index()
        {
            var detail = db.getAllUesrList();
            return View(detail);
        }

        public ActionResult Detail(int? id)
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
            return View(detail);
        }

        public ActionResult Create()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            ViewBag.DepartmentId = new SelectList(ddl.getDepartmentList(), "Id", "Name");
            ViewBag.UserTypeId = new SelectList(ddl.getUserTypeList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection frm)
        {
            Sys_User saveUser = new Sys_User();

            saveUser.FullName = frm["FullName"];
            saveUser.Email = frm["Email"];
            string password = frm["Password"];
            saveUser.DepartmentId = Convert.ToInt32(frm["DepartmentId"]);
            saveUser.UserType = Convert.ToInt32(frm["UserTypeId"]);

            string passwordSalt = CryptoService.CreateSalt(10);
            string passwordHash = CryptoService.GenerateSHA256Hash(password, passwordSalt);
            saveUser.PasswordSalt = Convert.ToString(passwordSalt);
            saveUser.Password = Convert.ToString(passwordHash);

            if (frm["IsActive"] == "Y")
                saveUser.IsActive = true;
            else
                saveUser.IsActive = false;

            saveUser.EnteredDate = DateTime.Now;
            saveUser.EnteredBy = Convert.ToInt32(Session["UserId"]);

            db.AddUsers(saveUser);
            return RedirectToAction("Index");
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
            ViewBag.UserTypeId = new SelectList(ddl.getUserTypeList(), "Id", "Name", detail.UserType);
            return View(detail);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm, int? id)
        {
            Sys_User updateUser = new Sys_User();

            updateUser.FullName = frm["FullName"];
            updateUser.Email = frm["Email"];
            updateUser.Password = frm["Password"];
            updateUser.DepartmentId = Convert.ToInt32(frm["DepartmentId"]);
            updateUser.UserType = Convert.ToInt32(frm["UserTypeId"]);

            if (frm["IsActive"] == "Y")
                updateUser.IsActive = true;
            else
                updateUser.IsActive = false;

            updateUser.LastUpdatedDate = DateTime.Now;
            updateUser.LastUpdatedBy = Convert.ToInt32(Session["UserId"]);

            db.UpdateUser(updateUser, (int)id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteUser((int)id, DateTime.Now, Convert.ToInt32(Session["UserId"]));
            return RedirectToAction("Index");
        }
    }
}