using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DL.Entities.SkinShop
{
    public class Basket : CommonFeilds
    {
        public virtual ICollection<Skin> Skins { get; set; }
    }
}
