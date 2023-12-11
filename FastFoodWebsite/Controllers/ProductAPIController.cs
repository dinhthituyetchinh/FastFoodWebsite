using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FastFoodWebsite.Models;

namespace FastFoodWebsite.Controllers
{
    public class ProductAPIController : ApiController
    {
        [HttpGet]
        public List<PRODUCT> getProductList()
        {
            DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
            return db.PRODUCTs.ToList();
        }

        [HttpGet]
        public PRODUCT getProduct(int id)
        {
            DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
            return db.PRODUCTs.FirstOrDefault(s => s.PRODUCTID == id);
        }

        [HttpPost]
        public int InsertNewProduct(string pName, string pDes, decimal pMoney, string pImg, DateTime create_At, DateTime update_At, int pType )
        {
            try
            {
                DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
                PRODUCT p = new PRODUCT();
                p.PRODUCTNAME = pName;
                p.PRODUCTDESCRIPTION = pDes;
                p.PRICE = pMoney;
                p.PICTURE = pImg;
                p.CREATED_AT_OF_PROD = create_At;
                p.UPDATED_AT_OF_PROD = update_At;
                p.PROD_TYPE_ID = pType;
                db.PRODUCTs.InsertOnSubmit(p);
                db.SubmitChanges();
                return 1;
            } catch
            {
                return 0;
            }
        }

        [HttpPut]

        public bool UpdateProduct(int pID, string pName, string pDes, decimal pMoney, string pImg, DateTime create_At, DateTime update_At, int pType)
        {
            try
            {
                DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
                PRODUCT p = db.PRODUCTs.FirstOrDefault(s => s.PRODUCTID == pID);
                if(p == null) return false;
                p.PRODUCTNAME = pName;
                p.PRODUCTDESCRIPTION = pDes;
                p.PRICE = pMoney;
                p.PICTURE = pImg;
                p.CREATED_AT_OF_PROD = create_At;
                p.UPDATED_AT_OF_PROD = update_At;
                p.PROD_TYPE_ID = pType;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpDelete]
        public bool DeleteProduct (int ma)
        {
            DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
            PRODUCT p = db.PRODUCTs.FirstOrDefault(s => s.PRODUCTID == ma);
            if (p == null) return false;
            db.PRODUCTs.DeleteOnSubmit(p);
            db.SubmitChanges();
            return true;
        }

    }
}
