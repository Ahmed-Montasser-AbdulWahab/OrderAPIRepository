using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Entities.ModelEntities
{
    public class OrderItemAddRequest
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid OrderId { get; set; }


        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "{0} is maximum {1} characters.")]
        public string? ProductName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "{0} can't be negative value.")]
        public double Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "{0} can't be negative value.")]
        public double UnitPrice { get; set; }


        public OrderItem ToOrderItem()
        {
            return new OrderItem()
            {
                OrderId = OrderId,
                OrderItemId = Guid.NewGuid(),
                ProductName = ProductName,
                Quantity = Quantity,
                UnitPrice = UnitPrice,
                TotalPrice = Quantity * UnitPrice
            };
        }
    }
}
