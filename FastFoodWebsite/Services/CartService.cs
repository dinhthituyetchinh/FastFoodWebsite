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

        private ORDER createOrder(int userId)
        {
            ORDER newOrder = new ORDER();
            newOrder.USERID = userId;
            newOrder.TOTAL_PRICE = 0;
            newOrder.STATUS_OF_ORDER = "Pending...";
            db.ORDERs.InsertOnSubmit(newOrder);
            db.SubmitChanges();

            return newOrder;
        }

        private void deleteCart(int userID)
        {
            List<CART> carts = this.getCartByUserID(userID);

            foreach (CART cart in carts)
            {
                db.CARTs.DeleteOnSubmit(cart);
                db.SubmitChanges();
            }
        }

        public void changeCartToOrderDetails(int userID)
        {
            List<CART> carts = this.getCartByUserID(userID);
            ORDER newOrder = this.createOrder(userID);

            ORDERDETAIL newORderDetail = new ORDERDETAIL();

            foreach (CART cart in carts)
            {
                newORderDetail.ORDERID = newOrder.ORDERID;
                newORderDetail.PRODUCTID = (int)cart.PRODUCTID;
                newORderDetail.NOTES = cart.NOTES;
                newORderDetail.QUANTITY = cart.QUANTITY;
                newORderDetail.PRICE = cart.PRICE;

                db.ORDERDETAILs.InsertOnSubmit(newORderDetail);
                db.SubmitChanges();
            }

            this.deleteCart(userID);
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