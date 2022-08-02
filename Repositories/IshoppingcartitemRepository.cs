using Lab.Models;
using System.Collections.Generic;

namespace Lab.Repositories
{
    public interface IshoppingcartitemRepository
    {
        int Delete(int id);
        List<Shopping_cart_item> GetAll();
        int Insert(Shopping_cart_item cart_item);
        int Update(int id, Shopping_cart_item cat);
    }
}