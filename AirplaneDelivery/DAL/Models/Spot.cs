using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Spot
    {
        public int Id { get; set; }
        public Product Products { get; set; }
        public int Count { get; set; }
    }
}
