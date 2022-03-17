using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Recipe
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public List<Product> Products { get; set; }
        public float Kkal { get; set; }

        public float Proteins { get; set; }

        public float Fats { get; set; }

        public float Carbohydrates { get; set; }

        public Uri Image { get; set; }
        public Uri UrlLink { get; set; }
    }
}
