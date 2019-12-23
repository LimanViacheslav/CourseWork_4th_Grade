using SkinShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.SkinShop.Entities
{
    public class OrderCount: ForIsDeleted, IForId
    {
        public int Id { get; set; }

        public int? SkinId { get; set; }
        public Skin Skin { get; set; }

        public int Count { get; set; }
    }
}
