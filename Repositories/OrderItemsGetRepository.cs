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
    public class OrderItemsGetRepository :  IGetRepository<OrderItem>
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<OrderItem> _dbSet;

        public OrderItemsGetRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Items;
        }

        

        public async Task<List<OrderItem>?> GetAllAsync(bool include = false)
        {
            return (include) ? await _dbSet.Include(e => e.Order).ToListAsync() : await _dbSet.ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(Guid id, bool include = false)
        {
            return (include) ? await _dbSet.Include(e => e.Order).FirstOrDefaultAsync(e => e.OrderItemId == id) : await _dbSet.FirstOrDefaultAsync(e => e.OrderItemId == id);
        }
    }
}
