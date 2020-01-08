using AutoMapper;
using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.SkinShop.Interfaces;
using SkinShop.BLL.SkinShop.Mappers;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using SkinShop.DL.Entities.SkinShop;
using SkinShop.DL.Interfaces.SkinShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.Services
{
    public class ApplicationServiceCRUD: IServiceCRUD
    {
        IUnitOfWorK Database;
        MappersForDTO _mappers = new MappersForDTO();

        public ApplicationServiceCRUD(IUnitOfWorK uof)
        {
            Database = uof;
        }

        public OperationDetails CreateGame(GameDTO item)
        {
            if(item == null)
            {
                return new OperationDetails(false, "ОбЪект ссылается на null", this.ToString());
            }
            Game game = _mappers.ToGame.Map<GameDTO, Game>(item);
            if(game == null)
            {
                return new OperationDetails(false, "Не удалось преобразовать объект", this.ToString());
            }
            foreach (var i in item.Images)
            {
                if (i != null)
                {
                    Image image = Database.Images.Find(x => x.Photo == i.Photo)?.FirstOrDefault();
                    if (image == null)
                    {
                        image = new Image() { Photo = i.Photo, Text = i.Text };
                    }
                    game.Images.Add(image);
                }
            }

            Database.Games.Add(game);
            Database.Save();
            return new OperationDetails(true, "Игра была успешно создана", this.ToString());
        }

        public OperationDetails UpdateGame(GameDTO item)
        {
            if (item == null)
            {
                return new OperationDetails(false, "ОбЪект ссылается на null", this.ToString());
            }
            Game oldGame = Database.Games.Get(item.Id);
            if (oldGame == null)
            {
                return new OperationDetails(false, "Не удалось найти объект", this.ToString());
            }
            foreach (var i in item.Images)
            {
                if (i != null && i.Photo != oldGame.Images.FirstOrDefault().Photo)
                {
                    Image image = Database.Images.Find(x => x.Photo == i.Photo)?.FirstOrDefault();
                    if (image == null)
                    {
                        image = new Image() { Photo = i.Photo, Text = i.Text };
                    }
                    oldGame.Images.Add(image);
                }
            }

            oldGame.IsThingGame = item.IsThingGame;
            oldGame.Name = item.Name;
            oldGame.Type = item.Type;

            Database.Games.Update(oldGame);
            Database.Save();
            return new OperationDetails(true, "Игра успешно изменeнa", this.ToString());
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
                return new OperationDetails(false, "Не удалось найти игру с таким названием", this.ToString());
            }
            _skin.Game = _game;
            SkinRarity _skinRarity = Database.SkinRareties.Find(x => x.RarityName == item.SkinRarity.RarityName)?.FirstOrDefault();
            if(_skinRarity == null)
            {
                _skinRarity = new SkinRarity {RarityName = item.SkinRarity.RarityName};
            }
            _skin.SkinRarity = _skinRarity;
            SkinType _skinType = Database.SkinTypes.Find(x => x.TypeName == item.SkinType.TypeName)?.FirstOrDefault();
            if (_skinType == null)
            {
                _skinType = new SkinType { TypeName = item.SkinType.TypeName };
            }
            foreach(var i in item.Images)
            {
                if(i != null)
                {
                    Image image = Database.Images.Find(x => x.Photo == i.Photo)?.FirstOrDefault();
                    if(image == null)
                    {
                        image = new Image() { Photo = i.Photo, Text = i.Text };
                    }
                    _skin.Images.Add(image);
                }
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
                    return new OperationDetails(false, "Не удалось найти игру с таким названием", this.ToString());
                }
                _oldSkin.Game = _game;
            }
            if(_oldSkin.SkinRarity.RarityName != _oldSkin.SkinRarity.RarityName)
            {
                SkinRarity _skinRarity = Database.SkinRareties.Find(x => x.RarityName == item.SkinRarity.RarityName)?.FirstOrDefault();
                if (_skinRarity == null)
                {
                    _skinRarity = new SkinRarity { RarityName = item.SkinRarity.RarityName };
                }
                _oldSkin.SkinRarity = _skinRarity;
            }
            if (_oldSkin.SkinType.TypeName != item.SkinType.TypeName)
            {
                SkinType _skinType = Database.SkinTypes.Find(x => x.TypeName == item.SkinType.TypeName)?.FirstOrDefault();
                if (_skinType == null)
                {
                    _skinType = new SkinType { TypeName = item.SkinType.TypeName };
                }
                _oldSkin.SkinType = _skinType;
            }
            foreach (var i in item.Images)
            {
                if (i != null && i.Photo != _oldSkin.Images.FirstOrDefault().Photo)
                {
                    Image image = Database.Images.Find(x => x.Photo == i.Photo)?.FirstOrDefault();
                    if (image == null)
                    {
                        image = new Image() { Photo = i.Photo, Text = i.Text };
                    }
                    _oldSkin.Images.Add(image);
                }
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
    }
}
