using SkinShop.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinShop.Models.SkinShop
{
    public class BasketDM
    {
        public int Id { get; set; }

        public virtual ICollection<SkinDM> Skins { get; set; }
    }
}