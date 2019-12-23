using SkinShop.DAL.Identity.Entities;
using SkinShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.SkinShop.Entities
{
    public class Favorites: ForIsDeleted, IForId
    {
        public int Id { get; set; }

        public virtual ICollection<Skin> FavoritesSkins { get; set; }
    }
}
