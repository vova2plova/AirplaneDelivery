using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserHistoryFK")]
        public int UserId { get; set; }
        public List<Product> Products { get; set; }
    }
}
