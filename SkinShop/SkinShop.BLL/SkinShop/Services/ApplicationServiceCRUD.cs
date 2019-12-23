using AutoMapper;
using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.SkinShop.Interfaces;
using SkinShop.BLL.SkinShop.Mappers;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using SkinShop.DAL.Identity.Entities;
using SkinShop.DAL.Identity.Interfaces;
using SkinShop.DAL.Identity.Repositories;
using SkinShop.DAL.SkinShop.Entities;
using SkinShop.DAL.SkinShop.Interfaces;
using SkinShop.DAL.SkinShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.Services
{
    public class ApplicationServiceCRUD: IServiceCRUD
    {
        IUnitOfWorkSkinShop Database;
        MappersForDTO _mappers = new MappersForDTO();
        IUnitOfWorkIdentity DatabaseIdentity;

        public ApplicationServiceCRUD(IUnitOfWorkSkinShop uof)
        {
            Database = uof;
            DatabaseIdentity = new UnitOfWorkIdentity("DefaultConnection");
        }

        public OperationDetails CreateSkin(SkinDTO item)
        {
            if (item == null)
            {
                return new OperationDetails(false, "ОбЪект ссылается на null", this.ToString());
            }
            Skin _skin = _mappers.ToSkin.Map<SkinDTO, Skin>(item);
            if (_skin == null)
            {
                return new OperationDetails(false, "Не удалось преобразовать объект", this.ToString());
            }
            Game _game = Database.Games.Find(x => x.Name == item.Game.Name)?.FirstOrDefault();
            if (_game == null)
            {
                _game = new Game { Name = item.Game.Name, Genre = item.Game.Genre, GameURL = item.Game.GameURL, IsThingGame = item.Game.IsThingGame, Type = item.Game.Type };
                Database.Games.Add(_game);
            }
            _skin.Game = _game;
            SkinRarity _skinRarity = Database.SkinRareties.Find(x => x.RarityName == item.SkinRarity.RarityName)?.FirstOrDefault();
            if(_skinRarity == null)
            {
                _skinRarity = new SkinRarity {RarityName = item.SkinRarity.RarityName};
                Database.SkinRareties.Add(_skinRarity);
            }
            _skin.SkinRarity = _skinRarity;
            SkinType _skinType = Database.SkinTypes.Find(x => x.TypeName == item.SkinType.TypeName)?.FirstOrDefault();
            if (_skinType == null)
            {
                _skinType = new SkinType { TypeName = item.SkinType.TypeName };
                Database.SkinTypes.Add(_skinType);
            }
            _skin.SkinType = _skinType;
            Database.Skins.Add(_skin);
            Database.Save();
            return new OperationDetails(true, "Скин успешно создан", this.ToString());
        }

        public OperationDetails UpdateSkin(SkinDTO item)
        {
            if (item == null)
            {
                return new OperationDetails(false, "ОбЪект ссылается на null", this.ToString());
            }
            Skin _oldSkin = Database.Skins.Get(item.Id);
            if (_oldSkin == null)
            {
                return new OperationDetails(false, "Не удалось найти объект", this.ToString());
            }
            if(_oldSkin.Game.Name != item.Game.Name)
            {
                Game _game = Database.Games.Find(x => x.Name == item.Game.Name)?.FirstOrDefault();
                if (_game == null)
                {
                    _game = new Game { Name = item.Game.Name, Genre = item.Game.Genre, GameURL = item.Game.GameURL, IsThingGame = item.Game.IsThingGame, Type = item.Game.Type };
                    Database.Games.Add(_game);
                }
                _oldSkin.Game = _game;
            }
            if(_oldSkin.SkinRarity.RarityName != _oldSkin.SkinRarity.RarityName)
            {
                SkinRarity _skinRarity = Database.SkinRareties.Find(x => x.RarityName == item.SkinRarity.RarityName)?.FirstOrDefault();
                if (_skinRarity == null)
                {
                    _skinRarity = new SkinRarity { RarityName = item.SkinRarity.RarityName };
                    Database.SkinRareties.Add(_skinRarity);
                }
                _oldSkin.SkinRarity = _skinRarity;
            }
            if(_oldSkin.SkinType.TypeName != item.SkinType.TypeName)
            {
                SkinType _skinType = Database.SkinTypes.Find(x => x.TypeName == item.SkinType.TypeName)?.FirstOrDefault();
                if (_skinType == null)
                {
                    _skinType = new SkinType { TypeName = item.SkinType.TypeName };
                    Database.SkinTypes.Add(_skinType);
                }
                _oldSkin.SkinType = _skinType;
            }
            _oldSkin.Name = item.Name;
            _oldSkin.Price = item.Price;
            _oldSkin.Sale = item.Sale;

            Database.Skins.Update(_oldSkin);
            Database.Save();
            return new OperationDetails(true, "Скин успешно изменён", this.ToString());
        }

        public ICollection<SkinDTO> GetSkins()
        {
            return _mappers.ToSkinDTO.Map<ICollection<Skin>, ICollection<SkinDTO>>(Database.Skins.Show());
        }

        public SkinDTO GetSkin(int? id)
        {
            if(id != null)
                return _mappers.ToSkinDTO.Map<Skin, SkinDTO>(Database.Skins.Get(Convert.ToInt32(id)));
            return null;
        }

        public ICollection<GameDTO> GetGames()
        {
            return _mappers.ToGameDTO.Map<ICollection<Game>, ICollection<GameDTO>>(Database.Games.Show());
        }

        public ICollection<SkinTypeDTO> GetTypes()
        {
            return _mappers.ToSkinTypeDTO.Map<ICollection<SkinType>, ICollection<SkinTypeDTO>>(Database.SkinTypes.Show());
        }

        public ICollection<SkinRarityDTO> GetRarity()
        {
            return _mappers.ToSkinRarityDTO.Map<ICollection<SkinRarity>, ICollection<SkinRarityDTO>>(Database.SkinRareties.Show());
        }

        public ICollection<OrderDTO> GetOredersForUser(int id)
        {
            return _mappers.ToOrderDTO.Map<IEnumerable<Order>, List<OrderDTO>>(Database.Orders.Find(x => x.ClientId == id));
        }
    }
}
