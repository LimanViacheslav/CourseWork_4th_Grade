using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinShop.Models.SkinShop
{
    public class GameDM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public string Type { get; set; }

        public string GameURL { get; set; }

        public bool IsThingGame { get; set; }

        public virtual List<ImageDM> Images { get; set; }
    }
}