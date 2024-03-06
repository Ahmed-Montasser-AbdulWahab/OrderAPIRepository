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
    public class OrderItemsEditRepository : IEditRepository<OrderItem>
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<OrderItem> _dbSet;

        public OrderItemsEditRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Items;
        }

        

        public async Task<bool> EditAsync(OrderItem entity)
        {
            var entityInDb = await _dbSet.FirstOrDefaultAsync(e => e.OrderItemId == entity.OrderItemId);
            if (entityInDb is null)
            {
                return false;
            }

            entityInDb.OrderId = entity.OrderId;
            entityInDb.UnitPrice = entity.UnitPrice;
            entityInDb.Quantity = entity.Quantity;
            entityInDb.TotalPrice = entity.TotalPrice;
            entityInDb.ProductName = entity.ProductName;
            

            return (await _db.SaveChangesAsync() > 0);
        }

    }
}
