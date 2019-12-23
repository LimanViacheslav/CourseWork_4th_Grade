using SkinShop.DAL.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.Identity.Interfaces
{
    public interface IClientManager: IDisposable
    {
        void Create(ClientProfile item);
        User GetUser(int id);
        ClientProfile GetClient(int id);
    }  
}
