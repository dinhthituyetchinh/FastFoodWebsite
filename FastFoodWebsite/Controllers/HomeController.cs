using FastFoodWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastFoodWebsite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
        public ActionResult Index()
        {
            ViewData["checkoutSuccess"] = TempData["checkoutSuccess"];
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                Session["c"] = new Contact() { fullName = contact.fullName, phone = contact.phone, email = contact.email, comments = contact.comments };

                //db.CONTACTs.InsertOnSubmit(Session["c"].ToString());
                db.SubmitChanges();
                return RedirectToAction("LogIn");
            }
            else
            {
                return View();
            }
        }
    }
}