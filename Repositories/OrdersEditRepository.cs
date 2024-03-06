using Microsoft.EntityFrameworkCore;
using Orders.Entities.DatabaseContext;
using Orders.Entities.ModelEntities;
using RepositoryContracts;
using System;
using System.Collections.Generic;


namespace Repositories
{
    public class OrdersEditRepository : IEditRepository<Order>
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<Order> _dbSet;

        public OrdersEditRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Orders;
        }
       
        public async Task<bool> EditAsync(Order entity)
        {
            var entityInDb = await _dbSet.FirstOrDefaultAsync(e => e.OrderId == entity.OrderId);
            if (entityInDb is null)
            {
                return false;
            }

            entityInDb.OrderDate = entity.OrderDate;
            entityInDb.OrderNumber = entity.OrderNumber;
            entityInDb.CustomerName = entity.CustomerName;
            entityInDb.TotalAmount = entity.TotalAmount;

            return (await _db.SaveChangesAsync() > 0);
        }

    }
}
