using SkinShop.BLL.Identity.Interfaces;
using SkinShop.BLL.SkinShop.Mappers;
using SkinShop.DAL.SkinShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.Interfaces
{
    public interface IMainService
    {
        IService StoreService { get; }

        IUserService UserService { get; }

        IServiceCRUD ServiceCRUD { get; }
    }
}
