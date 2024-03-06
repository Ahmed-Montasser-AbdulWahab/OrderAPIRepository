using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Entities.ModelEntities;
using Orders.ServiceContracts;
using Orders.ServiceContracts.DTOs;

namespace Orders.WebAPI.Controllers
{
    public class OrdersController : CustomController
    {
        private readonly IOrdersService _ordersService;
        private readonly IOrderItemsService _orderItemsService;

        public OrdersController(IOrdersService ordersService, IOrderItemsService orderItemsService)
        {
            _ordersService = ordersService;
            _orderItemsService = orderItemsService;
        }

        /// <summary>
        /// An Endpoint to get all Orders without including its items.
        /// </summary>
        /// <returns>List of Orders in the form of OrderResponses.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders()
        {
            var orders = await _ordersService.GetOrders();

            return (orders is null || !orders.Any()) ? NotFound() : orders;
        }

        [HttpGet("items")]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersWithItems()
        {
            var orders = await _ordersService.GetOrders(true);

            return (orders is null || !orders.Any()) ? NotFound() : orders;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(Guid id)
        {
            var order = await _ordersService.GetOrderById(id);

            return (order is null) ? NotFound() : order;
        }

        [HttpGet("{id:guid}/items")]
        public async Task<ActionResult<OrderResponse>> GetOrderWithItems(Guid id)
        {
            var order = await _ordersService.GetOrderById(id, true);

            return (order is null) ? NotFound() : order;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderAddRequest request)
        {
            var response = await _ordersService.AddOrder(request);

            return (response is null) ? Problem(detail: "You may added the order recently.", statusCode: 400, title: "Add Order Operation")
                : CreatedAtAction(nameof(GetOrder), new { id = response.OrderId }, response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateOrder(OrderUpdateRequest request, Guid id)
        {
            return (await _ordersService.UpdateOrder(request, id)) ? NoContent() :
                Problem(detail: $"Order with {id} may not exist OR Error in Update Operation",
                statusCode: 400, title: "Update Order Operation");
              
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            return (await _ordersService.DeleteOrder(id)) ? NoContent() :
                Problem(detail: $"Order with {id} may not exist OR Error in Delete Operation",
                statusCode: 400, title: "Delete Order Operation");

        }

        [HttpGet("{orderId:guid}/items/{id:guid}")]
        public async Task<ActionResult<OrderItemResponse>> GetOrderItemInOrder(Guid orderId, Guid id)
        {
            var order = await _ordersService.GetOrderById(orderId, true);

            if (order is null) return Problem(detail: $"Order with ID:{orderId} does not exist",
                statusCode: 400, title: $"Get OrderItem {id} in Order {orderId} Operation");

            if(order.Items is null || !order.Items.Any()) return Problem(detail: $"No items in Order with ID:{orderId}",
                statusCode: 400, title: $"Get OrderItem {id} in Order {orderId} Operation");

            var orderItem = order.Items.FirstOrDefault(item => item.OrderItemId == id);

            return (orderItem is null) ? NotFound() : orderItem;
        }

        [HttpPost("{orderId:guid}/items")]
        public async Task<ActionResult<OrderItemResponse>> PostOrderItemInOrder([FromRoute] Guid orderId, [FromBody] OrderItemAddRequest orderItemAddRequest)
        {
            if(orderId != orderItemAddRequest.OrderId)
            {
                return Problem(detail: $"Provided OrderIDs do not match.",
                statusCode: 400, title: $"Post OrderItem in Order {orderId} Operation");
            }
            var order = await _ordersService.GetOrderById(orderId, true);

            if (order is null) return Problem(detail: $"Order with ID:{orderId} does not exist",
                statusCode: 400, title: $"Post OrderItem in Order {orderId} Operation");

            var response = await _orderItemsService.AddOrderItem(orderItemAddRequest);

            return (response is null) ? Problem(detail: $"Order Item failed to be added",
                statusCode: 400, title: $"Post OrderItem in Order {orderId} Operation") :
                CreatedAtAction(nameof(GetOrderItemInOrder), new { orderId=response.OrderId, id=response.OrderItemId }, response);

        }

        [HttpPut("{orderId:guid}/items/{id:guid}")]
        public async Task<IActionResult> UpdateOrderItemInOrder([FromRoute] Guid orderId, [FromRoute] Guid id, [FromBody] OrderItemUpdateRequest orderItemUpdateRequest)
        {
            return (await _orderItemsService.UpdateOrderItem(orderId, id, orderItemUpdateRequest))? NoContent() :
                Problem(detail: $"Order Item failed to be updated",
                statusCode: 400, title: $"Update OrderItem in Order {orderId} Operation")
                ;
            

        }
        [HttpDelete("{orderId:guid}/items/{id:guid}")]
        public async Task<IActionResult> UpdateOrderItemInOrder([FromRoute] Guid orderId, [FromRoute] Guid id)
        {
            return (await _orderItemsService.DeleteOrderItem(orderId, id)) ? NoContent() :
                Problem(detail: $"Order Item failed to be updated",
                statusCode: 400, title: $"Update OrderItem in Order {orderId} Operation")
                ;


        }

    }
}
