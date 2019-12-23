using SkinShop.DAL.Identity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.Identity.Interfaces
{
   public interface IUnitOfWorkIdentity: IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }

        Task SaveAsync();
    }
}
