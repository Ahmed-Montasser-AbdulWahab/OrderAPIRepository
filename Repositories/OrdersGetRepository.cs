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
    public class OrdersGetRepository :  IGetRepository<Order>
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<Order> _dbSet;

        public OrdersGetRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Orders;
        }
        
        public async Task<List<Order>?> GetAllAsync(bool include = false)
        {
            return (include) ? await _dbSet.Include(e => e.Items).ToListAsync() : await _dbSet.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id, bool include = false)
        {
            return (include) ? await _dbSet.Include(e => e.Items).FirstOrDefaultAsync(e => e.OrderId == id) : await _dbSet.FirstOrDefaultAsync(e => e.OrderId == id);
        }
    }
}
