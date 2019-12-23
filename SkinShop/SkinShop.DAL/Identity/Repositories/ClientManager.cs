using SkinShop.DAL.Identity.Entities;
using SkinShop.DAL.Identity.Interfaces;
using SkinShop.DAL.SkinShop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.Identity.Repositories
{
    public class ClientManager: IClientManager
    {
        public Context Database { get; set; }
        public ClientManager(Context db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            if (item != null)
            {
                Database.ClientProfiles.Add(item);
                Database.SaveChanges();
            }
        }

        public User GetUser(int id)
        {
            return Database.Users.Find(id);
        }

        public ClientProfile GetClient(int id)
        {
            return Database.ClientProfiles.Find(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
