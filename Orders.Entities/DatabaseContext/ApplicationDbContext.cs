using Microsoft.EntityFrameworkCore;
using Orders.Entities.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Entities.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public ApplicationDbContext()
        {
            
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> Items { get; set; }
    }
}
