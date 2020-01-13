using SkinShop.Models.SkinShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinShop.Models.ViewModels
{
    public class SkinsVM
    {
        public IEnumerable<SkinDM> Skins { get; set; }

        public List<string> Games { get; set; }

        public List<string> Types { get; set; }

        public List<string> Rareties { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}