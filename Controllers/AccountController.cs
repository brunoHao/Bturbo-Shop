using Microsoft.AspNetCore.Mvc;

namespace DemoWebTemplate.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult login()
        {
            return View();
        }
        public IActionResult tracking()
        {
            return View();
        }
    }
}
