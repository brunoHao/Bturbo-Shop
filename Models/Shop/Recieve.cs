using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebTemplate.Models.Shop
{
    public class Recieve
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public double TotalBill { get; set; }

        [Required]
        public string? Address { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public Cart? Cart { get; set; }
    }
}
