using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebTemplate.Models.Shop
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [StringLength(225)]
        [Required]
        [Column(TypeName = "nvarchar")]
        public string? Name { get; set; }

        [Required]
        public Product? Product { get; set; }

    }
}
