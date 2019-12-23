using SkinShop.BLL.SkinShop.Services;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using SkinShop.Mappers;
using SkinShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkinShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController: Controller
    {
        Service _service = new Service();
        MappersForDM _mappers = new MappersForDM();

        public ActionResult CreateSkin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSkin(SkinCreateVM item)
        {
            if(ModelState.IsValid)
            {
                SkinDTO skin = new SkinDTO() { Name = item.Name, Price = item.Price, Sale = item.Sale};

                if(item.Image != null)
                {
                    ImageDTO image = new ImageDTO();

                    image.Text = item.Alt;
                    using (var reader = new BinaryReader(item.Image.InputStream))
                        image.Photo = reader.ReadBytes(item.Image.ContentLength);

                    skin.Images = new List<ImageDTO>() { image};
                }

                //if(item.Game != null)
                //{
                //    GameDTO game = new GameDTO() { Name = item.Game.Name, IsThingGame = item.Game.IsThingGame, GameURL = item.Game.GameURL, Genre = item.Game.Genre, Type = item.Game.Type };

                //    if(item.Game.Image != null)
                //    {
                //        ImageDTO image = new ImageDTO();

                //        image.Text = item.Game.Alt;
                //        using (var reader = new BinaryReader(item.Game.Image.InputStream))
                //            image.Photo = reader.ReadBytes(item.Game.Image.ContentLength);

                //        game.Images = new List<ImageDTO>() { image };
                //    }

                //    skin.Game = game;
                //}
                skin.Game = new GameDTO() { Name = "Fortnite" };
                if(item.SkinRarity != "")
                {
                    SkinRarityDTO skinRarity = new SkinRarityDTO() { Color = item.SkinRarityColor, RarityName = item.SkinRarity };
                    skin.SkinRarity = skinRarity;
                }

                if (item.SkinType != "")
                {
                    SkinTypeDTO skinType = new SkinTypeDTO() { TypeName = item.SkinType };
                    skin.SkinType = skinType;
                }

                if (item.Description != "")
                    skin.Description = item.Description;

                _service.ServiceForCRUD.CreateSkin(skin);
                return RedirectToAction("Message", new { message = "Скин успешно создан" });
            }
            else
            {
                return View();
            }
        }

        public ActionResult Message(string message)
        {
            ViewBag.Message = message;
            return View();
        }

    }
}