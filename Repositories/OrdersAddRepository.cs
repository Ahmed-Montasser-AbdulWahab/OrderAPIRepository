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
    public class OrdersAddRepository : IAddRepository<Order> { 
        private readonly ApplicationDbContext _db;
        private readonly DbSet<Order> _dbSet;

        public OrdersAddRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Orders;
        }
        public async Task<bool> AddAsync(Order entity)
        {
            if (await _dbSet.FirstOrDefaultAsync(e => e.OrderId == entity.OrderId) is not null)
            {
                return false;
            }

            _dbSet.Add(entity);
            return (await _db.SaveChangesAsync() > 0);
        }

    }
}
