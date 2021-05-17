using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }

        public int Quantity { get; set; }

        public double Weight { get; set; }
    }
}
