using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Models
{
    public class Shopping_cart_item
    {
       
        public int ID{ get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("user")]
     
        public int Quantity { get; set; }
        public string product_name { get; set; }
        public double product_price { get; set; }
        public virtual Product Product { get; set; }
     
     
    }
}
