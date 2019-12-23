﻿using SkinShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.SkinShop.Entities
{
    public class Skin: ForIsDeleted, IForId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? SkinTypeId { get; set; }
        public virtual SkinType SkinType { get; set; }

        public int? SkinRarityId { get; set; }
        public virtual SkinRarity SkinRarity { get; set; }

        public int? GameId { get; set; }
        public virtual Game Game { get; set; }

        public double Price { get; set; }

        public int Sale { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
