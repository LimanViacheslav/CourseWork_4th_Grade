using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DL.Entities.SkinShop
{
    public class OrderCount : CommonFeilds
    {
        public int? SkinId { get; set; }
        public virtual Skin Skin { get; set; }

        public int Count { get; set; }
    }
}
