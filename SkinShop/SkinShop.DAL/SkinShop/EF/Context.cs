using Microsoft.AspNet.Identity.EntityFramework;
using SkinShop.DAL.Identity.Entities;
using SkinShop.DAL.SkinShop.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SkinShop.DAL.SkinShop.EF
{
    public class Context: IdentityDbContext<User>
    {
        static Context()
        {
            Database.SetInitializer<Context>(new DbInitializer());
        }

        public Context(string conectionString) : base(conectionString) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Skin> Skins { get; set; }
        public DbSet<SkinRarity> SkinRarities { get; set; }
        public DbSet<SkinType> SkinTypes { get; set; }
        public DbSet<OrderCount> OrderCounts { get; set; }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
