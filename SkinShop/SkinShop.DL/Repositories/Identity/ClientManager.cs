using SkinShop.DL.EF;
using SkinShop.DL.Entities.Identity;
using SkinShop.DL.Interfaces.Identity;
using SkinShop.DL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DL.Repositories.Identity
{
    public class ClientManager : IClientManager
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

        public User GetUser(string id)
        {
            return Database.Users.Find(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return Database.Set<User>().Where(x => x.IsDeleted == false).ToList();
        }

        public ClientProfile GetClient(string id)
        {
            return Database.ClientProfiles.Find(id);
        }

        public IEnumerable<ClientProfile> GetClients()
        {
            return Database.Set<ClientProfile>().Where(x => x.User.IsDeleted == false).ToList();
        }

        public ClientProfile FindClient(Func<ClientProfile, bool> predicate)
        {
            return Database.Set<ClientProfile>().Where(predicate).FirstOrDefault();
        }

        public User FindUser(Func<User, bool> predicate)
        {
            return Database.Set<User>().Where(predicate).FirstOrDefault();
        }
    }
}
