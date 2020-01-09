using SkinShop.DL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DL.Interfaces.Identity
{
    public interface IClientManager
    {
        void Create(ClientProfile item);
        User GetUser(string id);
        ClientProfile GetClient(string id);
        ClientProfile FindClient(Func<ClientProfile, bool> predicate);
        User FindUser(Func<User, bool> predicate);
        IEnumerable<ClientProfile> GetClients();
        IEnumerable<User> GetUsers();
    }
}
