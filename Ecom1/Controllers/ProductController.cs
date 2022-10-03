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
    public class ProductController : Controller
    {
        private readonly IConfiguration config;
        ProductDAL db;
        CartDAL cd;
        OrderDetails od;
        public ProductController(IConfiguration config)
        {
            this.config = config;
            db = new ProductDAL(config);
            cd = new CartDAL(config);
            
        }

        public IActionResult Index()
        {
            var model = db.GetAllProducts();
            return View(model);
        }

        public IActionResult AddProductToCart(int id)
        {
          
            string userid = HttpContext.Session.GetString("userid");
            Cart cart = new Cart();
            cart.ProductId = id;
            cart.UserId = Convert.ToInt32(userid);
            int res = cd.AddToCart(cart);
            if (res == 1)
            {
                return RedirectToAction("ViewCart");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult ViewCart()
        {
            string userid = HttpContext.Session.GetString("userid");
            var model = cd.ViewProductsFromCart(userid);
            return View(model);
        }
        //new
        public IActionResult AddProductToOrder(int id)
        {

            string userid = HttpContext.Session.GetString("userid");
            OrderDetails order = new OrderDetails();
            order.ProductId = id;
            order.UserId = Convert.ToInt32(userid);
            int res = cd.AddToOrder(order);
            if (res == 1)
            {
                return RedirectToAction("PlaceOrder");
            }
            else
            {
                return View();
            }
        }
        //new
        public IActionResult PlaceOrder(int id)
        {
            string userid = HttpContext.Session.GetString("userid");
            var product = db.GetProductById(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult RemoveFromCart(int id)
        {

            int res = cd.RemoveFromCart(id);
            if (res == 1)
            {
                return RedirectToAction("ViewCart");
            }
            else
            {
                return View();
            }
        }

    }
}
