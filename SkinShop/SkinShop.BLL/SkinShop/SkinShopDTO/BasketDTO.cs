using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.DAL.Interfaces;
using SkinShop.DAL.SkinShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.SkinShopDTO
{
    public class BasketDTO: ForIsDeletedDTO, IForId
    {
        public int Id { get; set; }

        public virtual ICollection<SkinDTO> Skins { get; set; }
    }
}
