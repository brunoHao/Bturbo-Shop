using System.ComponentModel.DataAnnotations;

namespace DemoWebTemplate.Models.Shop
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        public string? CouponCode { get; set; }

        public int? discount { get; set; }

        public DateTime? Date { get; set; }
    }
}
