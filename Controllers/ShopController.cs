using Microsoft.AspNetCore.Mvc;

namespace DemoWebTemplate.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult category()
        {
            return View();
        }
        public IActionResult singleproduct()
        {
            return View();
        }
        public IActionResult checkout()
        {
            return View();
        }
        public IActionResult cart()
        {
            return View();
        }
        public IActionResult confirmation()
        {
            return View();
        }
    }
}
