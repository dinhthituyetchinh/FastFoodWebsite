using FastFoodWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace FastFoodWebsite.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();

        public ActionResult OrderView()
        {
            var orderList = db.ORDERs.ToList();
            return View(orderList);
        }

        public ActionResult DetailOrder()
        {
            return View();
        }
    }
}