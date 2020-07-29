using AG_AAC103.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dapper;

namespace AG_AAC103.Controllers
{
 
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "HomePage";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(EmpClass empClass, string returnUrl)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", empClass.UserName);
            param.Add("@Password", empClass.Password);
            param.Add("@IsActive", empClass.IsActive);
            var dataItem = DapperORM.ReturnList<EmpClass>("Login", param).FirstOrDefault<EmpClass>();
            
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.UserName, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.message = "Invalid Credentials";
                return View();

            }

        }
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

    }
}