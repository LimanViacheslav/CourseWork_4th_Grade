using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.SkinShop.Services;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.Interfaces
{
    public interface IServiceCRUD
    {
        OperationDetails CreateSkin(SkinDTO item);
        OperationDetails UpdateSkin(SkinDTO item);
        ICollection<SkinDTO> GetSkins();
        SkinDTO GetSkin(int? id);
        ICollection<GameDTO> GetGames();
        ICollection<SkinTypeDTO> GetTypes();
        ICollection<SkinRarityDTO> GetRarity();
    }
}
