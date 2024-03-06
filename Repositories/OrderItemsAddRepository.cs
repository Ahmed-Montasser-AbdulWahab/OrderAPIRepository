using Microsoft.EntityFrameworkCore;
using Orders.Entities.DatabaseContext;
using Orders.Entities.ModelEntities;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderItemsAddRepository : IAddRepository<OrderItem>
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<OrderItem> _dbSet;

        public OrderItemsAddRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Items;
        }

        public async Task<bool> AddAsync(OrderItem entity)
        {
            if (await _dbSet.FirstOrDefaultAsync(e => e.OrderItemId == entity.OrderItemId) is not null)
            {
                return false;
            }

            _dbSet.Add(entity);
            return (await _db.SaveChangesAsync() > 0);
        }

        
    }
}
