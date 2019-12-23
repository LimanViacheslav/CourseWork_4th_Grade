using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.SkinShop.Interfaces;
using SkinShop.DAL.SkinShop.Interfaces;
using SkinShop.DAL.SkinShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.Services
{
    public enum Tables
    {
        Orders = 1,
        Baskets,
        Favorites,
        Skins,
        Games,
        SkinTypes,
        SkinRarities
    }

    public class CommonOperations: ICommonOperations
    {
        public IUnitOfWorkSkinShop Database;
        public CommonOperations()
        {
            Database = new UnitOfWorkSkinShop("DefaultConnection");
        }

        public OperationDetails SoftDelete(Tables tables, int id)
        {
            switch ((int)tables)
            {
                case 1:
                    Database.Orders.SoftDelete(id);
                    Database.Save();
                    return new OperationDetails(true, "Заказ успешно удалён", this.ToString());
                case 2:
                    Database.Baskets.SoftDelete(id);
                    Database.Save();
                    return new OperationDetails(true, "Корзина успешно удалена", this.ToString());
                case 3:
                    Database.Favorites.SoftDelete(id);
                    Database.Save();
                    return new OperationDetails(true, "Список желаний успешно удалён", this.ToString());
                case 4:
                    Database.Skins.SoftDelete(id);
                    Database.Save();
                    return new OperationDetails(true, "Скин успешно удалён", this.ToString());
                case 5:
                    Database.Games.SoftDelete(id);
                    Database.Save();
                    return new OperationDetails(true, "Игра успешно удалена", this.ToString());
                case 6:
                    Database.SkinTypes.SoftDelete(id);
                    Database.Save();
                    return new OperationDetails(true, "Тип скина успешно удалён", this.ToString());
                case 7:
                    Database.SkinRareties.SoftDelete(id);
                    Database.Save();
                    return new OperationDetails(true, "Редкость для скина успешно удалена", this.ToString());
                default:
                    return new OperationDetails(false, "Таблица указана не верно", this.ToString());
            }
        }

        public OperationDetails Delete(Tables tables, int id)
        {
            switch ((int)tables)
            {
                case 1:
                    Database.Orders.Delete(id);
                    Database.Save();
                    return new OperationDetails(true, "Заказ успешно удалён", this.ToString());
                case 2:
                    Database.Baskets.Delete(id);
                    Database.Save();
                    return new OperationDetails(true, "Корзина успешно удалена", this.ToString());
                case 3:
                    Database.Favorites.Delete(id);
                    Database.Save();
                    return new OperationDetails(true, "Список желаний успешно удалён", this.ToString());
                case 4:
                    Database.Skins.Delete(id);
                    Database.Save();
                    return new OperationDetails(true, "Скин успешно удалён", this.ToString());
                case 5:
                    Database.Games.Delete(id);
                    Database.Save();
                    return new OperationDetails(true, "Игра успешно удалена", this.ToString());
                case 6:
                    Database.SkinTypes.Delete(id);
                    Database.Save();
                    return new OperationDetails(true, "Тип скина успешно удалён", this.ToString());
                case 7:
                    Database.SkinRareties.Delete(id);
                    Database.Save();
                    return new OperationDetails(true, "Редкость для скина успешно удалена", this.ToString());
                default:
                    return new OperationDetails(false, "Таблица указана не верно", this.ToString());
            }
        }
    }
}
