using DemoWebTemplate.Models.Shop;
using Microsoft.AspNetCore.Identity;

namespace DemoWebTemplate.Models
{
    public class AppUser: IdentityUser
    {
        public string? level { get; set; }
        public Coupon? Coupon { get; set; }
    }
}
