using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SkinShop.DL.EF;
using SkinShop.DL.Entities.Identity;
using SkinShop.DL.Entities.SkinShop;
using SkinShop.DL.Interfaces.Identity;
using SkinShop.DL.Interfaces.SkinShop;
using SkinShop.DL.Managers;
using SkinShop.DL.Repositories.Identity;

namespace SkinShop.DL.Repositories.SkinShop
{
    public class UnitOfWork : IUnitOfWorK
    {
        Context _context;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

        public UnitOfWork(string connectionString)
        {
            _context = new Context(connectionString);
            userManager = new ApplicationUserManager(new UserStore<User>(_context));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
            clientManager = new ClientManager(_context);
        }

        private AbstractRepository<Game> _games;
        private AbstractRepository<Order> _orders;
        private AbstractRepository<Basket> _baskets;
        private AbstractRepository<Favorites> _favorites;
        private AbstractRepository<Skin> _skins;
        private AbstractRepository<SkinRarity> _skinRareties;
        private AbstractRepository<SkinType> _skinTypes;
        private AbstractRepository<OrderCount> _orderCounts;
        private AbstractRepository<Image> _images;

        public virtual AbstractRepository<Game> Games
        {
            get
            {
                if (_games == null)
                    _games = new Repository<Game>(_context);
                return _games;
            }
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

        public virtual AbstractRepository<Order> Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new Repository<Order>(_context);
                return _orders;
            }
        }

        public virtual AbstractRepository<Basket> Baskets
        {
            get
            {
                if (_baskets == null)
                    _baskets = new Repository<Basket>(_context);
                return _baskets;
            }
        }

        public virtual AbstractRepository<Favorites> Favorites
        {
            get
            {
                if (_favorites == null)
                    _favorites = new Repository<Favorites>(_context);
                return _favorites;
            }
        }

        public virtual AbstractRepository<Skin> Skins
        {
            get
            {
                if (_skins == null)
                    _skins = new Repository<Skin>(_context);
                return _skins;
            }
        }

        public virtual AbstractRepository<SkinRarity> SkinRareties
        {
            get
            {
                if (_skinRareties == null)
                    _skinRareties = new Repository<SkinRarity>(_context);
                return _skinRareties;
            }
        }

        public virtual AbstractRepository<SkinType> SkinTypes
        {
            get
            {
                if (_skinTypes == null)
                    _skinTypes = new Repository<SkinType>(_context);
                return _skinTypes;
            }
        }

        public virtual AbstractRepository<OrderCount> OrderCounts
        {
            get
            {
                if (_orderCounts == null)
                    _orderCounts = new Repository<OrderCount>(_context);
                return _orderCounts;
            }
        }

        public virtual AbstractRepository<Image> Images
        {
            get
            {
                if (_images == null)
                    _images = new Repository<Image>(_context);
                return _images;
            }
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}
