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

        [StringLength(225)]
        [Required]
        [Column(TypeName = "nvarchar")]
        public string? Name { get; set; }

        [Required]
        [DisplayName("Quantity")]
        public int Count { get; set; }

        [Required]
        public Product? Product { get; set; }

        [Required]
        public double? Total { get; set; }

        [Required]
        public AppUser User { get; set; }
    }
}
