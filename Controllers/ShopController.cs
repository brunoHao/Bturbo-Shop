using DemoWebTemplate.Models;
using DemoWebTemplate.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace DemoWebTemplate.Controllers
{
    public class ShopController : Controller
    {
        private readonly MyDatabase _myDatabase;
        private readonly ILogger<ShopController> _logger;
        private readonly UserManager<AppUser> _userManager;
        public ShopController(MyDatabase myDatabase, ILogger<ShopController> logger, IConfiguration configuration, UserManager<AppUser> userManager)
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
        public IActionResult Cart(int Qty, int productId)
        {
            var product = _myDatabase.Products.Where(p => p.Id == productId).FirstOrDefault();
            var userId = _userManager.GetUserId(User);

            var productc = _myDatabase.Products.ToList();


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

        [HttpGet]
        public IActionResult Checkout()
        {
            var userId = _userManager.GetUserId(User);
            var liCart = _myDatabase.Carts.Include("Product").Where(c => c.UserId == userId).ToList();
            ViewData["subtotal"] = liCart.AsEnumerable().Sum(c => c.Total);

            ViewBag.liCart = liCart;

            return View();
        }


        [HttpPost]
        public IActionResult Recieve(Recieve model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            var userId = _userManager.GetUserId(User);
            //Listproduct mà user đã order
            var liCart = _myDatabase.Carts.Include("Product").Where(c => c.UserId == userId).ToList();
            var sumTotal = liCart.Sum(c => c.Total);
            //tổng sản phẩm mà user đã order.

            var recieve = new Recieve()
            {
                UserId = userId,
                TotalBill = (double)sumTotal,
                Address = model.Address,
                Phone = model.Phone,
                Date = DateTime.Now
            };
            _myDatabase.Recieves.Add(recieve);
            _myDatabase.SaveChanges();

            foreach(var cart in liCart)
            {
                var reciveDetails = new RecieveDetail()
                {
                    RecieveId = recieve.Id,
                    ProductId = cart.ProductId,
                    Count = cart.Qty
                };
                _myDatabase.RecieveDetails.Add(reciveDetails);
                _myDatabase.SaveChanges();
            }    

            //Cập nhật Count cho Product = SLT - SLD.
            var product = _myDatabase.Products.ToList();

            //list product của product có trong cart.
            var countProductInCart = (from p in product
                                      join c in liCart
                                      on
                                      p.Id equals c.Product.Id
                                      where
                                      c.UserId == userId
                                      select p).ToList();


            if(countProductInCart.Count == liCart.Count)
            {
                foreach (var item in countProductInCart)
                {
                    foreach (var cart in liCart)
                    {
                        if (item.Id == cart.ProductId)
                        {
                            var Pro = _myDatabase.Products.Where(p => p.Id == cart.ProductId).FirstOrDefault();
                            Pro.Count = item.Count - cart.Qty;
                            _myDatabase.Products.Update(Pro);
                            _myDatabase.SaveChanges();
                        }
                    }
                }
            }    
        

            //Xóa bảng cart sau khi recieve
            foreach (var c in liCart)
            {
                if (c.UserId == userId)
                {
                    _myDatabase.Carts.Remove(c);
                    _myDatabase.SaveChanges();
                } 
            }    

            return RedirectToAction("Confirmation",recieve);
        }


        [HttpGet]
        public IActionResult Confirmation()
        {
            var userId = _userManager.GetUserId(User);
            var recieve = _myDatabase.Recieves.Where(r => r.UserId == userId).ToList();
            return View(recieve);
        }

        //[HttpGet]
        //public IActionResult Confirmation(int id, string returnUrl = null)
        //{
        //    returnUrl ??= Url.Content("~/");
        //    ViewData["ReturnUrl"] = returnUrl;
        //    var recieve = _myDatabase.Recieves.Where(r => r.Id == id).FirstOrDefault();
        //    return View(recieve);
        //}

        [HttpGet]
        public IActionResult Tracking()
        {
            return View();
        }


        //[HttpPost]
        //public IActionResult Tracking(double phone)
        //{
        //    var recieve = _myDatabase.Recieves.Where(p => p.Phone == phone).FirstOrDefault();
        //    return RedirectToAction("Confirmation", recieve);
        //}




    }
}
