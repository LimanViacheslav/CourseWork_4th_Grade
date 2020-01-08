using SkinShop.BLL.Identity.Infrastructure;
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

                if (item.Image != null)
                {
                    ImageDTO image = new ImageDTO();

                    image.Text = item.Alt;
                    using (var reader = new BinaryReader(item.Image.InputStream))
                        image.Photo = reader.ReadBytes(item.Image.ContentLength);

                    skin.Images = new List<ImageDTO>();
                    skin.Images.Add(image);
               }
                if(item.Game != "")
                    skin.Game = new GameDTO() { Name = item.Game };

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

                OperationDetails result = _service.ServiceForCRUD.CreateSkin(skin);
                ViewBag.Result = result.Message;
                ViewBag.Status = result.Succedeed;
                return View();
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