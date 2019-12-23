using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.DAL.SkinShop.Interfaces
{
    public interface IDelete
    {
        void Delete(int id);

        void SoftDelete(int id);
    }
}
