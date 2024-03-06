using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Entities.ModelEntities;

namespace Orders.ServiceContracts.DTOs
{
    public class OrderAddRequest
    {
        [Required(ErrorMessage = "{0} field must be provided.")]
        [StringLength(50, ErrorMessage = "{0} field should be at maximum 50 characters.")]
        public string? CustomerName { get; set; }

        public Order ToOrder()
        {
            var orderId = Guid.NewGuid();
            return new Order()
            {
                CustomerName = CustomerName,
                OrderDate = DateTime.Now,
                OrderId = orderId,
                OrderNumber = $"Order_{CustomerName}_{orderId}",
                TotalAmount = 0
                
            };
        }
    }


}
