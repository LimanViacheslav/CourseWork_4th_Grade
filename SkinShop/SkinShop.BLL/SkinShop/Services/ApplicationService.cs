using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.SkinShop.BusinessModels;
using SkinShop.BLL.SkinShop.Interfaces;
using SkinShop.BLL.SkinShop.Mappers;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using SkinShop.DAL.Identity.Entities;
using SkinShop.DAL.Identity.Interfaces;
using SkinShop.DAL.Identity.Repositories;
using SkinShop.DAL.SkinShop.Entities;
using SkinShop.DAL.SkinShop.Interfaces;
using SkinShop.DAL.SkinShop.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.Services
{
    public class ApplicationService: IService
    {
        IUnitOfWorkSkinShop Database { get; set; }
        MappersForDTO _mappers = new MappersForDTO();
        UnitOfWorkIdentity EntityDB = new UnitOfWorkIdentity("DefaultConnection");

        public ApplicationService(IUnitOfWorkSkinShop uow)
        {
            Database = uow;
        }

        public OperationDetails MakeOrder(List<SkinDTO> skins, List<int> counts, int? id)
        {
            Order _order = new Order();

            double _price = 0;
            foreach(var i in skins)
            {
                if(i.Sale>0)
                {
                    _price += Discount.GetDiscountedPrice(i.Price, i.Sale);
                }
                else
                {
                    _price += i.Price;
                }
            }

            if (id != null)
            {
                ClientProfile _client = GetClient(Convert.ToInt32(id));
                _order.Client = _client;
                _order.ClientId =Convert.ToInt32(_client.Id);
            }

            _order.OrderTime = DateTime.Now;
            _order.Price = _price;
            _order.Status = OrderStatus.NotConfirmed;

            if (skins != null && counts != null)
            {
                for(int i = 0; i < skins.Count; i++)
                {
                    _order.OrderCounts.Add(new OrderCount() { Skin = _mappers.ToSkin.Map<SkinDTO, Skin>(skins[i]), Count = counts[i] });
                }
            }
            else
            {
                return new OperationDetails(false, "Не удалось найти скины для заказа", this.ToString());
            }
            Database.Orders.Add(_order);
            return new OperationDetails(true, "Заказ успешно оформлен", this.ToString());
        }

        public OperationDetails ConfirmOrder(int? orderId, int? employeeId)
        {
            if (orderId != null)
            {
                Order _order = Database.Orders.Get(Convert.ToInt32(orderId));

                if(employeeId != null)
                {
                    User _employee = GetUser(Convert.ToInt32(employeeId));
                    _order.Employee = _employee;
                    _order.EmployeeId = Convert.ToInt32(_employee.Id);
                    _order.Status = OrderStatus.Confirmed;
                }
                else
                {
                    return new OperationDetails(false, "Не удалось найти сотрудника", this.ToString());
                }
                Database.Orders.Update(_order);
                return new OperationDetails(true, "Заказ успешно подтверждён", this.ToString());
            }
            else
            {
                return new OperationDetails(false, "Не удалось найти заказ", this.ToString());
            }
        }

        public OperationDetails AddToFavorites(SkinDTO skin, int? id)
        {
            if(id != null)
            {
                ClientProfile _client = GetClient(Convert.ToInt32(id));
                IEnumerable<Favorites> _result = from f in Database.Favorites.Show()
                          where f.Id == _client.Favorites.Id
                          select f;
                if(_client == null)
                {
                    return new OperationDetails(false, "Не удалось найти клиента", this.ToString());
                }
                Favorites _favorites;
                if (_result != null)
                {
                    _favorites = _result.FirstOrDefault();
                    _favorites.FavoritesSkins.Add(_mappers.ToSkin.Map<SkinDTO, Skin>(skin));
                }
                else
                {
                    _favorites = new Favorites();
                    _favorites.FavoritesSkins.Add(_mappers.ToSkin.Map<SkinDTO, Skin>(skin));
                }
                return new OperationDetails(true, "Скин успешно добавлен в избранное", this.ToString());
            }
            else
            {
                return new OperationDetails(false, "Не удалось найти клиента", this.ToString());
            }
          
        }

        public OperationDetails DeleteFromFavorites(SkinDTO skin, int? id)
        {
            if (id != null)
            {
                ClientProfile _client = GetClient(Convert.ToInt32(id));
                IEnumerable<Favorites> _result = from f in Database.Favorites.Show()
                                                 where f.Id == _client.Favorites.Id
                                                 select f;
                if (_result != null)
                {
                    _result.FirstOrDefault().FavoritesSkins.Remove(_mappers.ToSkin.Map<SkinDTO, Skin>(skin));
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

        public OperationDetails AddToBasket(SkinDTO skin, int? id)
        {
            if (id != null)
            {
                ClientProfile _client = GetClient(Convert.ToInt32(id));
                IEnumerable<Basket> _result = from f in Database.Baskets.Show()
                                                 where f.Id == _client.Basket.Id
                                                 select f;
                if (_client == null)
                {
                    return new OperationDetails(false, "Не удалось найти клиента", this.ToString());
                }
                Basket _basket;
                if (_result != null)
                {
                    _basket = _result.FirstOrDefault();
                    _basket.Skins.Add(_mappers.ToSkin.Map<SkinDTO, Skin>(skin));
                }
                else
                {
                    _basket = new Basket();
                    _basket.Skins.Add(_mappers.ToSkin.Map<SkinDTO, Skin>(skin));
                }
                return new OperationDetails(true, "Скин успешно добавлен в корзину", this.ToString());
            }
            else
            {
                return new OperationDetails(false, "Не удалось найти клиента", this.ToString());
            }
        }

        public OperationDetails DeleteFromBasket(SkinDTO skin, int? id)
        {
            if (id != null)
            {
                ClientProfile _client = GetClient(Convert.ToInt32(id));
                IEnumerable<Basket> _result = from f in Database.Baskets.Show()
                                                 where f.Id == _client.Basket.Id
                                                 select f;
                if (_result != null)
                {
                    _result.FirstOrDefault().Skins.Remove(_mappers.ToSkin.Map<SkinDTO, Skin>(skin));
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

        public User GetUser(int id)
        {
                User item = EntityDB.ClientManager.GetUser(id);
                return item;
        }

        public ClientProfile GetClient(int id)
        {
            ClientProfile item = EntityDB.ClientManager.GetClient(id);
            return item;
        }

        public bool Pay()
        {
            return true;
        }
    }
}
