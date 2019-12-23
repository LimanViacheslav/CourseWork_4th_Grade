using Microsoft.AspNet.Identity.EntityFramework;
using SkinShop.DAL.Identity.Entities;
using SkinShop.DAL.Identity.Identity;
using SkinShop.DAL.Identity.Interfaces;
using SkinShop.DAL.SkinShop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.Identity.Repositories
{
    public class UnitOfWorkIdentity: IUnitOfWorkIdentity
    {
        private Context db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

        public UnitOfWorkIdentity(string connectionString)
        {
            db = new Context(connectionString);
            userManager = new ApplicationUserManager(new UserStore<User>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
