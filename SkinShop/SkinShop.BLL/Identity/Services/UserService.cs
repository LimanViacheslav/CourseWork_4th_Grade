using Microsoft.AspNet.Identity;
using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.Identity.Interfaces;
using SkinShop.DL.Entities.Identity;
using SkinShop.DL.Entities.SkinShop;
using SkinShop.DL.Interfaces.SkinShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.Identity.Services
{
    public class UserService : IUserService
    {
        IUnitOfWorK Database { get; set; }

        public UserService(IUnitOfWorK uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            User user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.Email, PhoneNumber = userDto.PhoneNumber, Name = userDto.Name, Adres = userDto.Address };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id};
                clientProfile.Basket = new Basket();
                clientProfile.Favorites = new Favorites();
                Database.ClientManager.Create(clientProfile);
                Database.Save();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<OperationDetails> CreateEmployee(UserDTO userDTO)
        {
            User user = await Database.UserManager.FindByEmailAsync(userDTO.Email);
            if (user == null)
            {
                user = new User { Email = userDTO.Email, UserName = userDTO.Email, PhoneNumber = userDTO.PhoneNumber, Name = userDTO.Name, Adres = userDTO.Address };
                var result = await Database.UserManager.CreateAsync(user, userDTO.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDTO.Role);
                Database.Save();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Менеджер с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            User user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await CreateEmployee(adminDto);
        }

        public void Dispose()
        {
            Database.UserManager.Dispose();
        }
    }
}
