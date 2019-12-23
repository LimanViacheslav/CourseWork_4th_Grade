using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.SkinShopDTO
{
    public class SkinDTO: ForIsDeletedDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual SkinTypeDTO SkinType { get; set; }

        public virtual SkinRarityDTO SkinRarity { get; set; }

        public virtual GameDTO Game { get; set; }

        public double Price { get; set; }

        public int Sale { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ImageDTO> Images { get; set; }
    }
}
