using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double price  { get; set; }
        public int Quantity  { get; set; }
        public string image { get; set; }
        [ForeignKey("category")]
        public int categoryID { get; set; }

        public virtual Category category { get; set; }
        List<Shopping_cart_item> shopping_Carts = new List<Shopping_cart_item>();
    }
}
