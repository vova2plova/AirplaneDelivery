using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public float Kkal { get; set; }
        [Required]
        public float Proteins { get; set; }
        [Required]
        public float Fats { get; set; }
        [Required]
        public float Carbohydrates { get; set; }
        [Required]
        public string Images { get; set; }
        [Required]
        public int CountInStorage { get; set; }
    }
}
