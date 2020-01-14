using SkinShop.Models.SkinShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinShop.Models.ViewModels
{
    public class GameDetailsVM
    {
        public GameDM Game { get; set; }

        public List<SkinDM> Skins{ get; set; }
    }
}