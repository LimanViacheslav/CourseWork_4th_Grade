using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.Interfaces
{
    public interface IService
    {
        OperationDetails MakeOrder(List<SkinDTO> skins, List<int> counts, int? id);
        OperationDetails AddToFavorites(SkinDTO skin, int? id);
        OperationDetails DeleteFromFavorites(SkinDTO skin, int? id);
        OperationDetails AddToBasket(SkinDTO skin, int? id);
        OperationDetails DeleteFromBasket(SkinDTO skin ,int? id);
        OperationDetails ConfirmOrder(int? orderId, int? employeeId);
    }
}
