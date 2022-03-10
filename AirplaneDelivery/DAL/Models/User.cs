using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public Cart Cart { get; set; }
        public List<Cart> HistoryOfOrders { get; set; }
    }
}
