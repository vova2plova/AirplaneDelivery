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
        public string Name { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        
        public float Kkal { get; set; }
        
        public float Proteins { get; set; }
        
        public float Fats { get; set; }
        
        public float Carbohydrates { get; set; }
        
        public string Images { get; set; }
        
        public int CountInStorage { get; set; }

        public List<Spot> Spot { get; set; }
    }
}
