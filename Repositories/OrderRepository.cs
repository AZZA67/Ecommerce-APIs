using Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        EntityContext context;
        public OrderRepository(EntityContext _context)
        {
            context = _context;
        }
        public List<Order> GetAll()
        {
            return context.Orders.ToList();

        }

        public int Insert(Order order)
        {
            context.Orders.Add(order);
            return context.SaveChanges();

        }
        public int Update(int id, Order order)
        {
            Order oldorder = context.Orders.FirstOrDefault(c => c.ID == id);
            if (oldorder != null)
            {
                oldorder.Shopping_cart_items = order.Shopping_cart_items;

                return context.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            Order oldorder = context.Orders.FirstOrDefault(o => o.ID == id);
            context.Remove(oldorder);
            return context.SaveChanges();
        }


    }
}
