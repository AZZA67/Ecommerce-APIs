using Lab.Models;
using System.Collections.Generic;

namespace Lab.Repositories
{
    public interface IProdutRepository
    {
        int Delete(int id);
        List<Product> GetAll();
        Product getByNAme(string name);
        int Insert(Product pro);
        int Update(int id, Product pro);
        List<Product> getBycatname(string name);
        List<Product> getBycatid(int id);
        Product getByprodbyid(int prodid);
    }
}