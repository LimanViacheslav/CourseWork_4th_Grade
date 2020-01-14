using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.Identity.Interfaces;
using SkinShop.BLL.SkinShop.Services;
using SkinShop.Filters;
using SkinShop.Mappers;
using SkinShop.Models;
using SkinShop.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SkinShop.Controllers
{
    [BanAttribute]
    public class AccountController : Controller
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

        public ActionResult UserInfo(string name="")
        {
            if (name == "")
                name = User.Identity.Name;
            UserDM user = _mappers.ToUserDM.Map<UserDTO, UserDM>(_service.StoreService.GetUserByName(name));
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if(User.Identity.Name == "")
            {
                await SetInitialDataAsync();
                if (ModelState.IsValid)
                {
                    UserDTO currentUser = _service.StoreService.GetUserByName(model.Email);
                    if(currentUser == null || !currentUser.IsBanned)
                    {
                        UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                        ClaimsIdentity claim = await UserService.Authenticate(userDto);
                        if (claim == null)
                        {
                            ModelState.AddModelError("", "Неверный логин или пароль.");
                        }
                        else
                        {
                            AuthenticationManager.SignOut();
                            AuthenticationManager.SignIn(new AuthenticationProperties
                            {
                                IsPersistent = true
                            }, claim);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Вы были забанены, Вам запрещён доступ");
                    }
                }
            }
            else
            {
                UserDTO user = _service.StoreService.GetUserByName(User.Identity.Name);
                if(user.IsBanned)
                {
                    ModelState.AddModelError("", "Вы были забанены, Вам запрещён доступ");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "user",
                    PhoneNumber = model.PhoneNumber
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        public ActionResult SuccessRegister()
        {
            return View();
        }

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "someemail@gmail.com",
                UserName = "someemail@gmail.com",
                Password = "password",
                Name = "Liman Viacheslav Vitalievich",
                Address = "ул. Спортивная, д.30, кв.75",
                Role = "admin",
            }, new List<string> { "user", "admin", "manager" });
        }
    }
}