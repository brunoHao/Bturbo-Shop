using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebTemplate.Models.Shop
{

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(225)]
        [Required]
        [Column(TypeName = "nvarchar")]
        public string? Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [DisplayName("Quantity")]
        public int Count { get; set; }

        
        public string Desc { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public string Image { get; set; }


    }
}
