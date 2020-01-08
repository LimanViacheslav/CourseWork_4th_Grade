using SkinShop.DL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DL.Entities.SkinShop
{
    public enum OrderStatus
    {
        NotConfirmed = 1,
        Confirmed,
        InProcessing,
        Rejected,
        Completed
    }

    public class Order : CommonFeilds
    {

        public string ClientId { get; set; }

        public ClientProfile Client { get; set; }

        public virtual ICollection<OrderCount> OrderCounts { get; set; }

        public string EmployeeId { get; set; }

        public User Employee { get; set; }

        public DateTime OrderTime { get; set; }

        public OrderStatus Status { get; set; }

        public double Price { get; set; }
    }
}
