using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using SkinShop.BLL.SkinShop.Services;
using SkinShop.Mappers;
using SkinShop.Models.SkinShop;
using System;
using SkinShop.Models.ViewModels;
using SkinShop.Models.Account;
using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.Filters;

namespace SkinShop.Controllers
{
    [BanAttribute]
    public class HomeController : Controller
    {
        Service _service = new Service();
        MappersForDM _mappers = new MappersForDM();
        
        public ActionResult Main()
        {
            List<GameDM> games = _mappers.ToGameDM.Map<ICollection<GameDTO>, List<GameDM>>(_service.ServiceForCRUD.GetGames());
            return View(games);
        }

        public ActionResult Index(int page = 1, string gameName = "")
        {
            IEnumerable<SkinDM> skins = _mappers.ToSkinDM.Map<ICollection<SkinDTO>, ICollection<SkinDM>>(_service.ServiceForCRUD.GetSkins());
            SkinsVM result = new SkinsVM();
            result.MinPrice = Convert.ToInt32((from t in skins
                                               orderby (t.Sale != 0) ? (t.Price - (t.Price * t.Sale) / 100) : (t.Price) ascending
                                               select (t.Sale != 0) ? (t.Price - (t.Price * t.Sale) / 100) : (t.Price)).First());
            result.MaxPrice = Convert.ToInt32((from t in skins
                                               orderby (t.Sale != 0) ? (t.Price - (t.Price * t.Sale) / 100) : (t.Price) descending
                                               select (t.Sale != 0) ? (t.Price - (t.Price * t.Sale) / 100) : (t.Price)).First());
            result.Games = (from t in _mappers.ToGameDM.Map<ICollection<GameDTO>, IEnumerable<GameDM>>(_service.ServiceForCRUD.GetGames())
                           select t.Name).ToList();
            result.Types = (from t in _mappers.ToSkinTypeDM.Map<ICollection<SkinTypeDTO>, IEnumerable<SkinTypeDM>>(_service.ServiceForCRUD.GetTypes())
                            select t.TypeName).ToList();
            result.Rareties = (from t in _mappers.ToSkinRarityDM.Map<ICollection<SkinRarityDTO>, IEnumerable<SkinRaretyDM>>(_service.ServiceForCRUD.GetRarity())
                            select t.RarityName).ToList();
            int pageSize = 18;

            result.Skins = skins.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = skins.Count() };
            result.PageInfo = pageInfo;
            result.CheckedGameName = gameName;
            return View(result);
        }

