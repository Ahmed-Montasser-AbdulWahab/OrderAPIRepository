using Orders.ServiceContracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.ServiceContracts
{
    public interface IOrdersService
    {
        public Task<List<OrderResponse>?> GetOrders(bool include = false);
        public Task<OrderResponse?> GetOrderById(Guid id, bool include = false);
        public Task<OrderResponse?> AddOrder(OrderAddRequest request);
        public Task<bool> UpdateOrder(OrderUpdateRequest request, Guid id);
        public Task<bool> DeleteOrder(Guid id);
    }
}
