using SkinShop.BLL.Identity.Interfaces;
using SkinShop.DAL.Identity.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.Identity.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new UnitOfWorkIdentity(connection));
        }
    }
}
