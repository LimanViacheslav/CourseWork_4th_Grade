using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.SkinShopDTO
{
    public class OrderCountDTO: ForIsDeletedDTO
    {
        public int Id { get; set; }

        public SkinDTO Skin { get; set; }

        public int Count { get; set; }
    }
}
