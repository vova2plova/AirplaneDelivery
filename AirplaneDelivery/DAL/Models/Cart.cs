using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<Spot> Spots {get;set ;}
        public string Status { get; set; }
        [NotMapped]
        public string Sum { get; set; }
    }
}
