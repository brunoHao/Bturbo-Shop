using DemoWebTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoWebTemplate.Controllers
{
    public class ShopController : Controller
    {
        private readonly MyDatabase _myDatabase;
        private readonly ILogger<ShopController> _logger;

        public ShopController(MyDatabase myDatabase,ILogger<ShopController> logger)
        {
            _myDatabase = myDatabase;
            _logger = logger;
        }

        public IActionResult Category(int Id)
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
        public IActionResult Singleproduct()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
