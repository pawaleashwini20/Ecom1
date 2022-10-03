using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom1.Models
{
    [Table("OrderDetails")]
    public class OrderDetails
    {
        [Key]
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
     
        public int Quantity { get; set; }
    }
}
