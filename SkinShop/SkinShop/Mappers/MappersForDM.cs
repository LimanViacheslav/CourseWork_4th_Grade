using AutoMapper;
using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.SkinShop.SkinShopDTO;
using SkinShop.Models.Account;
using SkinShop.Models.SkinShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinShop.Mappers
{
    public class MappersForDM
    {
        public virtual IMapper ToSkinDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<SkinDTO, SkinDM>()
                .ForMember(x => x.Game, c => c.MapFrom(k => ToGameDM.Map<GameDTO, GameDM>(k.Game)))
                .ForMember(x => x.SkinRarity, c => c.MapFrom(k => ToSkinRarityDM.Map<SkinRarityDTO, SkinRaretyDM>(k.SkinRarity)))
                .ForMember(x => x.SkinType, c => c.MapFrom(k => ToSkinTypeDM.Map<SkinTypeDTO, SkinTypeDM>(k.SkinType)))
                .ForMember(x => x.Images, c => c.MapFrom(k => ToImageDM.Map<ICollection<ImageDTO>, ICollection<ImageDM>>(k.Images)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToImageDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ImageDTO, ImageDM>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToSkinTypeDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<SkinTypeDTO, SkinTypeDM>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToSkinRarityDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<SkinRarityDTO, SkinRaretyDM>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToGameDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<GameDTO, GameDM>()
                .ForMember(x => x.Images, c => c.MapFrom(k => ToImageDM.Map<ICollection<ImageDTO>, ICollection<ImageDM>>(k.Images)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToUserDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserDM>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToClientProfileDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ClientProfileDTO, ClientProfileDM>()
                .ForMember(x => x.Basket, c => c.MapFrom(k => ToBasketDM.Map<BasketDTO, BasketDM>(k.Basket)))
                .ForMember(x => x.Favorites, c => c.MapFrom(k => ToFavoritesDM.Map<FavoritesDTO, FavoritesDM>(k.Favorites)))
                .ForMember(x => x.Orders, c => c.MapFrom(k => ToOrderDM.Map<ICollection<OrderDTO>, ICollection<OrderDM>>(k.Orders)))
                .ForMember(x => x.User, c => c.MapFrom(k => ToUserDM.Map<UserDTO, UserDM>(k.User)))
                ).CreateMapper();

            }
        }

        public virtual IMapper ToBasketDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<BasketDTO, BasketDM>()
                    .ForMember(x => x.Skins, c => c.MapFrom(k => ToSkinDM.Map<IEnumerable<SkinDTO>, List<SkinDM>>(k.Skins))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToFavoritesDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<FavoritesDTO, FavoritesDM>()
                .ForMember(x => x.FavoritesSkins, c => c.MapFrom(k => ToSkinDM.Map<ICollection<SkinDTO>, List<SkinDM>>(k.FavoritesSkins)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToOrderCountDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<OrderCountDTO, OrderCountDM>()
                .ForMember(x => x.Skin, c => c.MapFrom(k => ToSkinDM.Map<SkinDTO, SkinDM>(k.Skin)))
                )
                   .CreateMapper();
            }
        }

        public virtual IMapper ToOrderDM
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderDM>()
                .ForMember(x => x.Client, c => c.MapFrom(k => ToClientProfileDM.Map<ClientProfileDTO, ClientProfileDM>(k.Client)))
                .ForMember(x => x.Employee, c => c.MapFrom(k => ToUserDM.Map<UserDTO, UserDM>(k.Employee)))
                .ForMember(x => x.OrderCounts, c => c.MapFrom(k => ToOrderCountDM.Map<ICollection<OrderCountDTO>, List<OrderCountDM>>(k.OrderCounts)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToSkinDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<SkinDM, SkinDTO>()
                .ForMember(x => x.Game, c => c.MapFrom(k => ToGameDTO.Map<GameDM, GameDTO>(k.Game)))
                .ForMember(x => x.SkinRarity, c => c.MapFrom(k => ToSkinRarityDTO.Map<SkinRaretyDM, SkinRarityDTO>(k.SkinRarity)))
                .ForMember(x => x.SkinType, c => c.MapFrom(k => ToSkinTypeDTO.Map<SkinTypeDM, SkinTypeDTO>(k.SkinType)))
                .ForMember(x => x.Images, c => c.MapFrom(k => ToImageDTO.Map<ICollection<ImageDM>, ICollection<ImageDTO>>(k.Images)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToImageDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ImageDM, ImageDTO>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToSkinTypeDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<SkinTypeDM, SkinTypeDTO>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToSkinRarityDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<SkinRaretyDM, SkinRarityDTO>()
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToGameDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<GameDM, GameDTO>()
                 .ForMember(x => x.Images, c => c.MapFrom(k => ToImageDTO.Map<ICollection<ImageDM>, ICollection<ImageDTO>>(k.Images)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToUserDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<UserDM, UserDTO>())
                    .CreateMapper();
            }
        }

        public virtual IMapper ToClientProfileDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<ClientProfileDM, ClientProfileDTO>()
                .ForMember(x => x.Basket, c => c.MapFrom(k => ToBasketDTO.Map<BasketDM, BasketDTO>(k.Basket)))
                .ForMember(x => x.Favorites, c => c.MapFrom(k => ToFavoritesDTO.Map<FavoritesDM, FavoritesDTO>(k.Favorites)))
                .ForMember(x => x.Orders, c => c.MapFrom(k => ToOrderDTO.Map<ICollection<OrderDM>, ICollection<OrderDTO>>(k.Orders)))
                .ForMember(x => x.User, c => c.MapFrom(k => ToUserDTO.Map<UserDM, UserDTO>(k.User)))
                ).CreateMapper();

            }
        }

        public virtual IMapper ToBasketDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<BasketDM, BasketDTO>()
                    .ForMember(x => x.Skins, c => c.MapFrom(k => ToSkinDTO.Map<ICollection<SkinDM>, ICollection<SkinDTO>>(k.Skins))))
                    .CreateMapper();
            }
        }

        public virtual IMapper ToFavoritesDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<FavoritesDM, FavoritesDTO>()
                .ForMember(x => x.FavoritesSkins, c => c.MapFrom(k => ToSkinDTO.Map<ICollection<SkinDM>, ICollection<SkinDTO>>(k.FavoritesSkins)))
                )
                    .CreateMapper();
            }
        }

        public virtual IMapper ToOrderCountDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<OrderCountDM, OrderCountDTO>()
                .ForMember(x => x.Skin, c => c.MapFrom(k => ToSkinDTO.Map<SkinDM, SkinDTO>(k.Skin)))
                )
                   .CreateMapper();
            }
        }

        public virtual IMapper ToOrderDTO
        {
            get
            {
                return new MapperConfiguration(cfg => cfg.CreateMap<OrderDM, OrderDTO>()
                .ForMember(x => x.Client, c => c.MapFrom(k => ToClientProfileDTO.Map<ClientProfileDM, ClientProfileDTO>(k.Client)))
                .ForMember(x => x.Employee, c => c.MapFrom(k => ToUserDTO.Map<UserDM, UserDTO>(k.Employee)))
                .ForMember(x => x.OrderCounts, c => c.MapFrom(k => ToOrderCountDTO.Map<ICollection<OrderCountDM>, ICollection<OrderCountDTO>>(k.OrderCounts)))
                )
                    .CreateMapper();
            }
        }
    }
}