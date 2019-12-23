using SkinShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.SkinShop.Entities
{
    public class SkinRarity: ForIsDeleted, IForId
    {
        public int Id { get; set; }

        public string RarityName { get; set; }

        public string Color { get; set; }
    }
}
