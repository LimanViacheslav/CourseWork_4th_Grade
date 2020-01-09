using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.Identity.Interfaces;
using SkinShop.BLL.Identity.Services;
using SkinShop.BLL.SkinShop.Services;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using SkinShop.Mappers;
using SkinShop.Models;
using SkinShop.Models.Account;
using SkinShop.Models.SkinShop;
using SkinShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace SkinShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        Service _service = new Service();
        MappersForDM _mappers = new MappersForDM();

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult CreateSkin()
        {
            List<GameDM> games = _mappers.ToGameDM.Map<ICollection<GameDTO>, List<GameDM>>(_service.ServiceForCRUD.GetGames());
            SelectList gameNames = new SelectList(games, "Name", "Name");
            ViewData["GameNames"] = gameNames;
            return View();
        }

        [HttpPost]
        public ActionResult CreateSkin([Bind(Include = "Game")] SkinCreateVM item)
        {
            if (ModelState.IsValid)
            {
                SkinDTO skin = new SkinDTO() { Name = item.Name, Price = item.Price, Sale = item.Sale };

                if (item.Image != null)
                {
                    ImageDTO image = new ImageDTO();

                    image.Text = item.Alt;
                    using (var reader = new BinaryReader(item.Image.InputStream))
                        image.Photo = reader.ReadBytes(item.Image.ContentLength);

                    skin.Images = new List<ImageDTO>();
                    skin.Images.Add(image);
                }
                if (item.Game != "")
                    skin.Game = new GameDTO() { Name = item.Game };

                if (item.SkinRarity != "")
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

        [HttpGet]
        public ActionResult CreateGame()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGame(GameCreateVM item)
        {
            if (ModelState.IsValid)
            {
                GameDTO game = new GameDTO() { Name = item.Name, GameURL = item.GameURL, Genre = item.Genre, IsThingGame = item.IsThingGame, Type = item.Type };
                if (item.Image != null)
                {
                    ImageDTO image = new ImageDTO();

                    image.Text = item.Alt;
                    using (var reader = new BinaryReader(item.Image.InputStream))
                        image.Photo = reader.ReadBytes(item.Image.ContentLength);

                    game.Images = new List<ImageDTO>();
                    game.Images.Add(image);
                }
                OperationDetails result = _service.ServiceForCRUD.CreateGame(game);
                ViewBag.Result = result.Message;
                ViewBag.Status = result.Succedeed;
                return View();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateEmployee(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "manager",
                    PhoneNumber = model.PhoneNumber
                };
                OperationDetails result = await UserService.CreateEmployee(userDto);
                if (result.Succedeed)
                {
                    ViewBag.Result = result.Message;
                    return View();
                }
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }
            return View(model);
        }

        public ActionResult Users()
        {
            List<UserDM> users = _mappers.ToUserDM.Map<IEnumerable<UserDTO>, List<UserDM>>(_service.StoreService.GetUsers());
            List<ClientProfileDM> clients = _mappers.ToClientProfileDM.Map<IEnumerable<ClientProfileDTO>, List<ClientProfileDM>>(_service.StoreService.GetClients());
            UsersVM usersModel = new UsersVM() { Clients = clients, Users = users };
            return View(usersModel);
        }
    }
}