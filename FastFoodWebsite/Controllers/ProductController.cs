using FastFoodWebsite.Models;
using FastFoodWebsite.Services;
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

        public static int ITEM_SIZE = 3;

        public ActionResult MenuPartial(int pageNo = 1)
        {
            var product = db.PRODUCTs.ToList();

            return View(product);
            //    var allProducts = db.PRODUCTs.ToList();

            //    int pageSize = (int) allProducts.Count / ITEM_SIZE; // 2
            //    // page 1 => skip 0
            //    // page 2 => skip 3
            //    int skip = (pageNo - 1) * ITEM_SIZE;

            //    var product = db.PRODUCTs.Skip(skip).Take(ITEM_SIZE).ToList();

            //    ViewBag.pageSize = pageSize;
            //    return View(product);
        }
        public ActionResult Menu(int pageNo = 1)
        {
            int totalProduct = db.PRODUCTs.Count();

            int pageSize = (int)Math.Ceiling((double)totalProduct / ITEM_SIZE); // 2
            // page 1 => skip 0
            // page 2 => skip 3
            int skip = (pageNo - 1) * ITEM_SIZE;

            var product = db.PRODUCTs.Skip(skip).Take(ITEM_SIZE).ToList();

            ViewBag.pageSize = pageSize;
             ViewBag.currentPage = pageNo;

            return View(product);

        }
        public ActionResult DetailProduct(int id)
        {
            PRODUCT p = db.PRODUCTs.Single(ma => ma.PRODUCTID == id);
            return View(p);
        }
       
    }
}