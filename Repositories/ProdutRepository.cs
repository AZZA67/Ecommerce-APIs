using Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Repositories
{
    public class ProdutRepository : IProdutRepository
    {
       EntityContext context;
        public ProdutRepository(EntityContext _context)
        {
            context = _context;
        }
        public List<Product> GetAll()
        {
            return context.Products.ToList();

        }

        public int Insert(Product pro)
        {
            context.Products.Add(pro);
            return context.SaveChanges();

        }
        public int Update(int id, Product pro)
        {
            Product oldpro = context.Products.FirstOrDefault(p => p.ID == id);
            if (oldpro != null)
            {
                oldpro.Name = pro.Name;
                oldpro.price = pro.price;
                oldpro.image = pro.image;
                oldpro.Quantity = pro.Quantity;

                return context.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            Product oldp = context.Products.FirstOrDefault(p => p.ID == id);
            context.Remove(oldp);
            return context.SaveChanges();
        }
        public Product getByNAme(string name)
        {
            Product p =
               context.Products.FirstOrDefault(p => p.Name == name);

            return p;

        }

        public List<Product> getBycatname(string name)
        {
            Category c = context.Categories.FirstOrDefault(c => c.Name == name);
            List<Product> products =
                context.Products.Where(p => p.categoryID == c.ID).ToList();

            return products;

        }

        public List<Product> getBycatid(int catid)
        {
          
            List<Product> products =
                context.Products.Where(p => p.categoryID == catid).ToList();

            return products;

        }

        public Product getByprodbyid(int prodid)
        {

            Product product =
                context.Products.FirstOrDefault(p => p.ID == prodid);

            return product;

        }

       

    }
}


