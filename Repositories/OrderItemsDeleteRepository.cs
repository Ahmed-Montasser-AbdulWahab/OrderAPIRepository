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
    public class OrderItemsDeleteRepository : IDeleteRepository<OrderItem>
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<OrderItem> _dbSet;

        public OrderItemsDeleteRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Items;
        }

        

        public async Task<bool> DeleteAsync(OrderItem entity)
        {
            if (await _dbSet.FirstOrDefaultAsync(e => e.OrderItemId == entity.OrderItemId) is null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            return (await _db.SaveChangesAsync() > 0);
        }

        
    }
}
