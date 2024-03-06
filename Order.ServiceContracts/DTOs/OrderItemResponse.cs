using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Entities.ModelEntities
{
    public class OrderItemResponse
    {
        public Guid OrderItemId { get; set; }

        public Guid OrderId { get; set; }

        public string? ProductName { get; set; }

        public double Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double TotalPrice { get; set; }

    }

    public static class OrderItemExtensions
    {
        public static OrderItemResponse ToResponse(this OrderItem item)
        {
            return new OrderItemResponse()
            {
                OrderId = item.OrderId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice,
                OrderItemId = item.OrderItemId
            };
        }
    }
}
