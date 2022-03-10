using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Uri Image { get; set; }
    }
}
