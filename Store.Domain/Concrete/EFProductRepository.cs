using System;
using System.Collections.Generic;
using Store.Domain.Entities;
using Store.Domain.Abstract;
using System.Web;

namespace Store.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        EFDbContext context;

        public EFProductRepository()
        {
            string mdfFilePath = HttpContext.Current.Server.MapPath("~/App_Data/ProductStore.mdf");
            context = new EFDbContext(string.Format(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Gevorg\Desktop\TestSQLQuery1;Integrated Security=True;User Instance=True", mdfFilePath));
        }

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
                context.Products.Add(product);
            else
            {
                Product dbEntry = context.Products.Find(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products.Find(productId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
