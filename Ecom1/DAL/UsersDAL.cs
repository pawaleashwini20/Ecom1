using Ecom1.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom1.DAL
{
    public class UsersDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        private readonly IConfiguration config;

        public UsersDAL(IConfiguration config)
        {
            this.config = config;
            con = new SqlConnection(config["ConnectionString:defaultConnection"]);
        }

        public int UserSignUp(Users users)
        {
            string qry = "insert into Users values(@first,@last,@email,@pass)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@first", users.FirstName);
            cmd.Parameters.AddWithValue("@last", users.LastName);
            cmd.Parameters.AddWithValue("@email", users.Email);
            cmd.Parameters.AddWithValue("@pass", users.Password);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
        public Users UserLogin(Users users)
        {
            Users user = new Users();
            string qry = "select * from Users where Email=@email";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("email", users.Email);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    user.UserId = Convert.ToInt32(dr["UserId"]);
                    user.FirstName = dr["FirstName"].ToString();
                    user.LastName = dr["LastName"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.Password = dr["Password"].ToString();
                }
                con.Close();
                return user;

            }
            else
            {
                return user;
            }
        }
    }
}
