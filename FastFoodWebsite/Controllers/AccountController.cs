using FastFoodWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace FastFoodWebsite.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(AccountRegister account)
        {
            if (ModelState.IsValid)
            {
                if (account.password != account.confirmPassword)
                {
                    ViewBag.Error = "Mật khẩu không khớp";
                    return View();
                }
                AccountRegister newAccount = new AccountRegister();
                USER kh = newAccount.handleRegister(account.fullName, account.phone, account.email, account.dayOfBirth, account.address, account.password);
                db.USERs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("LogIn");
            }
            else
            {
                return View();
            }

        }
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]

        public ActionResult LogIn(AccountLogIn account)
        {
            AccountLogIn newAccount = new AccountLogIn();
            USER user = newAccount.handleLogin(account.emailOrPhone, account.password);

            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    ViewData["status"] = "Email/ phone hoặc mật khẩu không đúng";
                    return View();
                }
                else
                {
                    Session["user"] = user;
                    if(user.ROLEID == 2)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Index", "Admin");
                }

            }
            else
            {
                return View();
            }

        }
        public ActionResult ViewAccount()
        {
            USER user = Session["user"] as USER;
            return View(user);
        }
    }
}