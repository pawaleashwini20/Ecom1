using Ecom1.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom1.DAL
{
    public class CartDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        private readonly IConfiguration config;
        
        public CartDAL(IConfiguration config)
        {
          
            this.config = config;
            con = new SqlConnection(config["ConnectionString:defaultConnection"]);
           
        }
        private bool CheckCartData(Cart cart)
        {

            return true;
        }
        public int AddToCart(Cart cart)
        {
            bool result = CheckCartData(cart);
            if (result == true)
            {
                string qry = "insert into Cart values(@prodid,@userid)";
                cmd = new SqlCommand(qry, con);
                
                cmd.Parameters.AddWithValue("@prodid", cart.ProductId);
                cmd.Parameters.AddWithValue("@userid", cart.UserId);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }
            else
            {
                return 2;
            }
         }

        public List<Product> ViewProductsFromCart(string userid)
        {
            List<Product> plist = new List<Product>();
            string qry = "select p.ProductId,p.Name,p.price,p.CategoryId, c.CartId,c.UserId from Product p " +
                        " inner join Cart c on c.ProductId = p.ProductId " +
                        " where c.UserId = @id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(userid));
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
                   // p.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    p.CartId = Convert.ToInt32(dr["CartId"]);
                    //p.UserId = Convert.ToInt32(dr["UserId"]);
                    plist.Add(p);
                }
                con.Close();
                return plist;
            }
            else
            {
                return plist;
            }
        }

        private bool CheckOrderData(OrderDetails oid)
        {

            return true;
        }
        public int AddToOrder(OrderDetails oid)
        {
            bool result = CheckOrderData(oid);
            if (result == true)
            {
                string qry = "insert into OrderDetails values(@userid,@pid,@quantity)";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@userid", oid.UserId);
                cmd.Parameters.AddWithValue("@pid", oid.ProductId);
                cmd.Parameters.AddWithValue("@quantity", oid.Quantity);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }
            else
            {
                return 2;
            }
        
        }
       
        public int RemoveFromCart(int id)
        {

            string qry = "delete from Cart where CartId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}
