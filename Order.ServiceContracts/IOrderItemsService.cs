using Orders.Entities.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.ServiceContracts
{
    public interface IOrderItemsService
    {
        public Task<OrderItemResponse?> AddOrderItem(OrderItemAddRequest orderItemAddRequest);
        public Task<bool> UpdateOrderItem(Guid orderId, Guid id, OrderItemUpdateRequest orderItemUpdateRequest);
        public Task<bool> DeleteOrderItem(Guid orderId, Guid id);
    }
}
