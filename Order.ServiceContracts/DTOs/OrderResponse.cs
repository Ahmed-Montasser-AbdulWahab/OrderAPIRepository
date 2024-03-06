using Orders.Entities.ModelEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.ServiceContracts.DTOs
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }

        public string? OrderNumber { get; set; }

        public string? CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public double TotalAmount { get; set; }

        public List<OrderItemResponse>? Items { get; set; }
    }

    public static class OrderExtensions
    {
        public static OrderResponse ToResponse(this Order order)
        {
            var response =  new OrderResponse
            {
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount
            };

            if (order.Items != null )
            {
                response.Items = order.Items.Select(i => i.ToResponse()).ToList();
            }

            return response;
        }
    }
}
