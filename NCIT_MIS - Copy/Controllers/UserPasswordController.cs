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
    public class UserPasswordController : Controller
    {
        // GET: UserPassword
        UserPasswordRepo db = new UserPasswordRepo();
        PasswordHash CryptoService = new PasswordHash();
        public ActionResult Index()
        {
            string id = Convert.ToString(Session["UserId"]);
            if (id == null || id == "" || id == "0")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SystemUserVM detail = db.getUesrDetail(Convert.ToInt32(id));

            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        public ActionResult ChangePassword()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection frm)
        {
            Sys_User changeUserPass = new Sys_User();

            int userId = Convert.ToInt32(Session["UserId"]);
            Sys_User detail = db.getUesrDetailByUserId(userId);

            string dbOldPassword = detail.Password;
            string dbpasswordSalt = detail.PasswordSalt;
            string OldpasswordHash = CryptoService.GenerateSHA256Hash(frm["OldPassword"], dbpasswordSalt);

            var checkOldPass = db.CheckOldPassword(OldpasswordHash);
            if (frm["NewPassword"] == frm["RePassword"])
            {
                if (dbOldPassword == OldpasswordHash && checkOldPass != null)
                {
                    string password = frm["NewPassword"];

                    string passwordSalt = CryptoService.CreateSalt(10);
                    string passwordHash = CryptoService.GenerateSHA256Hash(password, passwordSalt);
                    changeUserPass.PasswordSalt = Convert.ToString(passwordSalt);
                    changeUserPass.Password = Convert.ToString(passwordHash);
                    changeUserPass.LastUpdatedBy = userId;
                    changeUserPass.LastUpdatedDate = DateTime.Now;
                    db.ChangePassword(changeUserPass, userId);
                    ViewBag.Message = "Password Changed";
                }
            }
            else
            {
                ViewBag.Message = "Password MissMatch!!!";
            }
            
            
            return View();
        }
    }
}