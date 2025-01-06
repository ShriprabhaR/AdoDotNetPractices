using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace EmployeePayRollAdo.Net
{
    internal class ProdRepository
    {
        public static string connString = @"Data Source=LAPTOP-NSJ2R3DO\SQLEXPRESS;Initial Catalog=UserDemo;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(connString);

        public bool AddProduct(ProductModel product)
        {
            SqlCommand cmd = new SqlCommand("spAddProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProdName", product.ProdName);
            cmd.Parameters.AddWithValue("@ProdPrice", product.ProdPrice);
            cmd.Parameters.AddWithValue("@ProdColor", product.ProdColor);
            cmd.Parameters.AddWithValue("@prodBrand", product.ProdBrand);
            conn.Open();
            var result = cmd.ExecuteNonQuery();
            if (result != 0)
            {
                return true;
            }
            return false;
        }

        public void GetAllProduct()
        {
            ProductModel prod = new ProductModel();
            using (this.conn)
            {
                string query = "select * from ProductDetails";
                this.conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        prod.ProdId = reader.GetInt32(0);
                        prod.ProdName = reader.GetString(1);
                        prod.ProdPrice = reader.GetInt32(2);
                        prod.ProdColor = reader.GetString(3);
                        prod.ProdBrand = reader.GetString(4);
                        Console.WriteLine("{0}, {1}, {2}, {3}, {4}", prod.ProdId, prod.ProdName, prod.ProdPrice, prod.ProdColor, prod.ProdBrand);
                    }
                }
                else
                {
                    Console.WriteLine("Data not found");
                }
            }

        }
        public void UpdateProduct()
        {
            Console.WriteLine("Enter Product Id to Update");
            int prodId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Product price to update");
            int prodPrice = int.Parse(Console.ReadLine());
            string updateQuery = "UPDATE ProductDetails SET ProdPrice = "+prodPrice+" WHERE ProdId = "+prodId+"";    
            SqlCommand sqlCommand = new SqlCommand(updateQuery, conn);
            this.conn.Open();
            sqlCommand.ExecuteNonQuery();
            Console.WriteLine("Data updated suceessfully");
        }

        public void DeleteProduct()
        {
            Console.WriteLine("Enter ProdId to delete");
            int prodId = int.Parse(Console.ReadLine()) ;
            string deleteQuery = "Delete from ProductDetails where ProdId = " + prodId + "";
            SqlCommand sqlCommand = new SqlCommand(deleteQuery, conn);
            this.conn.Open();
            sqlCommand.ExecuteNonQuery();
            Console.WriteLine("Data deleted");
        }
    }
}
