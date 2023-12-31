﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FastFoodWebsite.Models;
using FastFoodWebsite.Services;
namespace FastFoodWebsite.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
        CartService cartService = new CartService();
        public ActionResult ViewCart()
        {
            USER user = Session["user"] as USER;
            var cartList = cartService.getCartByUserID(user.USERID);
            if(cartList.Count == 0)
            {
                ViewData["NoProduct"] = "Cart is empty";
            }    
            return View(cartList);
        }
        [HttpPost]
        public ActionResult AddCart(int prodID, int num, string notes)
        {
            USER user = Session["user"] as USER;
            if(user == null)
            {
               return RedirectToAction("LogIn", "Account");
            }
            else
            {
                cartService.addCart(user.USERID, prodID, num, notes);
                return RedirectToAction("ViewCart");
            }

        }
        public ActionResult Deleted(int prodID)
        {          
            cartService.deletedCartItem(prodID);
            return RedirectToAction("ViewCart", "Cart");

        }

        public ActionResult CheckOut()
        {
            USER currentUser = Session["user"] as USER;

            cartService.changeCartToOrderDetails(currentUser.USERID);

            TempData["checkoutSuccess"] = "Checkout success!";
            return RedirectToAction("Index", "Home");
        }

       
    }
}