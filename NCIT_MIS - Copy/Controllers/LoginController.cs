using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NCIT_MIS.Models;
using NCIT_MIS.Repository;

namespace NCIT_MIS.Controllers
{
    public class LoginController : Controller
    {
        LoginRepo db = new LoginRepo();
        PasswordHash CryptoService = new PasswordHash();
        // GET: Login
        public ActionResult Index(string returnUrl = "")
        {
            ViewBag.SuccessMessage = "";
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            string userid = frm["Email"];
            string password = frm["Password"];

            var CheckEmail = db.GetSaltValue(userid); //Retrive Salt Value From Database Table
            string passwordSalt = CheckEmail.PasswordSalt;
            string passwordHash = CryptoService.GenerateSHA256Hash(password, passwordSalt);// Generating Hash Value using userpassword and above salt value
            
            var matchIdPassword = db.matchIdPassword(userid, passwordHash);
            if (matchIdPassword == null)
            {
                Session["ViewBagMessage"] = "Something is Wrong!!!";
                ViewBag.Message = "Something is Wrong!!!";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Session["UserId"] = matchIdPassword.UserId;
                Session["FullName"] = matchIdPassword.FullName;
                Session["UserType"] = matchIdPassword.UserType;
                Session["UserTypeTxt"] = db.getUserTypeName(matchIdPassword.UserType).UserTypeName;
                Session["DepartmentId"] = matchIdPassword.DepartmentId;
                return RedirectToAction("Index", "Dashboard");
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "You have logged out successfully";
            return RedirectToAction("Index", "Login");
        }
    }
}