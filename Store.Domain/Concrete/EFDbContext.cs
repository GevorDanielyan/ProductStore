using System;
using Store.Domain.Entities;
using System.Data.Entity;

namespace Store.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public EFDbContext(string connectionString) : base(nameOrConnectionString: connectionString)
        {
        }
    }
}
