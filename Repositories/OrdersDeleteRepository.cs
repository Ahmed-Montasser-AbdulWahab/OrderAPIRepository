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
    public class OrdersDeleteRepository : IDeleteRepository<Order>
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<Order> _dbSet;

        public OrdersDeleteRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Orders;
        }
        

        public async Task<bool> DeleteAsync(Order entity)
        {
            if (await _dbSet.FirstOrDefaultAsync(e => e.OrderId == entity.OrderId) is null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            return (await _db.SaveChangesAsync() > 0 );
        }

        
    }
}
