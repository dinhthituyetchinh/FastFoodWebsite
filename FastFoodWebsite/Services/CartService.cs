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
            newCart.USERID = userID;
            newCart.PRODUCTID = productID;
            newCart.QUANTITY = num;
            newCart.PRICE = getPriceOfProduct(productID) * num;
            newCart.NOTES = notes;
            db.CARTs.InsertOnSubmit(newCart);
            db.SubmitChanges();
        }
        private decimal getPriceOfProduct(int ProductID)
        {
            return db.PRODUCTs.Single(p => p.PRODUCTID == ProductID).PRICE;
        }
    }
}