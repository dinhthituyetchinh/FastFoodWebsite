﻿using FastFoodWebsite.Models;
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
                string selectStr = "SET DATEFORMAT DMY;UPDATE PRODUCTS SET PRODUCTNAME = 'N" + name+ "', PRODUCTDESCRIPTION = 'N" + description+ "', PRICE ="+price+", PICTURE = 'N"+img+ "', UPDATED_AT_OF_PROD ='"+updatedAt + "' , PROD_TYPE_ID ="+typeID + " WHERE PRODUCTID =" + id;
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

        public User getUserByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                string selectStr = "SELECT * FROM USERS WHERE USERID =" + id;
                SqlCommand cmd = new SqlCommand(selectStr, connection);
                SqlDataReader rdr = cmd.ExecuteReader();
                User user = null;
                while (rdr.Read())
                {
                    int uID = int.Parse(rdr["USERID"].ToString());
                    string name = rdr["FULLNAME"].ToString();
                    string phone = rdr["PHONE"].ToString();
                    string email = rdr["EMAIL"].ToString();
                    string dayOfBirth = rdr["DAYOFBIRTH"].ToString().Substring(0, 10);
                    DateTime DayOfBirth = DateTime.ParseExact(dayOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    string address = rdr["USERADDRESS"].ToString();
                    string password = rdr["USERPASSWORD"].ToString();
                    string createdAt = rdr["CREATEDAT"].ToString().Substring(0, 10);

                    DateTime updateDate = new DateTime();

                    if (!string.IsNullOrEmpty(rdr["UPDATEDAT"].ToString()))
                    {
                        string updatedAt = rdr["UPDATEDAT"].ToString().Substring(0, 10);
                        updateDate = DateTime.ParseExact(updatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    DateTime createDate = DateTime.ParseExact(createdAt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int roleID = int.Parse(rdr["ROLEID"].ToString());

                    user = new User(uID, name, phone, email, DayOfBirth, address, password, createDate, updateDate, roleID);
                }
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                return user;
            }
        }
        public void updatePassword(int userId, string fullName, string phone, string email, string password, string confirmPassword, DateTime updatedDate, int roleID)
        {
            string updatedAt = updatedDate.ToString().Substring(0, 10);

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                string selectStr = "SET DATEFORMAT DMY;UPDATE USERS SET FULLNAME = '" + fullName + "', PHONE = '" + phone + ", EMAIL = '" + email + "', UPDATEDAT ='" + updatedAt + "' , ROLEID =" + roleID + " WHERE USERID =" + userId;
                SqlCommand cmd = new SqlCommand(selectStr, connection);
                cmd.ExecuteNonQuery();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }




    }
}