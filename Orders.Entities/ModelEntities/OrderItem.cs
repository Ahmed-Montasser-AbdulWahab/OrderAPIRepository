using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Entities.ModelEntities
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order? Order { get; set; }

        [Required]
        [StringLength(50)]
        public string? ProductName { get; set; }

        [Range(0,double.MaxValue)]
        public double Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }


        [Range(0, double.MaxValue)]
        public double TotalPrice { get; set; }

    }
}
