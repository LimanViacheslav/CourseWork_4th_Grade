using SkinShop.BLL.Identity.Infrastructure;
using SkinShop.BLL.SkinShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.SkinShop.Interfaces
{
    public interface ICommonOperations
    {
        OperationDetails SoftDelete(Tables tables, int id);
        OperationDetails Delete(Tables tables, int id);
    }
}
