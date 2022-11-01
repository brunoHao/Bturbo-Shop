using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace DemoWebTemplate.Models.Shop
{
    public class Recieve
    {
        [Key]
        public int Id { get; set; }

        public int Qty { get; set; }

        [Required]
        public double TotalBill { get; set; }

        [Required]
        public string? Address { get; set; }

        public DateTime Date { get; set; }

        public double? Phone { get; set; }

        [Required]
        public string? UserId { get; set; }

        public string? status { get; set; }

        
        
    }
}
