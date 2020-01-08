using Microsoft.AspNet.Identity.EntityFramework;
using SkinShop.DL.Entities.Identity;
using SkinShop.DL.Entities.SkinShop;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DL.EF
{
    public class Context : IdentityDbContext<User>
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
        public DbSet<Image> Images { get; set; }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        //public override IDbSet<User> Users { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Skin>().HasMany(c => c.Baskets)
        //        .WithMany(s => s.Skins)
        //        .Map(t => t.MapLeftKey("SkinId")
        //        .MapRightKey("BasketId")
        //        .ToTable("SkinBasket"));
        //}
    }
}
