using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebTemplate.Models.Shop
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int Qty { get; set; }

        
        public double Price { get; set; }

        [Required]
        public double? Total { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string? UserId { get; set; }
    }
}
