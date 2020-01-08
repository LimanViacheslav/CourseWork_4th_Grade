using SkinShop.BLL.Identity.IdentityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.SkinShopDTO
{
    public class FavoritesDTO: ForIsDeletedDTO
    {
        public int Id { get; set; }

        public virtual ICollection<SkinDTO> FavoritesSkins { get; set; }
    }
}
