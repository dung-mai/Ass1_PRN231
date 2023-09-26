using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            OrderDetailDTOs = new HashSet<OrderDetailDTO>();
        }
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public decimal Freight { get; set; }

        public virtual Member Member { get; set; } = null!;

        public virtual ICollection<OrderDetailDTO> OrderDetailDTOs { get; set; }

    }
}
