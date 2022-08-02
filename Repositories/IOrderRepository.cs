using Lab.Models;
using System.Collections.Generic;

namespace Lab.Repositories
{
    public interface IOrderRepository
    {
        int Delete(int id);
        List<Order> GetAll();
        int Insert(Order order);
        int Update(int id, Order order);
    }
}