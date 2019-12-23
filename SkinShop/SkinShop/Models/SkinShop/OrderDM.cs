using SkinShop.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinShop.Models.SkinShop
{
    public enum OrderStatusDM
    {
        NotConfirmed = 1,
        Confirmed,
        InProcessing,
        Rejected,
        Completed
    }

    public class OrderDM
    {
        public int Id { get; set; }

        public virtual ClientProfileDM Client { get; set; }

        public virtual ICollection<OrderCountDM> OrderCounts { get; set; }

        public int EmployeeId { get; set; }
        public virtual UserDM Employee { get; set; }

        public DateTime OrderTime { get; set; }

        public OrderStatusDM Status { get; set; }

        public double Price { get; set; }
    }
}