using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom1.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        [ScaffoldColumn(false)]
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
