using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Entities.ModelEntities;

namespace Orders.ServiceContracts.DTOs
{
    public class OrderUpdateRequest
    {
        [Required(ErrorMessage = "{0} field must be provided.")]
        [StringLength(50, ErrorMessage = "{0} field should be at maximum 50 characters.")]
        public string? CustomerName { get; set; }
    }


}
