using DemoWebTemplate.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebTemplate.Controllers
{
    public class ShopController : Controller
    {
        private readonly MyDatabase _myDatabase;

        public ShopController(MyDatabase myDatabase)
        {
            _myDatabase = myDatabase;
        }
        public IActionResult Category()
        {
            return View(_myDatabase.Products.ToList());
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
