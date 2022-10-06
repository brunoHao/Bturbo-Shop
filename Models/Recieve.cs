﻿using DemoWebTemplate.Models.Shop;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebTemplate.Models
{
    public class Recieve
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public double Total { get; set; }

        [Required]
        public Cart? Cart { get; set; }
    }
}