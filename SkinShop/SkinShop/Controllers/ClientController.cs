using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.SkinShop.Services;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using SkinShop.Mappers;
using SkinShop.Models.Account;
using SkinShop.Models.SkinShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkinShop.Controllers
{
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
            IEnumerable<OrderDM> orders;
            if (User.IsInRole("manager"))
            {
                orders = _mappers.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(_service.StoreService.GetOrdersForEmployee());
            }
            else
            {
                orders = _mappers.ToOrderDM.Map<IEnumerable<OrderDTO>, IEnumerable<OrderDM>>(_service.StoreService.GetOrders(User.Identity.Name));
            }
            return View(orders);
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
                foreach(var i in basket.Skins)
                {
                    _service.StoreService.DeleteFromBasket(i.Id);
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
    }
}