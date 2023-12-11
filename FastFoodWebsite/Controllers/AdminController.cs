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
        DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
        public ActionResult Index()
        {
            var orderList = db.ORDERDETAILs.ToList();
            return View(orderList);
        }
        public ActionResult ViewProduct()
        {
            try
            {

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

        public ActionResult UpdateProduct(int id)
        {
            Admin_Product p_ID = adminService.getProductByID(id);
            return View(p_ID);
        }
        [HttpPost]
        public ActionResult UpdateProduct(Admin_Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    adminService.updateSql(product.productID, product.productName, product.productDescription, product.price, product.images, product.UpdatedDate, product.productTypeID);
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


        public ActionResult Customers()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            USER currentUser = Session["user"] as USER;
            User user = adminService.getUserByID(currentUser.USERID);
            return View(user);
        }
        [HttpPost]
        public ActionResult ResetPassword(User u)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    adminService.updatePassword(u.UserId, u.fullName, u.phone, u.email, u.password, u.confirmPassword, u.UpdatedDate, u.roleID);
                    return View();// để tạm, cho hiện lên cập nhật thành công
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