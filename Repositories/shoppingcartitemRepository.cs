using Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Repositories
{
    public class shoppingcartitemRepository : IshoppingcartitemRepository
    {
        EntityContext context;
        public shoppingcartitemRepository(EntityContext _context)
        {
            context = _context;
        }
        public List<Shopping_cart_item> GetAll()
        {
            return context.Carts.ToList();

        }
        public int Insert(Shopping_cart_item cart_item)
        {
            context.Carts.Add(cart_item);
            return context.SaveChanges();
        }
        public int Update(int id, Shopping_cart_item cat)
        {
            Product p = context.Products.FirstOrDefault(p => p.ID == id);
            Shopping_cart_item oldcat = context.Carts.FirstOrDefault(c => c.ProductId == p.ID);
            if (oldcat != null)
            {
                oldcat.product_name = cat.product_name;
                oldcat.product_price = cat.product_price;
                oldcat.Quantity = cat.Quantity;
                return context.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            Product p = context.Products.FirstOrDefault(p => p.ID == id);
            Shopping_cart_item oldc = context.Carts.FirstOrDefault(c => c.ProductId == p.ID);
            context.Remove(oldc);
            return context.SaveChanges();
        }



    }
}
