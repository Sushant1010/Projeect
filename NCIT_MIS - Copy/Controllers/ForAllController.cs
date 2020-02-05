using NCIT_MIS.Models;
using NCIT_MIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NCIT_MIS.Controllers
{
    public class ForAllController : Controller
    {
        // GET: ForAll
        GetDropDown ddl = new GetDropDown();
        UserPasswordRepo up = new UserPasswordRepo();
        PasswordHash CryptoService = new PasswordHash();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CheckPassword(string OldPassword)
        {
            JsonResult result = new JsonResult();
            int userId = Convert.ToInt32(Session["UserId"]);

            Sys_User detail = up.getUesrDetailByUserId(userId);

            string dbOldPassword = detail.Password;
            string dbpasswordSalt = detail.PasswordSalt;
            string OldpasswordHash = CryptoService.GenerateSHA256Hash(OldPassword, dbpasswordSalt);

            //var checkOldPass = up.CheckOldPassword(OldpasswordHash);
            var matching = ddl.CheckPassword(OldpasswordHash, userId);

            if (matching != null)
            {
                result.Data = "";
                return result;
            }
            else
            {
                result.Data = "Incorrect Password!!!";
                return result;
            }
            

        }

        public JsonResult GetItemByCategory(int catid)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            JsonResult result = new JsonResult();
            var matching = ddl.GetAssetItemListByCategoryId(depid, catid);


            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            }

            result.Data = msg;

            return result;
        }

        public JsonResult GetInvItemByCategory(int catid)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            JsonResult result = new JsonResult();
            var matching = ddl.GetInventoryItemListByCategoryId(depid, catid);


            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            }

            result.Data = msg;

            return result;
        }
        public JsonResult GetLocationListByDepartmentId(int catid)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            //JsonResult result = new JsonResult();

            var matching = ddl.getLocationListByDepartmentId(catid);
            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            }

            var depCode = ddl.getDepartmentCode(catid);
            string DepartCode = depCode;

            var result = new { msg, DepartCode };
            //result.Data = msg;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocationCode(int catid)
        {
            int depid = Convert.ToInt32(Session["DepartmentId"]);
            JsonResult result = new JsonResult();
            string matching = ddl.GetLocationCode(catid);

            result.Data = matching;

            return result;
        }

        public JsonResult GetAssetUniqueID(int assId, int LocId)
        {
            JsonResult result = new JsonResult();
            var assetcode = ddl.GetAssetCode(assId);
            var matching = ddl.GetAssetUniqueID(assId, LocId);

            string msg = assetcode.ToString() + "-" + matching.ToString();

            result.Data = msg;
            return result;
        }
        public JsonResult CheckQuantity(int qty, int assId)
        {
            JsonResult result = new JsonResult();
            var matching = ddl.GetQuantity(assId, qty);

            string msg = matching.ToString();

            result.Data = msg;
            return result;
        }

        public JsonResult CheckInvQuantity(int qty, int invId)
        {
            JsonResult result = new JsonResult();
            var matching = ddl.GetInvQuantity(invId, qty);

            string msg = matching.ToString();

            result.Data = msg;
            return result;
        }
    }
}