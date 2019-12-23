using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.SkinShopDTO
{
    public class GameDTO: ForIsDeletedDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public string Type { get; set; }

        public string GameURL { get; set; }

        public bool IsThingGame { get; set; }

        public virtual ICollection<ImageDTO> Images { get; set; }
    }
}
