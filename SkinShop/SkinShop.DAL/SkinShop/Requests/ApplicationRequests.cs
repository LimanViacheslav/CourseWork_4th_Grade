using SkinShop.DAL.SkinShop.EF;
using SkinShop.DAL.SkinShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.SkinShop.Requests
{
    public class ApplicationRequests<T> where T: class
    {
        protected Context _context;

        public ApplicationRequests(Context context)
        {
            _context = context;
        }
    }
}
