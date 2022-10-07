using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoWebTemplate.Models.Shop
{
    public class DetailRecieve
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public Recieve? Recieve { get; set; }
    }
}
