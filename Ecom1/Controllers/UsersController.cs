using Ecom1.DAL;
using Ecom1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom1.Controllers
{
    public class UsersController : Controller
    {
        private readonly IConfiguration config;
        UsersDAL db;
        // GET: UsersController
        public UsersController(IConfiguration config)
        {
            this.config = config;
            db = new UsersDAL(config);
        }


       // UsersDAL db = new UsersDAL();
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Users users)
        {
            try
            {
                int res = db.UserSignUp(users);
                if (res == 1)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(Users users)
        {
            Users user = db.UserLogin(users);
            if (user.Password == users.Password)
            {
                HttpContext.Session.SetString("username", user.FirstName + " " + user.LastName);
                HttpContext.Session.SetString("userid", user.UserId.ToString());
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }

    }
}
