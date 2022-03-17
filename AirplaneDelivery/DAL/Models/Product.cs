using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DAL.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Category CategoryProduct { get; set; }

        public int Weight { get; set; }
        
        public float Kkal { get; set; }
        
        public float Proteins { get; set; }
        
        public float Fats { get; set; }
        
        public float Carbohydrates { get; set; }
        
        public Uri Image { get; set; }
        
        public int CountInStorage { get; set; }
        public List<Recipe> Recipe { get; set; }
    }
}
