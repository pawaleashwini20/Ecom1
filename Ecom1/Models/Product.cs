using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom1.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int CartId { get; set; }
        public int UserId { get; set; }

    }
}
