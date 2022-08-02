using Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        EntityContext context;
        public CategoryRepository(EntityContext _context)
        {
            context = _context;
        }
        public List<Category> GetAll()
        {
            return context.Categories.ToList();

        }

        public int Insert(Category cat)
        {
            context.Categories.Add(cat);
            return context.SaveChanges();

        }
        public int Update(int id, Category cat)
        {
            Category oldcat = context.Categories.FirstOrDefault(c => c.ID == id);
            if (oldcat != null)
            {
                oldcat.Name = cat.Name;

                return context.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            Category oldc = context.Categories.FirstOrDefault(c => c.ID == id);
            context.Remove(oldc);
            return context.SaveChanges();
        }

        public Category getByNAme(string name)
        {
            Category cat =
               context.Categories.FirstOrDefault(c => c.Name == name);

            return cat;

        }

    }
}
