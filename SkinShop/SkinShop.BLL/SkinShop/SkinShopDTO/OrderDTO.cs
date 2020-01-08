using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.DL.Entities.SkinShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.SkinShopDTO
{
    public class OrderDTO: ForIsDeletedDTO
    {
        public int Id { get; set; }

        public virtual ClientProfileDTO Client { get; set; }

        public virtual ICollection<OrderCountDTO> OrderCounts { get; set; }

        public virtual UserDTO Employee { get; set; }

        public DateTime OrderTime { get; set; }

        public OrderStatus Status { get; set; }

        public double  Price { get; set; }
    }
}
