using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Models
{
    public class Order
    {
        public int ID { get; set; }
        public System.DateTime DateCreated { get; set; }
        [ForeignKey("user")]
        public string UserID { get; set; }
        public virtual ApplicationUser user { get; set; }
        public double Totalprice { get; set; }
        public ICollection<Shopping_cart_item> Shopping_cart_items { get; set; }
    }
}

