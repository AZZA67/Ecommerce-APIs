using Lab.Models;
using System.Collections.Generic;

namespace Lab.Repositories
{
    public interface ICategoryRepository
    {
        int Delete(int id);
        List<Category> GetAll();
        Category getByNAme(string name);
        int Insert(Category cat);
        int Update(int id, Category cat);
    }
}