using SkinShop.DL.Entities.SkinShop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DL.Entities.Identity
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        //public virtual ICollection<Order> Orders { get; set; }

        public int? BasketId { get; set; }
        public virtual Basket Basket { get; set; }

        public int? FavoritesId { get; set; }
        public virtual Favorites Favorites { get; set; }

        public virtual User User { get; set; }
    }
}
