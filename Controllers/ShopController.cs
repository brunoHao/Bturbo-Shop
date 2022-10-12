using DemoWebTemplate.Models;
using DemoWebTemplate.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DemoWebTemplate.Controllers
{
    public class ShopController : Controller
    {
        private readonly MyDatabase _myDatabase;
        private readonly ILogger<ShopController> _logger;

        public ShopController(MyDatabase myDatabase, ILogger<ShopController> logger,IConfiguration configuration)
        {
            _myDatabase = myDatabase;
            _logger = logger;
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
        public IActionResult Cart(int Id)
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

        //List<Cart> li = new List<Cart>();
        //[HttpPost]
        //public IActionResult Cart(Product Pi, string Count, int Id)
        //{
        //    var product = _myDatabase.Products.Where(p => p.Id == Id).SingleOrDefault();

        //    Cart c = new Cart();
        //    c.Product.Id = product.Id;
        //    c.Product.Price = (double)product.Price;
        //    c.Count = Convert.ToInt32(product.Count);
        //    c.Total = c.Product.Price * c.Count; 

        //     li.Add(c);

        //    TempData["Cart"] = li;
        //    TempData.Keep();

        //    return  RedirectToAction("Category");
        //}

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
