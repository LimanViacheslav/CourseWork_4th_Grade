﻿using System.Collections.Generic;
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

namespace SkinShop.Controllers
{
    public class HomeController : Controller
    {
        Service _service = new Service();
        MappersForDM _mappers = new MappersForDM();

        //string _user = System.Web.HttpContext.Current.User.Identity.Name;
        //Page lol1 = new Page();
        //var hhh = User.Identity.GetUserId();


        //SkinDTO skin = new SkinDTO();
        //skin.Game = _service.Games.Get(Tables.Games, 1);
        //skin.SkinRarity = _service.SkinRareties.Get(Tables.SkinRarities, 1);
        //skin.SkinType = _service.SkinTypes.Get(Tables.SkinTypes, 1);
        //_service.Skins.Create(skin);


        //var lol = lol1.User.Identity;

        public ActionResult Index()
        {
            ICollection<SkinDM> skins = _mappers.ToSkinDM.Map<ICollection<SkinDTO>, ICollection<SkinDM>>(_service.ServiceForCRUD.GetSkins());
            return View(skins);
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
                    return View(skin);
                }
            }
            return RedirectToAction("PageNotFound");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ICollection<SkinRarityDTO> rar = _service.ServiceForCRUD.GetRarity();
            return View(rar);
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