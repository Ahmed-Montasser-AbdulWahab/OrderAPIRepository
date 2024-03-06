using Orders.Entities.ModelEntities;
using Orders.ServiceContracts;
using Orders.ServiceContracts.DTOs;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IAddRepository<Order> _addRepository;
        private readonly IEditRepository<Order> _editRepository;
        private readonly IGetRepository<Order> _getRepository;
        private readonly IDeleteRepository<Order> _deleteRepository;

        public OrdersService(IAddRepository<Order> addRepository, IEditRepository<Order> editRepository, IGetRepository<Order> getRepository, IDeleteRepository<Order> deleteRepository)
        {
            _addRepository = addRepository;
            _editRepository = editRepository;
            _getRepository = getRepository;
            _deleteRepository = deleteRepository;
        }

        public async Task<List<OrderResponse>?> GetOrders(bool include = false)
        {
            var orders = await _getRepository.GetAllAsync(include);

            return orders?.Select(o => o.ToResponse()).ToList();
        }

        public async Task<OrderResponse?> GetOrderById(Guid id, bool include = false)
        {
            var order = await _getRepository.GetByIdAsync(id, include);

            return order?.ToResponse();
        }

        public async Task<OrderResponse?> AddOrder(OrderAddRequest request)
        {
            var orderEntity = request.ToOrder();
            if (!await _addRepository.AddAsync(orderEntity))
            {
                return null;
            }
            return orderEntity.ToResponse();
        }

        public async Task<bool> UpdateOrder(OrderUpdateRequest request, Guid id) {

            var entity = await _getRepository.GetByIdAsync(id);

            if(entity is null)
            {
                return false;
            }

            entity.CustomerName = request.CustomerName;

            return await _editRepository.EditAsync(entity);
        }
        public async Task<bool> DeleteOrder(Guid id)
        {

            var entity = await _getRepository.GetByIdAsync(id);

            if (entity is null)
            {
                return false;
            }

            return await _deleteRepository.DeleteAsync(entity);
        }
    }
}
