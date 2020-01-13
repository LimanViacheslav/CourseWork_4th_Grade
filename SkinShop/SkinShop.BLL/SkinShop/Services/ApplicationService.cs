using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.SkinShop.BusinessModels;
using SkinShop.BLL.SkinShop.Interfaces;
using SkinShop.BLL.SkinShop.Mappers;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using SkinShop.DL.Entities.Identity;
using SkinShop.DL.Entities.SkinShop;
using SkinShop.DL.Interfaces.SkinShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.Services
{
    public class ApplicationService: IService
    {
        IUnitOfWorK Database { get; set; }
        MappersForDTO _mappers = new MappersForDTO();

        public ApplicationService(IUnitOfWorK uow)
        {
            Database = uow;
        }   

        public OperationDetails MakeOrder(List<int> skinsId, List<int> counts, string clientName = "")
        {
            Order _order = new Order();

            double _price = 0;
            List<SkinDTO> skins = new List<SkinDTO>();
            foreach(var i in skinsId)
            {
                skins.Add(_mappers.ToSkinDTO.Map<Skin, SkinDTO>(Database.Skins.Get(i)));
            }

            for(int i=0;i<skins.Count;i++)
            {
                if(skins[i].Sale>0)
                {
                    _price += Discount.GetDiscountedPrice(skins[i].Price, skins[i].Sale) * counts[i];
                }
                else
                {
                    _price += skins[i].Price * counts[i];
                }
            }

            if (clientName != "")
            {
                ClientProfile _client = GetClient(clientName);
                _order.Client = _client;
                _order.ClientId =_client.Id;
            }

            _order.OrderTime = DateTime.Now;
            _order.Price = _price;
            _order.Status = OrderStatus.NotConfirmed;

            if (skins != null && counts != null)
            {
                _order.OrderCounts = new List<OrderCount>() { };
                for(int i = 0; i < skins.Count; i++)
                {
                    _order.OrderCounts.Add(new OrderCount() { Skin = Database.Skins.Get(skins[i].Id), Count = counts[i] });
                }
            }
            else
            {
                return new OperationDetails(false, "Не удалось найти скины для заказа", this.ToString());
            }
            Database.Orders.Add(_order);
            Database.Save();
            return new OperationDetails(true, "Заказ успешно оформлен", this.ToString());
        }

        public OperationDetails ConfirmOrder(int? orderId, string employeeName)
        {
            if (orderId != null)
            {
                Order _order = Database.Orders.Get(Convert.ToInt32(orderId));

                if(employeeName != null && _order.Employee==null)
                {
                    User _employee = GetUser(employeeName);
                    _order.Employee = _employee;
                    _order.EmployeeId = _employee.Id;
                }
                else
                {
                    return new OperationDetails(false, "Не удалось найти сотрудника", this.ToString());
                }
                _order.Status = OrderStatus.Confirmed;
                Database.Orders.Update(_order);
                Database.Save();
                return new OperationDetails(true, "Заказ успешно подтверждён", this.ToString());
            }
            else
            {
                return new OperationDetails(false, "Не удалось найти заказ", this.ToString());
            }
        }

        
        public OperationDetails Reject(int? orderId, string employeeName)
        {
            if (orderId != null)
            {
                Order _order = Database.Orders.Get(Convert.ToInt32(orderId));

                if (employeeName != null && _order.Employee == null)
                {
                    User _employee = GetUser(employeeName);
                    _order.Employee = _employee;
                    _order.EmployeeId = _employee.Id;
                }
                else
                {
                    return new OperationDetails(false, "Не удалось найти сотрудника", this.ToString());
                }
                _order.Status = OrderStatus.Rejected;
                Database.Orders.Update(_order);
                Database.Save();
                return new OperationDetails(true, "Заказ успешно подтверждён", this.ToString());
            }
            else
            {
                return new OperationDetails(false, "Не удалось найти заказ", this.ToString());
            }
        }

        public OperationDetails AddToFavorites(int skinid, string clientName = "")
        {
            if(clientName != "")
            {
                ClientProfile _client = GetClient(clientName);
                IEnumerable<Favorites> _result = from f in Database.Favorites.Show()
                          where f.Id == _client.Favorites.Id
                          select f;
                if(_client == null)
                {
                    return new OperationDetails(false, "Не удалось найти клиента", this.ToString());
                }
                Favorites _favorites;
                Skin skin = Database.Skins.Get(skinid);

                if(skin == null)
                    return new OperationDetails(false, "Не удалось найти скин", this.ToString());
                if (_result != null)
                {
                    _favorites = _result.FirstOrDefault();
                    _favorites.FavoritesSkins.Add(skin);
                    Database.Favorites.Update(_favorites);
                }
                else
                {
                    _favorites = new Favorites();
                    _favorites.FavoritesSkins.Add(skin);
                    Database.Favorites.Add(_favorites);
                }
                Database.Save();
                return new OperationDetails(true, "Скин успешно добавлен в избранное", this.ToString());
            }
            else
            {
                return new OperationDetails(false, "Не удалось найти клиента", this.ToString());
            }
          
        }

        public OperationDetails DeleteFromFavorites(int skinid, string clientName = "")
        {
            if (clientName != "")
            {
                ClientProfile _client = GetClient(clientName);
                IEnumerable<Favorites> _result = from f in Database.Favorites.Show()
                                                 where f.Id == _client.Favorites.Id
                                                 select f;
                if (_result != null)
                {
                    Skin skin = Database.Skins.Get(skinid);
                    if (skin == null)
                        return new OperationDetails(false, "Не удалось найти скин", this.ToString());
                    _result.FirstOrDefault().FavoritesSkins.Remove(skin);
                    Database.Favorites.Update(_result.FirstOrDefault());
                    Database.Save();
                }
                else
                {
                    return new OperationDetails(false, "Не удалось найти информацию об избранном пользователя", this.ToString());
                }
                return new OperationDetails(true, "Скин успешно удалён из избранного", this.ToString());
            }
            else
            {
                return new OperationDetails(false, "Не удалось найти клиента", this.ToString());
            }
        }

        public FavoritesDTO GetFavorites(string clientName)
        {
            ClientProfile user = GetClient(clientName);
            return _mappers.ToFavoritesDTO.Map<Favorites, FavoritesDTO>(Database.Favorites.Find(x => x.Id == user.BasketId).FirstOrDefault());
        }

        public BasketDTO GetBasket(string clientName)
        {
            ClientProfile user = GetClient(clientName);
            return _mappers.ToBasketDTO.Map<Basket, BasketDTO>(Database.Baskets.Find(x => x.Id == user.BasketId).FirstOrDefault());
        }

        public IEnumerable<OrderDTO> GetOrders(string clientName)
        {
            ClientProfile user = GetClient(clientName);
            return _mappers.ToOrderDTO.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(Database.Orders.Find(x => x.ClientId == user.Id));
        }

        public IEnumerable<OrderDTO> GetOrdersForEmployee()
        {
            return _mappers.ToOrderDTO.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(Database.Orders.Show());
        }

        public OperationDetails AddToBasket(int skinid, string clientName = "")
        {
            if (clientName != "")
            {
                ClientProfile _client = GetClient(clientName);
                IEnumerable<Basket> _result = from f in Database.Baskets.Show()
                                                 where f.Id == _client.Basket.Id
                                                 select f;
                if (_client == null)
                {
                    return new OperationDetails(false, "Не удалось найти клиента", this.ToString());
                }
                Basket _basket;
                Skin skin = Database.Skins.Get(skinid);

                if (skin == null)
                    return new OperationDetails(false, "Не удалось найти скин", this.ToString());
                if (_result != null)
                {
                    _basket = _result.FirstOrDefault();
                    _basket.Skins.Add(skin);
                    Database.Baskets.Update(_basket);
                }
                else
                {
                    _basket = new Basket();
                    _basket.Skins.Add(skin);
                    Database.Baskets.Add(_basket);
                }
                Database.Save();
                return new OperationDetails(true, "Скин успешно добавлен в корзину", this.ToString());
            }
            else
            {
                return new OperationDetails(false, "Не удалось найти клиента", this.ToString());
            }
        }

        public OperationDetails DeleteFromBasket(int skinid, string clientName = "")
        {
            if (clientName != "")
            {
                ClientProfile _client = GetClient(clientName);
                IEnumerable<Basket> _result = from f in Database.Baskets.Show()
                                                 where f.Id == _client.Basket.Id
                                                 select f;
                if (_result != null)
                {
                    Skin skin = Database.Skins.Get(skinid);

                    if (skin == null)
                        return new OperationDetails(false, "Не удалось найти скин", this.ToString());
                    _result.FirstOrDefault().Skins.Remove(skin);
                    Database.Baskets.Update(_result.FirstOrDefault());
                    Database.Save();
                }
                else
                {
                    return new OperationDetails(false, "Не удалось найти информацию о корзине пользователя", this.ToString());
                }
                return new OperationDetails(true, "Скин успешно удалён в корзину", this.ToString());
            }
            else
            {
                return new OperationDetails(false, "Не удалось найти клиента", this.ToString());
            }
        }

        public User GetUser(string userName)
        {
            User item = Database.ClientManager.FindUser(x => x.Email == userName);
            return item;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<UserDTO> users = _mappers.ToUserDTO.Map<IEnumerable<User>, ICollection<UserDTO>>(Database.ClientManager.GetUsers());
            foreach(var i in users)
            {
                Task<IList<string>> roles =  Database.UserManager.GetRolesAsync(i.Id);
                i.Role = roles.Result.FirstOrDefault();
            }
            users = from t in users
                    where t.Role != "admin"
                    select t;
            return users;
        }

        public UserDTO GetUserByName(string userName)
        {
            Database.Save();
            User item = Database.ClientManager.FindUser(x => x.Email == userName);
            UserDTO user = _mappers.ToUserDTO.Map<User, UserDTO>(item);
            if(item != null)
            {
                Task<IList<string>> roles = Database.UserManager.GetRolesAsync(item.Id);
                user.Role = roles.Result.FirstOrDefault();
            }
            return user;
        }

        public ClientProfile GetClient(string clientName)
        {
            ClientProfile user = Database.ClientManager.FindClient(x => x.User.Email == clientName);
            return user;
        }

        public ICollection<ClientProfileDTO> GetClients()
        {
            return _mappers.ToClientProfileDTO.Map<IEnumerable<ClientProfile>, ICollection<ClientProfileDTO>>(Database.ClientManager.GetClients());
        }

        public ClientProfileDTO GetClientDTO(string clientName)
        {
            ClientProfile user = Database.ClientManager.FindClient(x => x.User.Email == clientName);
            return _mappers.ToClientProfileDTO.Map<ClientProfile, ClientProfileDTO>(user);
        }

        public bool Pay()
        {
            return true;
        }
    }
}
