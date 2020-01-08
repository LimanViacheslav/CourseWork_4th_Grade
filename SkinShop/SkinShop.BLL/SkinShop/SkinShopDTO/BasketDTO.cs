using SkinShop.BLL.Identity.IdentityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.SkinShopDTO
{
    public class BasketDTO: ForIsDeletedDTO
    {
        public int Id { get; set; }

        public virtual ICollection<SkinDTO> Skins { get; set; }
    }
}