        public ActionResult SkinFilters(string searchbyname = "", string order = "", string[] games = null, string[] types = null, string[] rareties = null, int page = 1, string minPrice = "", string maxPrice = "")
        {
            SkinsVM result = new SkinsVM();
            int pageSize = 18;
            result.CheckedGameName = "";
            PageInfo pageInfo;

                IEnumerable<SkinDM> skins = _mappers.ToSkinDM.Map<ICollection<SkinDTO>, ICollection<SkinDM>>(_service.ServiceForCRUD.GetSkins());
                if (searchbyname != "")
                {
                    skins = from t in skins
                            where t.Name.Contains(searchbyname)
                            select t;
                }

            if (minPrice != "" && maxPrice != "")
            {
                int min = int.Parse(minPrice);
                int max = int.Parse(maxPrice);
                skins = from t in skins
                        where (t.Sale!=0)?(t.Price - (t.Price * t.Sale)/100 >= min && t.Price - (t.Price * t.Sale) / 100 <= max) : (t.Price >= min && t.Price <= max)
                        select t;
            }
            else if (minPrice != "")
            {
                int min = int.Parse(minPrice);
                skins = from t in skins
                        where (t.Sale != 0) ? (t.Price - (t.Price * t.Sale) / 100 >= min) : (t.Price >= min)
                        select t;
            }
            else if (maxPrice != "")
            {
                int max = int.Parse(maxPrice);
                skins = from t in skins
                        where (t.Sale != 0) ? (t.Price - (t.Price * t.Sale) / 100 <= max) : (t.Price <= max)
                        select t;
            }

            if (games != null)
                {
                    List<SkinDM> _skins = new List<SkinDM>();
                    foreach (var i in games)
                    {
                        List<SkinDM> localSkin;
                        localSkin = (from t in skins
                                     where t.Game.Name == i
                                     select t).ToList();
                        _skins.AddRange(localSkin);
                    }
                    skins = _skins;
                }

                if (types != null)
                {
                    List<SkinDM> _skins = new List<SkinDM>();
                    foreach (var i in types)
                    {
                        List<SkinDM> localSkin;
                        localSkin = (from t in skins
                                     where t.SkinType.TypeName == i
                                     select t).ToList();
                        _skins.AddRange(localSkin);
                    }
                    skins = _skins;
                }

                if (rareties != null)
                {
                    List<SkinDM> _skins = new List<SkinDM>();
                    foreach (var i in rareties)
                    {
                        List<SkinDM> localSkin;
                        localSkin = (from t in skins
                                     where t.SkinRarity.RarityName == i
                                     select t).ToList();
                        _skins.AddRange(localSkin);
                    }
                    skins = _skins;
                }

                if (order != "")
                {
                    switch (order)
                    {
                        case "По алфавиту А-Я":
                            skins = from t in skins
                                    orderby t.Name ascending
                                    select t;
                            break;
                        case "По алфавиту Я-А":
                            skins = from t in skins
                                    orderby t.Name descending
                                    select t;
                            break;
                        case "По увеличению стоимости":
                            skins = from t in skins
                                    orderby (t.Sale != 0) ? (t.Price - (t.Price * t.Sale) / 100) : (t.Price) ascending
                                    select t;
                            break;
                        case "По уменьшению стоимости":
                            skins = from t in skins
                                    orderby (t.Sale != 0) ? (t.Price - (t.Price * t.Sale) / 100) : (t.Price) descending
                                    select t;
                            break;
                    }
                }
                if(page > pageSize)
            {
                return RedirectToAction("PageNotFound");
            }
                result.Skins = skins.Skip((page - 1) * pageSize).Take(pageSize);
                pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = skins.Count() };
            result.PageInfo = pageInfo;
            return PartialView(result);
        }

        public ActionResult SkinDetails(int? id)
        {
            if(id!=null)
            {
                SkinDTO result = _service.ServiceForCRUD.GetSkin(id);
                if(result != null)
                {
                    SkinDetailsView skin = new SkinDetailsView();
                    skin.IsSkinAlreadyInBasket = false;
                    skin.IsSkinAlreadyInFavorites = false;
                    skin.Skin = _mappers.ToSkinDM.Map<SkinDTO, SkinDM>(result);
                    ClientProfileDM client =_mappers.ToClientProfileDM.Map<ClientProfileDTO, ClientProfileDM>(_service.StoreService.GetClientDTO(User.Identity.Name));
                    if(client != null)
                    {
                        foreach(var i in client.Favorites.FavoritesSkins)
                        {
                            if(i.Id == id)
                            {
                                skin.IsSkinAlreadyInFavorites = true;
                            }
                        }

                        foreach (var i in client.Basket.Skins)
                        {
                            if (i.Id == id)
                            {
                                skin.IsSkinAlreadyInBasket = true;
                                skin.IsSkinAlreadyInFavorites = true;
                            }
                        }
                    }

                    List<SkinDM> skins = (from t in _mappers.ToSkinDM.Map<ICollection<SkinDTO>, IEnumerable<SkinDM>>(_service.ServiceForCRUD.GetSkins())
                                                where t.Game.Name == skin.Skin.Game.Name && t.Id != skin.Skin.Id
                                                select t).ToList();
                    skin.OtherSkins = skins;
                    return View(skin);
                }
            }
            return RedirectToAction("PageNotFound");
        }

        public ActionResult GameDetails(int? id)
        {
            GameDetailsVM game = new GameDetailsVM();
            if(id != null)
            {
                GameDM result = _mappers.ToGameDM.Map<GameDTO, GameDM>(_service.ServiceForCRUD.GetGame(id));
                game.Game = result;
                List<SkinDM> skins = (from t in _mappers.ToSkinDM.Map<ICollection<SkinDTO>, ICollection<SkinDM>>(_service.ServiceForCRUD.GetSkins())
                                            where t.Game.Name == result.Name
                                            select t).ToList();
                game.Skins = skins;
                return View(game);
            }
            return RedirectToAction("PageNotFound");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}