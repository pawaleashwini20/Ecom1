   using Ecom1.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom1.DAL
{
    public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration config;

        public ProductDAL(IConfiguration config)
        {
            this.config = config;
            con = new SqlConnection(config["ConnectionString:defaultConnection"]);
        }

     

        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();
            string str = "select * from Product";
            cmd = new SqlCommand(str, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product p = new Product();
                    p.ProductId = Convert.ToInt32(dr["ProductId"]);
                    p.Name = dr["Name"].ToString();
                    p.price = Convert.ToInt32(dr["price"]);
                    p.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    list.Add(p);
                }
                con.Close();
                return list;
            }
            else
            {
                con.Close();
                return list;
            }

        }
        public Product GetProductById(int id)
        {
            Product p = new Product();
            string str = "select * from Product where ProductId=@id";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    p.ProductId = Convert.ToInt32(dr["ProductId"]);
                    p.Name = dr["Name"].ToString();
                    p.price = Convert.ToInt32(dr["price"]);
                    p.CategoryId = Convert.ToInt32(dr["CategoryId"]);

                }
                con.Close();
                return p;
            }
            else
            {
                con.Close();
                return p;
            }

        }

    }
}
