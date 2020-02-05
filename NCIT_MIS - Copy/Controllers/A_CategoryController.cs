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
    public class A_CategoryController : Controller
    {
        A_CategoryRepo db = new A_CategoryRepo();
        GetDropDown ddl = new GetDropDown();
        // GET: Department
        public ActionResult Index()
        {
            return View(db.GetAllCategoryList());
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_CategoryVM detail = db.getCategoryDetail((int)id);

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
            A_Category saveCate = new A_Category();

            saveCate.CategoryName = frm["CategoryName"];
            saveCate.CategoryCode = frm["CategoryCode"].ToUpper();
            saveCate.EnteredDate = DateTime.Now;
            saveCate.EnteredBy = Convert.ToInt32(Session["UserId"]);
            saveCate.DepartmentId = Convert.ToInt32(Session["DepartmentId"]);

            db.AddCategory(saveCate);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_CategoryVM detail = db.getCategoryDetail((int)id);

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
            A_Category updateCate = new A_Category();

            updateCate.CategoryName = frm["CategoryName"];
            updateCate.CategoryCode = frm["CategoryCode"].ToUpper();

            updateCate.LastUpdatedDate = DateTime.Now;
            updateCate.LastUpdatedBy = Convert.ToInt32(Session["UserId"]);

            db.UpdateCategory(updateCate, (int)id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteCategory((int)id, DateTime.Now, Convert.ToInt32(Session["UserId"]));
            return RedirectToAction("Index");
        }
    }
}