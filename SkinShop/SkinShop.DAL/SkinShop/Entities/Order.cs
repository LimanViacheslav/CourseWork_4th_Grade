using SkinShop.DAL.Identity.Entities;
using SkinShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.SkinShop.Entities
{
    public enum OrderStatus
    {
        NotConfirmed = 1,
        Confirmed,
        InProcessing,
        Rejected,
        Completed
    }

    public class Order: ForIsDeleted, IForId
    { 
        public int Id { get; set; }

        public int? ClientId { get; set; }
        public virtual ClientProfile Client { get; set; }

        public virtual ICollection<OrderCount> OrderCounts { get; set; }

        public int? EmployeeId { get; set; }
        public virtual User Employee { get; set; }

        public DateTime OrderTime { get; set; }

        public OrderStatus Status { get; set; }

        public double Price { get; set; }
    }
}
