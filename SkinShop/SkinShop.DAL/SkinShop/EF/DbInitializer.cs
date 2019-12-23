using SkinShop.DAL.SkinShop.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.SkinShop.EF
{
    public class DbInitializer: DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context db)
        {
            SkinRarity _rare = new SkinRarity() { Id = 1, RarityName = "Rare",IsDeleted = false, Color ="#ff0000" };
            SkinRarity _common = new SkinRarity() { Id = 2, RarityName = "Common", Color="#00ff00" };
            SkinRarity _legendary = new SkinRarity() { Id = 3, RarityName = "Legendary", Color="#0000ff" };

            db.SkinRarities.Add(_rare);
            db.SkinRarities.Add(_common);
            db.SkinRarities.Add(_legendary);

            SkinType _cloth = new SkinType() { Id = 1, TypeName = "Cloth" };
            SkinType _gun = new SkinType() { Id = 2, TypeName = "Gun" };
            SkinType _assessory = new SkinType() { Id = 2, TypeName = "Accessory" };

            db.SkinTypes.Add(_cloth);
            db.SkinTypes.Add(_gun);
            db.SkinTypes.Add(_assessory);

            Game _CS = new Game() { Id = 1, GameURL = "https://store.steampowered.com/app/730/CounterStrike_Global_Offensive/?l=russian", IsThingGame = true, Genre = "Shooter", Name = "Counter-Strike Global Offensive", Type = "Multiplayer" };
            Game _Fortnite = new Game() { Id = 2, GameURL = "https://www.epicgames.com/fortnite/ru/home", IsThingGame = false, Genre = "Battle Royal, Survival", Name = "Fortnite", Type = "Multiplayer" };

            db.Games.Add(_CS);
            db.Games.Add(_Fortnite);

            db.Skins.Add(new Skin() {Id = 1, GameId = 1, Game = _CS, Sale = 0, SkinRarity = _rare, SkinRarityId = 1, SkinTypeId = 2, SkinType = _gun, Name = "Kek", Description = "Top of the top", Price = 2187.12 });
            db.Skins.Add(new Skin() { Id = 2, GameId = 1, Game = _CS, Sale = 10, SkinRarity = _legendary, SkinRarityId = 3, SkinTypeId = 2, SkinType = _gun, Name="Cheburek", Description ="50% of users estimated this one like a very beautiful skin in the world", Price = 100.5 });
            db.Skins.Add(new Skin() { Id = 3, GameId = 2, Game = _Fortnite, Sale = 0, SkinRarity = _common, SkinRarityId = 2, SkinTypeId = 1, SkinType = _cloth, Name = "Fort", Description = "For those whoa appretiate", Price = 23.60 });
            db.Skins.Add(new Skin() { Id = 4, GameId = 1, Game = _CS, Sale = 0, SkinRarity = _rare, SkinRarityId = 1, SkinTypeId = 2, SkinType = _gun, Name = "Kek", Description = "Top of the top", Price = 2187.12 });
            db.Skins.Add(new Skin() { Id = 5, GameId = 1, Game = _CS, Sale = 10, SkinRarity = _legendary, SkinRarityId = 3, SkinTypeId = 2, SkinType = _gun, Name = "Cheburek", Description = "50% of users estimated this one like a very beautiful skin in the world", Price = 100.5 });
            db.Skins.Add(new Skin() { Id = 6, GameId = 2, Game = _Fortnite, Sale = 0, SkinRarity = _common, SkinRarityId = 2, SkinTypeId = 1, SkinType = _cloth, Name = "Fort", Description = "For those whoa appretiate", Price = 23.60 });

            base.Seed(db);
        }
    }
}
