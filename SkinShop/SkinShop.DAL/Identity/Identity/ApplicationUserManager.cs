using Microsoft.AspNet.Identity;
using SkinShop.DAL.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.Identity.Identity
{
    public class ApplicationUserManager: UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
                : base(store)
        {
        }
    }
}
