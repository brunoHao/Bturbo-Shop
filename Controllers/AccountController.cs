using System.Text;
using DemoWebTemplate.Models.Account;
using DemoWebTemplate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using DemoWebTemplate.ExtendMethods;
using DemoWebTemplate.Utilities;

namespace DemoWebTemplate.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;

        [TempData]
        public string StatusMessage { get; set; }
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        //public IActionResult Login()
        //{
        //    return View();
        //}

        public IActionResult Tracking()
        {
            return View();
        }
        //GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        ////
        //// POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(model.UserNameOrEmail, model.Password, model.RememberMe, lockoutOnFailure: true);
                // Tìm UserName theo Email, đăng nhập lại
                if ((!result.Succeeded) && AppUtilities.IsValidEmail(model.UserNameOrEmail))
                {
                    var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
                    if (user != null)
                    {
                        result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);
                        StatusMessage = "Success";
                        return RedirectToAction("Index", "Home", StatusMessage);

                    }
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    StatusMessage = "Success";
                    return RedirectToAction("Index", "Home",StatusMessage);
                }


                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "Your Account Are Locked");
                    StatusMessage = "Your Account Are Locked";
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError("Cannot SignIn.");
                    StatusMessage = "Cannot SignIn";
                    return View(model);
                }
            }
            return View(model);
        }

        //// POST: /Account/LogOff
        //[HttpPost("/logout/")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LogOff()
        //{
        //    await _signInManager.SignOutAsync();
        //    _logger.LogInformation("User đăng xuất");
        //    return RedirectToAction("Index", "Home", new {area = ""});
        //}
        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Created User Success");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    StatusMessage = "Created User Success";
                    return RedirectToAction("Index", "Home");


                    // Phát sinh token để xác nhận email
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    //// https://localhost:5001/confirm-email?userId=fdsfds&code=xyz&returnUrl=
                    //var callbackUrl = Url.ActionLink(
                    //    action: nameof(ConfirmEmail),
                    //    values: 
                    //        new { area = "Identity", 
                    //              userId = user.Id, 
                    //              code = code},
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(model.Email, 
                    //    "Xác nhận địa chỉ email",
                    //    @$"Bạn đã đăng ký tài khoản trên RazorWeb, 
                    //       hãy <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>bấm vào đây</a> 
                    //       để kích hoạt tài khoản.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return LocalRedirect(Url.Action(nameof(RegisterConfirmation)));
                    //}
                    //else
                    //{
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                    //}

                }
                StatusMessage = "Register Failed";
                ModelState.AddModelError(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/ConfirmEmail
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterConfirmation()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("ErrorConfirmEmail");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("ErrorConfirmEmail");
            }
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "ErrorConfirmEmail");
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User Logout");
            StatusMessage = "User Logout";
            return RedirectToAction("Index", "Home", StatusMessage);
        }
    }
}
