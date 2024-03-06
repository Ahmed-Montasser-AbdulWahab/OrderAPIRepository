using Orders.Entities.ModelEntities;
using Orders.ServiceContracts;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IAddRepository<OrderItem> _addRepository;
        private readonly IGetRepository<OrderItem> _getRepository;
        private readonly IEditRepository<OrderItem> _editRepository;
        private readonly IDeleteRepository<OrderItem> _deleteRepository;
        private readonly IEditRepository<Order> _editOrderRepository;
        private readonly IGetRepository<Order> _getOrderRepository;

        public OrderItemsService(IAddRepository<OrderItem> addRepository,
            IGetRepository<OrderItem> getRepository,
            IEditRepository<OrderItem> editRepository,
            IDeleteRepository<OrderItem> deleteRepository,
            IEditRepository<Order> editOrderRepository,
            IGetRepository<Order> getOrderRepository)
        {
            _addRepository = addRepository;
            _getRepository = getRepository;
            _editRepository = editRepository;
            _deleteRepository = deleteRepository;
            _editOrderRepository = editOrderRepository;
            _getOrderRepository = getOrderRepository;
        }

        public async Task<OrderItemResponse?> AddOrderItem(OrderItemAddRequest orderItemAddRequest)
        {
            var orderItem = orderItemAddRequest.ToOrderItem();

            if (!await _addRepository.AddAsync(orderItem)) {
                return null;
            };

            var order = await _getOrderRepository.GetByIdAsync(orderItem.OrderId);

            order!.TotalAmount += orderItem.TotalPrice;

            if (await _editOrderRepository.EditAsync(order))
            {
                return orderItem.ToResponse();
            }
            else
            {
                await _deleteRepository.DeleteAsync(orderItem);
                return null;
            }
            
        }


        public async Task<bool> UpdateOrderItem(Guid orderId, Guid id, OrderItemUpdateRequest orderItemUpdateRequest)
        {

            var orderItem = await _getRepository.GetByIdAsync(id);

            if(orderItem is null || orderItem.OrderId != orderId)
            {
                return false;
            }
            
            var order = await _getOrderRepository.GetByIdAsync(orderId);

            order!.TotalAmount += (orderItemUpdateRequest.UnitPrice * orderItemUpdateRequest.Quantity);

            if (!await _editOrderRepository.EditAsync(order))
            
            {
                return false;
            }

            orderItem.UnitPrice = orderItemUpdateRequest.UnitPrice;
            orderItem.Quantity = orderItemUpdateRequest.Quantity;
            orderItem.TotalPrice = orderItemUpdateRequest.Quantity * orderItemUpdateRequest.UnitPrice;
            orderItem.ProductName = orderItemUpdateRequest.ProductName;

            if (!await _editRepository.EditAsync(orderItem)) {
                order.TotalAmount -= (orderItemUpdateRequest.UnitPrice * orderItemUpdateRequest.Quantity);
                return false; 
            
            }
            
            return true;

            

        }

        public async Task<bool> DeleteOrderItem(Guid orderId, Guid id)
        {

            var orderItem = await _getRepository.GetByIdAsync(id);

            if (orderItem is null || orderItem.OrderId != orderId)
            {
                return false;
            }

            return await _deleteRepository.DeleteAsync(orderItem);



        }

    }
}
