using AutoMapper;
using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using SkinShop.DAL.Identity.Entities;
using SkinShop.DAL.SkinShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.Mappers
{
    public class MappersForDTO
    {
        public virtual IMapper ToSkinDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Skin, SkinDTO>()
                .ForMember(x => x.Game, c => c.MapFrom(k => ToGameDTO.Map<Game, GameDTO>(k.Game)))
                .ForMember(x => x.SkinRarity, c => c.MapFrom(k => ToSkinRarityDTO.Map<SkinRarity, SkinRarityDTO>(k.SkinRarity)))
                .ForMember(x => x.SkinType, c => c.MapFrom(k => ToSkinTypeDTO.Map<SkinType, SkinTypeDTO>(k.SkinType)))
                .ForMember(x => x.Images, c => c.MapFrom(k => ToImageDTO.Map<ICollection<Image>, ICollection<ImageDTO>>(k.Images)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToImageDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Image, ImageDTO>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToSkinTypeDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<SkinType, SkinTypeDTO>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToSkinRarityDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<SkinRarity, SkinRarityDTO>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToGameDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Game, GameDTO>()
                .ForMember(x => x.Images, c => c.MapFrom(k => ToImageDTO.Map<ICollection<Image>, ICollection<ImageDTO>>(k.Images)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToUserDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToClientProfileDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ClientProfile, ClientProfileDTO>()
                .ForMember(x => x.Basket, c => c.MapFrom(k => ToBasketDTO.Map<Basket, BasketDTO>(k.Basket)))
                .ForMember(x => x.Favorites, c => c.MapFrom(k => ToFavoritesDTO.Map<Favorites, FavoritesDTO>(k.Favorites)))
                .ForMember(x => x.Orders, c => c.MapFrom(k => ToOrderDTO.Map<ICollection<Order>, ICollection<OrderDTO>>(k.Orders)))
                .ForMember(x => x.User, c => c.MapFrom(k => ToUserDTO.Map<User, UserDTO>(k.User)))
                ).CreateMapper();
                
            }
        }

        public virtual IMapper ToBasketDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Basket, BasketDTO>()
                    .ForMember(x => x.Skins, c => c.MapFrom(k => ToSkinDTO.Map<IEnumerable<Skin>, List<SkinDTO>>(k.Skins))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToFavoritesDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Favorites,FavoritesDTO>()
                .ForMember(x => x.FavoritesSkins, c => c.MapFrom(k => ToSkinDTO.Map<ICollection<Skin>, List<SkinDTO>>(k.FavoritesSkins)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToOrderCountDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<OrderCount, OrderCountDTO>()
                .ForMember(x => x.Skin, c => c.MapFrom(k => ToSkinDTO.Map<Skin, SkinDTO>(k.Skin)))
                )
                   .CreateMapper();
            }
        }

        public virtual IMapper ToOrderDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()
                .ForMember(x => x.Client, c => c.MapFrom(k => ToClientProfileDTO.Map<ClientProfile, ClientProfileDTO>(k.Client)))
                .ForMember(x => x.Employee, c =>c.MapFrom(k => ToUserDTO.Map<User, UserDTO>(k.Employee)))
                .ForMember(x => x.OrderCounts, c => c.MapFrom(k => ToOrderCountDTO.Map<ICollection<OrderCount>, List<OrderCountDTO>>(k.OrderCounts)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToSkin
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<SkinDTO, Skin>()
                .ForMember(x => x.Game, c => c.MapFrom(k => ToGame.Map<GameDTO, Game>(k.Game)))
                .ForMember(x => x.SkinRarity, c => c.MapFrom(k => ToSkinRarity.Map<SkinRarityDTO, SkinRarity>(k.SkinRarity)))
                .ForMember(x => x.SkinType, c => c.MapFrom(k => ToSkinType.Map<SkinTypeDTO, SkinType>(k.SkinType)))
                .ForMember(x => x.Images, c => c.MapFrom(k => ToImage.Map<ICollection<ImageDTO>, ICollection<Image>>(k.Images)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToImage
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ImageDTO, Image>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToSkinType
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<SkinTypeDTO, SkinType>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToSkinRarity
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<SkinRarityDTO, SkinRarity>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToGame
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<GameDTO, Game>()
                 .ForMember(x => x.Images, c => c.MapFrom(k => ToImage.Map<ICollection<ImageDTO>, ICollection<Image>>(k.Images)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToUser
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToClientProfile
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ClientProfileDTO, ClientProfile>()
                .ForMember(x => x.Basket, c => c.MapFrom(k => ToBasket.Map<BasketDTO, Basket>(k.Basket)))
                .ForMember(x => x.Favorites, c => c.MapFrom(k => ToFavorites.Map<FavoritesDTO, Favorites>(k.Favorites)))
                .ForMember(x => x.Orders, c => c.MapFrom(k => ToOrder.Map<ICollection<OrderDTO>, ICollection<Order>>(k.Orders)))
                .ForMember(x => x.User, c => c.MapFrom(k => ToUser.Map<UserDTO, User>(k.User)))
                ).CreateMapper();

            }
        }

        public virtual IMapper ToBasket
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<BasketDTO, Basket>()
                    .ForMember(x => x.Skins, c => c.MapFrom(k => ToSkin.Map<ICollection<SkinDTO>, ICollection<Skin>>(k.Skins))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToFavorites
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<FavoritesDTO, Favorites>()
                .ForMember(x => x.FavoritesSkins, c => c.MapFrom(k => ToSkin.Map<ICollection<SkinDTO>, ICollection<Skin>>(k.FavoritesSkins)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToOrderCount
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<OrderCountDTO, OrderCount>()
                .ForMember(x => x.Skin, c => c.MapFrom(k => ToSkin.Map<SkinDTO, Skin>(k.Skin)))
                )
                   .CreateMapper();
            }
        }

        public virtual IMapper ToOrder
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, Order>()
                .ForMember(x => x.Client, c => c.MapFrom(k => ToClientProfile.Map<ClientProfileDTO, ClientProfile>(k.Client)))
                .ForMember(x => x.Employee, c => c.MapFrom(k => ToUser.Map<UserDTO, User>(k.Employee)))
                .ForMember(x => x.OrderCounts, c => c.MapFrom(k => ToOrderCount.Map<ICollection<OrderCountDTO>, ICollection<OrderCount>>(k.OrderCounts)))
                )
                    .CreateMapper();
            }
        }
    }
}