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
    public class A_AssetController : Controller
    {
        A_AssetRepo db = new A_AssetRepo();
        GetDropDown ddl = new GetDropDown();
        // GET: A_Asset
        public ActionResult Index()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            return View(db.getAssetList(depid));
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_AssetVM detail = db.getAssetDetail((int)id);

            if (detail == null)
            {
                return HttpNotFound();
            }
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            ViewBag.DepartmentId1 = new SelectList(ddl.getDepartmentList(), "Id", "Name", detail.DepartmentId);
            ViewBag.LocationId = new SelectList(ddl.getLocationList(), "Id", "Name", detail.LocationId);
            ViewBag.CategoryId = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name", detail.AssetCategoryId);
            ViewBag.AssetItemId = new SelectList(ddl.getAssetItemList(depid), "Id", "Name", detail.AssetItemId);
            return View(detail);
        }

        public ActionResult Create()
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            ViewBag.DepartmentId1 = new SelectList(ddl.getDepartmentList(), "Id", "Name");
            ViewBag.LocationId = new SelectList(ddl.getLocationList(), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name");
            ViewBag.AssetItemId = new SelectList(ddl.getAssetItemList(depid), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult create(FormCollection frm)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;

            A_Asset asset = new A_Asset();

            asset.DepartmentId = Convert.ToInt32(frm["DepartmentId1"]);
            asset.LocationId = Convert.ToInt32(frm["LocationId"]);
            asset.AssetItemId = Convert.ToInt32(frm["AssetItemId"]);
            asset.AssetUniqueCode = frm["AssetUniqueCodeDep"] + "-" + frm["AssetUniqueCodeLoc"] + "-" + frm["AssetUniqueCodeItem"];

            asset.AssetDate = frm["AssetDate"];
            asset.AssetDateBS = frm["AssetDateBS"];
            asset.Description = frm["Description"];
            //asset.Remarks = frm["Remarks"];
            asset.UsefullLife = Convert.ToInt32(frm["UsefullLife"]);

            if (frm["IsDepreciationApplicable"] == "Yes")
            {
                asset.IsDepreciationApplicable = true;
                asset.DepreciationStartDate = frm["DepreciationDate"];
                asset.DepreciationStartDateBS = frm["DepreciationDateBS"];
                asset.DepreciationRemarks = frm["DepreciationRemarks"];
            }
            else
            {
                asset.IsDepreciationApplicable = false;
                asset.DepreciationStartDate = null;
                asset.DepreciationStartDateBS = null;
                asset.DepreciationRemarks = string.Empty;// frm["DepreciationRemarks"];
            }

            if (frm["IsScrap"] == "Yes")
            {
                asset.IsScrap = true;
                asset.ScrapDate = frm["ScrapDate"];
                asset.ScrapDateBS = frm["ScrapDateBS"];
                asset.ScrapRealizedValue = Convert.ToDecimal(frm["ScrapRealizeValue"]);
            }
            else
            {
                asset.IsScrap = false;
                asset.ScrapDate = null;
                asset.ScrapDateBS = null;
                asset.ScrapRealizedValue = 0;
            }

            asset.EnteredBy = uid;
            asset.EnteredDate = cdate;
            asset.IsDeleted = false;

            db.AddAsset(asset);
            return RedirectToAction("Index");
        }

        public ActionResult Create1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int tokenId = Convert.ToInt32(id);
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            A_AssetVM depLoc  = db.getDepartmentLocationByTokenId((int)id);
            ViewBag.DepartmentId = depLoc.DepartmentId;
            ViewBag.DepartmenName = depLoc.DepartmentName;
            ViewBag.LocationId = depLoc.LocationId;
            ViewBag.LocationName = depLoc.LocationName;
            ViewBag.RequestQuantity = depLoc.RequestQuantity;
            ViewBag.CategoryId = depLoc.CategoryId;
            ViewBag.CategoryName = depLoc.CategoryName;
            ViewBag.AssetItemId = depLoc.AssetItemId;
            ViewBag.TokenId = tokenId;
            ViewBag.AssetItemName = depLoc.AssetItemName;
            return View();
        }

        [HttpPost]
        public ActionResult create1(FormCollection frm)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;
            int reqQuantity = Convert.ToInt32(frm["RequestQuantity"]);
            string[] itemCodeSplit = frm["AssetUniqueCodeItem"].Split('-');
            string itemCode = itemCodeSplit[0];
            int itemCodeNo = Convert.ToInt32(itemCodeSplit[1]);
            int tokenId = Convert.ToInt32(frm["TokenId"]);
            for (int i = 0; i < reqQuantity; i++)
            {
                A_Asset asset = new A_Asset();

                asset.DepartmentId = Convert.ToInt32(frm["DepartmentId1"]);
                asset.LocationId = Convert.ToInt32(frm["LocationId"]);
                asset.AssetItemId = Convert.ToInt32(frm["AssetItemId"]);
                asset.AssetUniqueCode = frm["AssetUniqueCodeDep"] + "-" + frm["AssetUniqueCodeLoc"] + "-" + itemCode + "-" +(itemCodeNo+i);

                asset.AssetDate = frm["AssetDate"];
                asset.AssetDateBS = frm["AssetDateBS"];
                asset.Description = frm["Description"];
                //asset.Remarks = frm["Remarks"];
                asset.UsefullLife = Convert.ToInt32(frm["UsefullLife"]);

                if (frm["IsDepreciationApplicable"] == "Yes")
                {
                    asset.IsDepreciationApplicable = true;
                    asset.DepreciationStartDate = frm["DepreciationDate"];
                    asset.DepreciationStartDateBS = frm["DepreciationDateBS"];
                    asset.DepreciationRemarks = frm["DepreciationRemarks"];
                }
                else
                {
                    asset.IsDepreciationApplicable = false;
                    asset.DepreciationStartDate = null;
                    asset.DepreciationStartDateBS = null;
                    asset.DepreciationRemarks = string.Empty;// frm["DepreciationRemarks"];
                }

                if (frm["IsScrap"] == "Yes")
                {
                    asset.IsScrap = true;
                    asset.ScrapDate = frm["ScrapDate"];
                    asset.ScrapDateBS = frm["ScrapDateBS"];
                    asset.ScrapRealizedValue = Convert.ToDecimal(frm["ScrapRealizeValue"]);
                }
                else
                {
                    asset.IsScrap = false;
                    asset.ScrapDate = null;
                    asset.ScrapDateBS = null;
                    asset.ScrapRealizedValue = 0;
                }

                asset.EnteredBy = uid;
                asset.EnteredDate = cdate;
                asset.IsDeleted = false;

                db.AddAsset(asset);
            }
            db.ApprovedRequestTokenStore(tokenId, uid, cdate);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A_AssetVM detail = db.getAssetDetail((int)id);

            if (detail == null)
            {
                return HttpNotFound();
            }
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            ViewBag.DepartmentId1 = new SelectList(ddl.getDepartmentList(), "Id", "Name", detail.DepartmentId);
            ViewBag.LocationId = new SelectList(ddl.getLocationList(), "Id", "Name", detail.LocationId);
            ViewBag.CategoryId = new SelectList(ddl.getAssetCategoryList(depid), "Id", "Name", detail.AssetCategoryId);
            ViewBag.AssetItemId = new SelectList(ddl.getAssetItemList(depid), "Id", "Name", detail.AssetItemId);
            return View(detail);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm, int? id)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            int uid = Convert.ToInt32(Session["UserId"]);
            var cdate = DateTime.Now;

            A_Asset asset = new A_Asset();

            //asset.DepartmentId = Convert.ToInt32(frm["DepartmentId1"]);
            //asset.LocationId = Convert.ToInt32(frm["LocationId"]);
            //asset.AssetItemId = Convert.ToInt32(frm["AssetItemId"]);
            //asset.AssetUniqueCode = frm["AssetUniqueCodeDep"] + "-" + frm["AssetUniqueCodeLoc"] + "-" + frm["AssetUniqueCodeItem"];

            asset.AssetDate = frm["AssetDate"];
            asset.AssetDateBS = frm["AssetDateBS"];
            asset.Description = frm["Description"];
            //asset.Remarks = frm["Remarks"];
            asset.UsefullLife = Convert.ToInt32(frm["UsefullLife"]);

            if (frm["IsDepreciationApplicable"] == "Yes")
            {
                asset.IsDepreciationApplicable = true;
                asset.DepreciationStartDate = frm["DepreciationDate"];
                asset.DepreciationStartDateBS = frm["DepreciationDateBS"];
                asset.DepreciationRemarks = frm["DepreciationRemarks"];
            }
            else
            {
                asset.IsDepreciationApplicable = false;
                asset.DepreciationStartDate = null;
                asset.DepreciationStartDateBS = null;
                asset.DepreciationRemarks = string.Empty;// frm["DepreciationRemarks"];
            }

            if (frm["IsScrap"] == "Yes")
            {
                asset.IsScrap = true;
                asset.ScrapDate = frm["ScrapDate"];
                asset.ScrapDateBS = frm["ScrapDateBS"];
                asset.ScrapRealizedValue = Convert.ToDecimal(frm["ScrapRealizeValue"]);
            }
            else
            {
                asset.IsScrap = false;
                asset.ScrapDate = null;
                asset.ScrapDateBS = null;
                asset.ScrapRealizedValue = 0;
            }

            asset.LastUpdatedBy = uid;
            asset.LastUpdatedDate = cdate;
            //asset.IsDeleted = false;

            db.UpdateAsset(asset,(int)id);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteAsset((int)id, DateTime.Now, Convert.ToInt32(Session["UserId"]));
            return RedirectToAction("Index");
        }
    }
}