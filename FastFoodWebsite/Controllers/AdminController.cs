using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FastFoodWebsite.Models;
using FastFoodWebsite.Services;
namespace FastFoodWebsite.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        AdminService adminService = new AdminService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewProduct()
        {
            try
            {
                //List<Subject> filterSubject = TempData["filterSubject"] as List<Subject>;
                //if (filterSubject != null && filterSubject.Count > 0)
                //{
                //    return View(filterSubject);
                //}

                ViewData["Err"] = TempData["Err"];
                List<Admin_Product> p = adminService.ExcuteSQL();
                return View(p);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Admin_Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    adminService.AddSql(product.productName, product.productDescription, product.price, product.images, product.CreatedDate, product.productTypeID);
                    return RedirectToAction("ViewProduct");
                }
                else
                {
                    return View();
                }
            } catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        public ActionResult DeleteProduct(int id)
        {
            try
            {
                if(!adminService.checkProductInCart(id))
                {
                    adminService.DeleteSql(id);
                    return RedirectToAction("ViewProduct");
                }   else
                {
                    TempData["Err"] = "Hiện tại chưa xoá được sản phẩm";
                    return RedirectToAction("ViewProduct");
                }    
               
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult UpdateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateProduct(Admin_Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    adminService.updateSql(product.productName, product.productDescription, product.price, product.images, product.UpdatedDate, product.productTypeID);
                    return RedirectToAction("ViewProduct");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

    }
}