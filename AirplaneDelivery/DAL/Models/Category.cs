using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Uri Image { get; set; }

        [NotMapped]
        public Color isChoosen { get; set; }
    }
}
