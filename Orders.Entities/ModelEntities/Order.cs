using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Entities.ModelEntities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public string? OrderNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string? CustomerName { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Range(0, double.MaxValue)]
        public double TotalAmount { get; set; }

        public virtual ICollection<OrderItem>? Items { get; set; }
    }
}
