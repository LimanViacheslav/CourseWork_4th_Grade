using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DL.Entities.SkinShop
{
    public class Favorites : CommonFeilds
    {
        public virtual ICollection<Skin> FavoritesSkins { get; set; }
    }
}
