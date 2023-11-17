using FastFoodWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastFoodWebsite.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
        public ActionResult MenuPartial()
        {
            var product = db.PRODUCTs.ToList();
            return View(product);
        }
        public ActionResult Menu()
        {
            var product = db.PRODUCTs.ToList();
            return View(product);
        }
        public ActionResult DetailProduct(int id)
        {
            PRODUCT p = db.PRODUCTs.Single(ma => ma.PRODUCTID == id);
            return View(p);
        }
    }
}