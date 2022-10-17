using DemoWebTemplate.Models;
using DemoWebTemplate.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Identity;
using System.Net.WebSockets;

namespace DemoWebTemplate.Controllers
{
    public class ShopController : Controller
    {
        private readonly MyDatabase _myDatabase;
        private readonly ILogger<ShopController> _logger;
        private readonly UserManager<AppUser> _userManager;
        public ShopController(MyDatabase myDatabase, ILogger<ShopController> logger,IConfiguration configuration,UserManager<AppUser> userManager)
        {
            _myDatabase = myDatabase;
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Index(int Id)
        {
            ViewBag.categoryType = _myDatabase.Categories.ToList();
            if (Id == 0)
            {
                return View(_myDatabase.Products.ToList());
            }
            else
            {
                return View(_myDatabase.Products.Where(x => x.Category.Id == Id).ToList());
            }
        }
      
        [HttpGet]
        public IActionResult ProductDetail(int Id)
        {
            if (Id == 0)
            {
                return RedirectToAction("Category", "Shop");
            }
            else
            {
              var product = _myDatabase.Products.Include("Category").Where(p => p.Id == Id).FirstOrDefault();
              return View(product);
            }
          
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var userId = _userManager.GetUserId(User);
            var liCart = _myDatabase.Carts.Include("Product").Where(c => c.UserId == userId).ToList();
            return View(liCart);
        }

        [HttpPost]
        public IActionResult Cart(int Qty,int productId)
        {
            var product = _myDatabase.Products.Where(p => p.Id == productId).FirstOrDefault();
            var userId = _userManager.GetUserId(User);

            Cart cart = new Cart()
            {
                UserId = userId,
                ProductId = productId,
                Qty = Qty,
                Price = product.Price,
                Name = product.Name,
                Total = Qty * product.Price
            };
            _myDatabase.Carts.Add(cart);
            _myDatabase.SaveChanges();

            return RedirectToAction("Cart");
        }

        public IActionResult ChangeQuantity(int Qty, int ProductId, int Id)
        {
            var product = _myDatabase.Products.Where(p => p.Id == ProductId).FirstOrDefault();
            var cart = _myDatabase.Carts.Where(c => c.Id == Id).FirstOrDefault();
            cart.Qty = Qty;
            cart.Total = Qty * product.Price;
            _myDatabase.Carts.Update(cart);
            _myDatabase.SaveChanges();
            return RedirectToAction("Cart");
        }

        public IActionResult Confirmation()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
