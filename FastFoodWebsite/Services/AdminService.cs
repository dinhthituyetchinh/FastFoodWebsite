using FastFoodWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FastFoodWebsite.Services
{
    public class AdminService
    {
        public string conStr = "Data Source = TUYETCHINH\\SQLSERVER1; Initial Catalog = DB_FASTFOOD; User ID = sa; Password = tietchin";
        public List<Admin_Product> ExcuteSQL()
        {
            List<Admin_Product> productList = new List<Admin_Product>();

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = conStr;
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                string selectStr = "SELECT * FROM PRODUCTS";
                SqlCommand cmd = new SqlCommand(selectStr, connection);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = int.Parse(rdr["PRODUCTID"].ToString());
                    string name = rdr["PRODUCTNAME"].ToString();
                    string description = rdr["PRODUCTDESCRIPTION"].ToString();
                    decimal price = decimal.Parse(rdr["PRICE"].ToString());
                    string img = rdr["PICTURE"].ToString();
                    string createdAt = rdr["CREATED_AT_OF_PROD"].ToString().Substring(0, 10);

                    DateTime updateDate = new DateTime();

                    if (!string.IsNullOrEmpty(rdr["UPDATED_AT_OF_PROD"].ToString()))
                    {
                        string updatedAt = rdr["UPDATED_AT_OF_PROD"].ToString().Substring(0, 10);
                        updateDate = DateTime.ParseExact(updatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    DateTime createDate = DateTime.ParseExact(createdAt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int typeID = int.Parse(rdr["PROD_TYPE_ID"].ToString());

                    Admin_Product product = new Admin_Product(id, name, description, price, img, createDate, updateDate, typeID);
                    productList.Add(product);
                }
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return productList;
        }

        public void AddSql(string name, string description, decimal price, string img, DateTime createdDate, int typeID)
        {

            string createdAt = createdDate.ToString().Substring(0, 10);
            
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                string selectStr = "SET DATEFORMAT DMY;INSERT INTO PRODUCTS (PRODUCTNAME, PRODUCTDESCRIPTION, PRICE, PICTURE,CREATED_AT_OF_PROD, PROD_TYPE_ID)" +
                    "VALUES (N'"+name+"' , N'"+description +"', "+price +", N'"+img+"', '" + createdAt + "', "+typeID+")";
                SqlCommand cmd = new SqlCommand(selectStr, connection);
                cmd.ExecuteNonQuery();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        public void DeleteSql(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                string selectStr = "DELETE PRODUCTS WHERE PRODUCTID = '"+id+"'";
                SqlCommand cmd = new SqlCommand(selectStr, connection);
                cmd.ExecuteNonQuery();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public bool checkProductInCart(int id)
        {
            using(SqlConnection connection = new SqlConnection(conStr))
            {
                if(connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                string selectStr = "SELECT COUNT(*) FROM CART JOIN PRODUCTS ON PRODUCTS.PRODUCTID = CART.PRODUCTID WHERE PRODUCTS.PRODUCTID = "+id;
                SqlCommand cmd = new SqlCommand(selectStr, connection);
                int count = (int)cmd.ExecuteScalar();               
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                return count > 0;
            }    
        }
        public void updateSql (int id, string name, string description, decimal price, string img, DateTime updateDate, int typeID)
        {
            string updatedAt = updateDate.ToString().Substring(0, 10);

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                string selectStr = "SET DATEFORMAT DMY;UPDATE PRODUCTS SET PRODUCTNAME = '" + name+ "', PRODUCTDESCRIPTION = '" + description+ "', PRICE ="+price+", PICTURE = '"+img+ "', UPDATED_AT_OF_PROD ='"+updatedAt + "' , PROD_TYPE_ID ="+typeID + " WHERE PRODUCTID =" + id;
                SqlCommand cmd = new SqlCommand(selectStr, connection);
                cmd.ExecuteNonQuery();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public Admin_Product getProductByID (int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                string selectStr = "SELECT * FROM PRODUCTS WHERE PRODUCTID ="+id;
                SqlCommand cmd = new SqlCommand(selectStr, connection);
                SqlDataReader rdr = cmd.ExecuteReader();
                Admin_Product product = null;
                while (rdr.Read())
                {
                    int pid = int.Parse(rdr["PRODUCTID"].ToString());
                    string name = rdr["PRODUCTNAME"].ToString();
                    string description = rdr["PRODUCTDESCRIPTION"].ToString();
                    decimal price = decimal.Parse(rdr["PRICE"].ToString());
                    string img = rdr["PICTURE"].ToString();
                    string createdAt = rdr["CREATED_AT_OF_PROD"].ToString().Substring(0, 10);

                    DateTime updateDate = new DateTime();

                    if (!string.IsNullOrEmpty(rdr["UPDATED_AT_OF_PROD"].ToString()))
                    {
                        string updatedAt = rdr["UPDATED_AT_OF_PROD"].ToString().Substring(0, 10);
                        updateDate = DateTime.ParseExact(updatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    DateTime createDate = DateTime.ParseExact(createdAt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int typeID = int.Parse(rdr["PROD_TYPE_ID"].ToString());

                    product = new Admin_Product(pid, name, description, price, img, createDate, updateDate, typeID);
                }
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                return product;
            }
        }

    }
}