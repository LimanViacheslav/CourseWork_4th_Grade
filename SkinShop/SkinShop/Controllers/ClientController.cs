using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.SkinShop.Services;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using SkinShop.Filters;
using SkinShop.Mappers;
using SkinShop.Models.Account;
using SkinShop.Models.SkinShop;
using SkinShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkinShop.Controllers
{
    [BanAttribute]
    public class ClientController : Controller
    {
        Service _service = new Service();
        MappersForDM _mappers = new MappersForDM();

        public ActionResult Favorites()
        {
            string clientName = System.Web.HttpContext.Current.User.Identity.Name;
            FavoritesDM favorites = _mappers.ToFavoritesDM.Map<FavoritesDTO, FavoritesDM>(_service.StoreService.GetFavorites(clientName));
            return View(favorites);
        }

        public ActionResult AddToBasket(int? skinid)
        {
            if (skinid == null)
                return RedirectToAction("PageNotFound");
            _service.StoreService.AddToBasket(Convert.ToInt32(skinid), System.Web.HttpContext.Current.User.Identity.Name);
            return RedirectToAction("Basket");
        }

        public ActionResult AddToFavorite(int? skinid)
        {
            if (skinid == null)
                return RedirectToAction("PageNotFound");
            _service.StoreService.AddToFavorites(Convert.ToInt32(skinid), System.Web.HttpContext.Current.User.Identity.Name);
            return RedirectToAction("Favorites");
        }

        public ActionResult DeleteFromBasket(int? skinid)
        {
            if (skinid == null)
                return RedirectToAction("PageNotFound");
            _service.StoreService.DeleteFromBasket(Convert.ToInt32(skinid), System.Web.HttpContext.Current.User.Identity.Name);
            return RedirectToAction("Basket");
        }

        public ActionResult DeleteFromFavorite(int? skinid)
        {
            if (skinid == null)
                return RedirectToAction("PageNotFound");
            _service.StoreService.DeleteFromFavorites(Convert.ToInt32(skinid), System.Web.HttpContext.Current.User.Identity.Name);
            return RedirectToAction("Favorites");
        }

        [HttpGet]
        public ActionResult Orders()
        {
            OrdersVM reslt = new OrdersVM();
            IEnumerable<OrderDM> orders;
            if (User.IsInRole("manager"))
            {
                orders = _mappers.ToOrderDM.Map<IEnumerable<OrderDTO>, IEnumerable<OrderDM>>(_service.StoreService.GetOrdersForEmployee());
            }
            else
            {
                orders = _mappers.ToOrderDM.Map<IEnumerable<OrderDTO>, IEnumerable<OrderDM>>(_service.StoreService.GetOrders(User.Identity.Name));
            }
            reslt.Orders = orders.ToList();
            reslt.MinPrice = Convert.ToInt32((from t in orders
                                              orderby t.Price ascending
                                              select t.Price).First());
            reslt.MaxPrice = Convert.ToInt32((from t in orders
                                              orderby t.Price descending
                                              select t.Price).First());
            return View(reslt);
        }
        
        public ActionResult OrderFilters(OrderStatusDM[] status = null, string userName = "", string minPrice = "", string maxPrice = "", string order = "")
        {
            OrdersVM reslt = new OrdersVM();
            IEnumerable<OrderDM> orders;
            if (User.IsInRole("manager"))
            {
                orders = _mappers.ToOrderDM.Map<IEnumerable<OrderDTO>, IEnumerable<OrderDM>>(_service.StoreService.GetOrdersForEmployee());
            }
            else
            {
                orders = _mappers.ToOrderDM.Map<IEnumerable<OrderDTO>, IEnumerable<OrderDM>>(_service.StoreService.GetOrders(User.Identity.Name));
            }
            if (userName != "")
            {
                orders = from t in orders
                         where t.Client.Name.Contains(userName)
                         select t;
            }

            if (status != null)
            {
                List<OrderDM> _orders = new List<OrderDM>();
                foreach (var i in status)
                {
                    List<OrderDM> localOrder;
                    localOrder = (from t in orders
                             where t.Status == i
                             select t).ToList();
                    _orders.AddRange(localOrder);
                }
                orders = _orders;
            }

            if (minPrice != "" && maxPrice != "")
            {
                int min = int.Parse(minPrice);
                int max = int.Parse(maxPrice);
                orders = from t in orders
                         where t.Price >= min && t.Price <= max
                         select t;
            }
            else if (minPrice != "")
            {
                int min = int.Parse(minPrice);
                orders = from t in orders
                         where t.Price >= min
                         select t;
            }
            else if (maxPrice != "")
            {
                int max = int.Parse(maxPrice);
                orders = from t in orders
                         where t.Price <= max
                         select t;
            }

            if (order != "")
            {
                switch (order)
                {
                    case "По алфавиту А-Я (клиент)":
                        orders = from t in orders
                                 orderby t.Client.Name ascending
                                 select t;
                        break;
                    case "По алфавиту Я-А (клиент)":
                        orders = from t in orders
                                 orderby t.Client.Name descending
                                 select t;
                        break;
                    case "По увеличению стоимости":
                        orders = from t in orders
                                 orderby t.Price ascending
                                 select t;
                        break;
                    case "По уменьшению стоимости":
                        orders = from t in orders
                                 orderby t.Price descending
                                 select t;
                        break;
                    case "По увеличению даты":
                        orders = from t in orders
                                 orderby t.OrderTime ascending
                                 select t;
                        break;
                    case "По убыванию даты":
                        orders = from t in orders
                                 orderby t.OrderTime descending
                                 select t;
                        break;
                }
            }

            reslt.Orders = orders.ToList();
            return PartialView(reslt);
        }

        [HttpGet]
        public ActionResult Basket()
        {
            string client = System.Web.HttpContext.Current.User.Identity.Name;
            BasketDM basket = _mappers.ToBasketDM.Map<BasketDTO, BasketDM>(_service.StoreService.GetBasket(client));
            return View(basket);
        }

        [HttpPost]
        public ActionResult Basket(BasketDM basket, List<int> counts)
        {
            List<int> skinsId = new List<int>();
            string client = System.Web.HttpContext.Current.User.Identity.Name;
            BasketDM Basket = _mappers.ToBasketDM.Map<BasketDTO, BasketDM>(_service.StoreService.GetBasket(client));

            if (Basket.Id == basket.Id)
            {
                foreach (var i in Basket.Skins)
                {
                    skinsId.Add(i.Id);
                }
            }
            OperationDetails result = _service.StoreService.MakeOrder(skinsId, counts, client);
            if(result.Succedeed)
            {
                foreach(var i in skinsId)
                {
                    _service.StoreService.DeleteFromBasket(i, client);
                }
                return RedirectToAction("Orders");
            }
            ViewBag.Result = result.Message;
            ViewBag.Status = result.Succedeed;
            return View(basket);
        }

        public ActionResult FromFavoritesToBasket(int? skinId)
        {
            if (skinId == null)
                return RedirectToAction("PageNotFound");
            string userName = User.Identity.Name;
            _service.StoreService.AddToBasket(Convert.ToInt32(skinId), userName);
            _service.StoreService.DeleteFromFavorites(Convert.ToInt32(skinId), userName);
            return RedirectToAction("Basket");
        }

        public ActionResult ConfirmOrder(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            _service.StoreService.ConfirmOrder(id, User.Identity.Name);
            return RedirectToAction("Orders");
        }

        public ActionResult Reject(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            _service.StoreService.Reject(id, User.Identity.Name);
            return RedirectToAction("Orders");
        }
        
    }
}