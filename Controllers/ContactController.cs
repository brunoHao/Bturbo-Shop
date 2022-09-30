using Microsoft.AspNetCore.Mvc;

namespace DemoWebTemplate.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
