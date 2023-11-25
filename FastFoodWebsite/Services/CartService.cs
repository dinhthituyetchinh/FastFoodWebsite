using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FastFoodWebsite.Models;
namespace FastFoodWebsite.Services
{
    public class CartService
    {
        DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
        public List<CART> getCartByUserID(int userID)
        {
            return db.CARTs.Where(c => c.USERID == userID).ToList(); 
        }
        public void addCart(int userID,int productID, int num, string notes)
        {

            CART newCart = new CART();

            // 1. Check productId tồn tại trong db chưa
            CART existtingCart = db.CARTs.SingleOrDefault(c => c.PRODUCTID == productID);
            if(existtingCart == null)
            {
                newCart.USERID = userID;
                newCart.PRODUCTID = productID;
                newCart.QUANTITY = num;
                newCart.PRICE = getPriceOfProduct(productID) * num;
                newCart.NOTES = notes;

            }
            else
            {
                existtingCart.QUANTITY += num;
                existtingCart.PRICE += getPriceOfProduct(productID) * num;

            }
            // nếu chưa - add
            // nếu có - tăng quantity và price
            db.CARTs.InsertOnSubmit(newCart);
            db.SubmitChanges();
        }
        private decimal getPriceOfProduct(int ProductID)
        {
            return db.PRODUCTs.Single(p => p.PRODUCTID == ProductID).PRICE;
        }

        public void deletedCartItem(int prodID)
        {
            CART cartItem = db.CARTs.Single(c => c.PRODUCTID == prodID);
            db.CARTs.DeleteOnSubmit(cartItem);
            db.SubmitChanges();
        }
    }
}