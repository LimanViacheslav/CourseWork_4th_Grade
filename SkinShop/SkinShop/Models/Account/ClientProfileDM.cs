using SkinShop.Models.SkinShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinShop.Models.Account
{
    public class ClientProfileDM
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public virtual ICollection<OrderDM> Orders { get; set; }

        public virtual BasketDM Basket { get; set; }

        public virtual FavoritesDM Favorites { get; set; }

        public virtual UserDM User { get; set; }
    }
}