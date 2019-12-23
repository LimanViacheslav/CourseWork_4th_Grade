using SkinShop.BLL.SkinShop.SkinShopDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.Identity.IdentityDTO
{
    public class ClientProfileDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public virtual ICollection<OrderDTO> Orders { get; set; }

        public virtual BasketDTO Basket { get; set; }

        public virtual FavoritesDTO Favorites { get; set; }

        public virtual UserDTO User { get; set; }
    }
}
