using SkinShop.DAL.SkinShop.EF;
using SkinShop.DAL.SkinShop.Entities;
using SkinShop.DAL.SkinShop.Interfaces;

namespace SkinShop.DAL.SkinShop.Repositories
{
    public class UnitOfWorkSkinShop : IUnitOfWorkSkinShop
    {
        Context _context;

        public UnitOfWorkSkinShop(string connectionString)
        {
            _context = new Context(connectionString);
        }

        private AbstractRepository<Game> _games;
        private AbstractRepository<Order> _orders;
        private AbstractRepository<Basket> _baskets;
        private AbstractRepository<Favorites> _favorites;
        private AbstractRepository<Skin> _skins;
        private AbstractRepository<SkinRarity> _skinRareties;
        private AbstractRepository<SkinType> _skinTypes;
        private AbstractRepository<OrderCount> _orderCounts;

        public virtual AbstractRepository<Game> Games
        {
            get
            {
                if (_games == null)
                    _games = new Repository<Game>(_context);
                return _games;
            }
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

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}
