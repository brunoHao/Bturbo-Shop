using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebTemplate.Models.Shop
{
    public class Tracking
    {
        [Key]
        public int Id { get; set; }
        public string? Status { get; set; }

        [Required]
        public int? RecieveId { get; set; }

        public Recieve? Recieve { get; set; }

    }
}
