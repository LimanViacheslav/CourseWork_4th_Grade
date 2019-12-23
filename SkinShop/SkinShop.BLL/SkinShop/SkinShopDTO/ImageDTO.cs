﻿using SkinShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.SkinShopDTO
{
    public class ImageDTO : IForId
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public byte[] Photo { get; set; }
    }
}
