﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.SkinShopDTO
{
    public class SkinRarityDTO: ForIsDeletedDTO
    {
        public int Id { get; set; }

        public string RarityName { get; set; }

        public string Color { get; set; }
    }
}
