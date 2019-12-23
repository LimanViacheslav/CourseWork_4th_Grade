using SkinShop.DAL.SkinShop.Entities;
using SkinShop.DAL.SkinShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.SkinShop.Interfaces
{
    public interface IUnitOfWorkSkinShop
    {
        AbstractRepository<Game> Games { get; }

        AbstractRepository<Order> Orders { get; }

        AbstractRepository<Basket> Baskets { get; }

        AbstractRepository<Favorites> Favorites { get; }

        AbstractRepository<Skin> Skins { get; }

        AbstractRepository<SkinRarity> SkinRareties { get; }

        AbstractRepository<OrderCount> OrderCounts { get; }
        
        AbstractRepository<SkinType> SkinTypes { get; }

        void Save();
    }
}
