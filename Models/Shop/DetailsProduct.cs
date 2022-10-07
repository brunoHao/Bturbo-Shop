using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebTemplate.Models.Shop
{
    public class DetailsProduct
    {
        [Key]
        public int Id { get; set; }

        [StringLength(225)]
        [Required]
        public string? Name { get; set; }

        [StringLength(225)]
        [Required]
        public string? Brand { get; set; }

        [Required]
        public Product? Product { get; set; }

    }
}
