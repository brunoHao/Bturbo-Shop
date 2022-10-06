using Microsoft.AspNetCore.Mvc;

namespace DemoWebTemplate.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Category()
        {
            return View();
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
