using SkinShop.BLL.Identity.IdentityDTO;
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
        OperationDetails MakeOrder(List<int> skinsId, List<int> counts, string clientName = "");
        OperationDetails AddToFavorites(int skinId, string clientName = "");
        OperationDetails DeleteFromFavorites(int skinId, string clientName = "");
        OperationDetails AddToBasket(int skinId, string clientName = "");
        OperationDetails DeleteFromBasket(int skinId ,string clientName = "");
        OperationDetails ConfirmOrder(int? orderId, string employeeName);
        FavoritesDTO GetFavorites(string clientName = "");
        BasketDTO GetBasket(string clientName = "");
        IEnumerable<OrderDTO> GetOrders(string clientName);
        ClientProfileDTO GetClientDTO(string clientName);
        IEnumerable<OrderDTO> GetOrdersForEmployee();
        ICollection<ClientProfileDTO> GetClients();
        IEnumerable<UserDTO> GetUsers();
    }
}
